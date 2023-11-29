using AMS_API.Models;
using AMS_API.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly AMSDbContext context;
        private readonly IConfiguration configuration;

        public authController(AMSDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] login req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(req.email) || string.IsNullOrEmpty(req.password) || string.IsNullOrEmpty(req.username))
            {
                return StatusCode(401, "Login failed");
            }
            try
            {
                var user = await context.tbl_users.Where(f => f.email == req.email || f.username == req.username).FirstOrDefaultAsync();
                if (user == null)
                {
                    return StatusCode(401, "Login failed");
                }

                if (!BCrypt.Net.BCrypt.Verify(req.password, user.password))
                {
                    throw new Exception("Username/Email or password is incorrect");
                }

                user.last_login = DateTime.Now;
                await context.SaveChangesAsync();

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("id_user", user.id_user.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var login = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: login
                    );
                return Ok(new { user.username, user.email, token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize]
        //[HttpGet("logout")]
        //public async Task<IActionResult> logout()
        //{
        //    try
        //    {
        //        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        //        return Ok("Logged out successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Failed to logout");
        //    }
        //}
    }
}
