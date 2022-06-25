using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.DTO;
using ProgSchool.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProgSchool.IntegrationTest.ControllerTests
{
    public class SheduleControllerTest
    {
        private readonly HttpClient _httpClient;

        public SheduleControllerTest()
        {
            _httpClient = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { }).CreateClient();
        }

        [Fact]
        public async Task Succsessful_Get_All_Shedule()
        {
            //Act
            var response = await _httpClient.GetAsync("api/shedule");
            string json = await response.Content.ReadAsStringAsync();
            var shedules = JsonConvert.DeserializeObject<BaseResponse<List<SheduleDto>>>(json);
            //Assert
            Assert.Equal(7, shedules.Data.Count);
            Assert.NotNull(shedules.Data[0].DirectionName);
            Assert.Equal("OK",response.StatusCode.ToString()); 
        }

        [Fact]
        public async Task Succsessfull_Create_Shedule_By_Teacher()
        {
            //Arrange
            var model = new SheduleModel { DirectionId = 1, TeacherId = 2, Date = DateTime.Now };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                "application/json");
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlRlYWNoZXIiLCJleHAiOjE2NTU0Njg4ODEsImlzcyI6InByb2dzY2hvb2wiLCJhdWQiOiJjbGllbnQifQ.tJImQM2rHmUlKrHEe71SW7aIraLLVshVV-KV0vFv-oY";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //Act
            var response = await _httpClient.PostAsync($"api/shedule", content);
            string json = await response.Content.ReadAsStringAsync();
            var result = await _httpClient.GetAsync("api/shedule");
            var jsonResult = await result.Content.ReadAsStringAsync();
            var shedules = JsonConvert.DeserializeObject<BaseResponse<List<SheduleDto>>>(jsonResult);
            //Assert
            Assert.Equal(8, shedules.Data.Count);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Unsuccessfull_Create_Shedule_By_Student()
        {
            //Arrange
            var model = new SheduleModel { DirectionId = 1, TeacherId = 2, Date = DateTime.Now };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                "application/json");
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //Act
            var response = await _httpClient.PostAsync($"api/shedule", content);
            //Assert
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Successfull_Update_Shedule_By_Teacher()
        {
            //Arrange
            int id = 11;
            var model = new SheduleModel { DirectionId = 2, TeacherId = 3, Date = DateTime.Now };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                "application/json");
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlRlYWNoZXIiLCJleHAiOjE2NTU0NzcxNDUsImlzcyI6InByb2dzY2hvb2wiLCJhdWQiOiJjbGllbnQifQ.Ec_EAalLoDzu1zseEoN_DF9FCFaYOd0IVrpStdObdGc";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //Act
            var response = await _httpClient.PutAsync($"api/shedule/{id}", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            //Arrange
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Successfull_Delete_Shedule_By_Teacher()
        {
            //Arrange
            int id = 11;
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlRlYWNoZXIiLCJleHAiOjE2NTU0NzcxNDUsImlzcyI6InByb2dzY2hvb2wiLCJhdWQiOiJjbGllbnQifQ.Ec_EAalLoDzu1zseEoN_DF9FCFaYOd0IVrpStdObdGc";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //Act
            var response = await _httpClient.DeleteAsync($"api/shedule/{id}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            //Arrange
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Unsuccessfull_Delete_Shedule_When_Id_Not_Found()
        {
            //Arrange
            int id = 11;
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlRlYWNoZXIiLCJleHAiOjE2NTU0NzcxNDUsImlzcyI6InByb2dzY2hvb2wiLCJhdWQiOiJjbGllbnQifQ.Ec_EAalLoDzu1zseEoN_DF9FCFaYOd0IVrpStdObdGc";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //Act
            var response = await _httpClient.DeleteAsync($"api/shedule/{id}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            //Arrange
            Assert.Equal("BadRequest", response.StatusCode.ToString());
            Assert.Contains("Расписания не существует", jsonResponse);
        }

        [Fact]
        public async Task Successfull_Get_Shedule_By_DirectionId()
        {
            //Arrange 
            int id = 4;
            //Act
            var response = await _httpClient.GetAsync($"api/shedule/{id}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var shedule = JsonConvert.DeserializeObject<BaseResponse<IEnumerable<SheduleDto>>>(jsonResponse);
            //Arrange
            Assert.NotNull(shedule);
        }
    }
}
