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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository) => _repository = repository;

        public async Task<BaseResponse<bool>> CreateStudentAsync(StudentModel model)
        {
            var response = new BaseResponse<bool>();

            try
            {
                Student student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DirectionId = model.DirectionId,
                    UserId = model.UserId,
                };
                response.Data = await _repository.CreateStudentAsync(student);
                response.Message = "Новый студент добавлен в базу данных";
                return response;
            }
            catch
            {
                return new BaseResponse<bool>
                {
                    Message = "Ошибка добавления студента. Обратитесь к администратору",
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<StudentDto>>> GetAllStudentsAsync()
        {
            var response = new BaseResponse<IEnumerable<StudentDto>>();

            try
            {
                var students = await _repository.GetAllStudentsAsync();
                response.Data = students;
                response.Message = "Успешный запрос";
                return response;
            }
            catch
            {
                return new BaseResponse<IEnumerable<StudentDto>>
                { Message = "Ошибка запроса.Обратитесь к администратору" };
            }
        }

        public async Task<BaseResponse<StudentDto>> GetStudentByIdAsync(int id)
        {
            var response = new BaseResponse<StudentDto>();

            try
            {
                var teacher = await _repository.GetStudentByIdAsync(id);
                if (teacher == null)
                {
                    response.Message = "Студент не найден";
                    return response;
                }
                response.Data = teacher;
                response.Message = "Успешный запрос";
                return response;
            }
            catch
            {
                return new BaseResponse<StudentDto> { Message = "Ошибка запроса.Обратитесь к администратору" };
            }
        }

        public async Task<BaseResponse<StudentDto>> GetStudentByUserIdAsync(int userId)
        {
            var response = new BaseResponse<StudentDto>();

            try
            {
                var student = await _repository.GetStudentByUserIdAsync(userId);

                if (student == null)
                {
                    response.Message = "Профиль не заполнен";
                    return response;
                }

                response.Data = student;
                response.Message = "Успешный запрос";
                return response;
            }
            catch
            {
                return new BaseResponse<StudentDto>
                { Message = "Неизвестная ошибка.Обратитесь к администратору" };
            }
        }
    }
}
