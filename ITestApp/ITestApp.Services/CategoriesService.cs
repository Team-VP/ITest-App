using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITestApp.Services
{
    public class CategoriesService : ICategoryService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Category> categories;

        public CategoriesService(ISaver saver, IMappingProvider mapper, IRepository<Category> categories)
        {
            this.saver = saver ?? throw new ArgumentNullException("Saver cannot be null");
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper cannot be null");
            this.categories = categories ?? throw new ArgumentNullException("Categorues repository cannot be null");
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var allCategories = categories.All
                ?? throw new ArgumentNullException("Collection of answers can not be Null.");

            IQueryable<CategoryDto> categoriesDto = mapper.ProjectTo<CategoryDto>(allCategories)
                ?? throw new ArgumentNullException("Collection of categories cannot be null.");

            return categoriesDto;
        }
    }
}
