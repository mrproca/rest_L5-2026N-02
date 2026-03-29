using System.ComponentModel.DataAnnotations;

namespace L5_2025N_02.Controllers.Dtos.Bids;

public class CreateBidRequest
{
    [Required]
    public Guid UserId { get; set; }

    [Range(0.01, 999999999)]
    public decimal Price { get; set; }
}