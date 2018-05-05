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

namespace ITestApp.Services.UniTests.StatusesServiceTests
{
    [TestClass]
    public class GetStatusByName_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenStatusNameParameterIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var statusesRepoMock = new Mock<IRepository<Status>>();

            var statusService = new StatusesService(mapperMock.Object, statusesRepoMock.Object);

            Assert.ThrowsException<ArgumentNullException>(()=> statusService.GetStatusByName(null));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenStatusNameParameterIsEmptyString()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var statusesRepoMock = new Mock<IRepository<Status>>();

            var statusService = new StatusesService(mapperMock.Object, statusesRepoMock.Object);

            Assert.ThrowsException<ArgumentException>(() => statusService.GetStatusByName(string.Empty));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenStatusEntityIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var statusesRepoMock = new Mock<IRepository<Status>>();

            var statusService = new StatusesService(mapperMock.Object, statusesRepoMock.Object);

            var data = new List<Status>()
            {
                new Status{Id = 1 ,Name = "Draft"},
                new Status{Id = 2 ,Name = "Publish"}
            };

            statusesRepoMock.Setup(x => x.All).Returns(data.AsQueryable());

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => statusService.GetStatusByName("sucks"));
        }

        [TestMethod]
        public void ReturnStatusDto_WhenInvokedWithCorrectParameters()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var statusesRepoMock = new Mock<IRepository<Status>>();

            var statusService = new StatusesService(mapperMock.Object, statusesRepoMock.Object);

            var data = new List<Status>()
            {
                new Status{Id = 1 ,Name = "Draft"},
                new Status{Id = 2 ,Name = "Publish"}
            };

            var dataMapper = new StatusDto() { Id = 1, Name = "Draft" }; 
            statusesRepoMock.Setup(x => x.All).Returns(data.AsQueryable());

            mapperMock.Setup(m => m.MapTo<StatusDto>(It.IsAny<Status>())).Returns(dataMapper);
            //Act
            var statusDto = statusService.GetStatusByName("Draft");

            //&Assert
            Assert.IsNotNull(statusDto);
            Assert.IsInstanceOfType(statusDto, typeof(StatusDto));
        }

        [TestMethod]
        public void InnokeMapperMapToMethod()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var statusesRepoMock = new Mock<IRepository<Status>>();

            var statusService = new StatusesService(mapperMock.Object, statusesRepoMock.Object);

            var data = new List<Status>()
            {
                new Status{Id = 1 ,Name = "Draft"},
                new Status{Id = 2 ,Name = "Publish"}
            };

            var dataMapper = new StatusDto() { Id = 1, Name = "Draft" };
            statusesRepoMock.Setup(x => x.All).Returns(data.AsQueryable());

            mapperMock.Setup(m => m.MapTo<StatusDto>(It.IsAny<Status>())).Returns(dataMapper);
            //Act
            var statusDto = statusService.GetStatusByName("Draft");

            //&Assert
            mapperMock.Verify(x => x.MapTo<StatusDto>(It.IsAny<Status>()), Times.Once);
        }
    }
}
