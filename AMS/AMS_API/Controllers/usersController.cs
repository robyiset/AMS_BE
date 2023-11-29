using AMS_API.Contexts;
using AMS_API.Models;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                    first_name = req.first_name,
                    last_name = req.last_name,
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
        public async Task<IActionResult> updateProfile([FromBody] register req)
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
                    first_name = req.first_name,
                    last_name = req.last_name,
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
    }
}
