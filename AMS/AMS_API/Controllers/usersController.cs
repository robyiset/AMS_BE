using AMS_API.Contexts;
using AMS_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly AMSDbContext context;
        private readonly IConfiguration configuration;

        public usersController(AMSDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
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
                if (await context.tbl_users.Where(f => f.email == req.email || f.username == req.username).FirstOrDefaultAsync() != null)
                {
                    return StatusCode(401, "User is already registered");
                }
                var newUser = new tbl_users
                {
                    username = req.username,
                    email = req.email,
                    password = BCrypt.Net.BCrypt.HashPassword(req.password),
                    created_at = DateTime.UtcNow,
                };
                await context.tbl_users.AddAsync(newUser);
                await context.SaveChangesAsync();
                var insertedUserId = newUser.id_user;
                return Ok("User created successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to create user");
            }
        }
        [Authorize]
        [HttpPost("updateProfile")]
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
                //int? id_company = null;
                //if (req.user_location != null)
                //{
                //    var user_location = new tbl_locations
                //    {
                //        address = req.user_location.address,
                //        city = req.user_location.city,
                //        state = req.user_location.state,
                //        country = req.user_location.country,
                //        zip = req.user_location.zip,
                //        created_at = DateTime.Now,
                //        created_by = Convert.ToInt32(id_user.Value)
                //    };
                //    await context.tbl_locations.AddAsync(user_location);
                //    id_location = user_location.id_location;
                //}
                if (req != null)
                {
                    var user_profile = await context.tbl_user_details.Where(f => f.id_user == Convert.ToInt32(id_user.Value)).FirstOrDefaultAsync();
                    if (user_profile != null)
                    {
                        user_profile.first_name = req.first_name;
                        user_profile.last_name = req.last_name;
                        user_profile.phone_number = req.phone_number;
                        user_profile.about = req.about;
                        user_profile.updated_at = DateTime.Now;
                        user_profile.updated_by = Convert.ToInt32(id_user.Value);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        await context.tbl_user_details.AddAsync(new tbl_user_details
                        {
                            id_user = Convert.ToInt32(id_user.Value),
                            first_name = req.first_name,
                            last_name = req.last_name,
                            phone_number = req.phone_number,
                            about = req.about,
                            created_at = DateTime.Now,
                            created_by = Convert.ToInt32(id_user.Value)
                        });
                    }
                    
                }
                return Ok("User profile updated successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to update profile user");
            }
        }
        [Authorize]
        [HttpPost("updateAddress")]
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
                int? id_location = null;
                if (req != null)
                {
                    var user_location = new tbl_locations
                    {
                        address = req.address,
                        city = req.city,
                        state = req.state,
                        country = req.country,
                        zip = req.zip,
                        created_at = DateTime.Now,
                        created_by = Convert.ToInt32(id_user.Value)
                    };
                    await context.tbl_locations.AddAsync(user_location);
                    id_location = user_location.id_location;
                }
                var user_profile = await context.tbl_user_details.Where(f => f.id_user == Convert.ToInt32(id_user.Value)).FirstOrDefaultAsync();
                if (user_profile != null)
                {
                    if (id_location != null)
                    {
                        var old_location = await context.tbl_locations.Where(f => f.id_location == user_profile.id_location).FirstOrDefaultAsync();
                        context.tbl_locations.Remove(old_location);
                        user_profile.id_location = id_location;
                    }
                    user_profile.updated_at = DateTime.Now;
                    user_profile.updated_by = Convert.ToInt32(id_user.Value);
                    await context.SaveChangesAsync();
                }
                return Ok("User created successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to create user");
            }
        }

        [Authorize]
        [HttpPut("activateUser")]
        public async Task<IActionResult> activateUser([FromBody] int id_activate_user)
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

            if (id_activate_user == Convert.ToInt32(id_user.Value) || id_activate_user == 0)
            {
                return StatusCode(401, "User ID not found");
            }
            try
            {
                var user = await context.tbl_users.Where(f => f.id_user == id_activate_user).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.activated = Convert.ToBoolean(user.activated) ? false : true;
                    user.updated_at = DateTime.Now;
                    user.updated_by = Convert.ToInt32(id_user.Value);
                    await context.SaveChangesAsync();
                }
                else
                {
                    return StatusCode(401, "User ID not found");
                }
                return Ok("User created successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to create user");
            }
        }
    }
}
