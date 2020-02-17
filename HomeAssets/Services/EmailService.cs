using HomeAssets.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace HomeAssets.Services
{
    public class EmailService : IMailService
    {
        // (Optional) the name of a configuration set to use for this message.
        // If you comment out this line, you also need to remove or comment out
        // the "X-SES-CONFIGURATION-SET" header below.
        //String CONFIGSET = "ConfigSet";

        private readonly SmtpSettings settings;

        public EmailService(IOptions<SmtpSettings> options)
        {
            settings = options.Value;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.IsBodyHtml = false;
            message.From = new MailAddress(settings.From, settings.Name);
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = body;
            // Comment or delete the next line if you are not using a configuration set
            //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            using SmtpClient client = new SmtpClient(settings.Server, settings.Port);
            client.Credentials = new NetworkCredential(settings.Username, settings.Password);
            client.EnableSsl = true;
            try
            {
                Console.WriteLine("Attempting to send email...");
                client.Send(message);
                Console.WriteLine("Email sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The email was not sent.");
                Console.WriteLine("Error message: " + ex.Message);
            }
        }
    }
}