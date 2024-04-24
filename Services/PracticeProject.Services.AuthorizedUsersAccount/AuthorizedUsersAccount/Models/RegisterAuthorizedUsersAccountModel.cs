namespace PracticeProject.Services.AuthorizedUsersAccount;

using FluentValidation;

public class RegisterAuthorizedUsersAccountModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
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