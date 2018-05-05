using System;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Services.UniTests.ResultServiceTests
{
    [TestClass]
    public class Contructor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenInvokedWithValidParameters()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            //Act
            var resultService = new ResultService(saverMock.Object,mapperMock.Object, userTestsRepoMock.Object);

            //Assert
            Assert.IsNotNull(resultService);
            Assert.IsInstanceOfType(resultService, typeof(IResultService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullMapperParameter()
        {
            //Arrange
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ResultService(saverMock.Object, null, userTestsRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullSaverParameter()
        {
            //Arrange
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();
            var mapperMock = new Mock<IMappingProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ResultService(null, mapperMock.Object, userTestsRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullUserTestsRepositoryParameter()
        {
            //Arrange
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var mapperMock = new Mock<IMappingProvider>();


            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ResultService(saverMock.Object, mapperMock.Object, null));
        }
    }
}
