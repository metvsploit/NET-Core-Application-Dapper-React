using ProgSchool.BLL.Services;
using ProgSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ProgSchool.IntegrationTest.UnitTest
{
    public class TokenHelperTest
    {
        [Fact]
        public void Succsessfull_Get_Token()
        {
            //Arrange
            var user = new User { Id = 2, Email = "teacher@mail.ru", Password = "123456", Role = DAL.Enum.Role.Teacher };
            //Act
            var token = TokenBuilder.GenerateJWT(user);
            //Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void Succsessfull_Get_Data_User()
        {
            //Arrange
            var user = new User { Id = 6, Email = "test@mail.ru", Password = "12345", Role = DAL.Enum.Role.Student };
            var token = TokenBuilder.GenerateJWT(user);
            //Act
            var data = TokenBuilder.GetUserData(token);
            string role = data.Claims.ToList()[1].Value;
            string id = data.Claims.ToList()[0].Value;
            //Assert
            Assert.Equal("Student", role);
            Assert.Equal("6", id);
        }
    }
}
