using System.ComponentModel.DataAnnotations;

public class CreateUserRequest
{
    [Required] 
    [MaxLength(100)] 
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = null!;


}