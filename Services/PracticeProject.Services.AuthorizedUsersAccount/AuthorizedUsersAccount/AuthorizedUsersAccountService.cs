namespace PracticeProject.Services.AuthorizedUsersAccount;

using AutoMapper;
using PracticeProject.Common.Exceptions;
using PracticeProject.Common.Validator;
using PracticeProject.Context.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class AuthorizedUsersAccountService : IAuthorizedUsersAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<AuthorizedUsers> userManager;
    private readonly IModelValidator<RegisterAuthorizedUsersAccountModel> registerAuthorizedUsersAccountModelValidator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AuthorizedUsersAccountService(
        IMapper mapper,
        UserManager<AuthorizedUsers> userManager,
        IModelValidator<RegisterAuthorizedUsersAccountModel> registerUserAccountModelValidator
        , IHttpContextAccessor httpContextAccessor
    )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerAuthorizedUsersAccountModelValidator = registerUserAccountModelValidator;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<AuthorizedUsersAccountModel> Create(RegisterAuthorizedUsersAccountModel model)
    {
        registerAuthorizedUsersAccountModelValidator.Check(model);

        // Find user by email
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        // Create user account
        user = new AuthorizedUsers()
        {
            Status = AuthorizedUsersStatus.Active,
            FullName = model.Name,
            UserName = model.Email.Split("@")[0],
            Email = model.Email,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        return mapper.Map<AuthorizedUsersAccountModel>(user);
    }

    public async Task<string> GetUser(ClaimsPrincipal claimsPrincipal)
    {
        var ss = await userManager.GetUserAsync(claimsPrincipal);
        return ss.UserName;
    }
}
