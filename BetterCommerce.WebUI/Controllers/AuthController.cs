using System.Threading.Tasks;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Entity.Dtos;
using BetterCommerce.WebUI.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.WebUI.Controllers
{
   
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
      

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userLogin = await _authService.Login(new UserForLoginDto()
                {
                    Email = model.Email,
                    Password = model.Password
                });
                if (!userLogin.Success)
                {
                    ModelState.AddModelError("", userLogin.Message);
                    return View(model);
                }

                var result = await _authService.CreateAccessToken(userLogin.Data);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userToCheck = await _authService.Register(new UserForRegisterDto()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password
                });
                if (!userToCheck.Success)
                {
                    ModelState.AddModelError("", userToCheck.Message);
                    return View(model);
                }

                var result = await _authService.CreateAccessToken(userToCheck.Data);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }


            return View(model);
        }
    }
}