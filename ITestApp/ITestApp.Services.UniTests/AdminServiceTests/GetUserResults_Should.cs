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


namespace ITestApp.Services.UnitTests.AdminServiceTests
{
    [TestClass]
    public class GetUserResults_Should
    {
        [TestMethod]
        public void ReturnIEnumerableOfUserTestDto_WhenCollectionIsNotNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "1", Id = 2, TestId = 2, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "2", Id = 3, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
            }.AsQueryable();

            userTestsRepoMock.Setup(x => x.All).Returns(data);

            var dataMapper = new List<UserTestDto>()
            {
                new UserTestDto{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTestDto{UserId = "1", Id = 2, TestId = 2, IsPassed = true , TimeExpire = DateTime.Now },
                new UserTestDto{UserId = "2", Id = 3, TestId = 1, IsPassed = true , TimeExpire = DateTime.Now },
            };

            mapperMock.Setup(m => m.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>())).Returns(new List<UserTestDto>(dataMapper).AsQueryable());

            //Act
            var adminService = new AdminService(saverMock.Object, mapperMock.Object, testsRepoMock.Object, userTestsRepoMock.Object);
            data.ToList();

            var usersTests = adminService.GetUserResults();
            //Assert
            Assert.AreEqual(data.Count(), usersTests.Count());
            Assert.IsNotNull(usersTests);
            Assert.IsInstanceOfType(usersTests, typeof(IEnumerable<UserTestDto>));
        }

        [TestMethod]
        public void Invoke_Mapper_ProjectTo()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var data = new List<UserTest>()
            {
                new UserTest{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "1", Id = 2, TestId = 2, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTest{UserId = "2", Id = 3, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
            }.AsQueryable();

            userTestsRepoMock.Setup(x => x.All).Returns(data);

            var dataMapper = new List<UserTestDto>()
            {
                new UserTestDto{UserId = "1", Id = 1, TestId = 1, IsPassed = true, TimeExpire = DateTime.Now  },
                new UserTestDto{UserId = "1", Id = 2, TestId = 2, IsPassed = true , TimeExpire = DateTime.Now },
                new UserTestDto{UserId = "2", Id = 3, TestId = 1, IsPassed = true , TimeExpire = DateTime.Now },
            };

            mapperMock.Setup(m => m.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>())).Returns(new List<UserTestDto>(dataMapper).AsQueryable());

            //Act
            var adminService = new AdminService(saverMock.Object, mapperMock.Object, testsRepoMock.Object, userTestsRepoMock.Object);
            data.ToList();
            var usersTests = adminService.GetUserResults();
            //Assert
            mapperMock.Verify(x => x.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>()), Times.Once);
        }
    }
}
