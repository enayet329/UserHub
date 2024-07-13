using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs
{
    public class UserHub : Hub
    {
        public async Task NotifyUserBlocked(string userEmail)
        {
            await Clients.User(userEmail).SendAsync("UserBlocked");
        }
    }
}