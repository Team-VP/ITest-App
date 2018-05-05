using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using ITestApp.Common.Providers;
using ITestApp.Data;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Services.UniTests.ResultServiceTests
{
    [TestClass]
    public class GetStartedTestShould
    {

        [TestMethod]
        public void ReturnUserTestDto_WhenInvokedWithRightParameters()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "1", Id = 2, TestId = 2, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "2", Id = 3, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
            }.AsQueryable();

            userTestsRepoMock.Setup(x => x.All).Returns(data);

            var dataMapper = new UserTestDto() { UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  };

            mapperMock.Setup(m => m.MapTo<UserTestDto>(It.IsAny<UserTest>())).Returns(dataMapper);

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);
            //Act
            var testDto = resultService.GetStartedTest("1", 1);
            //Assert
            Assert.IsNotNull(testDto);
            Assert.IsInstanceOfType(testDto, typeof(UserTestDto));
        }

        [TestMethod]
        public void InvokeMapperMethodMapTo()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "1", Id = 2, TestId = 2, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "2", Id = 3, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
            }.AsQueryable();

            userTestsRepoMock.Setup(x => x.All).Returns(data);

            var dataMapper = new UserTestDto() { UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now };

            mapperMock.Setup(m => m.MapTo<UserTestDto>(It.IsAny<UserTest>())).Returns(dataMapper);

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act
            var testDto = resultService.GetStartedTest("1", 1);

            //Assert
            mapperMock.Verify(x => x.MapTo<UserTestDto>(It.IsAny<UserTest>()), Times.Once);
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenUserIdIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);
            
            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => resultService.GetStartedTest(null, 1));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenUserIdIsEmpty()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentException>(() => resultService.GetStartedTest(string.Empty, 1));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenTestIdIsLessThanOne()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => resultService.GetStartedTest("1", 0));
        }
    }
}
