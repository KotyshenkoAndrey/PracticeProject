using PracticeProject.Web.Cars.Models;
using PracticeProject.Web.ViewRequest.Models;

namespace PracticeProject.Web.Pages.ViewRequest.Services;

public interface IViewRequestService
{
    Task<string> CreateViewingRequest(CreateViewingRequestViewModel model);
    Task<IEnumerable<ViewingRequestViewModel>> GetIncomingRequests();
    Task<IEnumerable<ViewingRequestViewModel>> GetOutgoingRequests();
    Task<bool> ChangeStatusRequest(SendEditStateModel model);
    Task<int> GetCountNewRequest();
    Task<SellerProfileModel> GetSellerContact(Guid requestId);
    Task<SellerProfileModel> GetUserProfile();
}