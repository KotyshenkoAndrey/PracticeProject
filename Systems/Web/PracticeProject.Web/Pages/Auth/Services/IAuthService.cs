using PracticeProject.Web.Pages.Auth.Models;

namespace PracticeProject.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
    Task<string> GetUserName();
    Task<bool> IsConfirmMail(string username);
    Task<string> Registration(RegisterAuthorizedUsersAccountModel registrationModel);
    Task<string> ForgotPassword(ForgotPasswordModel model);
    Task<string> SetNewPassword(NewPasswordModel model);
}