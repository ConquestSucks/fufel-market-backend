namespace FufelMarketBackend.Services;

public interface IMailService
{
    Task SendMailAsync(string email, string subject, string message);
    
    string GenerateConfirmationCode(int length = 6);
}