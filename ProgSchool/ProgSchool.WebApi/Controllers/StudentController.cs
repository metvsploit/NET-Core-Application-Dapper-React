using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.Interfaces;
using ProgSchool.BLL.Models;
using ProgSchool.BLL.Services;
using ProgSchool.DAL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController:ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service) => _service = service;

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<StudentDto>>> GetAllStudentsAsync()
        {
            var response = await _service.GetAllStudentsAsync();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentAsync(int id)
        {
            var response = await _service.GetStudentByIdAsync(id);

            if (response.Data == null)
                return NotFound();

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateStudentAsync(StudentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if (role != "Student")
                return Unauthorized(role);

            var response = await _service.CreateStudentAsync(model);

            if (response.Data == false)
                return BadRequest();

            return Ok(response);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetStudentByUserId()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var data = TokenBuilder.GetUserData(token).Claims.ToList();
            string role = data[1].Value;
            int userId = int.Parse(data[0].Value);

            if (role != "Student")
                return Unauthorized(role);

            var response = await _service.GetStudentByUserIdAsync(userId);

            if(response.Data == null)
                return BadRequest(response);
            
            return Ok(response);
        }
    }
}
