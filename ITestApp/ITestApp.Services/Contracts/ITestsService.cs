using ITestApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Services.Contracts
{
    public interface ITestsService
    {
        void Delete(int id);

        void Edit(TestDto test);

        void Publish(TestDto test);

        void SaveAsDraft(TestDto test);

        void DisableTest(int id);

        void PublishExistingTest(int id);

        TestDto GetById(int id);

        int GetTestDurationSeconds(int id);

        string GetCategoryNameByTestId(int id);

        int GetTestRequestedTime(int id);

        string GetStatusNameByTestId(int id);
    }
}
