using Microsoft.Extensions.Options;
using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.IOptions;
using ModeMan.Ecommerce.Services.Abstract;
using System.Net;
using System.Net.Mail;

namespace ModeMan.Ecommerce.Services.Concreete
{
    public class EmailService : IEmailService
    {
        private readonly ModeManDbContext _context;
        private readonly EmailSettings _settings;

        public EmailService(ModeManDbContext context, IOptions<EmailSettings> settings)
        {
            _context = context;
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            SmtpClient client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = _settings.EnableSSL,
                Credentials = new NetworkCredential(_settings.UserName, _settings.Password)
            };

            MailMessage message = new MailMessage(_settings.UserName, email, subject, body);

            //await client.SendMailAsync(message);

        }
    }
}
