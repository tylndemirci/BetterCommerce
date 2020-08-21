using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BetterCommerce.Core.Identity;
using BetterCommerce.Core.Utilities.Results;

namespace BetterCommerce.Business.Abstract
{
    public interface IRoleService
    {
        Task<IDataResult<ApplicationRole>> CreateRole(string roleName);
        Task<IResult> DeleteRole(string roleId);

        Task<IResult> AddClaims(string roleId, List<Claim> claims);
        Task<IResult> DeleteClaims(string roleId, List<Claim> claims);

    }
}    