using Juan.Models;
using System.Collections.Generic;

namespace Juan.ViewModels
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public Dictionary<int, int> CategoryProductCounts { get; set; }
        public Dictionary<int, int> ColorProductCounts { get; set; }
        public Dictionary<int, int> SizeProductCounts { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)System.Math.Ceiling((double)TotalItems / PageSize);
    }
}
