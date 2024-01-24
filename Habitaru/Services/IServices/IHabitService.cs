using Habitaru.Models;

namespace Habitaru.Services.IServices
{
    public interface IHabitService
    {
        public List<IdNameCurStreakDate> GetIdNameCurStreakDate();
        public List<Habit> GetAll();
        public bool Add(Habit userHabit);
        public bool Delete(int? id);
        public bool Update(Habit userHabit);
        public Habit GetById(int? id);
        public Habit CreateHabitDetails(Habit habit);
        public bool ResetCounter(int? id);
        public int GetMaxPeriod(int MaxStreakPeriod, DateTime CurStreakDate);
        public int GetMinPeriod(int MinStreakPeriod, DateTime CurStreakDate);

    }
}
