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
        private readonly ILogger<LoginController> logger;

        public LoginController(IUserManagement userManagement,
             ILogger<LoginController> logger)
        {
            this.userManagement = userManagement;
            this.logger = logger;
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
                logger.LogDebug("Logowanie przez użytkownika", new[] { exc.Message });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            try
            {
                userManagement.Login("Rogerottii", "Roger!994");
            }
            catch (Exception exc)
            {
                logger.LogDebug("Logowanie przez administratora", new[] { exc.Message });
            }
         
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                userManagement.Logout();
            }
            catch (Exception exc)
            {
                logger.LogDebug("Wylogowanie", new[] { exc.Message });
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
