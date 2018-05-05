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

namespace ITestApp.Services.UniTests.AdminServiceTests
{
    [TestClass]
    public class GetTestsByAuthorId_Should
    {
        [TestMethod]
        public void ReturnIEnumerableOfTestDto_WhenCollectionIsNotNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var author = new User() { UserName = "a@a.com", Id = "1"};
            var data = new List<Test>()
            {
                new Test { Title = "Test1", Author = author, AuthorId = "1"},
                new Test { Title = "Test2", Author = author, AuthorId = "1"},
                new Test { Title = "Test3", Author = author, AuthorId = "1"}
            }.AsQueryable();

            testsRepoMock.Setup(x => x.All).Returns(data);

            var dtoAuthor = new UserDto() { UserName = "a@a.com", Id = "1" };
            var mapperData = new List<TestDto>()
            {
                new TestDto { Title = "Test1", Author = dtoAuthor, AuthorId = "1"},
                new TestDto { Title = "Test2", Author = dtoAuthor, AuthorId = "1"},
                new TestDto { Title = "Test3", Author = dtoAuthor, AuthorId = "1"}
            };

            mapperMock.Setup(m => m.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(new List<TestDto>(mapperData).AsQueryable);
            var adminService = new AdminService(saverMock.Object, mapperMock.Object, testsRepoMock.Object, userTestsRepoMock.Object);
            data.ToList();

            //Act
            var tests = adminService.GetTestsByAuthor(author.Id);

            //Assert
            Assert.AreEqual(data.Count(), tests.Count());
            Assert.IsNotNull(tests);
            Assert.IsInstanceOfType(tests, typeof(IEnumerable<TestDto>));

        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenAuhotIdParameterIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var adminService = new AdminService(saverMock.Object, mapperMock.Object, testsRepoMock.Object, userTestsRepoMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => adminService.GetTestsByAuthor(null));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenAuhotIdParameterIsEmpty()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var adminService = new AdminService(saverMock.Object, mapperMock.Object, testsRepoMock.Object, userTestsRepoMock.Object);

            Assert.ThrowsException<ArgumentException>(() => adminService.GetTestsByAuthor(String.Empty));
        }

        [TestMethod]
        public void Invoke_Mapper_ProjectTo()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsRepoMock = new Mock<IRepository<Test>>();
            var userTestsRepoMock = new Mock<IRepository<UserTest>>();

            var author = new User() { UserName = "a@a.com", Id = "1" };
            var data = new List<Test>()
            {
                new Test { Title = "Test1", Author = author, AuthorId = "1"},
                new Test { Title = "Test2", Author = author, AuthorId = "1"},
                new Test { Title = "Test3", Author = author, AuthorId = "1"}
            }.AsQueryable();

            testsRepoMock.Setup(x => x.All).Returns(data);

            var dtoAuthor = new UserDto() { UserName = "a@a.com", Id = "1" };
            var mapperData = new List<TestDto>()
            {
                new TestDto { Title = "Test1", Author = dtoAuthor, AuthorId = "1"},
                new TestDto { Title = "Test2", Author = dtoAuthor, AuthorId = "1"},
                new TestDto { Title = "Test3", Author = dtoAuthor, AuthorId = "1"}
            };

            mapperMock.Setup(m => m.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(new List<TestDto>(mapperData).AsQueryable);
            var adminService = new AdminService(saverMock.Object, mapperMock.Object, testsRepoMock.Object, userTestsRepoMock.Object);
            data.ToList();

            //Act
            var tests = adminService.GetTestsByAuthor(author.Id);
            
            //Assert
            mapperMock.Verify(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>()), Times.Once);

        }
    }
}
