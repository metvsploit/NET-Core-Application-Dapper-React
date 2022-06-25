using ProgSchool.BLL.DTO;
using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.interfaces;
using ProgSchool.DAL.Entities;
using ProgSchool.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.BLL.Services
{
    public class DirectionService : IDirectionService
    {
        private readonly IDirectionRepository _repository;

        public DirectionService(IDirectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> CreateDirectionAsync(DirectionModel model)
        {
            var response = new BaseResponse<bool>();
            try
            {
                Direction direction = new Direction { DirectionName = model.DirectionName};
                var entity = await _repository.GetDirectionByName(model.DirectionName);
                if (entity != null)
                {
                    response.Data = false;
                    response.Message = "Данное направление уже существует!";
                    return response;
                }
                await _repository.CreateDirectionAsync(direction);
                response.Data = true;
                response.Message = "Направление успешно создано";
                return response;
            }
            catch
            {
                return new BaseResponse<bool>
                { Message =  $"Возникла неизвестная ошибка. Обратитесь к администратору " };
            }
        }

        public async Task<BaseResponse<bool>> DeleteDirectionAsync(int id)
        {
            var response = new BaseResponse<bool>();

            try
            {
                bool isDelete = await _repository.DeleteDirectionAsync(id);
                if(!isDelete)
                {
                    response.Data = false;
                    response.Message = "Возникла ошибка при удалении направления. Повторите попытку позже";
                    return response;
                }
                response.Data = true;
                response.Message = "Направление успешно удалено";
                return response;
            }
            catch
            {
                return new BaseResponse<bool> 
                { Message = "Возникла неизвестная ошибка. Обратитесь к администратору" };
            }
        }

        public async Task<BaseResponse<IEnumerable<Direction>>> GetAllDirectionAsync()
        {
            var response = new BaseResponse<IEnumerable<Direction>>();

            try
            {
                var directions = await _repository.GetAllDirectionsAsync();
                response.Data = directions;
                response.Message = "Успешное выполнение запроса";
                return response;
            }
            catch
            {
                return new BaseResponse<IEnumerable<Direction>>
                { Message = "Возникла неизвестная ошибка. Обратитесь к администратору" };
            }
        }

        public async Task<BaseResponse<bool>> UpdateDirectionAsync(int id, DirectionModel model)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var direct = await _repository.GetDirectionByIdAsync(id);
                if(direct == null)
                {
                    response.Data = false;
                    response.Message = "Данного направления не существует";
                    return response;
                }
                direct.Id = id;
                direct.DirectionName = model.DirectionName;
                await _repository.UpdateDirectionAsync(direct);
                response.Data = true;
                response.Message = "Объект успешно обновлен";
                return response;
            }
            catch
            {
                return new BaseResponse<bool> { Message = "Неизвестная ошибка. Обратитесь к администратору" };
            }
        }
    }
}
