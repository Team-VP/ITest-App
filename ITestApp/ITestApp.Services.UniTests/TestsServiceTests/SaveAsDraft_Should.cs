using ITestApp.Common.Exceptions;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Services.UnitTests.TestsServiceTests
{
    [TestClass]
    public class SaveAsDraft_Should
    {
        [TestMethod]
        public void ThrowInvalidTestException_WhenNullDtoTestProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            //Act & Assert
            Assert.ThrowsException<InvalidTestException>(() => sut.SaveAsDraft(null));
        }

        [TestMethod]
        public void InvokeMapperMapToMethodOnce_WhenValidTestDtoProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto();
            var test = new Test();

            mapperMock.Setup(x => x.MapTo<Test>(It.IsAny<TestDto>())).Returns(test);

            //Act
            sut.SaveAsDraft(testDto);

            //Assert
            mapperMock.Verify(x => x.MapTo<Test>(It.IsAny<TestDto>()), Times.Once);
        }

        [TestMethod]
        public void CallTestsRepositoryAddMethodOnce_WhenValidTestDtoProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto();
            var test = new Test();

            mapperMock.Setup(x => x.MapTo<Test>(It.IsAny<TestDto>())).Returns(test);

            //Act
            sut.SaveAsDraft(testDto);

            //Assert
            testsMock.Verify(x => x.Add(It.IsAny<Test>()), Times.Once);
        }

        [TestMethod]
        public void SetTheTestEntityModelStatusIdToOne_WhenValidTestDtoProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto();
            var test = new Test()
            {
                StatusId = 1
            };

            mapperMock.Setup(x => x.MapTo<Test>(It.IsAny<TestDto>())).Returns(test);

            //Act
            sut.SaveAsDraft(testDto);

            //Assert
            Assert.AreEqual(2, test.StatusId);
        }

        [TestMethod]
        public void CallSaverSaveChangesMethodOnce_WhenValidTestDtoProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto();
            var test = new Test();

            mapperMock.Setup(x => x.MapTo<Test>(It.IsAny<TestDto>())).Returns(test);

            //Act
            sut.SaveAsDraft(testDto);

            //Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
