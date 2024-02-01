using Habitaru.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Habitaru.Tests.RepositoryTests
{
    [TestFixture]
    internal class AccountRepositoryTests
    {
        [Test]
        public void GetCurrentUserId_WhenUserIsLoggedIn_ShouldReturnUserId()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claims = new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, "123")
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            httpContextAccessorMock.Setup(x => x.HttpContext.User)
                .Returns(principal);

            var accountRepository = new AccountRepository(httpContextAccessorMock.Object);
            // Act
            var result = accountRepository.GetCurrentUserId();

            // Assert
            Assert.That(result, Is.EqualTo(123));
        }
        [Test]
        public void GetCurrentUserId_WhenUserIsNotFound_ShouldReturnNull()
        {
            //arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>
            {
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            httpContextAccessorMock.Setup(x => x.HttpContext.User)
                .Returns(principal);

            var repository = new AccountRepository(httpContextAccessorMock.Object);
            //act
            var result = repository.GetCurrentUserId();
            Assert.That(result, Is.Null);
        }
    }
}
