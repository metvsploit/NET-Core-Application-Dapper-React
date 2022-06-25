using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Interfaces;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.DTO;
using ProgSchool.DAL.Entities;
using ProgSchool.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repository;

        public TeacherService(ITeacherRepository repository) => _repository = repository;

        public async Task<BaseResponse<bool>> CreateTeacherAsync(TeacherModel model)
        {
            var response = new BaseResponse<bool>();

            try
            {
                Teacher teacher = new Teacher {
                    FirstName = model.FirstName, LastName = model.LastName,
                    DirectionId = model.DirectionId, UserId = model.UserId,
                };
                response.Data = await _repository.CreateTeacherAsync(teacher);
                response.Message = "Новый учитель добавлен в базу данных";
                return response;
            }
            catch
            {
                return new BaseResponse<bool>
                {
                    Message = "Ошибка добавления учителя. Обратитесь к администратору",
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<TeacherDto>>> GetAllTeachersAsync()
        {
            var response = new BaseResponse<IEnumerable<TeacherDto>>();

            try
            {
                var teachers = await _repository.GetAllTeachersAsync();
                response.Data = teachers;
                response.Message = "Успешный запрос";
                return response;
            }
            catch
            {
                return new BaseResponse<IEnumerable<TeacherDto>>
                { Message = "Ошибка запроса.Обратитесь к администратору" };
            }
        }

        public async Task<BaseResponse<TeacherDto>> GetTeacherByIdAsync(int id)
        {
            var response = new BaseResponse<TeacherDto>();

            try
            {
                var teacher = await _repository.GetTeacherAsync(id);
                if(teacher == null)
                {
                    response.Message = "Учитель не найден";
                    return response;
                }
                response.Data = teacher;
                response.Message = "Успешный запрос";
                return response;
            }
            catch
            {
                return new BaseResponse<TeacherDto> { Message = "Ошибка запроса.Обратитесь к администратору" };
            }
        }

        public async Task<BaseResponse<TeacherDto>> GetTeacherByUserIdAsync(int userId)
        {
            var response = new BaseResponse<TeacherDto>();

            try
            {
                var teacher = await _repository.GetTeacherByUserIdAsync(userId);

                if (teacher == null)
                {
                    response.Message = "Профиль не заполнен";
                    return response;
                }

                response.Data = teacher;
                response.Message = "Успешный запрос";
                return response;
            }
            catch
            {
                return new BaseResponse<TeacherDto>
                { Message = "Неизвестная ошибка.Обратитесь к администратору" };
            }
        }
    }
}
