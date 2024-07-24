using System.ComponentModel.DataAnnotations;

namespace Juan.Models
{
    public class Category
    {
       
        public int CategoryId { get; set; }

        
        [StringLength(50)]
        public string CategoryName { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool IsDelete { get; set; }
    }

}
