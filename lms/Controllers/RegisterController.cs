using Microsoft.AspNetCore.Mvc;

namespace lms.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
