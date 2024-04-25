using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PracticeProject.Web;
using System.Net.NetworkInformation;


namespace PracticeProject.Web.Pages.Hub;
public class SignalRService
{
    private HubConnection hubConnection;
    private AppState appState;
    public SignalRService(NavigationManager navigationManager, AppState appState)
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri(Settings.ApiRoot + "/carHub"))
            .Build();
        this.appState = appState;
        hubConnection.On<string>("ReceiveCarUpdate", (message) =>
        {
            this.appState.AddMessage(message);
        });
        
    }

    public async Task StartConnection()
    {
        await hubConnection.StartAsync();
    }
    public HubConnectionState GetStateConnection() 
    {
        return hubConnection.State;
    }
}
