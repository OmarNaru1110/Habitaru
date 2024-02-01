using Habitaru.Models;
using Habitaru.Tests.TestModels;
using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitaru.Tests
{
    internal static class HabitObjs
    {
        static List<Habit> habits = new List<Habit> {
        new Habit
                {
                    Id = 1,
                    UserId = 1,
                    Name="a",
                    CurStreakDate = DateTime.Now,
                },
                new Habit
                {
                    Id = 2,
                    UserId = 1,
                    Name="b",
                    CurStreakDate = DateTime.Now,
                },
                new Habit
                {
                    Id = 3,
                    UserId = 2,
                    Name="c",
                    CurStreakDate = DateTime.Now,
                },
                new Habit
                {
                    Id = 4,
                    UserId = 2,
                    Name="e",
                    CurStreakDate = DateTime.Now,
                }
        };
        public static List<Habit> LoadHabits()
        {
            return habits;
        }
        public static List<HabitIdNameDate> LoadHabitIdNameCurStreakDate()
        {
            var list = new List<HabitIdNameDate>();
            foreach (var h in habits)
            {
                list.Add(new HabitIdNameDate
                {
                    Id = h.Id,
                    Name = h.Name,
                    CurDate = h.CurStreakDate,
                });
            }
            return list;
        }
    }
}
