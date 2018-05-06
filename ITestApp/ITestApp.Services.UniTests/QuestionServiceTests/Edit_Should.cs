using System;
using System.Collections.Generic;
using System.Linq;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Services.UniTests.QuestionServiceTests
{
    [TestClass]
    public class Edit_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenQuestionDtoParameterIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            var questionService = new QuestionsService(saverMock.Object, mapperMock.Object, questionsRepoMock.Object, answersRepoMock.Object);
            
            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(()=> questionService.Edit(null));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenQuestionEntityIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            var questionService = new QuestionsService(saverMock.Object, mapperMock.Object, questionsRepoMock.Object, answersRepoMock.Object);

            var data = new List<Question>()
            {
                new Question{Id = 1, Content = "AlaBala", TestId = 1 }
            };
            
            questionsRepoMock.Setup(x => x.All).Returns(data.AsQueryable());

            var dto = new QuestionDto() { Id = 2, Content = "No AlaBala", TestId = 1};
            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => questionService.Edit(dto));
        }

        [TestMethod]
        public void InvokeQuestionRepositoryMethodUpdate_WhenParametersAreCorrect()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            var questionService = new QuestionsService(saverMock.Object, mapperMock.Object, questionsRepoMock.Object, answersRepoMock.Object);

            var data = new List<Question>()
            {
                new Question{Id = 1, Content = "AlaBala", TestId = 1 }
            };

            questionsRepoMock.Setup(x => x.All).Returns(data.AsQueryable());

            var dto = new QuestionDto() { Id = 1, Content = "AlaBala", TestId = 1 };

            //Act
            questionService.Edit(dto);
            //Assert
            questionsRepoMock.Verify(x => x.Update(It.IsAny<Question>()), Times.Once);
        }

        [TestMethod]
        public void InvokeSaverMethodSaveChanges_WhenParametersAreCorrect()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var questionsRepoMock = new Mock<IRepository<Question>>();
            var answersRepoMock = new Mock<IRepository<Answer>>();

            var questionService = new QuestionsService(saverMock.Object, mapperMock.Object, questionsRepoMock.Object, answersRepoMock.Object);

            var data = new List<Question>()
            {
                new Question{Id = 1, Content = "AlaBala", TestId = 1 }
            };

            questionsRepoMock.Setup(x => x.All).Returns(data.AsQueryable());

            var dto = new QuestionDto() { Id = 1, Content = "AlaBala", TestId = 1 };

            //Act
            questionService.Edit(dto);
            //Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
