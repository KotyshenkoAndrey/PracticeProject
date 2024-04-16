namespace PracticeProject.Identity.Configuration;

using PracticeProject.Common.Security;
using Duende.IdentityServer.Models;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.AccessRead, "Read"),
            new ApiScope(AppScopes.AccessWrite, "Write")
        };
}