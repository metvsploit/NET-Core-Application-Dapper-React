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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service) => _service = service;

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<TeacherDto>>> GetAllTeachersAsync()
        {
            var response = await _service.GetAllTeachersAsync();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherAsync(int id)
        {
            var response = await _service.GetTeacherByIdAsync(id);

            if(response.Data == null)
                return NotFound();

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTeacherAsync(TeacherModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if (role != "Teacher")
                return Unauthorized(role);

            var response = await _service.CreateTeacherAsync(model);

            if (response.Data == false)
                return BadRequest();

            return Ok(response);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetTeacherByUserId()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var data = TokenBuilder.GetUserData(token).Claims.ToList();
            string role = data[1].Value;
            int userId = int.Parse(data[0].Value);

            if (role != "Teacher")
                return Unauthorized(role);

            var response = await _service.GetTeacherByUserIdAsync(userId);

            if (response.Data == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
