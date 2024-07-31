
using Microsoft.AspNetCore.Identity;

namespace Juan.Models
{
    public class AppUser:IdentityUser

    {
        public required string Fullname { get; set; }
        public bool IsBlocked { get; set; }
        public string? ConnectionId { get; set; }
    }
}
