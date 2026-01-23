using System.ComponentModel.DataAnnotations;

namespace AuthApi.Settings;

public class EmailSettings
{
    [Required]
    public string SmtpHost { get; set; } = string.Empty;
    
    [Required]
    public int SmtpPort { get; set; }
    
    [Required]
    public string SenderName { get; set; } = string.Empty;
    
    [Required]
    public string SenderEmail { get; set; } = string.Empty;
    
    [Required]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
}