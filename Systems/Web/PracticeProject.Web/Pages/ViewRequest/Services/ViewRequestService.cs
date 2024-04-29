using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using PracticeProject.Web.ViewRequest.Models;

namespace PracticeProject.Web.Pages.ViewRequest.Services;

public class ViewRequestService : IViewRequestService
{
    private readonly HttpClient httpClient;
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public ViewRequestService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
    {
        this.httpClient = httpClient;
        this.authenticationStateProvider = authenticationStateProvider;
    }
    public async Task<string> CreateViewingRequest(CreateViewingRequestViewModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("/createviewrequest/", requestContent);
        var contents = await response.Content.ReadAsStringAsync();
        return contents;
    }

    public async Task<IEnumerable<ViewingRequestViewModel>> GetIncomingRequests()
    {
        var response = await httpClient.GetAsync("/getincommingrequest/");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<IEnumerable<ViewingRequestViewModel>>() ?? new List<ViewingRequestViewModel>();
    }
    public async Task<IEnumerable<ViewingRequestViewModel>> GetOutgoingRequests()
    {
        var response = await httpClient.GetAsync("/getoutgoingrequests/");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<IEnumerable<ViewingRequestViewModel>>() ?? new List<ViewingRequestViewModel>();
    }

    public async Task<bool> ChangeStatusRequest(SendEditStateModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("/changestatusrequest/", requestContent);

        return response.IsSuccessStatusCode;
    }
    public async Task<int> GetCountNewRequest()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var response = await httpClient.GetAsync("/getcountnewrequest/");
        var content = await response.Content.ReadAsStringAsync();
        return Convert.ToInt32(content);
    }
}