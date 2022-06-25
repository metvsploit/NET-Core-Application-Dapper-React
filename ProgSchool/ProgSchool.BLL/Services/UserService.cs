using ProgSchool.BLL.Helpers;
using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Interfaces;
using ProgSchool.BLL.Models;
using ProgSchool.DAL.Entities;
using ProgSchool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgSchool.BLL.Services
{
    public class UserService:IUserService
    {

        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> CreateUserAsync(RegisterModel model)
        {
            var response = new BaseResponse<bool>();
            User user = new User { Email = model.Email, Password = model.Password.HashPassword() };
            try
            {
                var users = await _repository.CreateUserAsync(user);
                response.Data = true;
                response.Message = "Пользователь успешно создан";
                return response;
            }
            catch
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Message = "Неизвестная ошибка. Обратитесь к администратору"
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteUserAsync(int id)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var user = await _repository.GetUserByIdAsync(id);
                if(user == null)
                {
                    response.Data = false;
                    response.Message = "Данного пользователя не существует";
                    return response;
                }
                await _repository.DeleteUserAsync(id);
                response.Data = true;
                response.Message = "Пользователь успешно удалён";
                return response;
            }
            catch
            {
                return new BaseResponse<bool>
                      { Data = false, Message = $"Неизвестная ошибка. Обраритесь к администратору" };
            }
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetUsersAsync()
        {
            var response = new BaseResponse<IEnumerable<User>>();

            try
            {
                var users = await _repository.GetAllUsersAsync();
                response.Data = users;
                response.Message = "Успешный запрос";
                return response;
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<User>> { Message = $"Ошибка запроса {ex.Message}" };
            }
        }


        public async Task<BaseResponse<string[]>> LoginAsync(LoginModel model)
        {
            var response = new BaseResponse<string[]>();
            response.Data = new string[2];

            try
            {
                var user = await _repository.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    response.Data[0] = "token";
                    response.Message = "Пользователь не найден";
                    return response;
                }
                if(!user.Password.VerifyHashedPassword(model.Password))
                {
                    response.Data[0] = "token";
                    response.Message = "Неверный пароль";
                    return response;
                }
                response.Data[0] = TokenBuilder.GenerateJWT(user);
                response.Data[1] = user.Role.ToString();
                response.Message = "Успешный вход";
                return response;
            }
            catch(Exception ex)
            {
                return new BaseResponse<string[]>
                { Data = new string[1] {"token"}, Message = $"Неизвестная ошибка {ex.Message} {ex.StackTrace}.Обратитесь к администратору" };
            }
        }
    }
}
