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
using System.Collections.Generic;

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
                _mockUserFactory.Object,
                _mockValidationExceptionHandler.Object);
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
        public void CreateReturnsCreatedWhenValidData()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Email = "new@example.com",
                Name = "NewUser",
                Password = "strongpassword"
            };
            _mockValidationExceptionHandler.SetupGet(p => p.HasErrors).Returns(false);
            _mockUserFactory.Setup(m => m.Create(It.IsAny<string>(), It.IsAny<Email>(), It.IsAny<Password>(), _mockValidationExceptionHandler.Object))
                .Returns(new Maybe<User>(new User("NewUser", new Email("new@example.com"), new Password("strongpassword"))));
            
            // Act
            var result = _controller.Create(request);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            var createdObject = (CreatedAtActionResult)result.Result;

            Assert.IsInstanceOfType(createdObject.Value, typeof(UserCreationResponse));
            var response = (UserCreationResponse)createdObject.Value;

            Assert.AreEqual(0, response.Id);
            Assert.AreEqual(0, response.Errors.Count());

            _mockUserRepository.Verify(m => m.Add(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void CreateRepsondsWithErrorWhenNoPassword()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Email = "weak@example.com",
                Name = "weak",
            };
            _mockValidationExceptionHandler.SetupGet(p => p.HasErrors).Returns(true);
            _mockValidationExceptionHandler.SetupGet(p => p.Errors)
                .Returns(new List<string> { "Password too weak" });

            // Act
            var result = _controller.Create(request);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badObject = (BadRequestObjectResult)result.Result;

            Assert.IsInstanceOfType(badObject.Value, typeof(UserCreationResponse));
            var response = (UserCreationResponse)badObject.Value;

            _mockValidationExceptionHandler.Verify(m => m.Add(It.IsAny<ValidationException>()), Times.Once);

            Assert.AreEqual(0, response.Id);
            Assert.AreEqual(1, response.Errors.Count());
            CollectionAssert.Contains(response.Errors.ToList(), "Password too weak");
        }

        [TestMethod]
        public void CreateResondsWithErrorWhenPasswordTooShort()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Email = "weak@example.com",
                Name = "weak",
                Password = "weak"
            };
            _mockValidationExceptionHandler.SetupGet(p => p.HasErrors).Returns(true);
            _mockValidationExceptionHandler.SetupGet(p => p.Errors)
                .Returns(new List<string> { "Password too weak" });

            // Act
            var result = _controller.Create(request);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badObject = (BadRequestObjectResult)result.Result;

            Assert.IsInstanceOfType(badObject.Value, typeof(UserCreationResponse));
            var response = (UserCreationResponse)badObject.Value;

            _mockValidationExceptionHandler.Verify(m => m.Add(It.IsAny<ValidationException>()), Times.Once);

            Assert.AreEqual(0, response.Id);
            Assert.AreEqual(1, response.Errors.Count());
            CollectionAssert.Contains(response.Errors.ToList(), "Password too weak");
        }

        [TestMethod]
        public void CreateRespondsWithErrorWhenEmailNotValid()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Email = "invalid.com",
                Name = "email",
                Password = "password"
            };
            _mockValidationExceptionHandler.SetupGet(p => p.HasErrors).Returns(true);
            _mockValidationExceptionHandler.SetupGet(p => p.Errors)
                .Returns(new List<string> { "Bad email - invalid.com" });

            // Act
            var result = _controller.Create(request);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badObject = (BadRequestObjectResult)result.Result;

            Assert.IsInstanceOfType(badObject.Value, typeof(UserCreationResponse));
            var response = (UserCreationResponse)badObject.Value;

            _mockValidationExceptionHandler.Verify(m => m.Add(It.IsAny<ValidationException>()), Times.Once);

            Assert.AreEqual(0, response.Id);
            Assert.AreEqual(1, response.Errors.Count());
            CollectionAssert.Contains(response.Errors.ToList(), "Bad email - invalid.com");
        }

        [TestMethod]
        public void CreateRespondsWithErrorWhenEmailMissing()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "noemail",
                Password = "password"
            };
            _mockValidationExceptionHandler.SetupGet(p => p.HasErrors).Returns(true);
            _mockValidationExceptionHandler.SetupGet(p => p.Errors)
                .Returns(new List<string> { "Bad email - " });

            // Act
            var result = _controller.Create(request);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badObject = (BadRequestObjectResult)result.Result;

            Assert.IsInstanceOfType(badObject.Value, typeof(UserCreationResponse));
            var response = (UserCreationResponse)badObject.Value;

            _mockValidationExceptionHandler.Verify(m => m.Add(It.IsAny<ValidationException>()), Times.Once);

            Assert.AreEqual(0, response.Id);
            Assert.AreEqual(1, response.Errors.Count());
            CollectionAssert.Contains(response.Errors.ToList(), "Bad email - ");
        }

        [TestMethod]
        public void CreateRespondsWithErrorsWhenEmailAndPasswordMissing()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "missing",
            };
            _mockValidationExceptionHandler.SetupGet(p => p.HasErrors).Returns(true);
            _mockValidationExceptionHandler.SetupGet(p => p.Errors)
                .Returns(new List<string> { "Password too weak", "Bad email - " });

            // Act
            var result = _controller.Create(request);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badObject = (BadRequestObjectResult)result.Result;

            Assert.IsInstanceOfType(badObject.Value, typeof(UserCreationResponse));
            var response = (UserCreationResponse)badObject.Value;

            _mockValidationExceptionHandler.Verify(m => m.Add(It.IsAny<ValidationException>()), Times.Exactly(2));

            Assert.AreEqual(0, response.Id);
            Assert.AreEqual(2, response.Errors.Count());
            CollectionAssert.Contains(response.Errors.ToList(), "Password too weak");
            CollectionAssert.Contains(response.Errors.ToList(), "Bad email - ");
        }

        [TestMethod]
        public void CreateRespondsWithBadRequestWhenEmailAlreadyRegistered()
        {
            // Arrange
            _mockUserFactory.Setup(m => m.Create(It.IsAny<string>(), It.IsAny<Email>(), It.IsAny<Password>(), _mockValidationExceptionHandler.Object))
                .Returns(new Maybe<User>());
            _mockValidationExceptionHandler.SetupGet(p => p.Errors)
                .Returns(new List<string> { "Not unique email address - 1@example.com" });

            var request = new UserCreationRequest
            {
                Name = "noemail",
                Email = "1@example.com",
                Password = "password"
            };

            // Act
            var result = _controller.Create(request);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badObject = (BadRequestObjectResult)result.Result;

            Assert.IsInstanceOfType(badObject.Value, typeof(UserCreationResponse));
            var response = (UserCreationResponse)badObject.Value;

            Assert.AreEqual(0, response.Id);
            Assert.AreEqual(1, response.Errors.Count());
            CollectionAssert.Contains(response.Errors.ToList(), "Not unique email address - 1@example.com");
        }        
    }
}
