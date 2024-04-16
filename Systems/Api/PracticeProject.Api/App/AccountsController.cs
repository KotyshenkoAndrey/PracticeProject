namespace PracticeProject.API.Controllers;

using AutoMapper;
using  PracticeProject.Services.AuthorizedUsersAccount;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using PracticeProject.Services.AuthorizedUsersAccount;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Identity")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<AccountsController> logger;
    private readonly IAuthorizedUsersAccountService userAccountService;

    public AccountsController(IMapper mapper, ILogger<AccountsController> logger, IAuthorizedUsersAccountService userAccountService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.userAccountService = userAccountService;
    }

    [HttpPost("")]
    public async Task<AuthorizedUsersAccountModel> Register([FromQuery] RegisterAuthorizedUsersAccountModel request)
    {
        var user = await userAccountService.Create(request);
        return user;
    }
}
    