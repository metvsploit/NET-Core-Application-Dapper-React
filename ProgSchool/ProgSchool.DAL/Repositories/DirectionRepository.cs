using Dapper;
using ProgSchool.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace ProgSchool.DAL.Repositories
{
    public class DirectionRepository : IDirectionRepository
    {
        private string _connection = null;

        public DirectionRepository(string connection)
        {
            _connection = connection;
        }
        public async Task<bool> CreateDirectionAsync(Direction entity)
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                var query = $"INSERT INTO directions (direction_name) VALUES(@DirectionName)";
                await db.ExecuteAsync(query, new {DirectionName = entity.DirectionName});
                return true;
            }
        }

        public async Task<bool> DeleteDirectionAsync(int id)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = "DELETE FROM directions WHERE direction_id=@id";
                await db.ExecuteAsync(query, new {id});
                return true;
            }
        }

        public async Task<IEnumerable<Direction>> GetAllDirectionsAsync()
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryAsync<Direction>
                    ("SELECT direction_id Id, direction_name DirectionName FROM directions " +
                    "ORDER BY direction_id");
            }
        }

        public async Task<Direction> GetDirectionByIdAsync(int id)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryFirstOrDefaultAsync<Direction>
                    ("SELECT direction_id Id, direction_name DirectionName FROM directions " +
                    $"WHERE direction_id = {id}");
            }
        }

        public async Task<Direction> GetDirectionByName(string name)
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryFirstOrDefaultAsync<Direction>
                    ($"SELECT direction_name FROM directions WHERE direction_name='{name}'");
            }
        }

        public async Task<bool> UpdateDirectionAsync(Direction entity)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                var query = "UPDATE directions SET direction_name = @DirectionName WHERE direction_id = @Id";
                await db.ExecuteAsync(query, entity);
                return true;
            }
        }
    }
}
