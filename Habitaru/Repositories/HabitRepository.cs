using Habitaru.BLL.Context;
using Habitaru.Models;
using Habitaru.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Habitaru.BLL
{
    public class HabitRepository : IHabitRepository
    {
        private readonly HabitContext _context;
        public HabitRepository(HabitContext context)
        {
            _context = context;
        }
        public List<IdNameCurStreakDate> GetIdNameCurStreakDate(int userId)
        {
            var query = _context.Habits.Where(x => x.UserId == userId)
                .Select(x => new IdNameCurStreakDate
                {
                    Id = x.Id,
                    Name = x.Name,
                    CurStreakDate = x.CurStreakDate,
                    CurStreakPeriod = DateTime.Now - x.CurStreakDate
                });
            return query.ToList();
        }
        public List<Habit> GetAll() => _context.Habits.ToList();
        public void Add(Habit userHabit)
        {
            _context.Habits.Add(userHabit);
            Save();
        }
        public Habit GetById(int? id)
        {
            var userHabit = _context.Habits.FirstOrDefault(x => x.Id == id);
            return userHabit;
        }
        public void Delete(Habit userHabit)
        {
            _context.Habits.Remove(userHabit);
            Save();
        }
        public void Update(Habit userHabit)
        {
            _context.Habits.Update(userHabit);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
