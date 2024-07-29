using Juan.Data;
using Juan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var categories =await _context.Categories
                .Include(c=>c.ProductCategories)
                .Where(c=>!c.IsDelete)
                .AsNoTracking()
                .ToListAsync();
            return View(categories);
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            ViewBag.Categories = await _context.Categories
               .Include(c => c.ProductCategories)
               .Where(c => !c.IsDelete)
               .AsNoTracking()
               .ToListAsync();
            return View();
        }
    }
}
