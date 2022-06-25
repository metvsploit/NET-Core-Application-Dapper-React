using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Interfaces;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
using ProgSchool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.BLL.Services
{
    public class SheduleService:ISheduleService
    {
        private readonly ISheduleRepository _repository;

        public SheduleService(ISheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> CreateSheduleAsync(SheduleModel model)
        {
            var response = new BaseResponse<bool>();
            var shedule = new Shedule
            {
                DirectionId = model.DirectionId,
                TeacherId = model.TeacherId,
                Date = model.Date,
            };

            try
            {
                response.Data = await _repository.CreateSheduleAsync(shedule);
                response.Message = "Расписание создано";
                return response;
            }
            catch
            {
                return new BaseResponse<bool>
                {
                    Message = "Неизвестная ошибка.Обратитесь к администратору",
                    Data = false
                };
            }
        }


        public async Task<BaseResponse<IEnumerable<SheduleDto>>> GetSheduleByDirectionNameAsync(string name)
        {
            var response = new BaseResponse<IEnumerable<SheduleDto>>();

            try
            {
                name = name.Replace("%20", " ");
                var shedule = await _repository.GetSheduleByDirectionNameAsync(name);
                if(shedule == null)
                {
                    response.Message = "Расписание отсутствует";
                    return response;
                }
                response.Message = "Расписание получено";
                response.Data = shedule;
                return response;
            }
            catch
            {
                return new BaseResponse<IEnumerable<SheduleDto>>
                { Message = "Неизвестная ошибка. Обратитесь к администратору" };
            }
        }

        public async Task<BaseResponse<IEnumerable<SheduleDto>>> GetAllSheduleAsync()
        {
            var response = new BaseResponse<IEnumerable<SheduleDto>>();

            try
            {
                var shedule = await _repository.GetAllSheduleAsync();
                
                if(shedule == null)
                {
                    response.Message = "Расписание отсутствует";
                    return response;
                }

                response.Data = shedule;
                response.Message = "Расписание получено";
                return response;
            }
            catch
            {
                return new BaseResponse<IEnumerable<SheduleDto>>
                { Message = "Неизвестная ошибка. Обратитесь к администратору" };
            }
        }

        public async Task<BaseResponse<bool>> UpdateSheduleAsync(int id, SheduleModel model)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var shedule = await _repository.GetSheduleBySheduleIdAsync(id);
                if(shedule == null)
                {
                    response.Message = "Расписания не существует";
                    return response;
                }
                var entity = new Shedule
                {
                    DirectionId = model.DirectionId,
                    TeacherId = model.TeacherId,
                    Date = model.Date,
                };
                response.Data = await _repository.UpdateSheduleAsync(id,entity);
                response.Message = "Расписание обновлено";
                return response;
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool> { Message = $"Неизвестная ошибка. Обратитесь к администратору {ex.Message} " };
            }
        }

        public async Task<BaseResponse<bool>> DeleteSheduleAsync(int id)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var shedule = await _repository.GetSheduleBySheduleIdAsync(id);
                if(shedule == null)
                {
                    response.Data = false;
                    response.Message = "Расписания не существует";
                    return response;
                }
                response.Data = await _repository.DeleteSheduleAsync(id);
                response.Message = "Расписание удалено";
                return response;
            }
            catch
            {
                return new BaseResponse<bool> {
                    Data = false, Message = "Неизвестная ошибка.Обратитесь к администратору"
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<SheduleDto>>> GetSheduleByTeacherIdAsync(int id)
        {
            var response = new BaseResponse<IEnumerable<SheduleDto>>();

            try
            {
                var shedule = await _repository.GetSheduleByTeacherIdAsync(id);

                if (shedule == null)
                {
                    response.Message = "Расписание отсутствует";
                    return response;
                }

                response.Data = shedule;
                response.Message = "Расписание получено";
                return response;
            }
            catch
            {
                return new BaseResponse<IEnumerable<SheduleDto>>
                { Message = "Неизвестная ошибка. Обратитесь к администратору" };
            }
        }
    }
}
