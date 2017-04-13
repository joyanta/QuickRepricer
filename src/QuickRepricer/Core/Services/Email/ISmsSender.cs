using System.Threading.Tasks;

namespace QuickRepricer.Core.Services.Email
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
