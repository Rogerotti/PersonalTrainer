using Framework.Models.Dto;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace PersonalTrainerCore.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserManagement userManagement;
        private readonly ILogger<LoginController> logger;

        public RegisterController(
            IUserManagement userManagement,
            ILogger<LoginController> logger)
        {
            this.userManagement = userManagement;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Index(UserDto user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userManagement.RegisterUser(user.Login, user.Email, user.Password, user.Gender, user.Height, user.Weight, user.Age);
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("AdditionalValidation", exc.Message);
                logger.LogDebug("rejestracja nie powiodła się.", new[] { exc.Message });
                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserDto());
        }
    }
}
