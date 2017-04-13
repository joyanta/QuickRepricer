using System.Threading.Tasks;

namespace QuickRepricer.Core.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
