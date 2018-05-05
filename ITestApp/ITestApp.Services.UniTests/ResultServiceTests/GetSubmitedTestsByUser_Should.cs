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
    public class GetSubmitedTestsByUser_Should
    {
        [TestMethod]
        public void ReturnIEnumerableOfUserTestDto_WhenCollectionIsNotNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "1", Id = 2, TestId = 2, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "1", Id = 3, TestId = 3, IsPassed = true, TimeExpire = DateTime.Now  },
            }.AsQueryable();

            userTestsRepoMock.Setup(x => x.All).Returns(data);

            var dataMapper = new List<UserTestDto>()
            {
                new UserTestDto{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTestDto{UserId = "1", Id = 2, TestId = 2, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTestDto{UserId = "1", Id = 3, TestId = 3, IsPassed = true, TimeExpire = DateTime.Now  },
            }.AsQueryable();

            mapperMock.Setup(m => m.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>())).Returns(new List<UserTestDto>(dataMapper).AsQueryable());

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act
            var submittedTests = resultService.GetSubmitedTestsByUser("1");

            //Assert
            Assert.AreEqual(data.Count(), submittedTests.Count());
            Assert.IsNotNull(submittedTests);
            Assert.IsInstanceOfType(submittedTests, typeof(IEnumerable<UserTestDto>));
        }

        [TestMethod]
        public void ThrowArgumentNullExeption_WhenUserIdIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act&Assert

            Assert.ThrowsException<ArgumentNullException>(() => resultService.GetSubmitedTestsByUser(null));
        }

        [TestMethod]
        public void ThrowArgumentNullExeption_WhenUserIdIsEmpty()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentException>(() => resultService.GetSubmitedTestsByUser(string.Empty));
        }

        [TestMethod]
        public void Invoke_Mapper_ProjetTo()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "1", Id = 2, TestId = 2, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "1", Id = 3, TestId = 3, IsPassed = true, TimeExpire = DateTime.Now  },
            }.AsQueryable();

            userTestsRepoMock.Setup(x => x.All).Returns(data);

            var dataMapper = new List<UserTestDto>()
            {
                new UserTestDto{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTestDto{UserId = "1", Id = 2, TestId = 2, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTestDto{UserId = "1", Id = 3, TestId = 3, IsPassed = true, TimeExpire = DateTime.Now  },
            }.AsQueryable();

            mapperMock.Setup(m => m.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>())).Returns(new List<UserTestDto>(dataMapper).AsQueryable());

            var resultService = new ResultService(saverMock.Object, mapperMock.Object, userTestsRepoMock.Object);

            //Act
            var submittedTests = resultService.GetSubmitedTestsByUser("1");

            //Assert
            mapperMock.Verify(x => x.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>()), Times.Once);
        }
    }
}
