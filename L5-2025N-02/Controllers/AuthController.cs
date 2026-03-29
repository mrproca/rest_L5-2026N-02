using L5_2025N_02.Controllers.Dtos;
using L5_2025N_02.Services;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = L5_2025N_02.Controllers.Dtos.LoginRequest;
using RegisterRequest = L5_2025N_02.Controllers.Dtos.RegisterRequest;

namespace L5_2025N_02.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(AuthService authService, ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        logger.LogInformation("Logging in");
        var result = await authService.LoginAsync(request);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        logger.LogInformation("Registering");
        var result = await authService.RegisterAsync(request);
        return Ok(result);
    }
}