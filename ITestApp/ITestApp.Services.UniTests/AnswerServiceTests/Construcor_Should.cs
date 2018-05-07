using System;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Services.UniTests.AnswerServiceTests
{
    [TestClass]
    public class Construcor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenInvokedWithValidParameters()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var answerRepoMock = new Mock<IRepository<Answer>>();

            //Act
            var answerService = new AnswersService(saverMock.Object, mapperMock.Object, answerRepoMock.Object);

            //Assert
            Assert.IsNotNull(answerService);
            Assert.IsInstanceOfType(answerService, typeof(IAnswersService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullMapperParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var answerRepoMock = new Mock<IRepository<Answer>>();

            //Act
            var answerService = new AnswersService(saverMock.Object, mapperMock.Object, answerRepoMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AnswersService(saverMock.Object, null, answerRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullSaverParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var answerRepoMock = new Mock<IRepository<Answer>>();

            //Act
            var answerService = new AnswersService(saverMock.Object, mapperMock.Object, answerRepoMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AnswersService(null, mapperMock.Object, answerRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullAnswerRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var answerRepoMock = new Mock<IRepository<Answer>>();

            //Act
            var answerService = new AnswersService(saverMock.Object, mapperMock.Object, answerRepoMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AnswersService(saverMock.Object, mapperMock.Object, null));
        }
    }
}
