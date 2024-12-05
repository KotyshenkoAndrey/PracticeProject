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
using Google.Authenticator;
using PracticeProject.Services.AuthorizedUsersAccount.AuthorizedUsersAccount.Models;

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
        string message;
        if (user != null)
        {
            message = $"User account with email {model.Email} already exist.";
            message += !user.EmailConfirmed ? " The mail has not been confirmed." : "";
            return new OkObjectResult(message);
        }        
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
            PhoneNumber = model.PhoneNumber,
            PhoneNumberConfirmed = false,
            idConfirmEmail = idConfirmEmail,
            TwoFactorEnabled = !string.IsNullOrEmpty(model.KeyForTOTP),
            keyForTOTP = model.KeyForTOTP
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
        
        var registrationUser = await userManager.FindByEmailAsync(model.Email);
        if(registrationUser != null && registrationUser.Email != "admin@test.ru") 
        {
            List<string> email = new List<string>();
            email.Add(model.Email);
            await action.SendMail(new EmailSendModel()
            {
                Receiver = email,
                Subject = "Confirm email",
                Body = $"Dear {registrationUser.FullName}, welcome to the service!" +
                       $"\r\nYou have registered with the car sales service." +
                       $" To activate your account, you need to confirm your email address." +
                       $" After confirmation, you will be able to log in to the site using your personal username and password." +
                       $"\r\n" +
                       $"\r\nYour login details:" +
                       $"\r\nYour username: {registrationUser.UserName}" +
                       $"\r\nYour password: {model.Password}" +
                       $"\r\nThe link to confirm the mail: {settings.PublicUrl}/confirmemail/{idConfirmEmail}" +
                       $"\r\n" +
                       $"\r\nIf you have not registered with the service, just ignore this email."
            });
        }        
        return new OkObjectResult("Email confirmation request created");
    }

    public async Task<string> GetUser(ClaimsPrincipal claimsPrincipal)
    {
        var user = await userManager.GetUserAsync(claimsPrincipal);
        return user.UserName;
    }

    public async Task<Guid> GetGuidUser(ClaimsPrincipal claimsPrincipal)
    {
        var user = await userManager.GetUserAsync(claimsPrincipal);
        using var context = await dbContextFactory.CreateDbContextAsync();
        var userGuid = context.Sellers.Where(c=> c.Username == user.UserName).Select(s=>s.Uid).FirstOrDefault();
        return userGuid;
    }

    public async Task<bool> IsConfirmMail(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user != null) return user.EmailConfirmed;
        
        return false;
    }

    public async Task<bool> IsTwoFactorAuthenticator(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user != null) return user.TwoFactorEnabled;

        return false;
    }

    public async Task<bool> IsValidTOTPcode(LoginModel loginModel)
    {
        var user = await userManager.FindByNameAsync(loginModel.UserName);
        
        if (user != null)
        {
            return loginModel.TOTPCode == string.Join(Environment.NewLine, new TwoFactorAuthenticator(HashType.SHA256).GetCurrentPIN(user.keyForTOTP));           
        }

        return false;
    }

    public async Task<TwoFactorAuthenticatorModel> GetQrAndManualKey()
    {
        string privateKey = Guid.NewGuid().ToString().Replace("-", "");
        var tfA = new TwoFactorAuthenticator(HashType.SHA256);

        var setupCode = tfA.GenerateSetupCode(issuer: "CarSales", accountTitleNoSpaces: "CarSales", privateKey, false, 3);
        return new TwoFactorAuthenticatorModel { QrCodeSetupImageUrl = setupCode.QrCodeSetupImageUrl, ManualKey = setupCode.ManualEntryKey
                                                ,SecretKey = privateKey};
    }

    public async Task<IActionResult> ConfirmEmail(int id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var userToConfirm = await context.AuthorizedUsers.FirstOrDefaultAsync(s => s.idConfirmEmail == id);
        if (userToConfirm != null)
        {
            userToConfirm.EmailConfirmed = true;
            await context.Sellers.AddAsync(new Context.Entities.Seller()
            {
                Uid = Guid.NewGuid(),
                Username = userToConfirm.UserName,
                Email = userToConfirm.Email,
                FullName = userToConfirm.FullName,
                PhoneNumber = userToConfirm.PhoneNumber
            });
            await context.SaveChangesAsync();
            List<string> email = [userToConfirm.Email];
            await action.SendMail(new EmailSendModel()
            {
                Receiver = email,
                Subject = "Email confirmation",
                Body = "Email was confirmed successfully"
            });
            return new OkObjectResult("Email was confirmed");
        }
        return new OkObjectResult("Error confirming the mail");
    }

    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var registrationUser = await context.AuthorizedUsers.FirstOrDefaultAsync(c=>c.Email == model.Email);
        if (registrationUser != null)
        {
            Random rnd = new Random();
            int idConfirmEmail = rnd.Next(100, 1007483647);
            registrationUser.idConfirmEmail = idConfirmEmail;
            List<string> email = new List<string>();
            email.Add(model.Email);
            await action.SendMail(new EmailSendModel()
            {
                Receiver = email,
                Subject = "Password recovery",
                Body = $"Dear {registrationUser.FullName}" +
           $"\r\nYour password can be reset by clicking the link below. If you did not request a new password, please ignore this email." +
           $"\r\nPassword reset link: {settings.WebClientUrl}/resetpassword/{idConfirmEmail}"
            });

            var count =await context.SaveChangesAsync();
            return new OkObjectResult("The link to reset the password has been sent to the mail");
        }
        return new OkObjectResult("The entered email is not linked to any account");
    }

    public async Task<IActionResult> SetNewPassword(NewPasswordModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var user = await context.AuthorizedUsers.FirstOrDefaultAsync(c=>c.idConfirmEmail == Convert.ToInt32(model.Code));

        var autorizationUser = await userManager.FindByEmailAsync(user.Email);
        if (autorizationUser != null)
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(autorizationUser);

            var result = await userManager.ResetPasswordAsync(autorizationUser, token, model.Password);
            if(result.Succeeded)
            {
                return new OkObjectResult("The password has been successfully changed");
            }           
        }
        return new OkObjectResult("The password could not be reset");
    }
}
