using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Services.AuthorizedUsersAccount;

public class ForgotPasswordModel
{
    /// <summary>
    /// User's email
    /// </summary>
    public string Email { get; set; }
}
public class NewPasswordModel
{
    /// <summary>
    /// User's password
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Code by reset password
    /// </summary>
    public string Code { get; set; }
}