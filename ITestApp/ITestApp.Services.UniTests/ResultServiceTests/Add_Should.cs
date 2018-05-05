using System;
using System.Collections.Generic;
using System.Linq;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Services.UniTests.ResultServiceTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenUserTestDtoIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => resultService.Add(null));
        }

        [TestMethod]
        public void InvokeMapperMapTo()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest {UserId = "1", TestId = 1 }
            };

            userTestsRepoMock.Setup(x => x.All).Returns(data.AsQueryable);

            var dto = new UserTestDto() { UserId = "1", TestId = 1 };

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            ////Act
            resultService.Add(dto);
            //Assert
            mapperMock.Verify(x => x.MapTo<UserTest>(It.IsAny<UserTestDto>()), Times.Once);
        }

        [TestMethod]
        public void InvokeUserTestRepoAddMethod()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest {UserId = "1", TestId = 1 }
            };

            userTestsRepoMock.Setup(x => x.All).Returns(data.AsQueryable);

            var dto = new UserTestDto() { UserId = "1", TestId = 1 };

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            ////Act
            resultService.Add(dto);
            //Assert
            userTestsRepoMock.Verify(x => x.Add(It.IsAny<UserTest>()), Times.Once);
        }

        [TestMethod]
        public void InvokeSaverSaveChangesMethod()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest {UserId = "1", TestId = 1 }
            };

            userTestsRepoMock.Setup(x => x.All).Returns(data.AsQueryable);

            var dto = new UserTestDto() { UserId = "1", TestId = 1 };

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            ////Act
            resultService.Add(dto);
            //Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
