namespace Controllers {
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;

    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : Controller {
        private readonly DataContext _context;

        public BidsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBids() {
            return await _context.Bids.ToListAsync();//.Include(b => b.AuctionItem).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBid(int id) {
            var bid = await _context.Bids.FirstOrDefaultAsync(b => b.Id == id);//.Include(b => b.AuctionItem).FirstOrDefaultAsync(b => b.Id == id);

            if (bid == null) {
                return NotFound();
            }

            return bid;
        }

        [HttpGet("ai-{id}")]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidByAuctionItem(int id) {
            return await _context.Bids.Where(b => b.AuctionItemId == id).ToListAsync();//.Include(b => b.AuctionItem).FirstOrDefaultAsync(b => b.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<Bid>> CreateBid(Bid bid) {
            // Check if the associated AuctionItem exists
            var auctionItem = await _context.AuctionItems.FindAsync(bid.AuctionItemId);
            if (auctionItem == null) {
                return BadRequest("Invalid AuctionItemId");
            }

            // Optional: Validate bid amount against current price
            if (bid.Amount <= auctionItem.CurrentPrice) {
                return BadRequest("Bid amount must be higher than the current price");
            }

            bid.CreatedAt = DateTime.UtcNow;
            _context.Bids.Add(bid);

            // Update the current price of the auction item
            auctionItem.CurrentPrice = bid.Amount;
            _context.AuctionItems.Update(auctionItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBid), new { id = bid.Id }, bid);
        }
    }
}