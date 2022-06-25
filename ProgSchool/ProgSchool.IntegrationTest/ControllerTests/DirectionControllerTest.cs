using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProgSchool.BLL.DTO;
using ProgSchool.BLL.Infastructure;
using ProgSchool.DAL.Entities;
using ProgSchool.WebApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProgSchool.IntegrationTest.ControllerTests
{
    public class DirectionControllerTest
    {
        private readonly HttpClient _httpClient;

        public DirectionControllerTest()
        {
            _httpClient = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { }).CreateClient();
        }

        [Fact]
        public async Task Successful_Receipt_Of_All_Directions()
        {
            //Act
            var response = await _httpClient.GetAsync("api/direction");
            string json = await response.Content.ReadAsStringAsync();
            var directions = JsonConvert.DeserializeObject<BaseResponse<List<Direction>>>(json);
            //Assert
            Assert.Equal(4, directions.Data.Count);
            Assert.NotNull(directions.Data[0].DirectionName);
        }

        [Fact]
        public async Task Unsuccessful_Creation_Of_Direction_When_ModelStateError()
        {
            // Arrange
            var model = new DirectionModel { DirectionName = "" };
            var jsonModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync("api/direction", content);

            //Assert
            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Unsuccessful_Creation_Of_Direction_When_If_Exists_DirectionName()
        {
            // Arrange
            var model =  new DirectionModel { DirectionName = "Бизнес аналитика" };
            var jsonModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync("api/direction", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<bool>>(jsonResponse);

            //Assert
            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("Данное направление уже существует!", baseResponse.Message);
            Assert.False(baseResponse.Data);
        }

        [Fact]
        public async Task Succsessful_Creation_Of_Direction_When_By_Teacher()
        {
            //Arrange
            var model = new DirectionModel { DirectionName = "test" };
            var jsonModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVhY2hlckBtYWlsLnJ1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVGVhY2hlciIsImV4cCI6MTY1NTIyMjg0MSwiaXNzIjoicHJvZ3NjaG9vbCIsImF1ZCI6ImNsaWVudCJ9.K5EWhlOR43zg0mt-iWRp5LeL4V_My7gRI628A0iZ98o";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Act
            var response = await _httpClient.PostAsync("api/direction", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<bool>>(jsonResponse);
            var result = await _httpClient.GetAsync("api/direction");
            var json = await result.Content.ReadAsStringAsync();
            var directions = JsonConvert.DeserializeObject<BaseResponse<List<Direction>>>(json);

            //Arrange
            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("Направление успешно создано", baseResponse.Message);
            Assert.True(baseResponse.Data);
            Assert.Equal(5, directions.Data.Count);
        }

        [Fact]
        public async Task Succsessfull_Removal_Of_Direction_By_Teacher()
        {
            //Arrange
            int id = 6;
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVhY2hlckBtYWlsLnJ1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVGVhY2hlciIsImV4cCI6MTY1NTIyMjg0MSwiaXNzIjoicHJvZ3NjaG9vbCIsImF1ZCI6ImNsaWVudCJ9.K5EWhlOR43zg0mt-iWRp5LeL4V_My7gRI628A0iZ98o";

            //Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"api/direction/{id}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<bool>>(jsonResponse);

            //Arrange
            Assert.True(baseResponse.Data);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Succsessfull_Direction_Update_By_Teacher()
        {
            //Arrange
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVhY2hlckBtYWlsLnJ1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVGVhY2hlciIsImV4cCI6MTY1NTIyMjg0MSwiaXNzIjoicHJvZ3NjaG9vbCIsImF1ZCI6ImNsaWVudCJ9.K5EWhlOR43zg0mt-iWRp5LeL4V_My7gRI628A0iZ98o";
            int id = 1;
            var model = new DirectionModel { DirectionName = "Системная аналитика" };
            var jsonModel = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            //Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsync($"api/direction/{id}", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<bool>>(jsonResponse);
            var result = await _httpClient.GetAsync("api/direction");
            var json = await result.Content.ReadAsStringAsync();
            var directions = JsonConvert.DeserializeObject<BaseResponse<List<Direction>>>(json);

            //Assert
            Assert.True(baseResponse.Data);
            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("Системная аналитика", directions.Data[0].DirectionName);
        }
    }
}
