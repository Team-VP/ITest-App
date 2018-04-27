using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;

namespace ITestApp.Services
{
    public class ResultService : IResultService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<UserTest> userTests;

        public ResultService(ISaver saver, IMappingProvider mapper, IRepository<UserTest> userTests)
        {
            this.saver = saver ?? throw new ArgumentNullException("Saver can not be null");
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null"); ;
            this.userTests = userTests ?? throw new ArgumentNullException("Tests repo can not be null");
        }

        public IEnumerable<UserTestDto> GetSubmitedTestByUser (string id)
        {
            var tests = userTests.All.Where(t => t.UserId == id);

            var dto = mapper.ProjectTo<UserTestDto>(tests);

            return dto;
        }

        public void Submit(UserTestDto dto)
        {
            var testToUpdate = userTests.All.Where(ut => ut.UserId == dto.UserId && ut.TestId == dto.TestId).FirstOrDefault();

            testToUpdate.IsPassed = dto.IsPassed;
            testToUpdate.Points = dto.Points;
            testToUpdate.ExecutionTime = dto.ExecutionTime;

            userTests.Update(testToUpdate);
            saver.SaveChanges();


            
        }

        public void Add(UserTestDto dto)
        {

            var test = mapper.MapTo<UserTest>(dto);
            userTests.Add(test);

            saver.SaveChanges();
        }

        public UserTestDto GetStartedTest(int id)
        {
            var test = userTests.All.Where(t => t.TestId == id).FirstOrDefault();

            var dto = mapper.MapTo<UserTestDto>(test);

            return dto;
        }
    }
}
