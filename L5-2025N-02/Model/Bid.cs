namespace L5_2025N_02.Model;

public class Bid
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    
    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; } = default!;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}