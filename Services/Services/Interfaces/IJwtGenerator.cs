using Microsoft.AspNetCore.Identity;

namespace ReadLater5.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(IdentityUser user);
    }
}
