namespace PracticeProject.Services.AuthorizedUsersAccount;

using AutoMapper;
using PracticeProject.Common.Exceptions;
using PracticeProject.Common.Validator;
using PracticeProject.Context.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthorizedUsersAccountService : IAuthorizedUsersAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<AuthorizedUsers> userManager;
    private readonly IModelValidator<RegisterAuthorizedUsersAccountModel> registerAuthorizedUsersAccountModelValidator;

    public AuthorizedUsersAccountService(
        IMapper mapper, 
        UserManager<AuthorizedUsers> userManager, 
        IModelValidator<RegisterAuthorizedUsersAccountModel> registerUserAccountModelValidator
    )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerAuthorizedUsersAccountModelValidator = registerUserAccountModelValidator;
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
            UserName = model.Email,  
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
}
