using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;


namespace PracticeProject.Web.Pages.Hub;
public class SignalRService
{
    private HubConnection hubConnection;
    private CarState carState;
    private IncomingViewState incomingViewState;
    public SignalRService(NavigationManager navigationManager, CarState carState, IncomingViewState incomingViewState)
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri(Settings.ApiRoot + "/appHub"))
            .Build();
        this.carState = carState;
        hubConnection.On<string>("ReceiveCarUpdate", (message) =>
        {
            this.carState.AddMessage(message);
        });
        this.incomingViewState = incomingViewState;
        hubConnection.On<string>("ReceiveIncomeRequestUpdate", (message) =>
        {
            this.incomingViewState.AddMessage(message);
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
