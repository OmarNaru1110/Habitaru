using Habitaru.Models;
using Habitaru.Repositories.IRepositories;
using Habitaru.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Habitaru.Controllers
{
    public class HabitController : Controller
    {
        private readonly IHabitService _habitService;

        public HabitController(IHabitService habitService)
        {
            _habitService = habitService;
        }
        public IActionResult Index()
        {
            List<IdNameCurStreakDate> habits = _habitService.GetIdNameCurStreakDate();

            return View(habits);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Habit habit)
        {
            habit.CurStreakDate = habit.FirstStreakDate;
            if (ModelState.IsValid)
            {
                _habitService.Add(habit);
                var date = habit.CurStreakDate - DateTime.Now;
                
                return RedirectToAction("Index");
            }
            else
                return View(habit);
        }
        public IActionResult Details(int? id)
        {
            var userHabit = _habitService.GetById(id);
            userHabit = _habitService.CreateHabitDetails(userHabit);
            if (userHabit == null)
                return NotFound();
            _habitService.Update(userHabit);
            ViewBag.HabitName = userHabit.Name;
            return View(userHabit);
        }
    }
}
