using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Common.Security;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Logger;
using PracticeProject.Services.Sellers;

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

        public SellerController(IAppLogger logger, ISellerService sellerService)
        {
            this.logger = logger;
            this.sellerService = sellerService;
        }
//        [Authorize(AppScopes.AccessRead)]
        [HttpGet("")]
        public async Task<IEnumerable<SellerViewModel>> GetAll()
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
    }
}
