using System.ComponentModel.DataAnnotations;

namespace Juan.Models
{
    public class Banner
    {
        public int BannerId { get; set; }

       
        public string Subtitle { get; set; }

      
        public string Title { get; set; }

       
        public string ImageUrl { get; set; }

    
        public string Link { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool IsDelete { get; set; }

        [MaxLength(2)]
        public int count { get; set; }
    }
}
