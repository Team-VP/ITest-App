using ITestApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();

        IEnumerable<CategoryDto> GetAll();

        IEnumerable<TestDto> GetCategoryTests(int categoryId);

        CategoryDto GetCategoryByName(string name);
    }
}
