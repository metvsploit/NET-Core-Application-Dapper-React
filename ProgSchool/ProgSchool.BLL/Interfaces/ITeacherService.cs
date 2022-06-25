using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgSchool.BLL.Interfaces
{
    public interface ITeacherService
    {
        Task<BaseResponse<bool>> CreateTeacherAsync(TeacherModel model);
        Task<BaseResponse<IEnumerable<TeacherDto>>> GetAllTeachersAsync();
        Task<BaseResponse<TeacherDto>> GetTeacherByIdAsync(int id);
        Task<BaseResponse<TeacherDto>> GetTeacherByUserIdAsync(int userId);
    }
}
