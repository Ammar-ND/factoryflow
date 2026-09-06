using Microsoft.AspNetCore.Mvc;

namespace FactoryFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
}