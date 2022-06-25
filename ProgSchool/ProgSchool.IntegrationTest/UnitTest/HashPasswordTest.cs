using ProgSchool.BLL.Helpers;
using Xunit;

namespace ProgSchool.IntegrationTest.UnitTest
{
    public class HashPasswordTest
    {
        [Fact]
        public void Succsessfull_Hashing_Password()
        {
            //Arrange
            string password = "test123";
            //Act
            string hashPass = password.HashPassword();
            //Assert
            Assert.NotEqual(password, hashPass);
        }

        [Fact]
        public void Failed_Verification_Of_Invalid_Password()
        {
            //Arrange
            string password = "123456";
            //Act
            string hashPass = password.HashPassword();
            bool isVerification = hashPass.VerifyHashedPassword("12345");
            //Assert
            Assert.False(isVerification);
        }

        [Fact]
        public void Succsessfull_Verification_Of_Valid_Password()
        {
            //Arrange
            string password = "Abcqwe123";
            //Act
            string hashPass = password.HashPassword();
            bool isVerification = hashPass.VerifyHashedPassword("Abcqwe123");
            //Assert
            Assert.True(isVerification);
        }
    }
}
