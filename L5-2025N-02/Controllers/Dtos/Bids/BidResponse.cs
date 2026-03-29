namespace L5_2025N_02.Controllers.Dtos.Bids;

public class BidResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AuctionId { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedOn { get; set; }
}