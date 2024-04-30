using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Common.Security;
using PracticeProject.Services.AuthorizedUsersAccount;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Logger;
using PracticeProject.Services.Sellers;
using System.Security.Claims;

namespace PracticeProject.Api.App
{
    /// <summary>
    /// Controller for get update, delete seller
    /// </summary>
//    [Authorize]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Seller")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly ISellerService sellerService;
        private IAuthorizedUsersAccountService userAccountService;
        /// <summary>
        /// Constructor for seller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="sellerService"></param>
        /// <param name="userAccountService"></param>
        public SellerController(IAppLogger logger, ISellerService sellerService
            , IAuthorizedUsersAccountService userAccountService)
        {
            this.logger = logger;
            this.sellerService = sellerService;
            this.userAccountService = userAccountService;
        }
        /// <summary>
        /// Get all seller
        /// </summary>
        /// <response code="200">Enumerable seller model</response>
//        [Authorize(AppScopes.AccessRead)]
        [HttpGet("/getsellers")]
        public async Task<IEnumerable<SellerViewModel>> GetAllSellers()
        {
            var result = await sellerService.GetAll();

            return result;
        }
        /// <summary>
        /// Get seller by Guid(not used)
        /// </summary>
        /// <param name="id">Guid seller</param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await sellerService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Create seller(not used)
        /// </summary>
        /// <param name="request">Create model new seller</param>
        /// <returns></returns>
//        [Authorize(AppScopes.AccessWrite)]
        [HttpPost("")]
        public async Task<SellerViewModel> Create(CreateSellerViewModel request)
        {
            var result = await sellerService.Create(request);

            return result;
        }

        /// <summary>
        /// Update seller by Guid(not used)
        /// </summary>
        /// <param name="id">Guid seller</param>
        /// <param name="request">Update model seller</param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public async Task Update([FromRoute] Guid id, UpdateSellerViewModel request)
        {
            await sellerService.Update(id, request);
        }

        /// <summary>
        /// Delete seller by Guid(not used)
        /// </summary>
        /// <param name="id">Guid seller</param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await sellerService.Delete(id);
        }
        /// <summary>
        /// Get info about seller
        /// </summary>
        /// <response code="200">Seller model by profile</response>
        [HttpGet("/getuserprofile/")]
        public async Task<SellerProfileModel> GetUserProfile()
        {
            ClaimsPrincipal currentUser = User;
            Guid userUid = Guid.Empty;
            if (currentUser != null && currentUser.Identity.IsAuthenticated)
            {
                userUid = await userAccountService.GetGuidUser(currentUser);
            }
            return await sellerService.GetUserProfile(userUid);
        }
        /// <summary>
        /// Get seller contact by request
        /// </summary>
        /// <param name="requestId">Guid view request</param>
        /// <response code="200">Seller model</response>
        [HttpGet("/getsellercontact/{requestId:Guid}")]
        public async Task<SellerProfileModel> GetSellerContact([FromRoute] Guid requestId)
        {
            var result = await sellerService.GetSellerContact(requestId);
            return result;
        }
    }
}
