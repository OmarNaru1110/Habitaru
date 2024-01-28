using Habitaru.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Habitaru.BLL.Context
{
    public class HabitContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public HabitContext(DbContextOptions<HabitContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habit>(habit =>
            {
                //set up primary key
                habit.HasKey(x => x.Id);
                habit.HasOne(h => h.User)
                .WithMany(u => u.Habits)
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ApplicationUser>(user =>
            {
                user.HasIndex(x => x.UserName)
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Habit> Habits { get; set; }
    }
}
