using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace PersonalTrainerDiet.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserManagement userManagement;

        public AdminController(IUserManagement userManagement)
        {
            this.userManagement = userManagement;
        }

        [HttpGet]
        public IActionResult Users()
        {
            try
            {
                var users = userManagement.GetAllUsers();
                return View(users);
            }
            catch (Exception exc)
            {
                ModelState.TryAddModelError("AdditionalValidation", exc.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult Products()
        {
            return null;
        }

        [HttpPost]
        public JsonResult GetUserDetails([FromBody]JToken jsonBody)
        {
            var id = jsonBody.Value<String>("Id");
            var userDetails = userManagement.GetUser(new Guid(id));
            return new JsonResult(userDetails);
        }
    }
}
