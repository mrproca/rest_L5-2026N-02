using System.ComponentModel.DataAnnotations;

public class UpdateUserRequest
{
    [Required, MaxLength(100)] 
    public string Name { get; set; } = null!;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = null!;


}