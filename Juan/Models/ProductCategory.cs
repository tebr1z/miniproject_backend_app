using System.ComponentModel.DataAnnotations;

namespace Juan.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
