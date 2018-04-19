using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;

namespace ITestApp.Services
{
    public class TestsService : ITestsService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Test> tests;

        public TestsService(ISaver saver, IMappingProvider mapper, IRepository<Test> tests)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.tests = tests;
        }
    }
}
