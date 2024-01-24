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
                habit = _habitService.CreateHabitDetails(habit);
                _habitService.Add(habit);
                
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
        public IActionResult Reset(int? id)
        {
            if (_habitService.ResetCounter(id) == false)
                return NotFound();
            return RedirectToAction("Details", new {ID=id});
        }
    }
}
