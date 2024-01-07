using Habitaru.Models;

namespace Habitaru.BLL
{
    public interface IHabitBLL
    {
        public List<IdNameCurStreakDate> GetIdNameCurStreakDate(); 
        public List<Habit> GetAll(); 
        public bool Add(Habit userHabit);
        public bool Delete(int? id);
        public bool Update(int? id);

    }
}
