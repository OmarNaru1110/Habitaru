using Habitaru.Models;
using Habitaru.Repositories.IRepositories;
using Habitaru.Services.IServices;
using Habitaru.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Habitaru.Controllers
{
    [Authorize]
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
            if(habits==null)
                return NotFound();
            return View(habits);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(HabitCreationVM habitVM)
        {
            var habit = new Habit
            {
                Name = habitVM.Name,
                FirstStreakDate = habitVM.FirstStreakDate,
                CurStreakDate = habitVM.FirstStreakDate
            };

            habit = _habitService.CreateHabitDetails(habit);

            //modelstate isn't valid cuz the User property in habit model
            //is null which is wrong and i don't know what is the best practice
            //to use it, so ask chatgpt 
            if (ModelState.IsValid)
            {
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
