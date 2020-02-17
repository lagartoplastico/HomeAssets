using System.Threading.Tasks;

namespace HomeAssets.Services
{
    public interface IMailService
    {
        void SendEmail(string toEmail, string subject, string body);
    }
}