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
    public class Submit_Should
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
            Assert.ThrowsException<ArgumentNullException>(()=> resultService.Submit(null));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenUserTestEntityToUpdateNull()
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

            var dto = new UserTestDto() { UserId = "2", TestId = 1};

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => resultService.Submit(dto));
        }

        [TestMethod]
        public void InvokeSaverSaveChanges_WhenParametersIsCorrect()
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

            //Act
            resultService.Submit(dto);
            //Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void InvokeRepoUpdate_WhenParametersIsCorrect()
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

            //Act
            resultService.Submit(dto);
            //Assert
            userTestsRepoMock.Verify(x => x.Update(It.IsAny<UserTest>()), Times.Once);
        }


    }
}
