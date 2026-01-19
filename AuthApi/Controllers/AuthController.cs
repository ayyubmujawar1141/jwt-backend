using AuthApi.Dtos.RequestDtos.Auths;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequestDto request)
    {
        var result = await _authService.Signup(request);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var result = await _authService.Login(request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordDto request)
    {
        return Ok(new
        {
            Message=
            "Hey chacha kya re!"
        });

    }

}