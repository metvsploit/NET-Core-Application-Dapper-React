using Microsoft.IdentityModel.Tokens;
using ProgSchool.DAL.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProgSchool.BLL.Services
{
    public static class TokenBuilder
    {
        public static string GenerateJWT(User user)
        {
            var claims = new[] {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            };

            var token = new JwtSecurityToken(
              issuer: AuthOptions.ISSUER,
              audience: AuthOptions.AUDIENCE,
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static JwtSecurityToken GetUserData(string token)
        {
            try
            {
                JwtSecurityToken data = new JwtSecurityTokenHandler().ReadJwtToken(token);
                return data;
            }
            catch
            {
                return null;
            }
        }
    }
    public static class AuthOptions
    {
        public const string ISSUER = "progschool"; 
        public const string AUDIENCE = "client"; 
        const string KEY = "mysupersecret_secretkey!228";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
