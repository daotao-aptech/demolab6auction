using Microsoft.AspNetCore.SignalR;

namespace Hubs
{
    public class AuctionHub : Hub
    {
        public async Task JoinAuctionRoom(int AuctionId, string _name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction_{AuctionId}");
            await Clients.Group($"auction_{AuctionId}").SendAsync("NewJoin", new {name=_name});
        }

        public async Task LeaveAuctionRoom(int AuctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction_{AuctionId}");
        }
    }
}