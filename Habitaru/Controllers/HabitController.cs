using Microsoft.AspNetCore.Mvc;

namespace Habitaru.Controllers
{
    public class HabitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
