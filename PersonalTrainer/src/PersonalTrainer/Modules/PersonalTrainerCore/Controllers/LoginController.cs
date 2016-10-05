using Framework.Models;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace PersonalTrainerCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserManagement userManagement;

        public LoginController(IUserManagement userManagement)
        {
            this.userManagement = userManagement;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserDto());
        }

        [HttpPost]
        public IActionResult Login(UserDto user)
        {
            try
            {
                userManagement.Login(user.Login, user.Password);
                var item = userManagement.GetCurrentUser();
            }
            catch (Exception exc)
            {
        
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            userManagement.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}
