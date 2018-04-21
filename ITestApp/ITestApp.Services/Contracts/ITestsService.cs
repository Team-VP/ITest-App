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

        void AddQuestionToTheTest(QuestionDto dto);

        void SaveAsDraft(TestDto test);

        void Publish(TestDto test);

        void EditTest(TestDto test);

        void DeleteTest(int id);

        void CreateNewTest(TestDto newTest);

        ICollection<TestDto> GetAllTests();

        ICollection<TestDto> GetTestByAuthorId(string id);
    }
}
