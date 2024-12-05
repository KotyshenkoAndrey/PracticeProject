using Microsoft.AspNetCore.Identity;

namespace PracticeProject.Context.Entities.Identity
{
    public class AuthorizedUsers : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public int? idConfirmEmail {  get; set; }
        public string? keyForTOTP { get; set; }
        public AuthorizedUsersStatus Status { get; set; }
    }
}
