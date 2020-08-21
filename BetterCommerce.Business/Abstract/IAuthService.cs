using System.Threading.Tasks;
using BetterCommerce.Core.Identity;
using BetterCommerce.Core.Security.Jwt;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.Entity.Dtos;


namespace BetterCommerce.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<ApplicationUser>> Login(UserForLoginDto userForLoginDto);
        Task<IDataResult<ApplicationUser>> Register(UserForRegisterDto userForRegisterDto);
        Task<IDataResult<AccessToken>> CreateAccessToken(ApplicationUser user);

        Task<IDataResult<ApplicationUser>> ChangePassword(UserForChangePassword userForChangePassword);
        Task<IResult> ChangeEmail(UserForChangeEmail userForChangeEmail);
    }
}