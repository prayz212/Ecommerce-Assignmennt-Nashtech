using CustomerSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTest.CustomerSiteProject.Controllers.Error
{
    public class IndexShould
    {
        [Fact]
        public void ReturnViewWhenCalling()
        {
            //Arrange
            var errorController = new ErrorController();

            //Act
            var result = errorController.Index();

            //Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
    }
}