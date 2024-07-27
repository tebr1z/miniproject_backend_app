using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Juan.Models;
using Juan.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Juan.Data;

namespace Juan.Controllers
{
    public class ShopController : Controller
    {
        private readonly JuanDbContext _context;
        private const int PageSize = 9;

        public ShopController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int[] categoryIds, int[] colorIds, int[] sizeIds, int page = 1)
        {
            var productsQuery = _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes)
                .AsQueryable();

            if (categoryIds.Any())
            {
                productsQuery = productsQuery.Where(p => p.ProductCategories.Any(pc => categoryIds.Contains(pc.CategoryId)));
            }

            if (colorIds.Any())
            {
                productsQuery = productsQuery.Where(p => p.ProductColors.Any(pc => colorIds.Contains(pc.ColorId)));
            }

            if (sizeIds.Any())
            {
                productsQuery = productsQuery.Where(p => p.ProductSizes.Any(ps => sizeIds.Contains(ps.SizeId)));
            }

            var totalItems = await productsQuery.CountAsync();
            var products = await productsQuery
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var viewModel = new ShopViewModel
            {
                Products = products,
                Categories = await _context.Categories.ToListAsync(),
                Colors = await _context.Colors.ToListAsync(),
                Sizes = await _context.Sizes.ToListAsync(),
                CategoryProductCounts = _context.Categories
                    .Select(c => new { c.CategoryId, Count = _context.ProductCategories.Count(pc => pc.CategoryId == c.CategoryId) })
                    .ToDictionary(x => x.CategoryId, x => x.Count),
                ColorProductCounts = _context.Colors
                    .Select(c => new { c.ColorId, Count = _context.ProductColors.Count(pc => pc.ColorId == c.ColorId) })
                    .ToDictionary(x => x.ColorId, x => x.Count),
                SizeProductCounts = _context.Sizes
                    .Select(s => new { s.SizeId, Count = _context.ProductSizes.Count(ps => ps.SizeId == s.SizeId) })
                    .ToDictionary(x => x.SizeId, x => x.Count),
                CurrentPage = page,
                TotalItems = totalItems,
                PageSize = PageSize
            };

            return View(viewModel);
        }
    }
}
