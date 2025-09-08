namespace Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;

    [ApiController]
    [Route("api/[controller]")]
    public class AuctionItemsController : Controller
    {
        private readonly DataContext _context;

        public AuctionItemsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionItem>>> GetAuctionItems()
        {
            return await _context.AuctionItems.ToListAsync(); //.Include(ai => ai.Bids).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionItem>> GetAuctionItem(int id)
        {
            var auctionItem = await _context.AuctionItems.FirstOrDefaultAsync(ai => ai.Id == id); //.Include(ai => ai.Bids).FirstOrDefaultAsync(ai => ai.Id == id);

            if (auctionItem == null)
            {
                return NotFound();
            }

            return auctionItem;
        }

        [HttpPost]
        public async Task<ActionResult<AuctionItem>> CreateAuctionItem(AuctionItem auctionItem)
        {
            _context.AuctionItems.Add(auctionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuctionItem), new { id = auctionItem.Id }, auctionItem);
        }
    }
}