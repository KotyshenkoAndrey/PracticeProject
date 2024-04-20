using PracticeProject.Web.Pages.Auth.Models;

namespace PracticeProject.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}