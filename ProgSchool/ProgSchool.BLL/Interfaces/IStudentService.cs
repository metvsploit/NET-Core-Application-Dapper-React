using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgSchool.BLL.Interfaces
{
    public interface IStudentService
    {
        Task<BaseResponse<bool>> CreateStudentAsync(StudentModel model);
        Task<BaseResponse<IEnumerable<StudentDto>>> GetAllStudentsAsync();
        Task<BaseResponse<StudentDto>> GetStudentByIdAsync(int id);
        Task<BaseResponse<StudentDto>> GetStudentByUserIdAsync(int userId);
    }
}
