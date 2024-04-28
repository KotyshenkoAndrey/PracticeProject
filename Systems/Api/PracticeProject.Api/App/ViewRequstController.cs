using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Common.Security;
using PracticeProject.Context.Entities;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Logger;
using PracticeProject.Services.Sellers;
using PracticeProject.Services.ViewingRequests;
using PracticeProject.Services.ViewingRequests.Models;

namespace PracticeProject.Api.App
{
    //    [Authorize]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "ViewRequest")]
    [ApiController]
    public class ViewRequestController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly IViewRequest viewRequestService;

        public ViewRequestController(IAppLogger logger, IViewRequest viewRequestService)
        {
            this.logger = logger;
            this.viewRequestService = viewRequestService;
        }
        //        [Authorize(AppScopes.AccessRead)]
        [HttpPut("/createviewrequest")]
        public async Task<ViewingRequestViewModel> CreateViewingRequest(CreateViewingRequestViewModel model)
        {
            var result = await viewRequestService.CreateViewingRequest(model);

            return result;
        }

        [HttpGet("/getincommingrequest")]
        public async Task<IEnumerable<ViewingRequestViewModel>> GetIncomingRequests(Guid sellerId)
        {
            var result = await viewRequestService.GetIncomingRequests(sellerId);

            return result;
        }

        [HttpGet("/getoutgoingrequest")]
        public async Task<IEnumerable<ViewingRequestViewModel>> GetOutgoingRequests(Guid sellerId)
        {
            var result = await viewRequestService.GetOutgoingRequests(sellerId);

            return result;
        }

        [HttpPost("changestatusrequest")]
        public async Task ChangeStatusRequest(Guid idRequest, StatusConfirm state)
        {
            await viewRequestService.ChangeStatusRequest(idRequest, state);
        }
    }
}
