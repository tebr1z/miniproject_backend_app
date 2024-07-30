using Juan.Data;
using Juan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    { 
        private readonly JuanDbContext _context;

        public CategoryController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.ProductCategories)
                .AsNoTracking()
                .ToListAsync();
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request)
        {
            if (request.CategoryId <= 0)
            {
                return Json(new { success = false, message = "Invalid CategoryId" });
            }

            var existingCategory = await _context.Categories.FindAsync(request.CategoryId);
            if (existingCategory == null)
            {
                return Json(new { success = false, message = "Category not found" });
            }

            existingCategory.CategoryName = request.CategoryName;
            existingCategory.IsDelete = request.IsDelete;
            existingCategory.ModifiedDate = DateTime.Now;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public class UpdateCategoryRequest
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public bool IsDelete { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return Json(new { success = false, message = "Category not found" });
            }

            category.IsDelete = true;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories
               .Include(c => c.ProductCategories)
               .Where(c => !c.IsDelete)
               .AsNoTracking()
               .ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            category.CreatedDate = DateTime.Now;

            ViewBag.Categories = await _context.Categories
               .Include(c => c.ProductCategories)
               .Where(c => !c.IsDelete)
               .AsNoTracking()
               .ToListAsync();

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            return Json(new { success = false, message = "Invalid model state" });
        }
    }
}
