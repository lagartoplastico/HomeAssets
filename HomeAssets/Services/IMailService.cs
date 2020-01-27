using System.Threading.Tasks;

namespace HomeAssets.Services
{
    public interface IMailService
    {
        Task SendEmail(string toEmail, string subject, string plainTextContent);
    }
}