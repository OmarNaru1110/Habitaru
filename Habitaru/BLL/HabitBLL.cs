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
        public List<IdNameCurStreakDate> GetNameAndCurStreakDate()
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
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
