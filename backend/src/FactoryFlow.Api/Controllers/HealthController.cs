using Microsoft.AspNetCore.Mvc;

namespace FactoryFlow.Api.Controllers;

public sealed class HealthController : ApiController
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