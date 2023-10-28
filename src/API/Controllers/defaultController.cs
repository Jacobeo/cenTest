using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class defaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello world");
        }
    }
}
