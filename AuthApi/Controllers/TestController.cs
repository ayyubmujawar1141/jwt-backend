using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile([FromHeader(Name="Authorization")] string auth)
    {
        return Ok(new
        {
            Message = "You are Authorized, Mate!",
            User = User.Identity?.Name,
            TokenRecieved = auth
        });
    }
}