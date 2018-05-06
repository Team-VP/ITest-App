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
    public class GetCategoryByName_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenParameterIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(()=> categoriesService.GetCategoryByName(null));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenParameterIsEmpty()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentException>(() => categoriesService.GetCategoryByName(string.Empty));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenCollectionOfCategoriesIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            categoriesRepoMock.Setup(x => x.All).Returns((IQueryable<Category>)null);

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            //Act&Assert
            Assert.ThrowsException<ArgumentNullException>(() => categoriesService.GetCategoryByName(".Net"));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenCollectionOfCategoriesDtoIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

            var data = new List<Category>()
            {
                new Category{Id = 1, Name = ".Net"},
                new Category{Id = 2, Name = "Java"},
                new Category{Id = 3, Name = "SQL"}
            };

            categoriesRepoMock.Setup(x => x.All).Returns(data.AsQueryable);


            mapperMock.Setup(m => m.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns((IQueryable<CategoryDto>)null);

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            //Act
            Assert.ThrowsException<ArgumentNullException>(() => categoriesService.GetCategoryByName(".Net"));
        }

        [TestMethod]
        public void ReturnCategoryDto_WhenAllParametersAndCollectionsExists()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

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
            mapperMock.Setup(m => m.MapTo<CategoryDto>(It.IsAny<Category>())).Returns(dataMapper[0]);

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            //Act
            var category = categoriesService.GetCategoryByName(".Net");

            //Assert
            Assert.IsNotNull(category);
            Assert.IsInstanceOfType(category, typeof(CategoryDto));
        }

        [TestMethod]
        public void InvokeMapperMethodMapTo_WhenAllParametersAndCollectionsExists()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var categoriesRepoMock = new Mock<IRepository<Category>>();

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
            mapperMock.Setup(m => m.MapTo<CategoryDto>(It.IsAny<Category>())).Returns(dataMapper[0]);

            var categoriesService = new CategoriesService(saverMock.Object, mapperMock.Object, categoriesRepoMock.Object);

            var category = categoriesService.GetCategoryByName(".Net");
            //Act
            mapperMock.Verify(m => m.MapTo<CategoryDto>(It.IsAny<Category>()), Times.Once);
        }
    }
}
