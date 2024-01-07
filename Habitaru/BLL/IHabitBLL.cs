using Habitaru.Models;

namespace Habitaru.BLL
{
    public interface IHabitBLL
    {
        public List<IdNameCurStreakDate> GetNameAndCurStreakDate(); 
        public List<Habit> GetAll(); 
        public void Add();
        public void Delete();
        public void Update();

    }
}
