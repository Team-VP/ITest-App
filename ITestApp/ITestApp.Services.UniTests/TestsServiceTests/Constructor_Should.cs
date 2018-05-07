using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Services.UnitTests.TestsServiceTests
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
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            //Act
            var statusService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            //Assert
            Assert.IsNotNull(statusService);
            Assert.IsInstanceOfType(statusService, typeof(ITestsService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullSaverParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestsService(null, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullMapperParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestsService(saverMock.Object, null, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullTestsRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestsService(saverMock.Object, mapperMock.Object, null, questionsMock.Object, answersMock.Object, userTestsMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullQuestionRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, null, answersMock.Object, userTestsMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullAnswersRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, null, userTestsMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullUserTestsRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, null));
        }
    }
}
