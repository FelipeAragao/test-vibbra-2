using System.ComponentModel.DataAnnotations;
using Glauber.NotificationSystem.Api.Extensions;

namespace Glauber.NotificationSystem.Api.DTOs;

public class RegisterUserDTO
{
    [Required(ErrorMessage = "Field {0} is required")]
    [EmailAddress(ErrorMessage = "Field {0} is in invalid format")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Field {0} is required")]
    [StringLength(100, ErrorMessage = "Field {0} must contain between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [StringLength(100, ErrorMessage = "Field {0} must contain between {2} and {1} characters", MinimumLength = 2)]
    public string Name { get; set; }

    public string CompanyName { get; set; }

    [StringPhoneNumber(ErrorMessage = "The field {0} is in an incorret phone number format")]
    public string PhoneNumber { get; set; }

    public string CompanyAddress { get; set; }
}

public class LoginUserDTO
{
    [Required(ErrorMessage = "Field {0} is required")]
    [EmailAddress(ErrorMessage = "Field {0} is in invalid format")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Field {0} is required")]
    [StringLength(100, ErrorMessage = "Field {0} must contain between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
}

public class UserTokenDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class LoginResponseDTO
{
    public string Token { get; set; }
    public double ExpiresIn { get; set; }
    public UserTokenDTO User { get; set; }
}

public class ClaimDTO 
{
    public string Value { get; set; }
    public string Type { get; set; }
}

