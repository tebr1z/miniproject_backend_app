namespace Juan.Models
{
    public class ProductImage
    {
       
            public int ProductImageId { get; set; }
            public int ProductId { get; set; }
            public string ImageUrl { get; set; }
            public virtual Product Product { get; set; }
        
    }
}
