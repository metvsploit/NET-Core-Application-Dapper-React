using Dapper;
using Npgsql;
using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
using ProgSchool.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Repositories
{
    public class SheduleRepository : ISheduleRepository
    {
        private string _connection = null;

        public SheduleRepository(string connection)
        {
            _connection = connection;
        }
        public async Task<bool> CreateSheduleAsync(Shedule shedule)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = $"INSERT INTO shedule (direction_id, teacher_id, date_start)" +
                    " VALUES(@DirectionId, @TeacherId, @Date)";
                await db.ExecuteAsync(query, shedule);
                return true;
            }
        }

        public async Task<bool> DeleteSheduleAsync(int id)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = "DELETE FROM shedule WHERE shedule_id=@id";
                await db.ExecuteAsync(query, new { id });
                return true;
            }
        }

        public async Task<IEnumerable<SheduleDto>> GetAllSheduleAsync()
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = "SELECT s.shedule_id AS Id, d.direction_name AS DirectionName," +
                    "CONCAT(t.first_name, ' ', t.last_name) AS TeacherName," +
                    "s.date_start AS DateTime " +
                    "FROM shedule AS s " +
                    "JOIN directions AS d USING(direction_id) " +
                    "JOIN teachers AS t USING(teacher_id) " +
                    "ORDER BY s.date_start";

                return await db.QueryAsync<SheduleDto>(query);
            }
        }

        public async Task<SheduleDto> GetSheduleBySheduleIdAsync(int id)
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                var query = "SELECT s.shedule_id AS Id, d.direction_name AS DirectionName," +
                    "CONCAT(t.first_name, ' ', t.last_name) AS TeacherName," +
                    "s.date_start AS DateTime " +
                    "FROM shedule AS s " +
                    "JOIN directions AS d USING(direction_id) " +
                    "JOIN teachers AS t USING(teacher_id) " +
                    $"WHERE s.shedule_id= {id}";

                return await db.QueryFirstOrDefaultAsync<SheduleDto>(query);
            }
        }

        public async Task<IEnumerable<SheduleDto>> GetSheduleByDirectionNameAsync(string name)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = "SELECT s.shedule_id AS Id, d.direction_name AS DirectionName," +
                    "CONCAT(t.first_name, ' ', t.last_name) AS TeacherName," +
                    "s.date_start AS DateTime " +
                    "FROM shedule AS s " +
                    "JOIN directions AS d USING(direction_id) " +
                    "JOIN teachers AS t USING(teacher_id) " +
                    $"WHERE d.direction_name = '{name}' " +
                    "ORDER BY s.date_start";

                return await db.QueryAsync<SheduleDto>(query);
            }
        }

        public async Task<bool> UpdateSheduleAsync(int id, Shedule shedule)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = $"UPDATE shedule SET direction_id = @DirectionId," +
                    $" teacher_id=@TeacherId, date_start=@Date WHERE shedule_id = {id}";
                await db.ExecuteAsync(query, shedule);
                return true;
            }
        }

        public async Task<IEnumerable<SheduleDto>> GetSheduleByTeacherIdAsync(int id)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = "SELECT s.shedule_id AS Id, d.direction_name AS DirectionName," +
                    "CONCAT(t.first_name, ' ', t.last_name) AS TeacherName," +
                    "s.date_start AS DateTime " +
                    "FROM shedule AS s " +
                    "JOIN directions AS d USING(direction_id) " +
                    "JOIN teachers AS t USING(teacher_id) " +
                    $"WHERE t.teacher_id = {id} " +
                    "ORDER BY s.date_start";
                return await db.QueryAsync<SheduleDto>(query);
            }
            
        }
    }
}
