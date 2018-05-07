using AutoMapper;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Common.Provides.UniTests.MappingProviderTests
{
    [TestClass]
    public class MappingProvider_Should
    {
        [TestMethod]
        public void CallMethodMap()
        {
            //Arrange
            var test = new Test();

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<TestDto>(It.IsAny<Test>())).Verifiable();

            var mapperProvider = new MappingProvider(mapperMock.Object);

            //Act
            var dto = mapperProvider.MapTo<TestDto>(test);

            //Assert
            mapperMock.Verify(x => x.Map<TestDto>(test), Times.Once);
        }
    }
}
