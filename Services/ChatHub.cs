using Microsoft.AspNetCore.SignalR;

namespace SpeedDeal.Services;

public class ChatHub : Hub
{
    public async Task Send(string message)
    {
        await this.Clients.All.SendAsync("Receive", message);
    }
}