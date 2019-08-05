using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.IntegrationTest
{
    /// <summary>
    /// See https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.2
    /// </summary>
    [TestClass]
    public class UsersControllerTests 
    {
        private WebApplicationFactory<UsersRestApi.Startup> _factory;
        private HttpClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task GetWhenValidUserShouldReturnEmailAddress()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/users/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var email = JsonConvert.DeserializeObject<Email>(content);
            Assert.AreEqual("1@example.com", email.Address);
        }

        [TestMethod]
        public async Task GetWhenInvalidUserShouldReturnNotFound()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/users/5");

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task PostShouldAddUserWhenValid()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "test",
                Email = "test@example.com",
                Password = "password",
            };
            var json = JsonConvert.SerializeObject(request);

            // Act
            var response = await _client.PostAsync("/api/users", new StringContent(json, Encoding.UTF8, "application/json"));

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsTrue(response.Headers.Location.OriginalString.EndsWith("/api/users/2", StringComparison.CurrentCultureIgnoreCase));
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserCreationResponse>(content);
            Assert.AreEqual(2, result.Id);
            Assert.AreEqual(0, result.Errors.Count());
        }

        [TestMethod]
        public async Task PostShouldReturnBadRequestWhenEmailInvalid()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "test",
                Email = "invalid.com",
                Password = "password",
            };
            var json = JsonConvert.SerializeObject(request);

            // Act
            var response = await _client.PostAsync("/api/users", new StringContent(json, Encoding.UTF8, "application/json"));

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserCreationResponse>(content);
            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(1, result.Errors.Count());
            CollectionAssert.Contains(result.Errors.ToList(), "Bad email - invalid.com");
        }


        [TestMethod]
        public async Task PostShouldReturnBadRequestWhenPasswordInvalid()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "test",
                Email = "test@valid.com",
                Password = "pass",
            };
            var json = JsonConvert.SerializeObject(request);

            // Act
            var response = await _client.PostAsync("/api/users", new StringContent(json, Encoding.UTF8, "application/json"));

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserCreationResponse>(content);
            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(1, result.Errors.Count());
            CollectionAssert.Contains(result.Errors.ToList(), "Password too weak");
        }

        [TestMethod]
        public async Task PostShouldReturnBadRequestWhenEmailAlreadyRegistered()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "test",
                Email = "1@example.com",
                Password = "password",
            };
            var json = JsonConvert.SerializeObject(request);

            // Act
            var response = await _client.PostAsync("/api/users", new StringContent(json, Encoding.UTF8, "application/json"));

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserCreationResponse>(content);
            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(1, result.Errors.Count());
            CollectionAssert.Contains(result.Errors.ToList(), "Not unique email address - 1@example.com");
        }


    }
}
