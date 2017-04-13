using FluentAssertions;
using Moq;
using QuickRepricer.Core.Services.Email;
using System.Threading.Tasks;
using Xunit;

namespace QuickRepricer.Web.Tests.Services.Email
{
    public class AuthMessageSenderTest
    {
        private Mock<IAuthMessageSender> _authMessageSender;

        public AuthMessageSenderTest()
        {
           _authMessageSender = new Mock<IAuthMessageSender>();
        }
        
        [Theory]
        [InlineData("user@gmail.com", "subject", "message")]
        [InlineData("user@gmail1.com", "subject1", "message")]
        public void Send_AuthenticationEmailSucessfully(string email, string subject, string message)
        {
            // ste up 
            _authMessageSender.Setup( stub => stub.SendEmailAsync(email, subject, message)).Returns(Task.FromResult(0));

            //action 
            var task = _authMessageSender.Object.SendEmailAsync(email, subject, message);

            // result
            task.Should().NotBeNull();
          
        }

        [Theory]
        [InlineData("0207909090", "message2")]
        [InlineData("020723948290", "message1")]
        public void Send_AuthenticationSmSucessfully(string number, string message)
        {
            //setup 
            _authMessageSender.Setup(stub => stub.SendSmsAsync(number, message)).Returns(Task.FromResult(0));

            //action
            var task = _authMessageSender.Object.SendSmsAsync(number, message);

            //result
            task.Should().NotBeNull();
        }
    }
}
