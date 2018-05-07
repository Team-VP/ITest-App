using ITestApp.Common.Exceptions;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITestApp.Services.UnitTests.TestsServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void ThrowArgumentOutOfRangeException_WhenIdLessThanZeroIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();
            var id = -1;

            var testsService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);
            
            //Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testsService.Delete(id));
        }

        [TestMethod]
        public void ThrowTestNotFoundException_WhenTestWithProvidedIdIsNotFound()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();
            var id = 100;

            var testsService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var data = new List<Test>()
            {
                new Test
                {
                    Id = 1,
                    IsDeleted = false,
                    CreatedOn = new DateTime(1991, 5, 1),
                    Title = "TestTitle",
                    RequiredTime = 60,
                    AuthorId = "TestAuthorId",
                    CategoryId = 1,
                    StatusId = 1
                }
            };

            testsMock.Setup(x => x.All).Returns(data.AsQueryable);
            
            //Act & Assert
            Assert.ThrowsException<TestNotFoundException>(() => testsService.Delete(id));
        }

        //[TestMethod]
        //public void ThrowTestNotFoundException_WhenTestWithProvidedIdIsNotFound()
        //{
        //    //Arrange
        //    var mapperMock = new Mock<IMappingProvider>();
        //    var saverMock = new Mock<ISaver>();
        //    var testsMock = new Mock<IRepository<Test>>();
        //    var questionsMock = new Mock<IRepository<Question>>();
        //    var answersMock = new Mock<IRepository<Answer>>();
        //    var userTestsMock = new Mock<IRepository<UserTest>>();
        //    var id = 100;

        //    var testsService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

        //    var data = new List<Test>()
        //    {
        //        new Test
        //        {
        //            Id = 1,
        //            IsDeleted = false,
        //            CreatedOn = new DateTime(1991, 5, 1),
        //            Title = "TestTitle",
        //            RequiredTime = 60,
        //            AuthorId = "TestAuthorId",
        //            CategoryId = 1,
        //            StatusId = 1
        //        }
        //    };

        //    testsMock.Setup(x => x.All).Returns(data.AsQueryable);

        //    //Act & Assert
        //    Assert.ThrowsException<TestNotFoundException>(() => testsService.Delete(id));
        //}
    }
}
