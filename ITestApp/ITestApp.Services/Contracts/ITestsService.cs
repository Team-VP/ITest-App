using ITestApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Services.Contracts
{
    public interface ITestsService
    {
        TestDto GetById(int id);

        IEnumerable<QuestionDto> GetQuestions(int testId);

        void SaveAsDraft(TestDto test);

        void Publish(TestDto test);

        void Edit(TestDto test);

        void Delete(int id);

        void SaveToDb(TestDto test);

        TestDto CreateNew(TestDto test);

        IEnumerable<TestDto> GetAllTests();

        IEnumerable<TestDto> GetByAuthorId(string id);
    }
}
