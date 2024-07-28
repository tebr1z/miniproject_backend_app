using Juan.Data;
using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
namespace Juan.Controllers
{
    public class BasketController:Controller
    {

   private readonly JuanDbContext _context;

        public BasketController(JuanDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return BadRequest();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null) return NotFound();

            string basket = HttpContext.Request.Cookies["basket"];
            List<BasketVM> baskets;

            if (string.IsNullOrWhiteSpace(basket))
            {
                baskets = new List<BasketVM>();
            }
            else
            {
                baskets = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }

            var basketProduct = baskets.FirstOrDefault(p => p.Id == id);

            if (basketProduct != null)
            {
                basketProduct.Count++;
            }
            else
            {
                baskets.Add(new BasketVM
                {
                    Id = product.ProductId,  
                    Name = product.ProductName,
                    Price = product.Price,
                    Image = product.Img,
                    Count = 1  
                });
            }

            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));

            return PartialView("_BasketPartial",baskets);
        }

        public IActionResult GetBasket()
        {
            var Result = HttpContext.Request.Cookies["basket"];
            return Json(Result);
        }
      
    }
}
