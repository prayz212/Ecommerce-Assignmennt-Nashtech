using CustomerSite.Controllers;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTest.CustomerSiteProject.Controllers.Home
{
    public class AboutShould
    {
        [Fact]
        public void ReturnViewWhenCalling()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            var homeController = new HomeController(mockProductService.Object);

            //Act
            var result = homeController.About();

            //Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}