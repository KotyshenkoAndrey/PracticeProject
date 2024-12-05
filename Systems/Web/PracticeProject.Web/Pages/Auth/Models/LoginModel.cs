﻿using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Web.Pages.Auth.Models;

public class LoginModel
{
    [Required(ErrorMessage = "User name is required")]
    [StringLength(40, ErrorMessage = "User name must be no more than 40 characters")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string Password { get; set; }

    [StringLength(6)]
    public string TOTPCode { get; set; }

    public bool RememberMe { get; set; }

    public bool isTwoFactorAuthenticator { get; set; }
}