using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Identity;
using BetterCommerce.Core.Security.Jwt;
using BetterCommerce.Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;

namespace BetterCommerce.Business.Concrete
{
    public class RoleManager : BaseManager, IRoleService
    {

        public async Task<IDataResult<ApplicationRole>> CreateRole(string roleName)
        {
            var roleToCheck = await _roleManager.RoleExistsAsync(roleName);
            if (!roleToCheck)
            {
                return new ErrorDataResult<ApplicationRole>("Role exists.");
            }

            var role = new ApplicationRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return new ErrorDataResult<ApplicationRole>("Could not create the role");
            }

            return new SuccessDataResult<ApplicationRole>(role);
        }

        public async Task<IResult> DeleteRole(string roleId)
        {
            var roleToCheck = await _roleManager.FindByIdAsync(roleId);
            if (roleToCheck == null)
            {
                return new ErrorResult("Could not find the role");
            }

            roleToCheck.IsDeleted = true;
            var result = await _roleManager.UpdateAsync(roleToCheck);
            if (!result.Succeeded)
            {
                return new ErrorResult("Could not update the role");
            }

            return new SuccessResult("Role successfully deleted.");
        }

        public async Task<IResult> AddClaims(string roleId, List<Claim> claims)
        {
            var roleToCheck = await _roleManager.FindByIdAsync(roleId);
            if (roleToCheck == null)
            {
                return new ErrorResult("Role not found.");
            }

            try
            {
                claims.ForEach(x => _roleManager.AddClaimAsync(roleToCheck, x));
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

            return new SuccessResult("Claims successfully added.");
        }

        public async Task<IResult> DeleteClaims(string roleId, List<Claim> claims)
        {
            var roleToCheck = await _roleManager.FindByIdAsync(roleId);
            if (roleToCheck == null)
            {
                return new ErrorResult("Role not found.");
            }

            if (!claims.Any())
            {
                return new ErrorResult("There is no claim.");
            }

            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(roleToCheck, claim);
            }

            return new SuccessResult("Selected claims successfully removed.");
        }


        public RoleManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IPasswordValidator<ApplicationUser> passwordValidator, ITokenHelper tokenHelper,
            RoleManager<ApplicationRole> roleManager, IPasswordHasher<ApplicationUser> passwordHasher)
            : base(userManager, signInManager, passwordValidator, tokenHelper, roleManager, passwordHasher)
        {
          
        }
    }
}