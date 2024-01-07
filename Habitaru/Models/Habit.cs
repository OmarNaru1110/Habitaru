namespace Habitaru.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResetCount { get; set; } = 0;
        public DateTime FirstStreakDate { get; set; }
        public DateTime CurStreakDate { get; set; }
        public DateTime MaxStreakPeriod{ get; set; } = DateTime.MinValue;
        public DateTime MinStreakPeriod { get; set; } = DateTime.MaxValue;
        public DateTime AvgStreakPeriod { get; set; }
        public DateTime PrevStreakPeriod{ get; set; } = DateTime.MinValue;

    }
}
