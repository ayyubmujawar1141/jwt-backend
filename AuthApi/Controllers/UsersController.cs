using AuthApi.Dtos.ResponseDtos.Users;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task<IActionResult> GetAll()
    {
        return Ok(await _usersService.GetUsers());
    }
}