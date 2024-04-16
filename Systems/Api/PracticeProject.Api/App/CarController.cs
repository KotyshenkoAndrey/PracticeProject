using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Common.Security;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Logger;

namespace PracticeProject.Api.App
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly ICarService carService;

        public CarController(IAppLogger logger, ICarService carService)
        {
            this.logger = logger;
            this.carService = carService;
        }
        [Authorize(AppScopes.AccessRead)]
        [HttpGet("")]
        public async Task<IEnumerable<CarViewModel>> GetAll()
        {
            var result = await carService.GetAll();

            return result;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await carService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize(AppScopes.AccessWrite)]
        [HttpPost("")]
        public async Task<CarViewModel> Create(CreateCarViewModel request)
        {
            var result = await carService.Create(request);

            return result;
        }

        [HttpPut("{id:Guid}")]
        public async Task Update([FromRoute] Guid id, UpdateCarViewModel request)
        {
            await carService.Update(id, request);
        }

        [HttpDelete("{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await carService.Delete(id);
        }
    }
}
