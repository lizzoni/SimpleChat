﻿using System.ComponentModel.DataAnnotations;

namespace SimpleChat.Core.Domain.Models;

public class UserLogin
{
    [Required(ErrorMessage = "Field {0} is required")]
    [EmailAddress(ErrorMessage = "Field {0} is invalid")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Field {0} is required")]
    [StringLength(100, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }    
}
