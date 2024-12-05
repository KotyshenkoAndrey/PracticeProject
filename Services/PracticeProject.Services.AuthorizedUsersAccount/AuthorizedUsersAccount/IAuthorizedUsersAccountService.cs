
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Services.AuthorizedUsersAccount.AuthorizedUsersAccount.Models;
using System.Security.Claims;

namespace PracticeProject.Services.AuthorizedUsersAccount;

public interface IAuthorizedUsersAccountService
{
    Task<bool> IsEmpty();
    Task<IActionResult> Create(RegisterAuthorizedUsersAccountModel model);
    Task<string> GetUser(ClaimsPrincipal claimsPrincipal);
    Task<Guid> GetGuidUser(ClaimsPrincipal claimsPrincipal);
    Task<IActionResult> ConfirmEmail(int id);
    Task<bool> IsConfirmMail(string username);
    Task<bool> IsTwoFactorAuthenticator(string username);
    Task<bool> IsValidTOTPcode(LoginModel loginModel);
    Task<IActionResult> ForgotPassword(ForgotPasswordModel model);
    Task<TwoFactorAuthenticatorModel> GetQrAndManualKey();
    Task<IActionResult> SetNewPassword(NewPasswordModel model);
}
