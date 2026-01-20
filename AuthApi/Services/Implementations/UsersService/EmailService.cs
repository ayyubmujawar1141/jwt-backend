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
    public async Task<bool> SendEmailAsync(string toEmail, string subject, string resetLink)
    {
        //Create a email message
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.SenderName,_settings.SenderEmail));
        email.To.Add(new MailboxAddress("Recipient name",toEmail));
        email.Subject = subject;
        // You can send plain text or HTML body
        resetLink = "https://youtu.be/UJHN0ONDnrs?si=iD1yNilD3CD0ZwSl";
        string body = $@"
            <h2>I am the CEO of Ubuntu Distro and also talented and skilled .Net Developer.</h2>
            <p>Click below to see Ubuntu features:</p>
               <a href='{resetLink}' style='padding:10px 15px;background:#0000FF;color:#fff;text-decoration:none;border-radius:6px;'>
                Download Ubuntu
            </a>
              <p>if you are having questions like </p>
              <p>why Ubuntu instead of Windows? because</p>
              <p><b>IT IS FUCKING LIGHTWEIGHT, BITCH!</b></p>
                
              ";

        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
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