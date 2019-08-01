using Microsoft.VisualStudio.TestTools.UnitTesting;
using UsersRestApi.Controllers;
using UsersRestApi.Interfaces;
using Moq;
using UsersRestApi.Domain;
using Microsoft.AspNetCore.Mvc;
using UsersRestApi.UnitTest.Helpers;
using UsersRestApi.Models;
using UsersRestApi.Exceptions;
using UsersRestApi.Core;
using System.Linq;

namespace UsersRestApi.UnitTest
{
    /// <summary>
    /// See https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.2
    /// </summary>
    [TestClass]
    public class UsersControllerTests
    {
        private UsersController _controller;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IUsersFinderService> _mockUsersFinderService;
        private Mock<IUserFactory> _mockUserFactory;

        private Mock<IValidationExceptionHandler> _mockValidationExceptionHandler;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUsersFinderService = new Mock<IUsersFinderService>();
            _mockUserFactory = new Mock<IUserFactory>();

            _mockValidationExceptionHandler = new Mock<IValidationExceptionHandler>();
            
            _controller = new UsersController(_mockUserRepository.Object,
                _mockUsersFinderService.Object, 
                _mockUserFactory.Object);
        }

        [TestMethod]
        public void GetReturnsEmailWhenUserExists()
        {
            // Arrange
            _mockUsersFinderService.Setup(m => m.FindUserEmailById(It.Is<int>(x => x == 1))).Returns(new Maybe<Email>(new Email("1@example.com")));

            // Act
            var result = _controller.GetEmail(1);

            // Assert
            var email = AssertHelpers.IsOkResult<Email>(result);
            Assert.AreEqual(new Email("1@example.com"), email);
        }

        [TestMethod]
        public void GetReturnsNotFoundWhenUserDoesNotExist()
        {
            // Arrange
            _mockUsersFinderService.Setup(m => m.FindUserEmailById(It.IsAny<int>())).Returns(new Maybe<Email>());

            // Act
            var result = _controller.GetEmail(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateRaisesPasswordTooWeakExceptionWhenNoPassowrd()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Email = "weak@example.com",
                Name = "weak",
            };

            // Act
            var result = _controller.Create(request);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            var response = result.Value;
            Assert.AreEqual(0, response.Id);
            Assert.AreEqual(1, response.Errors.Count());
            CollectionAssert.Contains(response.Errors.ToList(), "Password too weak");
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordTooWeakException))]
        public void CreateRaisesPasswordTooWeakExceptionWhenPasswordTooShort()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Email = "weak@example.com",
                Name = "weak",
                Password = "weak"
            };

            // Act
            var result = _controller.Create(request);

            // Assert - Expected Exception
        }

        [TestMethod]
        [ExpectedException(typeof(BadEmailException))]
        public void CreateRaisesBadEmailExceptionWhenEmailNotValid()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Email = "invalid.com",
                Name = "email",
                Password = "password"
            };

            // Act
            var result = _controller.Create(request);

            // Assert - Expected Exception
        }

        [TestMethod]
        [ExpectedException(typeof(BadEmailException))]
        public void CreateRaisesBadEmailExceptionWhenEmailMissing()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "noemail",
                Password = "password"
            };

            // Act
            var result = _controller.Create(request);

            // Assert - Expected Exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotUniqueEmailAddress))]
        public void CreateRaisesNotUniqueEmailAddressWhenEmailAlreadyRegistered()
        {
            // Arrange
            var value = new Email("1@example.com");
            _mockUserFactory.Setup(m => m.Create(It.IsAny<string>(), value, It.IsAny<Password>(), _mockValidationExceptionHandler.Object))
                .Throws(new NotUniqueEmailAddress(value));

            var request = new UserCreationRequest
            {
                Name = "noemail",
                Email = "1@example.com",
                Password = "password"
            };

            // Act
            var result = _controller.Create(request);

            // Assert - Expected Exception
        }        
    }
}
