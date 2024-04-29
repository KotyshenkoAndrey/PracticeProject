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

        public SellerController(IAppLogger logger, ISellerService sellerService
            , IAuthorizedUsersAccountService userAccountService)
        {
            this.logger = logger;
            this.sellerService = sellerService;
            this.userAccountService = userAccountService;
        }
//        [Authorize(AppScopes.AccessRead)]
        [HttpGet("/getsellers")]
        public async Task<IEnumerable<SellerViewModel>> GetAllSellers()
        {
            var result = await sellerService.GetAll();

            return result;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await sellerService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

//        [Authorize(AppScopes.AccessWrite)]
        [HttpPost("")]
        public async Task<SellerViewModel> Create(CreateSellerViewModel request)
        {
            var result = await sellerService.Create(request);

            return result;
        }

        [HttpPut("{id:Guid}")]
        public async Task Update([FromRoute] Guid id, UpdateSellerViewModel request)
        {
            await sellerService.Update(id, request);
        }

        [HttpDelete("{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await sellerService.Delete(id);
        }

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

        [HttpGet("/getsellercontact/{requestId:Guid}")]
        public async Task<SellerProfileModel> GetSellerContact([FromRoute] Guid requestId)
        {
            var result = await sellerService.GetSellerContact(requestId);
            return result;
        }
    }
}
