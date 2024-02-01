using Habitaru.Controllers;
using Habitaru.Models;
using Habitaru.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitaru.Tests.ControllersTests
{
    [TestFixture]
    internal class AccountControllerTests
    {
        Mock<UserManager<ApplicationUser>> _userManager;
        Mock<SignInManager<ApplicationUser>> _signInManager;
        Mock<RoleManager<ApplicationRole>> _roleManager;

        [SetUp]
        public void Setup()
        {
            Mock<IUserStore<ApplicationUser>> _userStoreMock =new Mock<IUserStore<ApplicationUser>>();
            _userManager = new Mock<UserManager<ApplicationUser>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);   
            _userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            _userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());
        
            _signInManager = new Mock<SignInManager<ApplicationUser>>(
                _userManager.Object,
                 new Mock<IHttpContextAccessor>().Object,
                 Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                 null,
                 null,
                 null,
                 null
                );

            Mock<IRoleStore<ApplicationRole>> _roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();
            _roleManager = new Mock<RoleManager<ApplicationRole>>(_roleStoreMock.Object,null,null,null,null);
        }
        [Test]
        public async Task Register_InvalidModelState_ReturnsSameView()
        {
            //arrange
            var accountController = new AccountController(
                _userManager.Object,_signInManager.Object,_roleManager.Object);

            accountController.ModelState.AddModelError("", "test");

            var temp = new UserRegisterationVM();
            
            //act
            var result = await accountController.Register(temp);

            //assert
            Assert.That(result,Is.TypeOf<ViewResult>());

        }
        [Test]
        public async Task Register_ValidModelState_SuccessfulIdentityResult_ReturnsRedirectToActionResult()
        {
            //arrange
            var accountController = new AccountController(
                _userManager.Object, _signInManager.Object, _roleManager.Object);
            
            _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _roleManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationRole { Name = "Admin" });

            var temp = new UserRegisterationVM();

            //act
            var result = await accountController.Register(temp);

            //assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

        }
        [Test]
        public async Task Register_ValidModelState_FailedIdentityResult_ReturnsSameView()
        {
            //arrange
            var accountController = new AccountController(
                _userManager.Object, _signInManager.Object, _roleManager.Object);

            _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var temp = new UserRegisterationVM();

            //act
            var result = await accountController.Register(temp);

            //assert
            Assert.That(result, Is.TypeOf<ViewResult>());

        }

    }
}
