using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Common.Security;
using PracticeProject.Context.Entities;
using PracticeProject.Services.AuthorizedUsersAccount;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Logger;
using PracticeProject.Services.Sellers;
using PracticeProject.Services.ViewingRequests;
using PracticeProject.Services.ViewingRequests.Models;
using PracticeProject.Services.ViewRequest.BusinessModels;
using System.Security.Claims;

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
        private readonly IAuthorizedUsersAccountService userAccountService;

        public ViewRequestController(IAppLogger logger
            , IViewRequest viewRequestService
            , IAuthorizedUsersAccountService userAccountService)
        {
            this.logger = logger;
            this.viewRequestService = viewRequestService;
            this.userAccountService = userAccountService;
        }
        //        [Authorize(AppScopes.AccessRead)]
        [HttpPost("/createviewrequest")]
        public async Task<IActionResult> CreateViewingRequest(CreateViewingRequestViewModel model)
        {
            ClaimsPrincipal currentUser = User;
            if (currentUser != null && currentUser.Identity.IsAuthenticated)
            {
                var userGuid = await userAccountService.GetGuidUser(currentUser);
                model.SenderId = userGuid;
                var result = await viewRequestService.CreateViewingRequest(model);
                return result;
            }
            return BadRequest("Error");
        }

        [HttpGet("/getincommingrequest")]
        public async Task<IEnumerable<ViewingRequestViewModel>> GetIncomingRequests(Guid sellerId)
        {
            ClaimsPrincipal currentUser = User;
            if (currentUser != null && currentUser.Identity.IsAuthenticated)
            {
                var userGuid = await userAccountService.GetGuidUser(currentUser);
                sellerId = userGuid;
            }
            var result = await viewRequestService.GetIncomingRequests(sellerId);
            return result;
        }

        [HttpGet("/getoutgoingrequest")]
        public async Task<IEnumerable<ViewingRequestViewModel>> GetOutgoingRequests(Guid sellerId)
        {          
            ClaimsPrincipal currentUser = User;
            if (currentUser != null && currentUser.Identity.IsAuthenticated)
            {
                var userGuid = await userAccountService.GetGuidUser(currentUser);
                sellerId = userGuid;
            }
            var result = await viewRequestService.GetOutgoingRequests(sellerId);
            return result;
        }

        [HttpPost("/changestatusrequest")]
        public async Task ChangeStatusRequest(SendEditStateModel model)
        {
            await viewRequestService.ChangeStatusRequest(model.idRequest, model.state);
        }
    }
}
