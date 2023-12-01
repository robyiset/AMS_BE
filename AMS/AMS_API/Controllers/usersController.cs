using AMS_API.Models;
using AMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private usersService service;

        public usersController(IConfiguration configuration)
        {
            service = new usersService(configuration);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] register req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (req.password != req.repeat_password || req == null)
            {
                return StatusCode(401, "Please check your password");
            }
            try
            {
                if (req != null)
                {
                    returnService result = await service.register(req);
                    if (!result.status)
                    {
                        return StatusCode(401, result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return StatusCode(401, "Failed to create user");
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
        [Authorize]
        [HttpGet("getUsers")]
        public async Task<IActionResult> getUsers(string? search = null)
        {
            try
            {
                return Ok(await service.getUsers(search));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("updateProfile")]
        public async Task<IActionResult> updateProfile([FromBody] profile req)
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
                    returnService result = await service.udpateProfile(req);
                    if (!result.status)
                    {
                        return StatusCode(401, result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return StatusCode(401, "Failed to update profile user");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("updateAddress")]
        public async Task<IActionResult> updateAddress([FromBody] locations req)
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
                    returnService result = await service.updateAddress(req, Convert.ToInt32(id_user.Value));
                    if (!result.status)
                    {
                        return StatusCode(401, result.message);
                    }
                    return Ok(result.message);
                }
                else
                {
                    return StatusCode(401, "Failed to update user address");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[Authorize]
        //[HttpPut("activateUser")]
        //public async Task<IActionResult> activateUser([FromBody] int id_activate_user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var id_user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id_user");
        //    if (id_user == null && !int.TryParse(id_user.Value, out int userId))
        //    {
        //        return StatusCode(401, "User ID not found in token");
        //    }

        //    if (id_activate_user == Convert.ToInt32(id_user.Value) || id_activate_user == 0)
        //    {
        //        return StatusCode(401, "User ID not found");
        //    }
        //    try
        //    {
        //        var user = await context.tbl_users.Where(f => f.id_user == id_activate_user).FirstOrDefaultAsync();
        //        if (user != null)
        //        {
        //            user.activated = Convert.ToBoolean(user.activated) ? false : true;
        //            user.updated_at = DateTime.Now;
        //            user.updated_by = Convert.ToInt32(id_user.Value);
        //            await context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            return StatusCode(401, "User ID not found");
        //        }
        //        return Ok("User activated!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Failed to activate user");
        //    }
        //}
    }
}
