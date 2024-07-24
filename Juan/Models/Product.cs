using System.ComponentModel.DataAnnotations;

namespace Juan.Models
{
    public class Product
    {
       
        public int ProductId { get; set; }

        
        public string ProductName { get; set; }
        public string Img { get; set; }




        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string Desc { get; set; }
        public int  Count { get; set; }



        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
