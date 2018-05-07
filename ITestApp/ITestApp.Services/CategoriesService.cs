using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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

            return mapper.ProjectTo<CategoryDto>(allCategories);
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var allCategories = categories.All
                .Include(c => c.Tests).ThenInclude(t => t.Status)
                .Include(c => c.Tests).ThenInclude(t => t.Category)
                .Include(c => c.Tests).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers);


            return mapper.ProjectTo<CategoryDto>(allCategories);
        }

        public IEnumerable<TestDto> GetCategoryTests(int categoryId)
        {
            var testsInCategory = categories.All
                .Where(c => c.Id == categoryId)
                .Include(c => c.Tests) ?? throw new ArgumentNullException("Collection of tests in category is null");

            return mapper.ProjectTo<TestDto>(testsInCategory);
        }

        public CategoryDto GetCategoryByName(string name)
        {
            var category = categories.All.Where(c => c.Name == name).FirstOrDefault() ?? throw new ArgumentNullException("Category not found!");

            return mapper.MapTo<CategoryDto>(category);
        }
    }
}
