namespace PracticeProject.Services.AppCarHub;

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


public class CarHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        // Как только клиент подключается к хабу
        await base.OnConnectedAsync();
    }
    public async Task SendCarUpdateNotification()
    {
        await Clients.All.SendAsync("ReceiveCarUpdate");
    }
}
