using AMS_API.Models;
using AMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class suppliersController : ControllerBase
    {
        private readonly suppliersService service;
        public suppliersController(IConfiguration configuration)
        {
            service = new suppliersService(configuration);
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
        public async Task<IActionResult> newData(suppliers req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return StatusCode(401, "User ID not found in token");
            }
            try
            {
                if (req != null)
                {
                    req.id_user = Convert.ToInt32(id_user.Value);
                    returnService result = await service.newData(req);
                    if (!result.status)
                    {
                        return StatusCode(401, result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return StatusCode(401, "Failed to create new supplier");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("updateData")]
        public async Task<IActionResult> updateData(suppliers req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return StatusCode(401, "User ID not found in token");
            }
            try
            {
                if (req != null)
                {
                    req.id_user = Convert.ToInt32(id_user.Value);
                    returnService result = await service.updateData(req);
                    if (!result.status)
                    {
                        return StatusCode(401, result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return StatusCode(401, "Failed to update supplier");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("updateAddress")]
        public async Task<IActionResult> updateAddress(locations req, int id_company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return StatusCode(401, "User ID not found in token");
            }
            try
            {
                if (req != null)
                {
                    returnService result = await service.updateAddress(req, id_company, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return StatusCode(401, result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return StatusCode(401, "Failed to update supplier address");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("deleteData")]
        public async Task<IActionResult> deleteData(suppliers req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return StatusCode(401, "User ID not found in token");
            }
            try
            {
                if (req != null)
                {
                    req.id_user = Convert.ToInt32(id_user.Value);
                    returnService result = await service.deleteData(req);
                    if (!result.status)
                    {
                        return StatusCode(401, result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return StatusCode(401, "Failed to delete supplier");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("removeData")]
        public async Task<IActionResult> removeData(int? id_supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return StatusCode(401, "User ID not found in token");
            }
            try
            {
                if (id_supplier != null)
                {
                    returnService result = await service.removeData(id_supplier);
                    if (!result.status)
                    {
                        return StatusCode(401, result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return StatusCode(401, "Failed to remove supplier");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
