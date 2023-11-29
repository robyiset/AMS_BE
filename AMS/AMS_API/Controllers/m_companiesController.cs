using AMS_API.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class m_companiesController : ControllerBase
    {
        private readonly AMSDbContext context;
        private readonly IConfiguration configuration;

        public m_companiesController(AMSDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }

    }
}
