using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProgSchool.BLL.Models;
using ProgSchool.WebApi;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProgSchool.IntegrationTest.ControllerTests
{
    public class UserControllerTest
    {
        private readonly HttpClient _httpClient;
        public UserControllerTest()
        {
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            _httpClient = webHost.CreateClient();
        }
        [Fact]
        public async Task Succsessfull_Login_To_Account()
        {
            //Arrange
            var loginModel = new LoginModel { Email = "teacher@mail.ru", Password = "123456" };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(loginModel),
                Encoding.UTF8, "application/json");
            //Act
            var response = await _httpClient.PostAsync("api/user/login", content);
            var token = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.NotNull(token);
            Assert.DoesNotContain("token", token);
        } 
    }
}
