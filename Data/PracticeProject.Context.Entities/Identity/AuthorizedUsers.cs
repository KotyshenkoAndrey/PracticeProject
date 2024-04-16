using Microsoft.AspNetCore.Identity;

namespace PracticeProject.Context.Entities.Identity
{
    public class AuthorizedUsers : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public AuthorizedUsersStatus Status { get; set; }
    }
}
