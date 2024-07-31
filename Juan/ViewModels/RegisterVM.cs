using System.ComponentModel.DataAnnotations;

namespace Juan.ViewModels
{
    public class RegisterVM
    {
        [Required, MaxLength(30)]
        public string Fullname { get; set; }
        [Required, MaxLength(30), EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(30)]
        public string Username { get; set; }
        [Required, MaxLength(30), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(30), DataType(DataType.Password), Compare(nameof(Password)), Display(Name = "Confirm Password")]
        public string RePassword { get; set; }
    }
}
