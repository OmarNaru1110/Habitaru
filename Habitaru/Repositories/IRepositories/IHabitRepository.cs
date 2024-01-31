using Habitaru.Models;
using Microsoft.EntityFrameworkCore;

namespace Habitaru.Repositories.IRepositories
{
    public interface IHabitRepository
    {
        public List<IdNameCurStreakDate> GetIdNameCurStreakDate(int userId);
        public List<Habit> GetAll();
        public Habit GetById(int? id);
        public void Add(Habit userHabit);
        public void Delete(Habit userHabit);
        public void Update(int id, string newHabit);
        public void Save();
    }
}
