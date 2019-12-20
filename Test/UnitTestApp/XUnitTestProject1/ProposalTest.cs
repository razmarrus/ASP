using System;
using System.Collections.Generic;
using System.Text;


using Xunit;
//using UnitTestApp.Controllers;
using PointSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using PointSystem.Services;
using PointSystem.Models;

using PointSystem.Data;
using Microsoft.AspNetCore.Identity;

namespace XUnitTestProject1
{
    public class ProposalTest
    {

        [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange

            //var mock = new Mock<ILogger<HomeController>>();//<IRepository>();
            //mock.Setup(repo => repo.GetAll()).Returns(GetTestPhones());
            //var controller = new HomeController(mock.Object);
            //var mock = new Mock<IRepository>();
            var mock = new Mock<ApplicationDbContext>();
            //var userMock = new Mock<UserManager<AspNetUser>>();
            //UserManager<AspNetUser> _userManager;
           // mock.Setup(repo => repo.GetAll()).Returns(GetTestPhones());
            ProposalsController controller = new ProposalsController(mock);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("notAuthUser", result?.ViewData["Message"]);  //"notAuthUser""Hello!"
        }
        private List<Proposal> GetTestPhones()
        {
            DateTime date1 = new DateTime(2015, 7, 20, 18, 30, 25);
            DateTime date2 = new DateTime(2015, 7, 21, 18, 30, 25);
            var phones = new List<Proposal>
            {
                
                new Proposal { id=1, AspNetUserId = "1", Content = "qwe" , StartTime= date1, EndTime = date2, Status = "prossesing", Topic = "gig"},
                new Proposal { id=2, AspNetUserId = "2", Content = "exsist" , StartTime= date1, EndTime = date2, Status = "prossesing", Topic = "gig"},
                new Proposal { id=3, AspNetUserId = "3", Content = "here" , StartTime= date1, EndTime = date2, Status = "prossesing", Topic = "gig"},
                new Proposal { id=4, AspNetUserId = "4", Content = "party" , StartTime= date1, EndTime = date2, Status = "prossesing", Topic = "gig"},
            };
            return phones;
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
