
using Microsoft.AspNetCore.Mvc;
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
}
