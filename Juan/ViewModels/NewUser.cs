using System.ComponentModel.DataAnnotations;

namespace Juan.ViewModels
{
    public class NewUserVM
    {
        [MaxLength(100)]
        public required string Fullname { get; set; }
        public required string Username { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare(nameof(Password)), DataType(DataType.Password), Display(Name = "Confirm Password")]
        public string? RePassword { get; set; }
        [DataType(DataType.Password), Display(Name = "Current Password")]
        public string? CurrentPassword { get; set; }
    }
}
