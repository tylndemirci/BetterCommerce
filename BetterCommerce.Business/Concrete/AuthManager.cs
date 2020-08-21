using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Identity;
using BetterCommerce.Core.Security.Jwt;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.Entity.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BetterCommerce.Business.Concrete
{
    public class AuthManager :IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly ITokenHelper _tokenHelper;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AuthManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IPasswordValidator<ApplicationUser> passwordValidator, ITokenHelper tokenHelper,
            RoleManager<ApplicationRole> roleManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordValidator = passwordValidator;
            _tokenHelper = tokenHelper;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }
        public async Task<IDataResult<ApplicationUser>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userManager.FindByEmailAsync(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<ApplicationUser>(new ApplicationUser() {Email = userForLoginDto.Email}, "User not found.");
            }

            var passwordCheck = await _passwordValidator.ValidateAsync(_userManager, userToCheck, userForLoginDto.Password);

            if (!passwordCheck.Succeeded)
            {
                return new ErrorDataResult<ApplicationUser>(new ApplicationUser() {Email = userForLoginDto.Email}, "Password or Email is wrong.");
            }

            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(userToCheck, userForLoginDto.Password, false, false);
            if (result.Succeeded)
            {
                return new SuccessDataResult<ApplicationUser>(userToCheck);
            }

            return new ErrorDataResult<ApplicationUser>(new ApplicationUser() {Email = userForLoginDto.Email}, "Something went wrong.");
        }

        public async Task<IDataResult<ApplicationUser>> Register(UserForRegisterDto userForRegisterDto)
        {
            var emailToCheck = await _userManager.FindByEmailAsync(userForRegisterDto.Email);
            if (emailToCheck != null)
            {
                return new ErrorDataResult<ApplicationUser>(null, "There is already an account with the same Email.");
            }

            var applicationUser = new ApplicationUser()
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                UserName = userForRegisterDto.Email,
                Email = userForRegisterDto.Email,
                
            };
            var result = await _userManager.CreateAsync(applicationUser, userForRegisterDto.Password);
            if (!result.Succeeded)
            {
                return new ErrorDataResult<ApplicationUser>(applicationUser, "Something went wrong."); //could not create the user
            }

            return new SuccessDataResult<ApplicationUser>(applicationUser); //return user
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(ApplicationUser user)
        {
            var userRoles = _userManager.GetRolesAsync(user).Result;
            List<ApplicationRole> role = new List<ApplicationRole>();

            foreach (var userRole in userRoles)
            {
                var newRole = await _roleManager.FindByIdAsync(userRole);
                role.Add(newRole);
            }

            var accessToken = _tokenHelper.CreateToken(user, role);
            if (accessToken==null)
            {
                await _signInManager.SignOutAsync();
                return new ErrorDataResult<AccessToken>("Token not created.");
            }
            return new SuccessDataResult<AccessToken>(accessToken, "Access Token created.");
        }

        public async Task<IDataResult<ApplicationUser>> ChangePassword(UserForChangePassword userForChangePassword)
        {
            var userToCheck = await _userManager.FindByEmailAsync(userForChangePassword.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<ApplicationUser>(null, "User not found.");
            }

            var validatePassword = await _passwordValidator.ValidateAsync(_userManager, userToCheck, userForChangePassword.OldPassword);
            if (!validatePassword.Succeeded)
            {
                return new ErrorDataResult<ApplicationUser>(userToCheck, "Old password is wrong.");
            }

            if (userForChangePassword.NewPassword != userForChangePassword.NewPasswordToCheck)
            {
                return new ErrorDataResult<ApplicationUser>(userToCheck, "New passwords do not match.");
            }

            _passwordHasher.HashPassword(userToCheck, userForChangePassword.OldPassword);
            userToCheck.ModifiedAt = DateTime.Now;
            var result = await _userManager.UpdateAsync(userToCheck);
            if (!result.Succeeded)
            {
                return new ErrorDataResult<ApplicationUser>(userToCheck, "Could not update the user");
            }

            return new SuccessDataResult<ApplicationUser>(userToCheck);
        }

        public async Task<IResult> ChangeEmail(UserForChangeEmail userForChangeEmail)
        {
            var userToCheck = await _userManager.FindByEmailAsync(userForChangeEmail.Email);
            if (userToCheck == null)
            {
                return new ErrorResult("User not found.");
            }

            var validatePassword = await _passwordValidator.ValidateAsync(_userManager, userToCheck, userForChangeEmail.Password);
            if (!validatePassword.Succeeded)
            {
                return new ErrorResult("Password does not match.");
            }

            userToCheck.Email = userForChangeEmail.Email;
            userToCheck.UserName = userForChangeEmail.Email;
            userToCheck.ModifiedAt = DateTime.Now;
            var result = await _userManager.UpdateAsync(userToCheck);
            if (!result.Succeeded)
            {
                return new ErrorResult("Something went wrong.");
            }

            return new SuccessResult("Email successfully changed.");
        }


    }
}