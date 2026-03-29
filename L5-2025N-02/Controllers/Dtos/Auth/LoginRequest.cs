using System.ComponentModel.DataAnnotations;

namespace L5_2025N_02.Controllers.Dtos;

public class LoginRequest
{
    [Required, EmailAddress] public string Email { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}