using BetterCommerce.Core.Identity;
using BetterCommerce.Core.Security.Jwt;
using Microsoft.AspNetCore.Identity;

namespace BetterCommerce.Business.Concrete
{
    public class BaseManager
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly ITokenHelper _tokenHelper;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        protected readonly RoleManager<ApplicationRole> _roleManager;
        protected readonly IPasswordHasher<ApplicationUser> _passwordHasher;
       
       

        protected BaseManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IPasswordValidator<ApplicationUser> passwordValidator, ITokenHelper tokenHelper, RoleManager<ApplicationRole> roleManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordValidator = passwordValidator;
            _tokenHelper = tokenHelper;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
           
        }

        
    }
}