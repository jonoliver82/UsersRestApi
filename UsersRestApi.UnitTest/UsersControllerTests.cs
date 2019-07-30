using Microsoft.VisualStudio.TestTools.UnitTesting;
using UsersRestApi.Controllers;
using UsersRestApi.Interfaces;
using Moq;
using UsersRestApi.Domain;
using Microsoft.AspNetCore.Mvc;
using UsersRestApi.UnitTest.Helpers;

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

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUsersFinderService = new Mock<IUsersFinderService>();

            _controller = new UsersController(_mockUserRepository.Object, _mockUsersFinderService.Object);
        }

        [TestMethod]
        public void GetReturnsEmailWhenUserExists()
        {
            // Arrange
            _mockUsersFinderService.Setup(m => m.FindUserEmailById(It.Is<int>(x => x == 1))).Returns(new Email("1@example.com"));

            // Act
            var result = _controller.GetEmail(1);

            // Assert
            var email = AssertHelpers.IsOkResult<Email>(result);
            Assert.AreEqual(new Email("1@example.com"), email);
        }

        [TestMethod]
        public void GetReturnsEmptyEmailWhenUserDoesNotExist()
        {
            // Arrange
            _mockUsersFinderService.Setup(m => m.FindUserEmailById(It.IsAny<int>())).Returns(new Email(string.Empty));

            // Act
            var result = _controller.GetEmail(1);

            // Assert
            var email = AssertHelpers.IsOkResult<Email>(result);
            Assert.AreEqual(new Email(string.Empty), email);
        }
    }
}
