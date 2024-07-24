using System.ComponentModel.DataAnnotations;

namespace Juan.Models
{
    public class Setting : BaseEntity
    {
       
        public string Key{ get; set; }

        [MaxLength(100)]
        public string? Value { get; set; }
    }
}
