namespace PracticeProject.Test.Api;

using Microsoft.AspNetCore.Mvc;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Cars.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial interface IApi
{
    #region Cars

    [HttpGet("/getallcars")]
    Task<IEnumerable<CarViewModel>> V1_Car_GetAll([Authorize("Bearer")] string token);

    [HttpGet("/getcarbyid/{id:Guid}")]
    Task<CarViewModel> V1_Car_GetById([Authorize("Bearer")] string token, Guid id);

    [HttpPost("/addcar/")]
    Task<CarViewModel> V1_Car_Create([Authorize("Bearer")] string token, CreateCarViewModel request);

    [HttpDelete("/deletecar/{id:Guid}")]
    Task V1_Car_Delete([Authorize("Bearer")] string token, Guid id);

    #endregion
}