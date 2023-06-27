namespace Practice.Services.UserAccount;

using AutoMapper;
using Practice.Common.Exceptions;
using Practice.Common.Validator;
using Practice.Context.Entities;
using Practice.Services.UserAccount;
using Microsoft.AspNetCore.Identity;

public class UserAccountService : IUserAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;

    public UserAccountService(IMapper mapper, UserManager<User> userManager, IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator)
    {
        this.mapper = mapper;
        this.userManager = userManager;
       this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        // Find user by email
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        // Create user account
        user = new User()
        {
            Status = UserStatus.Active,
            FullName = model.Name,
            UserName = model.Email,  
            Email = model.Email,
            EmailConfirmed = true, 
            PhoneNumber = null,
            PhoneNumberConfirmed = false
            
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {String.Join(", ", result.Errors.Select(s => s.Description))}");


        // Returning the created user
        return mapper.Map<UserAccountModel>(user);
    }
}
