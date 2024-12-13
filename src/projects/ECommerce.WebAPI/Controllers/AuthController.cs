using Core.Security.Dtos;
using ECommerce.Application.Services.Users.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService _authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]UserForLoginDto dto,CancellationToken cancellationToken)
    {
        var response = await _authService.LoginAsync(dto,cancellationToken);
        return Ok(response);
    }
}