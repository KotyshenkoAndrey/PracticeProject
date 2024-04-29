using PracticeProject.Web.ViewRequest.Models;

namespace PracticeProject.Web.Pages.ViewRequest.Services;

public interface IViewRequestService
{
    Task<string> CreateViewingRequest(CreateViewingRequestViewModel model);
    Task<IEnumerable<ViewingRequestViewModel>> GetIncomingRequests();
    Task<IEnumerable<ViewingRequestViewModel>> GetOutgoingRequests();
    Task ChangeStatusRequest(Guid idRequest, StatusConfirm state);
}