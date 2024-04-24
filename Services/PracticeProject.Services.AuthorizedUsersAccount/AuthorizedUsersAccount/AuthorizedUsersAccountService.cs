namespace PracticeProject.Services.AuthorizedUsersAccount;

using AutoMapper;
using PracticeProject.Common.Exceptions;
using PracticeProject.Common.Validator;
using PracticeProject.Context.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using PracticeProject.Services.Actions;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Context;
using PracticeProject.Services.Settings;

public class AuthorizedUsersAccountService : IAuthorizedUsersAccountService
{
    private readonly MainSettings settings;
    private readonly IMapper mapper;
    private readonly UserManager<AuthorizedUsers> userManager;
    private readonly IModelValidator<RegisterAuthorizedUsersAccountModel> registerAuthorizedUsersAccountModelValidator;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IAction action;
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public AuthorizedUsersAccountService(MainSettings settings,
        IMapper mapper,
        UserManager<AuthorizedUsers> userManager,
        IModelValidator<RegisterAuthorizedUsersAccountModel> registerUserAccountModelValidator
        , IHttpContextAccessor httpContextAccessor
        , IAction action
        , IDbContextFactory<MainDbContext> dbContextFactory
    )
    {
        this.settings = settings;
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerAuthorizedUsersAccountModelValidator = registerUserAccountModelValidator;
        this.httpContextAccessor = httpContextAccessor;
        this.action = action;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<IActionResult> Create(RegisterAuthorizedUsersAccountModel model)
    {
        registerAuthorizedUsersAccountModelValidator.Check(model);

        // Find user by email
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");
        
        Random rnd = new Random();       
        int idConfirmEmail = rnd.Next(100, 1007483647);
        // Create user account
        user = new AuthorizedUsers()
        {
            Status = AuthorizedUsersStatus.Active,
            FullName = model.Name,
            UserName = model.Email.Split("@")[0],
            Email = model.Email,
            EmailConfirmed = false,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            idConfrirmEmail = idConfirmEmail,
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
        
        var registrationUser = await userManager.FindByEmailAsync(model.Email);
        await action.SendMail(new EmailSendModel()
        {
            Receiver = model.Email,
            Subject = "Confirm email",
            Body = "To confirm registration, follow the link: " +
                   settings.PublicUrl + "/confirmemail/" + idConfirmEmail
        });

        return new OkObjectResult("Email confirmation request created");
    }

    public async Task<string> GetUser(ClaimsPrincipal claimsPrincipal)
    {
        var user = await userManager.GetUserAsync(claimsPrincipal);
        return user.UserName;
    }

    public async Task<bool> IsConfirmMail(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user != null) return user.EmailConfirmed;
        
        return false;
    }
    public async Task<IActionResult> ConfirmEmail(int id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var userToConfirm = await context.AuthorizedUsers.FirstOrDefaultAsync(s => s.idConfrirmEmail == id);
        if(userToConfirm != null)
        {
            userToConfirm.EmailConfirmed = true;
            await context.Sellers.AddAsync(new Context.Entities.Seller()
            {
                Uid = Guid.NewGuid(),
                Username = userToConfirm.UserName,
                Email  = userToConfirm.Email,
                FullName = userToConfirm.FullName
            });
            await context.SaveChangesAsync();
            await action.SendMail(new EmailSendModel()
            {
                Receiver = userToConfirm.Email,
                Subject = "Email confirmation",
                Body = "Email was confirmed successfully"
            });
            return new OkObjectResult("Email was confirmed");
        }
        return new OkObjectResult("Error confirming the mail");
    }
}
