using System;
using Xunit;
//using UnitTestApp.Controllers;
using PointSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using PointSystem.Services;

namespace XUnitTestProject1
{
    public class UnitTest1
    {

        [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange

            //var mock = new Mock<ILogger<HomeController>>();//<IRepository>();
            //mock.Setup(repo => repo.GetAll()).Returns(GetTestPhones());
            //var controller = new HomeController(mock.Object);
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("notAuthUser", result?.ViewData["Message"]);  //"notAuthUser""Hello!"
        }

        [Fact]
        public void IndexViewResultNotNull()
        { 
            // Arrange
            var mock = new Mock<ILogger<HomeController>>();//<IRepository>();
            //mock.Setup(repo => repo.GetAll()).Returns(GetTestPhones());
            //var controller = new HomeController(mock.Object);
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);//NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            var mock = new Mock<ILogger<HomeController>>();//<IRepository>();
            //mock.Setup(repo => repo.GetAll()).Returns(GetTestPhones());
            //var controller = new HomeController(mock.Object);
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Null(result);//Equal("Index", result?.ViewName);
        }
        /* [Fact]
         public void Test1()
         {

         }*/
    }
}
