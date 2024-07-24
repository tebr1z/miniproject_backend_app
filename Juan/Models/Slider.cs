using System.ComponentModel.DataAnnotations;

namespace Juan.Models
{
    public class Slider:BaseEntity
    {
        
   
        [Required]
        [StringLength(100)]
        public string SlideSubtitle { get; set; }

        [Required]
        [StringLength(100)]
        public string SlideTitle { get; set; }

        [Required]
        [StringLength(500)]
        public string SlideDesc { get; set; }

        [Required]
        [StringLength(200)]
        public string SliderImg { get; set; }

        [Required]
        [StringLength(200)]
        public string Link { get; set; }
    }
}
