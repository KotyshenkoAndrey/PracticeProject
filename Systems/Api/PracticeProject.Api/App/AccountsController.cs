namespace PracticeProject.API.Controllers;

using AutoMapper;
using PracticeProject.Services.AuthorizedUsersAccount;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PracticeProject.Services.AuthorizedUsersAccount.AuthorizedUsersAccount.Models;

/// <summary>
/// Controller for workaround with user accounts. Such as registration, login, password reset
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Identity")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<AccountsController> logger;
    private readonly IAuthorizedUsersAccountService userAccountService;

    /// <summary>
    /// Main constructor
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="userAccountService"></param>
    public AccountsController(IMapper mapper, ILogger<AccountsController> logger, IAuthorizedUsersAccountService userAccountService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.userAccountService = userAccountService;
    }
    /// <summary>
    /// User registration
    /// </summary>
    /// <param name="request">model for registration user</param>
    /// <returns>Result request</returns>
    [HttpPost("/createaccount")]
    public async Task<IActionResult> Register(RegisterAuthorizedUsersAccountModel request)
    {
        var result = await userAccountService.Create(request);
        return result;
    }
    /// <summary>
    /// Get current user login(username)
    /// </summary>
    /// <returns>username</returns>
    [Authorize]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Identity")]
    [HttpGet("/getCurrentUser")]
    public async Task<string> GetCurrentUserName()
    {
        ClaimsPrincipal currentUser = User;
        if (currentUser != null && currentUser.Identity.IsAuthenticated)
        {
            var username = await userAccountService.GetUser(currentUser);
            return username;
        }
        return string.Empty;
    }
    /// <summary>
    /// Confirm email
    /// </summary>
    /// <param name="id">Id for confirm email</param>
    /// <returns>Result request</returns>
    [HttpGet("/confirmemail/{id:int}")]
    public async Task<IActionResult> ConfirmEmail([FromRoute] int id)
    {
        var result = await userAccountService.ConfirmEmail(id);
        return result;
    }
    /// <summary>
    /// Check confirm email
    /// </summary>
    /// <param name="username">user's username(login)</param>
    /// <returns></returns>
    [HttpGet("/isconfirmmail/")]
    public async Task<bool> IsConfirmEmail(string username)
    {
        var result = await userAccountService.IsConfirmMail(username);
        return result;
    }

    /// <summary>
    /// Check enable Two-Factor Authenticator
    /// </summary>
    /// <param name="username">user's username(login)</param>
    /// <returns></returns>
    [HttpGet("/isTwoFactorAuthenticator/")]
    public async Task<bool> IsTwoFactorAuthenticator(string username)
    {
        var result = await userAccountService.IsTwoFactorAuthenticator(username);
        return result;
    }

    /// <summary>
    /// Check valid TOTP code
    /// </summary>
    /// <param name="username">user's login model</param>
    /// <returns></returns>
    [HttpPost("/isValidTOTPcode/")]
    public async Task<bool> IsValidTOTPcode(LoginModel username)
    {
        var result = await userAccountService.IsValidTOTPcode(username);
        return result;
    }

    /// <summary>
    /// Method for password reset
    /// </summary>
    /// <param name="model">model for password reset</param>
    /// <returns>Result request</returns>
    [HttpPost("/forgotpassword/")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        var result = await userAccountService.ForgotPassword(model);
        return result;
    }

    /// <summary>
    /// get Qr code And ManualKey
    /// </summary>
    /// <param name="model">Model For TOTP</param>
    /// <returns>Model for display QR code</returns>
    [HttpGet("/getQrAndManualKey/")]
    public async Task<TwoFactorAuthenticatorModel> GetQrAndManualKey()
    {
        var result = await userAccountService.GetQrAndManualKey();
        return result;
    }
    /// <summary>
    /// Set new password
    /// </summary>
    /// <param name="model">model for set new password</param>
    /// <returns></returns>
    [HttpPost("/setnewpassword/")]
    public async Task<IActionResult> SetNewPassword(NewPasswordModel model)
    {
        var result = await userAccountService.SetNewPassword(model);
        return result;
    }
}
