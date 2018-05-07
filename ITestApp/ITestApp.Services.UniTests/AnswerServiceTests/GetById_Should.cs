using System;
using System.Collections.Generic;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using ITestApp.DTO;

namespace ITestApp.Services.UniTests.AnswerServiceTests
{
    [TestClass]
    public class GetById_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenAnswerEntityIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var answerRepoMock = new Mock<IRepository<Answer>>();

            //Act
            var answerService = new AnswersService(saverMock.Object, mapperMock.Object, answerRepoMock.Object);

            var data = new List<Answer>()
            {
                new Answer{Id = 1, Content = "AlaBala", QuestionId = 1 }
            };

            answerRepoMock.Setup(a => a.All).Returns(data.AsQueryable());

            Assert.ThrowsException<ArgumentNullException>(()=> answerService.GetById(2));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenAnswerDtoIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var answerRepoMock = new Mock<IRepository<Answer>>();

            //Act
            var answerService = new AnswersService(saverMock.Object, mapperMock.Object, answerRepoMock.Object);

            var data = new List<Answer>()
            {
                new Answer{Id = 1, Content = "AlaBala", QuestionId = 1 }
            };

            answerRepoMock.Setup(a => a.All).Returns(data.AsQueryable());

            Assert.ThrowsException<ArgumentNullException>(()=> answerService.GetById(1));
        }

        [TestMethod]
        public void ReturnAnswerDto_WhenParametersAreCorrect()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var answerRepoMock = new Mock<IRepository<Answer>>();

            var answerService = new AnswersService(saverMock.Object, mapperMock.Object, answerRepoMock.Object);

            var data = new List<Answer>()
            {
                new Answer{Id = 1, Content = "AlaBala", QuestionId = 1 }
            };

            answerRepoMock.Setup(a => a.All).Returns(data.AsQueryable());
            var mapperData = new AnswerDto() { Id = 1, Content = "AlaBala", QuestionId = 1 };
            mapperMock.Setup(m => m.MapTo<AnswerDto>(It.IsAny<Answer>())).Returns(mapperData);

            //Act
            var dto = answerService.GetById(1);

            //Assert
            Assert.IsNotNull(dto);
            Assert.IsInstanceOfType(dto, typeof(AnswerDto));
        }

        [TestMethod]
        public void InvokeMapperMethodMapTo_WhenParametersAreCorrect()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var answerRepoMock = new Mock<IRepository<Answer>>();

            var answerService = new AnswersService(saverMock.Object, mapperMock.Object, answerRepoMock.Object);

            var data = new List<Answer>()
            {
                new Answer{Id = 1, Content = "AlaBala", QuestionId = 1 }
            };

            answerRepoMock.Setup(a => a.All).Returns(data.AsQueryable());
            var mapperData = new AnswerDto() { Id = 1, Content = "AlaBala", QuestionId = 1 };
            mapperMock.Setup(m => m.MapTo<AnswerDto>(It.IsAny<Answer>())).Returns(mapperData);

            //Act
            var dto = answerService.GetById(1);

            //Assert
            mapperMock.Verify(x => x.MapTo<AnswerDto>(It.IsAny<Answer>()), Times.Once);
        }
    }
}
