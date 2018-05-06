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

namespace ITestApp.Services.UniTests.CategoryServiceTests
{
    [TestClass]
    public class GetAllCategories_Should
    {
        [TestMethod]
        public void ReturnIEnumeranleOfCategoryDto_WhenCollectionIsNotNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            var data = new List<Category>()
            {
                new Category{Id = 1, Name = ".Net"},
                new Category{Id = 2, Name = "Java"},
                new Category{Id = 3, Name = "SQL"}
            };

            categoriesRepoMock.Setup(x => x.All).Returns(data.AsQueryable);

            var dataMapper = new List<CategoryDto>()
            {
                new CategoryDto{Id = 1, Name = ".Net"},
                new CategoryDto{Id = 2, Name = "Java"},
                new CategoryDto{Id = 3, Name = "SQL"}
            };

            mapperMock.Setup(m => m.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(new List<CategoryDto>(dataMapper).AsQueryable);

            //Act
            var collection = categoriesService.GetAllCategories();
            
            //Assert
            Assert.AreEqual(data.Count, collection.Count());
            Assert.IsInstanceOfType(collection, typeof(IEnumerable<Category>));

        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenCollectionIsEmptyOrNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);
            
            //Act
            categoriesRepoMock.Setup(x => x.All).Returns((IQueryable<Category>)null);

            //Assert
            Assert.ThrowsException<ArgumentNullException>(()=> categoriesService.GetAllCategories());
        }

        [TestMethod]
        public void InvokeMapperMethodProjectTo_WhenCollectionIsNotNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            var data = new List<Category>()
            {
                new Category{Id = 1, Name = ".Net"},
                new Category{Id = 2, Name = "Java"},
                new Category{Id = 3, Name = "SQL"}
            };

            categoriesRepoMock.Setup(x => x.All).Returns(data.AsQueryable);

            var dataMapper = new List<CategoryDto>()
            {
                new CategoryDto{Id = 1, Name = ".Net"},
                new CategoryDto{Id = 2, Name = "Java"},
                new CategoryDto{Id = 3, Name = "SQL"}
            };

            mapperMock.Setup(m => m.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(new List<CategoryDto>(dataMapper).AsQueryable);

            //Act
            var collection = categoriesService.GetAllCategories();

            //Assert
            mapperMock.Verify(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>()), Times.Once);

        }
    }
}
