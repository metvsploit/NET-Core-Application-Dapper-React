using Dapper;
using Npgsql;
using ProgSchool.DAL.Entities;
using ProgSchool.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connection = null;

        public UserRepository(string connection) => _connection = connection;

        public async Task<bool> CreateUserAsync(User entity)
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                var query = "INSERT INTO users (email, password, role) VALUES (@Email, @Password, @Role)";
                await db.ExecuteAsync(query, entity);
                return true;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = "DELETE FROM users WHERE user_id=@id";
                await db.ExecuteAsync(query, new {id});
                return true;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryAsync<User>
                    ("SELECT user_id Id, email Email, password Password, role Role FROM users " +
                    "ORDER BY user_id=@Id");
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryFirstOrDefaultAsync<User>
                    ("SELECT user_id as Id, email as Email, password as Password, role as Role FROM users " +
                    $"WHERE user_id = {id}");
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryFirstOrDefaultAsync<User>
                    ("SELECT user_id as Id, email as Email, password as Password, role as Role FROM users " +
                    $"WHERE Email = '{email}'");
            }
        }
    }
}
