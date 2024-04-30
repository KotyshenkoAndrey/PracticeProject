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
    /// <summary>
    /// Controller for get, create, update, delete car
    /// </summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="carService"></param>
        /// <param name="userAccountService"></param>
        public CarController(IAppLogger logger
            , ICarService carService
            , IAuthorizedUsersAccountService userAccountService)
        {
            this.logger = logger;
            this.carService = carService;
            this.userAccountService = userAccountService;
        }
        /// <summary>
        /// Getting all the cars of all the sellers
        /// </summary>
        /// <response code="200">Enumerable all car</response>
        [AllowAnonymous]
        [HttpGet("/getallcars")]
        public async Task<IEnumerable<CarViewModel>> GetAll()
        {
            var result = await carService.GetAll();

            return result;
        }
        /// <summary>
        /// Get car by Id
        /// </summary>
        /// <param name="id">Guid car</param>
        /// <returns></returns>
        [HttpGet("/getcarbyid/{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await carService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Create new car
        /// </summary>
        /// <param name="request">Model for create car</param>
        ///  <response code="200">Created car model</response>
        [AllowAnonymous]
        [HttpPost("/addcar/")]
        public async Task<CarViewModel> Create(CreateCarViewModel request)
        {
            ClaimsPrincipal currentUser = User;
            Guid sellerId = Guid.Empty;
            if (currentUser != null && currentUser.Identity.IsAuthenticated)
            {
                sellerId = await userAccountService.GetGuidUser(currentUser);
            }
            request.SellerId = sellerId;
            var result = await carService.Create(request);

            return result;
        }

        /// <summary>
        /// Update car by Id
        /// </summary>
        /// <param name="id">Id for update car</param>
        /// <param name="request">Model for update car</param>
        /// <returns></returns>
        [HttpPut("/editcar/{id:Guid}")]
        public async Task Update([FromRoute] Guid id, UpdateCarViewModel request)
        {
            await carService.Update(id, request);
        }

        /// <summary>
        /// Delete car by Id
        /// </summary>
        /// <param name="id">Id car for delete</param>
        /// <returns></returns>
        [HttpDelete("/deletecar/{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await carService.Delete(id);
        }

        /// <summary>
        /// Get all the cars of the current user
        /// </summary>
        ///  <response code="200">Enumerable all car for current user</response>
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
