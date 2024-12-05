namespace PracticeProject.Services.AuthorizedUsersAccount;

using FluentValidation;

public class RegisterAuthorizedUsersAccountModel
{
    /// <summary>
    /// User's full name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// User's email
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// User's password
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// User's phone number
    /// </summary>
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// User's code TOTP
    /// </summary>
    public string? KeyForTOTP { get; set; }
}

public class RegisterUserAccountModelValidator : AbstractValidator<RegisterAuthorizedUsersAccountModel>
{
    public RegisterUserAccountModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User name is required.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is long.");
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(13).WithMessage("Phone number is long.");
    }
}