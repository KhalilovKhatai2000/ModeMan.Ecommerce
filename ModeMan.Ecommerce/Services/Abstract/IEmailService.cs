namespace ModeMan.Ecommerce.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
