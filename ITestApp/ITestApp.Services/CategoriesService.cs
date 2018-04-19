using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Category> categories;

        public CategoriesService(ISaver saver, IMappingProvider mapper, IRepository<Category> categories)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.categories = categories;
        }
    }
}
