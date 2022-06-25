using ProgSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User entity);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string enail);
        Task<bool> DeleteUserAsync(int id);
        
    }
}
