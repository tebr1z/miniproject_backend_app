using Juan.Models;

namespace Juan.ViewModels
{
    public class HomeVM
    {
        public IEnumerable <Slider> sliders { get; set; }
        public IEnumerable<Product> Products{ get; set; }
        public IEnumerable<Product> SelectedProducts { get; set; }

        public List<Banner> Banners { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Size> Sizes { get; set; }


    }
}
