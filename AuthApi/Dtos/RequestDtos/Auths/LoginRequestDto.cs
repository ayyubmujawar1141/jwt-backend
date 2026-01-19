using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos.RequestDtos.Auths;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    [StringLength(50,MinimumLength = 15)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}