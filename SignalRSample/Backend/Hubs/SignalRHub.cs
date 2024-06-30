using Backend.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs
{
    public class SignalRHub : Hub
    {
        public async Task BroadcastMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        
        public async Task BroadcastProductAdded(ProductDto prod)
        {
            await Clients.All.SendAsync("ReceiveProductAdd", prod);
        }
    }
}
