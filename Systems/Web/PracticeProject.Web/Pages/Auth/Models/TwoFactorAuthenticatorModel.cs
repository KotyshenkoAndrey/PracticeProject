using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Web.Pages.Auth.Models
{
    public class TwoFactorAuthenticatorModel
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Code is required")]
        [StringLength(6)]
        public string Code { get; set; }

        public string? ManualKey { get; set; }
        public string? SecretKey { get; set; }
        public string? QrCodeSetupImageUrl { get; set; }

    }
}
