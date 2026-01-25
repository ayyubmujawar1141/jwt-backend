using System.Security.Cryptography;
using AuthApi.Services.Interfaces;
using AuthApi.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AuthApi.Services.Implementations.UsersService;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }
    
    
    
    public async Task<bool> SendEmailAsync(string toEmail, string otp)
    {
        //Generate the OTP
       
        //Create a email message
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.SenderName,_settings.SenderEmail));
        email.To.Add(new MailboxAddress("Recipient name",toEmail));
        email.Subject = "Your OTP Code";
        
        email.Body = new TextPart("html")
        {
            Text = $"<b>Your OTP is:</b> {otp}<br/><br/>This OTP will expire in 5 minutes."
        };


        // You can send plain text or HTML body
        // 3. Compose and send the email
        
        // 2. Send the email using SmtpClient
        using (var client = new SmtpClient())
        {
            try
            {
                // Connect to the SMTP server (e.g., smtp.gmail.com, Port 587 for StartTls
                await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
                

                // Authenticate with credentials
                await client.AuthenticateAsync(_settings.Username, _settings.Password);
                //Send the message
                await client.SendAsync(email);
                Console.WriteLine("Email sent successfully!");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
            finally
            {
                // Disconnect from the server
                await client.DisconnectAsync(true);
            }
        }

        return true;
    }

    
}