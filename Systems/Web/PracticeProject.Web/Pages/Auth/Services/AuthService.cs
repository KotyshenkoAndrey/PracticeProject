using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PracticeProject.Web.Pages.Auth.Models;
using PracticeProject.Web.Providers;

namespace PracticeProject.Web.Pages.Auth.Services;

public class AuthService : IAuthService
{  
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var url = $"{Settings.IdentityRoot}/connect/token";

        var request_body = new[] 
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.UserName!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

        var requestContent = new FormUrlEncodedContent(request_body);

        var response = await _httpClient.PostAsync(url, requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
        loginResult.Successful = response.IsSuccessStatusCode;

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }
        var confirmResult = await IsConfirmMail(loginModel.UserName);
        if (!confirmResult)
        {
            loginResult.Successful = false;
            loginResult.Error = "The mail has not been confirmed";
            return loginResult;
        }

        var isTwoFactorAuthenticatorEnable = await IsTwoFactorAuthenticator(loginModel.UserName);

        if (isTwoFactorAuthenticatorEnable)
        {
            loginResult.Successful = true;
            loginResult.isTwoFactorAuthenticator = true;
            return loginResult;
        }

        await Authenticating(loginModel, loginResult);
       
        return loginResult;
    }

    public async Task Authenticating(LoginModel loginModel, LoginResult loginResult)
    {
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel, loginResult);
    }

    public async Task Logout()
    {
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();        
    }
    public async Task<string> GetUserName()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var response = await _httpClient.GetAsync("/getCurrentUser/");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        var contents = await response.Content.ReadAsStringAsync();
        return contents;
    }

    public async Task<bool> IsTwoFactorAuthenticator(string username)
    {
        var response = await _httpClient.GetAsync($"/isTwoFactorAuthenticator/?username={username}");
        if (!response.IsSuccessStatusCode)
        {
            var contents = await response.Content.ReadAsStringAsync();
            throw new Exception(contents);
        }
        var content = await response.Content.ReadAsStringAsync();
        bool result;
        if (bool.TryParse(content, out result))
        {
            return result;
        }
        else
        {
            throw new Exception("Некорректный формат ответа");
        }
    }

    public async Task<bool> IsValidTOTPcode(LoginModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await _httpClient.PostAsync("/isValidTOTPcode/", requestContent);
        var content = await response.Content.ReadAsStringAsync();
        bool result;
        if (bool.TryParse(content, out result))
        {
            return result;
        }
        else
        {
            throw new Exception("Некорректный формат ответа");
        }
    }

    public async Task<bool> IsConfirmMail(string username)
    {
        var response = await _httpClient.GetAsync($"/isconfirmmail/?username={username}");
        if (!response.IsSuccessStatusCode)
        {
            var contents = await response.Content.ReadAsStringAsync();
            throw new Exception(contents);
        }
        var content = await response.Content.ReadAsStringAsync();
        bool result;
        if (bool.TryParse(content, out result))
        {
            return result;
        }
        else
        {
            throw new Exception("Некорректный формат ответа");
        }
    }

    public async Task<TwoFactorAuthenticatorModel> GetQrAndManualKey()
    {
        var response = await _httpClient.GetAsync($"/getQrAndManualKey/");
        return await response.Content.ReadFromJsonAsync<TwoFactorAuthenticatorModel>() ?? new();
    }

    public async Task<string> Registration(RegisterAuthorizedUsersAccountModel registrationModel)
    {
        var requestContent = JsonContent.Create(registrationModel);
        var response = await _httpClient.PostAsync("/createaccount/", requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
    public async Task<string> ForgotPassword(ForgotPasswordModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await _httpClient.PostAsync("/forgotpassword/", requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
    public async Task<string> SetNewPassword(NewPasswordModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await _httpClient.PostAsync("/setnewpassword/", requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}