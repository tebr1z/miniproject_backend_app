using Juan.Data;
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
        public async Task <IActionResult> ProductModal(int?productId)
        {


            if (productId == null) return BadRequest();

            var product =await _context.Products
                .AsNoTracking()
                .Where(p=>!p.IsDelete)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p =>  p.ProductId == productId);

            if (product == null) return NotFound();
            
            return PartialView("_ModalPartial",product);
        }


    }
}
