using Microsoft.AspNetCore.Identity;
using System;

namespace ReadLater5.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
