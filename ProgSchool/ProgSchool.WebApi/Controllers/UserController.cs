using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Interfaces;
using ProgSchool.BLL.Models;
using ProgSchool.BLL.Services;
using ProgSchool.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service) => _service = service;

        [HttpGet]
        [Authorize]
        public async Task<BaseResponse<IEnumerable<User>>> GetAllDirectionsAsync()
        {
            var response = await _service.GetUsersAsync();
            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if (role != "Teacher")
                return Unauthorized(role);

            var response = await _service.CreateUserAsync(model);

            if(!response.Data)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.LoginAsync(model);
            if(response.Data[0] == "token")
                return BadRequest(response);

            HttpContext.Response.Cookies.Append("Bearer", response.Data[0]);

            return Ok(response.Data);
        }

   
        [HttpGet]
        [Route("api/[controller]/logout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Bearer");
            return Ok();
        }
    }
}
