using Habitaru.Models;
using Microsoft.EntityFrameworkCore;

namespace Habitaru.Repositories.IRepositories
{
    public interface IHabitRepository
    {
        public List<IdNameCurStreakDate> GetIdNameCurStreakDate();
        public List<Habit> GetAll();
        public Habit GetById(int? id);
        public void Add(Habit userHabit);
        public void Delete(Habit userHabit);
        public void Update(Habit userHabit);
        public void Save();
    }
}
