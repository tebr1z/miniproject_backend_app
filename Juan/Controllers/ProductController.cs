using Juan.Data;
using Juan.Models;
using Juan.Services;
using Juan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;

namespace Juan.Controllers
{

    public class ProductController : Controller
    {
        private readonly JuanDbContext _context;

        public ProductController(JuanDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task <IActionResult> ProductModal(int?Id)
        {


            if (Id == null) return BadRequest();
            var product =await _context.Products
                .AsNoTracking()
                .Where(p=>!p.IsDelete)
                .Include(p=>p.ProductImages)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .FirstOrDefaultAsync(p =>  p.ProductId ==Id);
            if (product == null) return NotFound();
          
            return PartialView("_ProductPartial",product);
        }

        public async Task<IActionResult> SearchProduct(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest();

            var products = await _context.Products
            .Where(p => !p.IsDelete &&
            (p.ProductName.Contains(searchTerm) ||
             p.ProductCategories
            .Any(pc => pc.Category.CategoryName
            .Contains(searchTerm))))
            .Include(p => p.ProductColors)
            .ThenInclude(pc => pc.Color)
            .Include(p => p.ProductImages)
            .Include(p => p.ProductSizes)
            .ThenInclude(ps => ps.Size)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Take(3)
            .ToListAsync();

            return PartialView("_SearchPartial",products);
        }
        public async Task<IActionResult> Detail(int? Id)
        {
            var product = await _context.Products
               .AsNoTracking()
               .Where(p => !p.IsDelete)
               .Include(p => p.ProductImages)
               .Include(p => p.ProductSizes)
               .ThenInclude(ps => ps.Size)
               .Include(p => p.ProductColors)
               .ThenInclude(pc => pc.Color)
               .FirstOrDefaultAsync(p => p.ProductId == Id);

            if (product == null) return NotFound();

            var selectedProducts = await _context.Products
      .Where(p => !p.IsDelete && p.IsSelected && p.Count > 0)
      .Include(p => p.ProductCategories)
          .ThenInclude(pc => pc.Category)
      .Include(p => p.ProductImages)
      .ToListAsync();

           
            ViewBag.SelectedProducts = selectedProducts;



            return View(product);
        }
    }
}
