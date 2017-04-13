using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QuickRepricer.Controllers.Web;
using QuickRepricer.Core.Models;
using QuickRepricer.Core.ViewModels;
using QuickRepricer.Web.Tests.Extensions;
using QuickRepricer.Web.Tests.Helpers;
using System;
using Xunit;

namespace QuickRepricer.Web.Tests.Controllers.Web
{
    public class AppControllerTests : IDisposable
    {
        private Mock<SignInManager<ApplicationUser>> _mockSignInManager;

        private AppController _appController;

        public AppControllerTests()
        {
            _mockSignInManager = MockHelpers.MockSignInManager<ApplicationUser>();
            
            _appController = new AppController(_mockSignInManager.Object);
            _appController.MockCurrentUser("1", "user@gmail.com");
        }

        public void Dispose()
        {
            _appController.Dispose();   
        }

        [Fact]
        public void About_ReturnsANonNullViewResult()
        {
            // Arrange
           
            //// Act
            var result = _appController.About();

            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }


        [Fact]
        public void Blog_ReturnsANonNullViewResult()
        {
            // Arrange

            //// Act
            var result = _appController.Blog();

            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        
        [Fact]
        public void Faq_ReturnsANonNullViewResult()
        {
            // Arrange
  

            //// Act
            var result = _appController.Faq();

            //// Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
      
        }

        
        [Fact]
        public void Privacy_ReturnsANonNullViewResult()
        {
            // Arrange

            //// Act
            var result = _appController.Privacy();

            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void Terms_ReturnsANonNullViewResult()
        {
            // Arrange
          
            //// Act
            var result = _appController.Terms();

            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void RefundPolicy_ReturnsANonNullViewResult()
        {
            // Arrange
           
            //// Act
            var result = _appController.RefundPolicy();

            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void Error_ReturnsANonNullViewResult()
        {
            // Arrange
           
            //// Act
            var result = _appController.Error();

            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }


        [Fact]
        public void Contact_ReturnsANonNullViewResult()
        {
            // Arrange
         
            //// Act
            var result = _appController.Contact();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }


        [Fact]
        public void ContactPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _appController.ModelState.AddModelError("Name", "Required");
            _appController.ModelState.AddModelError("Email", "Required");

            //// Act
            var result = _appController.Contact(new ContactViewModel());

            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Subject.Value.Should()
                .BeOfType<SerializableError>();
        }


        [Fact]
        public void ContactPost_ReturnsARedirectToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var contactViewModel = new ContactViewModel()
            {
                Name = "Joker",
                Email = "joker@gmail.com",
                Message = "test test test",
                Phone = "0207 60 60 60"
            };

            //// Act
            var result = _appController.Contact(contactViewModel);

            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().Be("Index");
        }



        [Fact]
        public void Index_ReturnsANonNullViewResult_NotSignedIn()
        {
            //Arrange 

            //Act
            var result = _appController.Index();
            //// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }



        [Fact]
        public void Index_ReturnsARedirectToDashboardIndex_WhenSignedIn()
        {
            //Arrage
            _mockSignInManager.Setup(x => x.IsSignedIn(_appController.User)).Returns(true);

            //act
            var result = _appController.Index();


            //// Assert
            result.Should().NotBeNull();

            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ControllerName.Should().Be("Dashboard");

            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().Be("Index");
        }
    }
}
