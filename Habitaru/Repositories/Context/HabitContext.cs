using Habitaru.Models;
using Microsoft.EntityFrameworkCore;

namespace Habitaru.BLL.Context
{
    public class HabitContext : DbContext
    {
        public HabitContext(DbContextOptions<HabitContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habit>(habit =>
            {
                //set up primary key
                habit.HasKey(x => x.Id);
            });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Habit> Habits { get; set; }
    }
}
