using PracticeProject.Context.Entities;
using PracticeProject.Services.ViewingRequests.Models;

namespace PracticeProject.Services.ViewingRequests;

public interface IViewRequest
{
    Task<ViewingRequestViewModel> CreateViewingRequest(CreateViewingRequestViewModel model);
    Task<IEnumerable<ViewingRequestViewModel>> GetIncomingRequests(Guid sellerId);
    Task<IEnumerable<ViewingRequestViewModel>> GetOutgoingRequests(Guid sellerId);
    Task ChangeStatusRequest(Guid idRequest, StatusConfirm state);
}

