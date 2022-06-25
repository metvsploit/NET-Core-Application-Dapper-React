using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Interfaces
{
    public interface ISheduleRepository
    {
        Task<bool> CreateSheduleAsync(Shedule shedule);
        Task<IEnumerable<SheduleDto>> GetAllSheduleAsync();
        Task<IEnumerable<SheduleDto>> GetSheduleByDirectionNameAsync(string name);
        Task<SheduleDto> GetSheduleBySheduleIdAsync(int id);
        Task<bool> UpdateSheduleAsync(int id, Shedule shedule);
        Task<bool> DeleteSheduleAsync(int id);
        Task<IEnumerable<SheduleDto>> GetSheduleByTeacherIdAsync(int id);
    }
}
