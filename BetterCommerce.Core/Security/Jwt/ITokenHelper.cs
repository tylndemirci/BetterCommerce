using System.Collections.Generic;
using BetterCommerce.Core.Identity;

namespace BetterCommerce.Core.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(ApplicationUser user, List<ApplicationRole> roles);
    }
}   