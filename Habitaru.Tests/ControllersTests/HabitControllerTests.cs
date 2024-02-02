using Habitaru.Controllers;
using Habitaru.Services;
using Habitaru.Services.IServices;
using Habitaru.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitaru.Tests.ControllersTests
{
    [TestFixture]
    internal class HabitControllerTests
    {
        Mock<IHabitService> _serviceMock;
        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IHabitService>();
        }

        [Test]
        public void Add_FutureDate_ReturnsSameView()
        {
            //arrange
            var controller = new HabitController(_serviceMock.Object);
            var habit = new HabitCreationVM { FirstStreakDate = DateTime.Now.AddDays(5) };

            //act
            controller.Add(habit);
            var result = controller.ModelState.Values.Any(x => x.Errors.Any(x => x.ErrorMessage == "Your date can't be in the future"));
            //assert
            Assert.That(result, Is.True);
        }
    }
}
