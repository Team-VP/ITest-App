using System;
using System.Collections.Generic;
using System.Text;
using ITestApp.DTO;

namespace ITestApp.Services.Contracts
{
    public interface IResultService
    {
        IEnumerable<UserTestDto> GetSubmitedTestsByUser(string id);

        void Submit(UserTestDto dto);

        void Add(UserTestDto dto);

        UserTestDto GetStartedTest(string userId, int testId);
    }
}
