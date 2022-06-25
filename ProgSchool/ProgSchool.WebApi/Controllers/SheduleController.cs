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
    public class SheduleController:ControllerBase
    {
        private readonly ISheduleService _service;

        public SheduleController(ISheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<SheduleDto>>> GetAllDirectionsAsync()
        {
            var response = await _service.GetAllSheduleAsync();
            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSheduleAsync([FromBody] SheduleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if (role != "Teacher")
                return Unauthorized(role);

            var response = await _service.CreateSheduleAsync(model);
            if(!response.Data)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSheduleAsync(int id, [FromBody] SheduleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if (role != "Teacher")
                return Unauthorized(role);

            var response = await _service.UpdateSheduleAsync(id, model);
            if (!response.Data)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSheduleAsync(int id)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if (role != "Teacher")
                return Unauthorized(role);

            var response = await _service.DeleteSheduleAsync(id);
            if (!response.Data)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetSheduleByDirectionNameAsync(string name)
        {
            var response = await _service.GetSheduleByDirectionNameAsync(name);

            if(response.Data == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet("teacher/{id}")]
        public async Task<BaseResponse<IEnumerable<SheduleDto>>> GetSheduleByTeacherIdAsync(int id)
        {
            var response = await _service.GetSheduleByTeacherIdAsync(id);
            return response;
        }
    }
}
