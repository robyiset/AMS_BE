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
        private readonly companiesController service;
        public companiesController(companiesController _service)
        {
            service = _service;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> getdata()
        {
            try
            {
                return Ok(await service.getdata());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
