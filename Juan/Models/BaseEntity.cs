using System.ComponentModel.DataAnnotations;

namespace Juan.Models
{
    public class BaseEntity
    {
        
        public int Id { get; set; }

      
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool IsDelete{ get; set; }
    }
}
