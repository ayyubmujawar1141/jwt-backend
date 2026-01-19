using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Dtos.RequestDtos.Auths;

public class SignupRequestDto
{
    [Required]
    [StringLength(25, MinimumLength = 3)]
    public string Name { get;set; }
    
    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; }
    
    [Required]
    [StringLength(15, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    
}