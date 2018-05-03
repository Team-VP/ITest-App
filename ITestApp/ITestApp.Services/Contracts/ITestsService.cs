using ITestApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Services.Contracts
{
    public interface ITestsService
    {
        void DisableTest(int id);

        void PublishExistingTest(int id);

        void AddQuestionToTheTest(QuestionDto dto);

        void SaveAsDraft(TestDto test);

        void Publish(TestDto test);

        void Edit(TestDto test);

        void Delete(int id);

        void SaveToDb(TestDto test);

        int GetTestDurationSeconds(int id);

        int GetTestRequestedTime(int id);

        string GetCategoryNameByTestId(int id);

        string GetStatusNameByTestId(int id);

        TestDto GetById(int id);

        TestDto CreateNew(TestDto test);

        IEnumerable<QuestionDto> GetQuestions(int testId);

        IEnumerable<TestDto> GetAllTests();

        IEnumerable<TestDto> GetByAuthorId(string id);

        
    }
}
