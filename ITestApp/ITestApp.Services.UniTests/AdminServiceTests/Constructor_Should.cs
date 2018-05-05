using System;
using ITestApp.Common.Providers;
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

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullMapperParameter()
        {
            //Arrange
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminService(saverMock.Object, null, testsRepoMock.Object, userTestsRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullSaverParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminService(null, mapperMock.Object ,testsRepoMock.Object, userTestsRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullTestRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminService(saverMock.Object, mapperMock.Object, null, userTestsRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullUserTestRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminService(saverMock.Object, mapperMock.Object, testsRepoMock.Object, null));
        }
    }
}
