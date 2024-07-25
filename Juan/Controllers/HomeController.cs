using Juan.Data;
using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Controllers
{
    public class HomeController : Controller
    {
        private readonly JuanDbContext _context;

        public HomeController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                sliders = await _context.Sliders.ToListAsync(),
                Products = await _context.Products
                                        .Where(p => !p.IsDelete && p.Count > 0)
                                        .Include(p => p.ProductCategories)
                                        .ThenInclude(pc => pc.Category)
                                        .ToListAsync(),
                SelectedProducts = await _context.Products
                                            .Where(p => !p.IsDelete && p.IsSelected && p.Count > 0)
                                            .Include(p => p.ProductCategories)
                                            .ThenInclude(pc => pc.Category)
                                            .ToListAsync(),

                                             Banners = await _context.Banners
                                             .ToListAsync(),

                                             Settings = await _context.Settings
                                             .ToListAsync()
            };

            return View(homeVM);
        }
    }
}
