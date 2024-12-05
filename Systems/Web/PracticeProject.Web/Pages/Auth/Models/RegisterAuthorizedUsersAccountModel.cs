using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Web.Pages.Auth.Models;

public class RegisterAuthorizedUsersAccountModel
{

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(40, ErrorMessage = "Full name must be no more than 40 characters")]
    public string Name { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    [StringLength(40, ErrorMessage = "Phone number must be no more than 40 characters")]
    public string PhoneNumber { get; set; }

    public bool EnableTOTP {  get; set; }
    public string? KeyForTOTP { get; set; }
}
