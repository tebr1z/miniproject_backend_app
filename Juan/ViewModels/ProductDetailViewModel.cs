namespace Juan.ViewModels
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<ProductColorDetail> Colors { get; set; }
        public List<ProductSizeDetail> Sizes { get; set; }
        public int TotalQuantity { get; set; }
    }

    public class ProductColorDetail
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int SizeCount { get; set; } // Bu reng toplam beden sayisi gosterir
    }

    public class ProductSizeDetail
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int ColorCount { get; set; } // Bu bedende toplam reng sayıs
    }

}
