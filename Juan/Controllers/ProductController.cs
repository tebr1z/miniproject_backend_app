using Juan.Data;
using Juan.Models;
using Juan.Services;
using Juan.ViewModels;
using Microsoft.AspNetCore.Mvc;
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


    }
}
