using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FactoryFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "ok",
                message = "FactoryFlow API is running"
            });
        }
    }
}
