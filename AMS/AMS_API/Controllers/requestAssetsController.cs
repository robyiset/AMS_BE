using AMS_API.Models;
using AMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class requestAssetsController : ControllerBase
    {
        private readonly requestableAssetService service;
        public requestAssetsController(IConfiguration configuration)
        {
            service = new requestableAssetService(configuration);
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
        public async Task<IActionResult> newData(add_request_asset req)
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
                    return BadRequest("Failed to create request asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("acceptRequest")]
        public async Task<IActionResult> acceptRequest(accept_request_asset req)
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
                    returnService result = await service.acceptRequest(req, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return BadRequest(result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest("Failed to accept request asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("updateData")]
        public async Task<IActionResult> updateData(update_request_asset req)
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
                    return BadRequest("Failed to update request asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("deleteData")]
        public async Task<IActionResult> deleteData(int id_request)
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
                if (id_request != null || id_request > 0)
                {
                    returnService result = await service.deleteData(id_request, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return BadRequest(result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest("Failed to delete request asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("removeData")]
        public async Task<IActionResult> removeData(int? id_request)
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
                if (id_request != null || id_request > 0)
                {
                    returnService result = await service.removeData(id_request);
                    if (!result.status)
                    {
                        return BadRequest(result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest("Failed to remove request asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
