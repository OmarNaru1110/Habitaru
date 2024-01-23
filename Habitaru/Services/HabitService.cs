using Habitaru.Models;
using Habitaru.Repositories.IRepositories;
using Habitaru.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Habitaru.Services
{
    public class HabitService : IHabitService
    {
        private readonly IHabitRepository _habitRepository;

        public HabitService(IHabitRepository habitRepository)
        {
            _habitRepository = habitRepository;
        }
        public bool Add(Habit userHabit)
        {
            if (userHabit == null)
                return false;
            _habitRepository.Add(userHabit);
            return true;
        }

        public bool Delete(int? id)
        {
            if (id == null)
                return false;
            var userHabit = GetById(id);
            if (userHabit == null)
                return false;
            _habitRepository.Delete(userHabit);
            return true;
        }

        public List<Habit> GetAll()
        {
            return _habitRepository.GetAll();
        }
        public Habit GetById(int? id)
        {
            if (id == null)
                return null;
            return _habitRepository.GetById(id);
        }
        public List<IdNameCurStreakDate> GetIdNameCurStreakDate()
        {
            return _habitRepository.GetIdNameCurStreakDate();
        }
        public bool Update(Habit userHabit)
        {
            if (userHabit == null)
                return false;
            _habitRepository.Update(userHabit);
            return true;
        }
        public Habit CreateHabitDetails(Habit habit)
        {
            //periods in minutes
            if (habit == null)
                return null;
            
            habit.MaxStreakPeriod = Math.Max(
                habit.MaxStreakPeriod,
                (int)(DateTime.Now - habit.CurStreakDate).TotalMinutes);

            try
            {
                habit.AvgStreakPeriod = (int)(DateTime.Now - habit.FirstStreakDate).TotalMinutes / habit.ResetCount;
            }
            catch(DivideByZeroException)
            {
                habit.AvgStreakPeriod = (int)(DateTime.Now - habit.FirstStreakDate).TotalMinutes;
            }

            habit.MinStreakPeriod = Math.Min(
                habit.MinStreakPeriod,
                (int)(DateTime.Now - habit.CurStreakDate).TotalMinutes);

            return habit;
        }
    }
}
