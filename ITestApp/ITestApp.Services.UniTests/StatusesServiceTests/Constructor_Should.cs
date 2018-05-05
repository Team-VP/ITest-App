using System;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Services.UniTests.StatusesServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenInvokedWithValidParameters()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var statusesRepoMock = new Mock<IRepository<Status>>();

            //Act
            var statusService = new StatusesService(mapperMock.Object, statusesRepoMock.Object);

            //Assert
            Assert.IsNotNull(statusService);
            Assert.IsInstanceOfType(statusService, typeof(IStatusesService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullMapperParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var statusesRepoMock = new Mock<IRepository<Status>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(()=> new StatusesService(null, statusesRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullStatusesRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var statusesRepoMock = new Mock<IRepository<Status>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new StatusesService(mapperMock.Object, null));
        }
    }
}
