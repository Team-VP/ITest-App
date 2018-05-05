using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bytes2you.Validation;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ITestApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Test> tests;
        private readonly IRepository<UserTest> userTests;

        public AdminService(ISaver saver, IMappingProvider mapper, IRepository<Test> tests, IRepository<UserTest> userTests)
        {
            this.saver = saver ?? throw new ArgumentNullException("Saver can not be null");
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests repo can not be null");
            this.userTests = userTests ?? throw new ArgumentNullException("Tests repo can not be null");

        }

        public IEnumerable<TestDto> GetTestsByAuthor(string id)
        {
            Guard.WhenArgument(id, "Author's Id").IsNullOrEmpty().Throw();

            var authorTests = tests.All.Where(t => t.AuthorId == id).Include(c => c.Category);

            var dto = mapper.ProjectTo<TestDto>(authorTests);

            return dto;
        }

        public IEnumerable<UserTestDto> GetUserResults()
        {
            var userResults = userTests.All.Include(t => t.Test).Include(u => u.User)
                .Where(ur => ur.TimeExpire < DateTime.Now.AddSeconds(5) || ur.SubmittedOn != null);

            var dto = mapper.ProjectTo<UserTestDto>(userResults);

            return dto;
        }
    }
}
