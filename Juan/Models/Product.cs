using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juan.Models
{
    public class Product
    {
       
        public int ProductId { get; set; }

    
        public string ProductName { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string Desc { get; set; }
        public int  Count { get; set; }


        public virtual ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
        public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public virtual ICollection<ProductImage> ProductImages { get; set; } 


        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsSelected { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
