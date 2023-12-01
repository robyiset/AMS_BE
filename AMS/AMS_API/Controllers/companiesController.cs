using AMS_API.Models;
using AMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class companiesController : ControllerBase
    {
        private readonly companiesService service;
        public companiesController(IConfiguration configuration)
        {
            service = new companiesService(configuration);
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
        public async Task<IActionResult> newData(List<AddCompany> req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest( "User ID not found in token");
            }
            try
            {
                if (req.Any())
                {
                    returnService result = await service.newDataRange(req, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return BadRequest( result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest( "Failed to create new company");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("updateData")]
        public async Task<IActionResult> updateData(companies req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest( "User ID not found in token");
            }
            try
            {
                if (req != null)
                {
                    req.id_user = Convert.ToInt32(id_user.Value);
                    returnService result = await service.updateData(req);
                    if (!result.status)
                    {
                        return BadRequest( result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest( "Failed to create new company");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("updateAddress")]
        public async Task<IActionResult> updateAddress(company_location req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest( "User ID not found in token");
            }
            try
            {
                if (req != null || req.id_company != null || req.id_company > 0)
                {
                    returnService result = await service.updateAddress(req, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return BadRequest( result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest( "Failed to update company address");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("deleteData")]
        public async Task<IActionResult> deleteData(companies req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest( "User ID not found in token");
            }
            try
            {
                if (req != null)
                {
                    req.id_user = Convert.ToInt32(id_user.Value);
                    returnService result = await service.deleteData(req);
                    if (!result.status)
                    {
                        return BadRequest( result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest( "Failed to delete company");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("removeData")]
        public async Task<IActionResult> removeData(int? id_company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
            if (id_user == null && !int.TryParse(id_user.Value, out int userId))
            {
                return BadRequest( "User ID not found in token");
            }
            try
            {
                if (id_company != null)
                {
                    returnService result = await service.removeData(id_company);
                    if (!result.status)
                    {
                        return BadRequest( result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return BadRequest( "Failed to remove company");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
