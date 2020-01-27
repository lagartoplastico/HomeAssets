using HomeAssets.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HomeAssets.Services
{
    public class EmailService : IMailService
    {
        private readonly SmtpSettings settings;
        private readonly SmtpClient client;
        public EmailService(IOptions<SmtpSettings> options)
        {
            settings = options.Value;
            client = new SmtpClient(settings.Server)
            {
                Credentials = new NetworkCredential(settings.Username, settings.Password)
            };
        }

        public Task SendEmail(string toEmail, string subject, string plainTextContent)
        {
            var message = new MailMessage(settings.From, toEmail, subject, plainTextContent);
            return client.SendMailAsync(message);
        }
    }
}