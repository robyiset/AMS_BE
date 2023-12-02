using AMS_API.Models;
using AMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class assetMaintenancesController : ControllerBase
    {
        private readonly assetMaintenanceService service;
        public assetMaintenancesController(IConfiguration configuration)
        {
            service = new assetMaintenanceService(configuration);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> getData(string? search = null)
        {
            try
            {
                return Ok(await service.getData(search));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPost("newData")]
        public async Task<IActionResult> newData(add_maintenance req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest("User ID not found in token");
            }
            try
            {
                if (req != null)
                {
                    returnService result = await service.newData(req, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return BadRequest(result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest("Failed to create maintenance asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("updateData")]
        public async Task<IActionResult> updateData(update_maintenance req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest("User ID not found in token");
            }
            try
            {
                if (req != null)
                {
                    returnService result = await service.updateData(req, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return BadRequest(result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest("Failed to update maintenance asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("deleteData")]
        public async Task<IActionResult> deleteData(int id_maintenance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest("User ID not found in token");
            }
            try
            {
                if (id_maintenance != null || id_maintenance > 0)
                {
                    returnService result = await service.deleteData(id_maintenance, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return BadRequest(result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest("Failed to delete maintenance asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("removeData")]
        public async Task<IActionResult> removeData(int? id_maintenance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest("User ID not found in token");
            }
            try
            {
                if (id_maintenance != null)
                {
                    returnService result = await service.removeData(id_maintenance);
                    if (!result.status)
                    {
                        return BadRequest(result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest("Failed to remove maintenance asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
