using Habitaru.BLL;
using Habitaru.BLL.Context;
using Habitaru.Models;
using Habitaru.Repositories;
using Habitaru.Repositories.IRepositories;
using Habitaru.Services;
using Habitaru.Services.IServices;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitaru.Tests.Services
{
    [TestFixture]
    internal class HabitServiceTests
    {
        Mock<IHabitRepository> _habitRepository;
        Mock<IAccountService> _accountService;
        [SetUp]
        public void Setup() 
        {
            _habitRepository = new Mock<IHabitRepository>();
            _accountService = new Mock<IAccountService>();
        }

        [Test]
        public void Delete_InputNullId_ReturnsFalse()
        {
            //arrange
            HabitService habitService = new HabitService(
                _habitRepository.Object,
                _accountService.Object);
            //act
            bool result =habitService.Delete(null);
            
            //assert
            Assert.That(result, Is.False);
        }
        [Test]
        public void Delete_InputInvalidId_ReturnsFalse()
        {
            //arrange
            HabitService habitService = new HabitService(
                _habitRepository.Object,
                _accountService.Object);

            _habitRepository.Setup(x=>x.GetById(It.IsAny<int>()))
                .Returns(()=>null);
            //act
            bool result = habitService.Delete(5);

            //assert
            Assert.That(result, Is.False);
        }
        [Test]
        public void Delete_InputValidId_ReturnsTrue()
        {
            //arrange
            HabitService habitService = new HabitService(
                _habitRepository.Object,
                _accountService.Object);

            _habitRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(() => new Habit());
            //act
            bool result = habitService.Delete(5);

            //assert
            Assert.That(result, Is.True);
        }
        [Test]
        public void CreateHabitDetails_InputNullHabit_ReturnsNull()
        {
            //arrange
            HabitService habitService = new HabitService(
                _habitRepository.Object,
                _accountService.Object);
            //act
            var result = habitService.CreateHabitDetails(null);

            //assert
            Assert.That(result, Is.Null);
        }
        [Test]
        public void CreateHabitDetails_InputValidHabit_NullUserId_ReturnsNull()
        {
            //arrange
            HabitService habitService = new HabitService(
                _habitRepository.Object,
                _accountService.Object);

            _accountService.Setup(x => x.GetCurrentUserId()).Returns(()=>null);
            //act
            var result = habitService.CreateHabitDetails(null);

            //assert
            Assert.That(result, Is.Null);
        }
    }
}
