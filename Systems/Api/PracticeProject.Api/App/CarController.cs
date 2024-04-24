using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Common.Security;
using PracticeProject.Services.AuthorizedUsersAccount;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Cars.Models;
using PracticeProject.Services.Logger;
using System.Security.Claims;

namespace PracticeProject.Api.App
{
//    [Authorize]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly ICarService carService;
        private readonly IAuthorizedUsersAccountService userAccountService;

        public CarController(IAppLogger logger
            , ICarService carService
            , IAuthorizedUsersAccountService userAccountService)
        {
            this.logger = logger;
            this.carService = carService;
            this.userAccountService = userAccountService;
        }
        [AllowAnonymous]
        [HttpGet("/getallcars")]
        public async Task<IEnumerable<CarViewModel>> GetAll()
        {
            var result = await carService.GetAll();

            return result;
        }

        [HttpGet("/getcarbyid/{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await carService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("/addcar/")]
        public async Task<CarViewModel> Create(CreateCarViewModel request)
        {
            var result = await carService.Create(request);

            return result;
        }

        [HttpPut("/editcar/{id:Guid}")]
        public async Task Update([FromRoute] Guid id, UpdateCarViewModel request)
        {
            await carService.Update(id, request);
        }

        [HttpDelete("/deletecar/{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await carService.Delete(id);
        }

        [HttpGet("/getmycars/")]
        public async Task<IEnumerable<CarViewModel>> GetMyCars()
        {
            ClaimsPrincipal currentUser = User;
            if (currentUser != null && currentUser.Identity.IsAuthenticated)
            {
                var username = await userAccountService.GetUser(currentUser);
                var result = await carService.GetMyCars(username);
                return result;
            }
            return Enumerable.Empty<CarViewModel>();                    
        }
    }
}
