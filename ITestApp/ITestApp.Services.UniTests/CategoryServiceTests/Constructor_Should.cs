using System;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace ITestApp.Services.UniTests.CategoryServiceTests
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
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            //Act
            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            //Assert
            Assert.IsNotNull(categoriesService);
            Assert.IsInstanceOfType(categoriesService, typeof(ICategoryService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullMapperParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(()=> new CategoriesService(saverMock.Object, null, categoriesRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullSaverParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CategoriesService(null, mapperMock.Object, categoriesRepoMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenInvokedWithNullCategoriesRepositoryParameter()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CategoriesService(saverMock.Object, mapperMock.Object, null));
        }
    }
}
