using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Services.AuthorizedUsersAccount.AuthorizedUsersAccount.Models
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
