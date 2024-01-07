namespace Habitaru.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResetCount { get; set; } = 0;
        public DateTime FirstStreakDate { get; set; }
        public DateTime CurStreakDate { get; set; }
        // all in minutes
        public int MaxStreakPeriod { get; set; } = int.MinValue;
        public int MinStreakPeriod { get; set; } = int.MaxValue;
        public int AvgStreakPeriod { get; set; } = 1;
        public int PrevStreakPeriod { get; set; } = 1;

    }
}
