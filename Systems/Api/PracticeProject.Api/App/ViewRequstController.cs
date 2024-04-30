using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Services.AuthorizedUsersAccount;
using PracticeProject.Services.Logger;
using PracticeProject.Services.Sellers;
using PracticeProject.Services.ViewingRequests;
using PracticeProject.Services.ViewingRequests.Models;
using PracticeProject.Services.ViewRequest.BusinessModels;
using System.Security.Claims;

namespace PracticeProject.Api.App
{
    /// <summary>
    /// Controller by view request
    /// </summary>
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

        /// <summary>
        /// Constructor controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRequestService"></param>
        /// <param name="userAccountService"></param>
        public ViewRequestController(IAppLogger logger
            , IViewRequest viewRequestService
            , IAuthorizedUsersAccountService userAccountService)
        {
            this.logger = logger;
            this.viewRequestService = viewRequestService;
            this.userAccountService = userAccountService;
        }
        /// <summary>
        /// Create view request
        /// </summary>
        /// <param name="model">Model for create view request</param>
        /// <returns></returns>
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
        /// <summary>
        /// Get incomming request
        /// </summary>
        /// <param name="sellerId"> Guid seller for Swagger</param>
        /// <response code="200">Enumerable request model</response>
        [HttpGet("/getincommingrequest")]
        public async Task<IEnumerable<ViewingRequestViewModel>> GetIncomingRequests(Guid sellerId)//Guid for compatibility with swagger
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

        /// <summary>
        /// Get outgoing request
        /// </summary>
        /// <param name="sellerId"> Guid seller for Swagger</param>
        /// <response code="200">Enumerable request model</response>
        [HttpGet("/getoutgoingrequests")]
        public async Task<IEnumerable<ViewingRequestViewModel>> GetOutgoingRequests(Guid sellerId)//Guid for compatibility with swagger
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
        /// <summary>
        /// Confirm or Reject view request
        /// </summary>
        /// <param name="model">Model for change state request</param>
        /// <returns></returns>
        [HttpPost("/changestatusrequest")]
        public async Task ChangeStatusRequest(SendEditStateModel model)
        {
            await viewRequestService.ChangeStatusRequest(model.idRequest, model.state);
        }
        /// <summary>
        /// Get count new request(status wait)
        /// </summary>
        /// <param name="sellerId">Guid seller for swagger</param>
        /// <returns>count new request</returns>
        [HttpGet("/getcountnewrequest")]
        public async Task<int> GetCountNewRequest(Guid sellerId)//Guid for compatibility with swagger
        {
            ClaimsPrincipal currentUser = User;
            if (currentUser != null && currentUser.Identity.IsAuthenticated)
            {
                var userGuid = await userAccountService.GetGuidUser(currentUser);
                sellerId = userGuid;
            }
            var result = await viewRequestService.getCountNewRequest(sellerId);
            return result;
        }
    }
}
