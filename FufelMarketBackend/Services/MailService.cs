using System.Security.Cryptography;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace FufelMarketBackend.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings = MailSettings.LoadFromEnvironment();
    
    public async Task SendMailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, _mailSettings.UseSsl);
        await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
    
    public string GenerateConfirmationCode(int length = 6)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var code = new char[length];
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        for (var i = 0; i < code.Length; i++)
        {
            code[i] = chars[bytes[i] % chars.Length];
        }
        return new string(code);
    }
}

public class MailSettings
{
    public required string DisplayName { get; set; }
    public required string From { get; set; }
    public required string Host { get; set; }
    public int Port { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public bool UseSsl { get; set; }

    public static MailSettings LoadFromEnvironment()
    {
        return new MailSettings
        {
            DisplayName = Environment.GetEnvironmentVariable("MAIL_DISPLAYNAME") ?? "",
            From = Environment.GetEnvironmentVariable("MAIL_FROM") ?? "",
            Host = Environment.GetEnvironmentVariable("MAIL_HOST") ?? "",
            Port = int.Parse(Environment.GetEnvironmentVariable("MAIL_PORT") ?? "587"),
            UserName = Environment.GetEnvironmentVariable("MAIL_USERNAME") ?? "",
            Password = Environment.GetEnvironmentVariable("MAIL_PASSWORD") ?? "",
            UseSsl = bool.Parse(Environment.GetEnvironmentVariable("MAIL_USESSL") ?? "true")
        };
    }
}