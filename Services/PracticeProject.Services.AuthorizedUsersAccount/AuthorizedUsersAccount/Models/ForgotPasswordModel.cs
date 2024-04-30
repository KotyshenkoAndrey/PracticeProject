using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Services.AuthorizedUsersAccount;

public class ForgotPasswordModel
{
    public string Email { get; set; }
}
public class NewPasswordModel
{
    public string Password { get; set; }
    public string Code { get; set; }
}