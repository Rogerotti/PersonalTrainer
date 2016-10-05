using Framework.Models;
using Framework.Models.Database;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;

namespace PersonalTrainerCore.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserManagement userManagement;

        public RegisterController(IUserManagement userManagement)
        {
            this.userManagement = userManagement;
        }

        [HttpPost]
        public IActionResult Index(UserDto user)
        {
            if (ModelState.IsValid)
            {
                if (userManagement.Validation(user.Login))
                {
                    userManagement.RegisterUser(user.Login, user.Email, user.Password, user.Gender, user.Height, user.Weight, user.Age);
                }
            }
            return View(user);

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserDto());
        }

        [HttpPost]
        public IActionResult Register(UserDto user)
        {
            return View(new UserDto());
        }
    }
}
