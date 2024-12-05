using PracticeProject.Web.Pages.Auth.Models;

namespace PracticeProject.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Authenticating(LoginModel loginModel, LoginResult loginResult);
    Task Logout();
    Task<string> GetUserName();
    Task<bool> IsConfirmMail(string username);
    Task<bool> IsTwoFactorAuthenticator(string username);
    Task<bool> IsValidTOTPcode(LoginModel model);
    Task<string> Registration(RegisterAuthorizedUsersAccountModel registrationModel);
    Task<TwoFactorAuthenticatorModel> GetQrAndManualKey();
    Task<string> ForgotPassword(ForgotPasswordModel model);
    Task<string> SetNewPassword(NewPasswordModel model);
}