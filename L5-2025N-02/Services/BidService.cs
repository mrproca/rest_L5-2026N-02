using L5_2025N_02.Controllers.Dtos.Bids;
using L5_2025N_02.Database;
using L5_2025N_02.Exceptions;
using L5_2025N_02.Model;
using Microsoft.EntityFrameworkCore;

namespace L5_2025N_02.Services;

public class BidService(AppDbContext db)
{
    public async Task<BidResponse> PlaceBidAsync(Guid auctionId, CreateBidRequest request, CancellationToken ct)
    {
        var auction = await db.Auctions
            .Include(x => x.Bids)
            .FirstOrDefaultAsync(x => x.Id == auctionId, ct);

        if (auction is null)
            throw new NotFoundException("Aukcja nie istnieje.");

        var userExists = await db.Users.AnyAsync(x => x.Id == request.UserId, ct);
        if (!userExists)
            throw new BadRequestException("Użytkownik składający ofertę nie istnieje.");

        if (auction.Status != AuctionStatus.Active)
            throw new BadRequestException("Aukcja nie jest aktywna.");

        if (DateTime.UtcNow > auction.EndedOn)
            throw new BadRequestException("Nie można licytować po zakończeniu aukcji.");

        if (request.Price <= auction.CurrentHighestBid)
            throw new BadRequestException("Nowa oferta musi być wyższa od aktualnej najwyższej oferty.");

        var bid = new Bid
        {
            Id = Guid.NewGuid(),
            AuctionId = auctionId,
            UserId = request.UserId,
            Price = request.Price,
            CreatedOn = DateTime.UtcNow
        };

        auction.CurrentHighestBid = request.Price;
        auction.Bids.Add(bid);

        await db.SaveChangesAsync(ct);

        return new BidResponse
        {
            Id = bid.Id,
            AuctionId = bid.AuctionId,
            UserId = bid.UserId,
            Price = bid.Price,
            CreatedOn = bid.CreatedOn
        };
    }
}