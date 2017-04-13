using QuickRepricer.Core.Services.Email;
using System.Threading.Tasks;

namespace QuickRepricer.Services.Email
{
    public class AuthMessageSenderFake : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }
    }
}
