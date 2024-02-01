using Habitaru.BLL;
using Habitaru.BLL.Context;
using Habitaru.Models;
using Habitaru.Tests.TestModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitaru.Tests.RepositoryTests
{
    [TestFixture]
    public class HabitRepositoryTests
    {
        [Test]
        public void GetIdNameCurStreakDate_returnsExactCount()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<HabitContext>()
                .UseInMemoryDatabase(databaseName: "habit_context1").Options;
            using (var context = new HabitContext(options))
            {
                var repository = new HabitRepository(context);
                foreach (var item in HabitObjs.LoadHabits())
                {
                    repository.Add(item);
                }

                //act
                var list = repository.GetIdNameCurStreakDate(1);
                var actualList = new List<HabitIdNameDate>();
                foreach (var item in list)
                {
                    actualList.Add(new HabitIdNameDate
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CurDate = item.CurStreakDate
                    });
                }
                var expectedList = new List<HabitIdNameDate>()
                {
                    HabitObjs.LoadHabitIdNameCurStreakDate()[0],
                    HabitObjs.LoadHabitIdNameCurStreakDate()[1]
                };

                //assert
                Assert.That(actualList.Count, Is.EqualTo(2));
                //CollectionAssert.AreEquivalent(expectedList, actualList);
            }
        }
        [Test]
        public void GetIdNameCurStreakDate_returnsExactItems()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<HabitContext>()
                .UseInMemoryDatabase(databaseName: "habit_context2").Options;
            using (var context = new HabitContext(options))
            {
                var repository = new HabitRepository(context);
                foreach (var item in HabitObjs.LoadHabits())
                {
                    repository.Add(item);
                }

                //act
                var list = repository.GetIdNameCurStreakDate(2);
                var actualList = new List<HabitIdNameDate>();
                foreach (var item in list)
                {
                    actualList.Add(new HabitIdNameDate
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CurDate = item.CurStreakDate
                    });
                }
                var expectedList = new List<HabitIdNameDate>()
                {
                    HabitObjs.LoadHabitIdNameCurStreakDate()[2],
                    HabitObjs.LoadHabitIdNameCurStreakDate()[3]
                };

                //assert
                CollectionAssert.AreEquivalent(expectedList, actualList);
            }

        }
        [Test]
        public void GetIdNameCurStreakDate_returnsEmptyList()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<HabitContext>()
                .UseInMemoryDatabase(databaseName: "habit_context3").Options;
            using (var context = new HabitContext(options))
            {
                var repository = new HabitRepository(context);
                foreach (var item in HabitObjs.LoadHabits())
                {
                    repository.Add(item);
                }

                //act
                var list = repository.GetIdNameCurStreakDate(5);

                //assert
                Assert.That(list, Is.Empty);
            }
        }
    }
}
