using ProgSchool.BLL.DTO;
using ProgSchool.BLL.Infastructure;
using ProgSchool.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.BLL.interfaces
{
    public interface IDirectionService
    {
        Task<BaseResponse<bool>> CreateDirectionAsync(DirectionModel model);
        Task<BaseResponse<IEnumerable<Direction>>> GetAllDirectionAsync();
        Task<BaseResponse<bool>> UpdateDirectionAsync(int id, DirectionModel model);
        Task<BaseResponse<bool>> DeleteDirectionAsync(int id);
    }
}
