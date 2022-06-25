using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgSchool.BLL.DTO;
using ProgSchool.BLL.Infastructure;
using ProgSchool.BLL.interfaces;
using ProgSchool.BLL.Services;
using ProgSchool.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectionController : ControllerBase
    {
        private readonly IDirectionService _service;
        
        public DirectionController(IDirectionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<Direction>>> GetAllDirectionsAsync()
        {
            var response = await _service.GetAllDirectionAsync();
            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateDirectionAsync([FromBody]DirectionModel direction)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if(role != "Teacher")
                return Unauthorized(role);

            var response = await _service.CreateDirectionAsync(direction);
            if (response.Data)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirectionAsync(int id)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if (role != "Teacher")
                return Unauthorized(role);

            var response = await _service.DeleteDirectionAsync(id);
            if(response.Data)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirectionAsync(int id, [FromBody] DirectionModel direction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string role = TokenBuilder.GetUserData(token).Claims.ToList()[1].Value;

            if (role != "Teacher")
                return Unauthorized(role);

            var response = await _service.UpdateDirectionAsync(id, direction);

            if (response.Data)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
