using Dapper;
using Npgsql;
using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
using ProgSchool.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Repositories
{
    public class TeacherRepository:ITeacherRepository
    {
        private string _connection = null;
        public TeacherRepository(string connection) => _connection = connection;

        public async Task<bool> CreateTeacherAsync(Teacher teacher)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = "INSERT INTO teachers (first_name, last_name, direction_id, user_id)" +
                    " VALUES (@FirstName, @LastName, @DirectionId, @UserId)";
                await db.ExecuteAsync(query, teacher);
                return true;
            }
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryAsync<TeacherDto>
                    ("SELECT teacher_id as Id, first_name as FirstName, last_name as LastName," +
                    " direction_name as DirectionName FROM teachers " +
                    "JOIN directions USING(direction_id)");
            }
        }

        public async Task<TeacherDto> GetTeacherAsync(int id)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryFirstOrDefaultAsync<TeacherDto>
                    ("SELECT teacher_id as Id, first_name as FirstName, last_name as LastName," +
                    " direction_name as DirectionName FROM teachers " +
                    "JOIN directions USING(direction_id) " +
                    $"WHERE teacher_id={id}");
            }
        }

        public async Task<TeacherDto> GetTeacherByUserIdAsync(int userId)
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryFirstOrDefaultAsync<TeacherDto>
                    ("SELECT teacher_id as Id, first_name as FirstName, last_name as LastName, " +
                    "direction_name as DirectionName, direction_id as DirectionId FROM teachers " +
                    "JOIN directions USING(direction_id)" +
                    $"WHERE user_id={userId}");
            }
        }

    }
}
