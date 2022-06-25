using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
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
    public class TeacherControllerTest
    {
        private readonly HttpClient _httpClient;

        public TeacherControllerTest()
        {
            _httpClient = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { }).CreateClient();
        }

        [Fact]
        public async Task Succsessfull_Get_All_Teachers()
        {
            //Act
            var response = await _httpClient.GetAsync("api/teacher");
            string json = await response.Content.ReadAsStringAsync();
            var teachers = JsonConvert.DeserializeObject<BaseResponse<List<TeacherDto>>>(json);
            //Assert
            Assert.Equal(5, teachers.Data.Count);
            Assert.NotNull(teachers.Data[0].DirectionName);
        }

        [Fact]
        public async Task Succsessfull_Get_Teacher_By_Id()
        {
            //Arrange
            int id = 1;
            //Act
            var response = await _httpClient.GetAsync($"api/teacher/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var teacher = JsonConvert.DeserializeObject<BaseResponse<TeacherDto>>(json);
            //Assert
            Assert.NotNull(teacher.Data);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Unsuccsessful_Create_Teacher_When_User_IsNot_Teacher()
        {
            //Arrange 
            var teacher = new TeacherModel { DirectionId = 1, LastName = "test", FirstName = "test", UserId = 1 };
            var jsonModel = JsonConvert.SerializeObject(teacher);
            HttpContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            //Act
            var response = await _httpClient.PostAsync("api/teacher", content);
            //Arrange
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }
    }
}
