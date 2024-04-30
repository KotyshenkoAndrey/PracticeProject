using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Web.Pages.Auth.Models;

public class ForgotPasswordModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}

public class NewPasswordModel
{
    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string Password { get; set; }
    public string Code { get; set; }
}
