using AMS_API.Models;
using AMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class assetsController : ControllerBase
    {
        private readonly assetsService service;
        public assetsController(IConfiguration configuration)
        {
            service = new assetsService(configuration);
        }
        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> getData(string? search = null)
        //{
        //    try
        //    {
        //        return Ok(await service.getData(search));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        [Authorize]
        [HttpPost("newData")]
        public async Task<IActionResult> newData(assets req)
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
    }
}
