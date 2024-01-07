using Habitaru.BLL;
using Habitaru.Context;
using Habitaru.Models;
using Microsoft.AspNetCore.Mvc;

namespace Habitaru.Controllers
{
    public class HabitController : Controller
    {
        private readonly IHabitBLL habitBll;

        public HabitController(IHabitBLL habitBll)
        {
            this.habitBll = habitBll;
        }
        public IActionResult Index()
        {
            List<IdNameCurStreakDate> habits = habitBll.GetIdNameCurStreakDate();

            return View(habits);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Add(Habit habit)
        //{
        //    habit.ResetCount = 0;
        //    habit.CurStreakDate = habit.FirstStreakDate;
        //    var date = habit.FirstStreakDate - DateTime.Now;
        //}
    }
}
