﻿using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PracticeProject.Web.Pages.Auth.Models;

namespace PracticeProject.Web.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(savedToken))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
    }

    public async void MarkUserAsAuthenticated(LoginModel loginModel, LoginResult loginResult)
    {
        if (loginModel.RememberMe)
        {
            await _localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
            await _localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);
        }
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.Name, loginModel.UserName)], "apiauth"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);
    }

    public async void MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);
        await _localStorage.RemoveItemAsync(LocalStorageRefreshTokenKey);

        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

        if (roles != null)
        {
            if (roles.ToString().Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}