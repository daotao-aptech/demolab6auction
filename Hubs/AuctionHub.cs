using Microsoft.AspNetCore.SignalR;

namespace Hubs
{
    public class AuctionHub : Hub
    {
        public async Task JoinAuctionRoom(int AuctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction_{AuctionId}");
        }

        public async Task LeaveAuctionRoom(int AuctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction_{AuctionId}");
        }
    }
}