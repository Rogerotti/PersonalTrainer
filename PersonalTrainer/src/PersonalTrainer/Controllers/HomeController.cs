using Microsoft.AspNetCore.Mvc;

namespace PersonalTrainer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
