using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace ProgSchool.BLL.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<string[]>> LoginAsync(LoginModel model);
        Task<BaseResponse<bool>> CreateUserAsync(RegisterModel model);
        Task<BaseResponse<bool>> DeleteUserAsync(int id);
        Task<BaseResponse<IEnumerable<User>>> GetUsersAsync();
    }
}
