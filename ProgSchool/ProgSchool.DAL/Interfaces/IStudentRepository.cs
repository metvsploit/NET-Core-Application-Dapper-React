using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Interfaces
{
    public interface IStudentRepository
    {
        Task<bool> CreateStudentAsync(Student student);
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<StudentDto> GetStudentByUserIdAsync(int userId);
    }
}
