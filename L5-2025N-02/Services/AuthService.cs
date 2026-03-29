using L5_2025N_02.Controllers.Dtos;
using L5_2025N_02.Database;
using L5_2025N_02.Exceptions;
using L5_2025N_02.Model;
using Microsoft.EntityFrameworkCore;
using RegisterRequest = L5_2025N_02.Controllers.Dtos.RegisterRequest;

namespace L5_2025N_02.Services;

public class AuthService(AppDbContext dbContext, TokenService tokenService)
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var exists = await dbContext.Users.AnyAsync(e => e.Email == request.Email);
        if (exists)
            throw new BadRequestException("Email już istnieje!");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "USER"
        };
        
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return new AuthResponse()
        {
            Token = tokenService.GenerateToken(user)
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(e => e.Email == request.Email);
        if (user is null)
            throw new BadRequestException("Nieprawdiłowe dane logowania!");
        var valid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!valid)
            throw new BadRequestException("Nieprawidłowe dane logowania!");

        return new AuthResponse()
        {
            Token = tokenService.GenerateToken(user)
        };
    }
}