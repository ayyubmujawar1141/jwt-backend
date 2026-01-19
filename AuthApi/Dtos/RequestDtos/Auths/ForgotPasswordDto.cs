using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos.RequestDtos.Auths;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}