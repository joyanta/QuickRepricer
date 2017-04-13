using Microsoft.AspNetCore.Identity;
using Moq;
using QuickRepricer.Controllers.Web;
using QuickRepricer.Core.Models;
using QuickRepricer.Messaging.Configuration;
using QuickRepricer.Web.Tests.Extensions;
using QuickRepricer.Web.Tests.Helpers;
using System;

namespace QuickRepricer.Web.Tests.Controllers.Web
{
    public class DashboardControllerTests : IDisposable
    {
        private UserManager<ApplicationUser> _userManager;

        private DashboardController _controller;

        private Mock<MessagingConfiguration> _messagingConfig;


        public void Dispose()
        {
            _controller.Dispose();
        }


        public DashboardControllerTests()
        {
            _userManager = MockHelpers.TestUserManager<ApplicationUser>();
            _controller = new DashboardController(_userManager, _messagingConfig.Object);
            _controller.MockCurrentUser("1", "user@gmail.com");
            
        }
        
        // rest of the test cases needs to be done later;
    }
}
