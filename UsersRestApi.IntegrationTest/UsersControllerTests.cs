using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UsersRestApi.Domain;

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
    }
}
