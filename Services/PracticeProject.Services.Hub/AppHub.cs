namespace PracticeProject.Services.AppHubs;

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


public class AppHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        // Как только клиент подключается к хабу
        await base.OnConnectedAsync();
    }
}
