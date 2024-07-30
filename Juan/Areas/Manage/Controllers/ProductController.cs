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
    public class ProductController : Controller
    {
        private readonly JuanDbContext _context;

        public ProductController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products =await _context.Products
                .AsNoTracking()
                .Include(p=>p.ProductCategories)
                .Include(p=>p.ProductColors)
                .Include(p => p.ProductSizes)
                .Where(p=> !p.IsDelete)
                .ToListAsync();
            foreach (var product in products)
            {
                if (product.ProductColors == null)
                {
                    product.ProductColors = new List<ProductColor>();
                }
            }
            return View(products);
        }

        public IActionResult Create() 
        {
         return View();
        }
    }
}
