using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Interfaces
{
    public interface ITeacherRepository
    {
        Task<bool> CreateTeacherAsync(Teacher teacher);
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto> GetTeacherAsync(int id);
        Task<TeacherDto> GetTeacherByUserIdAsync(int userId);
    }
}
