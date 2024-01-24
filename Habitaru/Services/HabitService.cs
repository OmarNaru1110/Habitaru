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

            habit.MaxStreakPeriod = GetMaxPeriod(habit.MaxStreakPeriod, habit.CurStreakDate);

            try
            {
                habit.AvgStreakPeriod = (int)(DateTime.Now - habit.FirstStreakDate).TotalMinutes / habit.ResetCount;
            }
            catch(DivideByZeroException)
            {
                habit.AvgStreakPeriod = (int)(DateTime.Now - habit.FirstStreakDate).TotalMinutes;
            }

            habit.MinStreakPeriod = GetMinPeriod(habit.MinStreakPeriod, habit.CurStreakDate);

            return habit;
        }
        public bool ResetCounter(int? id)
        {
            if (id == null)
                return false;
            var habit = GetById(id);
            if (habit == null) 
                return false;
            habit.ResetCount++;
            habit.PrevStreakPeriod = (int)(DateTime.Now - habit.CurStreakDate).TotalMinutes;
            habit.CurStreakDate = DateTime.Now;
            habit.MaxStreakPeriod = GetMaxPeriod(habit.MaxStreakPeriod, habit.CurStreakDate);
            habit.MinStreakPeriod = GetMinPeriod(habit.MinStreakPeriod, habit.CurStreakDate);
            habit.AvgStreakPeriod = (int)(DateTime.Now - habit.FirstStreakDate).TotalMinutes / habit.ResetCount;
            _habitRepository.Save();

            return true;
        }
        public int GetMaxPeriod(int MaxStreakPeriod, DateTime CurStreakDate)
        {
            return Math.Max(
                            MaxStreakPeriod,
                            (int)(DateTime.Now - CurStreakDate).TotalMinutes);
        }
        public int GetMinPeriod(int MinStreakPeriod, DateTime CurStreakDate)
        {
            return Math.Min(
                            MinStreakPeriod,
                            (int)(DateTime.Now - CurStreakDate).TotalMinutes);
        }
    
    }
}
