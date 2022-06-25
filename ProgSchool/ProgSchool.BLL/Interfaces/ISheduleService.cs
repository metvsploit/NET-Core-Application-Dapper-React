using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.BLL.Interfaces
{
    public interface ISheduleService
    {
        Task<BaseResponse<bool>> CreateSheduleAsync(SheduleModel model);
        Task<BaseResponse<IEnumerable<SheduleDto>>> GetSheduleByDirectionNameAsync(string name);
        Task<BaseResponse<IEnumerable<SheduleDto>>> GetAllSheduleAsync();
        Task<BaseResponse<bool>> UpdateSheduleAsync(int id, SheduleModel model);
        Task<BaseResponse<bool>> DeleteSheduleAsync(int id);
        Task<BaseResponse<IEnumerable<SheduleDto>>> GetSheduleByTeacherIdAsync(int id);
    }
}
