using System.Collections.Generic;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
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

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(TestDto test)
        {
            throw new System.NotImplementedException();
        }

        public TestDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<QuestionDto> GetQuestions(int testId)
        {
            throw new System.NotImplementedException();
        }

        public void Publish(TestDto test)
        {
            throw new System.NotImplementedException();
        }

        public void SaveAsDraft(TestDto test)
        {
            throw new System.NotImplementedException();
        }
    }
}
