using System;
using System.Collections.Generic;
using System.Text;
using ITestApp.Common.Providers;
using ITestApp.Data;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Services.UniTests.AdminServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenInvokedWithValidParameters()
        {
            //Arrange
            var dbcontextMock = new Mock<ITestAppDbContext>();
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            //Act
            var adminService = new AdminService(saverMock.Object, mapperMock.Object, testsRepoMock.Object, userTestsRepoMock.Object);

            //Assert
            Assert.IsNotNull(adminService);
            Assert.IsInstanceOfType(adminService, typeof(IAdminService));
        }
    }
}
