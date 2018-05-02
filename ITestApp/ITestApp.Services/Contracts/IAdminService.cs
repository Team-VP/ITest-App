using System;
using System.Collections.Generic;
using System.Text;
using ITestApp.Data.Models;
using ITestApp.DTO;

namespace ITestApp.Services.Contracts
{
    public interface IAdminService
    {
        IEnumerable<UserTestDto> GetUserResults();

        IEnumerable<TestDto> GetTestsByAuthor(string id);

    }
}
