using Juan.Data;
using Juan.Interfaces;

using Juan.Models;
using Juan.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace Juan.Services
{
    public class LayoutService:ILayoutService
    {
        private readonly JuanDbContext _context;

        private readonly IHttpContextAccessor _contextAccessor;
        public LayoutService(JuanDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public IEnumerable<BasketVM> GetBasket()
        {
            List<BasketVM> list = new();
           string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basket) )
            {
                return list;
            }
            else
            {
                list=JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (var basketProduct in list) {
                    var existProduct = _context.Products.FirstOrDefault(p => p.ProductId == basketProduct.Id);
                    basketProduct.Price = existProduct.Price;
                    basketProduct.Name=existProduct.ProductName;
                    basketProduct.Image=existProduct.Img;
                    basketProduct.OldPrice = existProduct.OldPrice;
                }
            }
            return list;

        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories =await _context.Categories
                .AsNoTracking()
                .Where(c=>!c.IsDelete)
                .ToListAsync();
            return categories;
        }

        public IDictionary<string, string> GetSettings() => _context.Settings
            .Where(s => !s.IsDelete)
            .ToDictionary(s => s.Key, s =>s.Value);
            
    }
}
