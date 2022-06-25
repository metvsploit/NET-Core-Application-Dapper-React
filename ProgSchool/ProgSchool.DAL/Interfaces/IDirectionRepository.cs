using ProgSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgSchool.DAL.Repositories
{
    public interface IDirectionRepository
    {
        Task<bool> CreateDirectionAsync(Direction entity);
        Task<IEnumerable<Direction>> GetAllDirectionsAsync();
        Task<Direction> GetDirectionByIdAsync(int id);
        Task<bool> UpdateDirectionAsync(Direction entity);
        Task<bool> DeleteDirectionAsync(int id);
        Task<Direction> GetDirectionByName(string name);
    }
}
