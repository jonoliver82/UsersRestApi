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

        [TestInitialize]
        public void TestInitialize()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task GetWhenValidUserShouldReturnEmailAddress()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/users/1");

            // Assert
            response.EnsureSuccessStatusCode(); // Status code 200-299
            var content = await response.Content.ReadAsStringAsync();
            var email = JsonConvert.DeserializeObject<Email>(content);
            Assert.AreEqual("1@example.com", email.Address);
        }

        [TestMethod]
        public async Task PostShouldAddUser()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new UserCreationRequest
            {
                Name = "test",
                Email = "test@example.com",
                Password = "password",
            };
            var json = JsonConvert.SerializeObject(request);

            // Act
            var response = await client.PostAsync("/api/users", new StringContent(json, Encoding.UTF8, "application/json"));

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsTrue(response.Headers.Location.OriginalString.EndsWith("/api/users/2", StringComparison.CurrentCultureIgnoreCase));
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserCreationResponse>(content);
            Assert.AreEqual(2, result.Id);
            Assert.AreEqual(0, result.Errors.Count());
        }

        // TODO Post errors
    }
}
