using Dapper;
using Npgsql;
using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
using ProgSchool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Repositories
{
    public class StudentRepository:IStudentRepository
    {
        private string _connection = null;
        public StudentRepository(string connection) => _connection = connection;

        public async Task<bool> CreateStudentAsync(Student student)
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                var query = "INSERT INTO students (first_name, last_name, direction_id, user_id)" +
                    " VALUES (@FirstName, @LastName, @DirectionId, @UserId)";
                await db.ExecuteAsync(query, student);
                return true;
            }
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryAsync<StudentDto>
                    ("SELECT student_id as Id, first_name as FirstName, last_name as LastName," +
                    " direction_name as DirectionName FROM students " +
                    "JOIN directions USING(direction_id)");
            }
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            using (var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryFirstOrDefaultAsync<StudentDto>
                    ("SELECT student_id as Id, first_name as FirstName, last_name as LastName," +
                    " direction_name as DirectionName FROM students " +
                    "JOIN directions USING(direction_id) " +
                    $"WHERE student_id={id}");
            }
        }

        public async Task<StudentDto> GetStudentByUserIdAsync(int userId)
        {
            using(var db = new NpgsqlConnection(_connection))
            {
                return await db.QueryFirstOrDefaultAsync<StudentDto>
                    ("SELECT student_id as Id, first_name as FirstName, last_name as LastName, " +
                    "direction_name as DirectionName FROM students " +
                    "JOIN directions USING(direction_id)" +
                    $"WHERE user_id={userId}");
            }
        }
    }
}
