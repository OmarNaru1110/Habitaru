using Habitaru.Context;
using Habitaru.Models;
using Microsoft.EntityFrameworkCore;

namespace Habitaru.BLL
{
    public class HabitBLL : IHabitBLL
    {
        private readonly HabitContext _context;
        public HabitBLL(HabitContext context)
        {
            _context = context;
        }
        public List<IdNameCurStreakDate> GetIdNameCurStreakDate()
        {
            var query = _context.Habits.Select(x =>
                new IdNameCurStreakDate {
                    Id = x.Id,
                    Name = x.Name,
                    CurStreakDate = x.CurStreakDate
                }
            );
            return query.ToList();
        }
        public List<Habit> GetAll() => _context.Habits.ToList();
        public bool Add(Habit userHabit)
        {
            if (userHabit == null)
                return false;
            var result = _context.Habits.Add(userHabit);
            return true;
        }
        public bool Delete(int? id)
        {
            if(id == null) 
                return false;
            var userHabit = _context.Habits.FirstOrDefault(x=>x.Id==id.Value);
            if (userHabit == null)
                return false;
            _context.Habits.Remove(userHabit);
            return true;
        }

        public bool Update(int? id)
        {
            if (id == null) 
                return false;
            var userHabit = _context.Habits.FirstOrDefault(x => x.Id == id.Value);
            if (userHabit == null)
                return false;
            _context.Habits.Update(userHabit);
            return true;
        }
    }
}
