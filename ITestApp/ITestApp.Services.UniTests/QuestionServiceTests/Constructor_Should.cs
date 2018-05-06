using System;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Services.UniTests.QuestionServiceTests
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
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            //Act
            var questionService = new QuestionsService(saverMock.Object, mapperMock.Object, questionsRepoMock.Object, answersRepoMock.Object);

            //Assert
            Assert.IsNotNull(questionService);
            Assert.IsInstanceOfType(questionService, typeof(IQuestionsService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullMapperParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => new QuestionsService(saverMock.Object, null, questionsRepoMock.Object, answersRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullSaverParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => new QuestionsService(null, mapperMock.Object, questionsRepoMock.Object, answersRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullQuestionsRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => new QuestionsService(saverMock.Object, mapperMock.Object, null, answersRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullAnswersRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => new QuestionsService(saverMock.Object, mapperMock.Object, questionsRepoMock.Object, null));
        }
    }
}
