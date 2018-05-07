using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ITestApp.Common.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ITestApp.Common.Provides.UniTests.MappingProviderTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenMapperParameterIsNotNull()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();

            var mapperProvider = new MappingProvider(mapperMock.Object);

            Assert.IsNotNull(mapperProvider);
            Assert.IsInstanceOfType(mapperProvider, typeof(IMappingProvider));
        }
    }
}
