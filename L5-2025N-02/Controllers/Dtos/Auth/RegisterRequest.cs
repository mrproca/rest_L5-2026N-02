using System.ComponentModel.DataAnnotations;

namespace L5_2025N_02.Controllers.Dtos;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}