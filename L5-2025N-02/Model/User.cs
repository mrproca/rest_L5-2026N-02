namespace L5_2025N_02.Model;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    
    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = "User";

    public ICollection<Auction> Auctions { get; set; } = new List<Auction>();
    public ICollection<Bid> Bids { get; set; } = new List<Bid>();
}