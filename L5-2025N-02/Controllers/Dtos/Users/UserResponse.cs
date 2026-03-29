namespace L5_2025N_02.Controllers.Dtos;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}