using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Habitaru.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; } 
        [Required]
        public string Name { get; set; }
        public int ResetCount { get; set; } = 0;
        [Required]
        public DateTime FirstStreakDate { get; set; }
        public DateTime CurStreakDate { get; set; }
        // all in minutes and present them in days hours and minutes
        public int MaxStreakPeriod { get; set; } = int.MinValue;
        public int MinStreakPeriod { get; set; } = int.MaxValue;
        public int AvgStreakPeriod { get; set; } = 1;
        public int PrevStreakPeriod { get; set; } = 1;

    }
}
