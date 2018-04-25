using System.Collections.Generic;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace ITestApp.Services
{
    public class TestsService : ITestsService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Test> tests;
        private readonly IRepository<Question> questions;
        private readonly IRepository<Answer> answers;
        private readonly IRepository<Category> categories;

        public TestsService(ISaver saver, IMappingProvider mapper, IRepository<Test> tests, IRepository<Question> questions, IRepository<Answer> answers, IRepository<Category> categories)
        {
            this.saver = saver ?? throw new ArgumentNullException("Saver can not be null");
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null"); ;
            this.tests = tests ?? throw new ArgumentNullException("Tests repo can not be null");
            this.answers = answers ?? throw new ArgumentNullException("Answers repo can not be null");
            this.questions = questions ?? throw new ArgumentNullException("Questions repo can not be null");
            this.categories = categories ?? throw new ArgumentNullException("Categories repo can not be null");
        }

        public void DeleteTest(int id)
        {
            var testToDelete = tests.All.Include(q => q.Questions).ThenInclude(a => a.Answers)
                .FirstOrDefault(t => t.Id == id) ?? throw new ArgumentNullException("Test can not be null");

            tests.Delete(testToDelete);

            foreach (var question in testToDelete.Questions)
            {
                this.questions.Delete(question);
                foreach (var answer in question.Answers)
                {
                    this.answers.Delete(answer);
                }
            }

            saver.SaveChanges();
        }

        public void EditTest(TestDto test)
        {
            Test testToEdit = tests.All.Where(t => t.Id == test.Id)
                .Include(q => q.Questions)
                .ThenInclude(a => a.Answers).FirstOrDefault() ?? throw new ArgumentNullException("Test can not be null.");

            testToEdit.Title = test.Title;
            testToEdit.CategoryId = test.CategoryId;
            testToEdit.RequiredTime = test.RequiredTime;

            tests.Update(testToEdit);
            saver.SaveChanges();
        }

        public TestDto GetById(int id)
        {
            Test testWithId = tests.All.Where(t => t.Id == id)
                .Include(q => q.Questions).ThenInclude(a => a.Answers)
                .FirstOrDefault() ?? throw new ArgumentNullException("Test can not be null");
            //var testQuestions = this.questions.All.Where(q => q.TestId == testWithId.Id);

            //foreach (var question in testQuestions)
            //{

            //}
            return mapper.MapTo<TestDto>(testWithId);
        }

        public IEnumerable<QuestionDto> GetQuestions(int testId)
        {
            var curTest = tests.All
                .Where(t => t.Id == testId)
                .Select(q => q.Questions) ?? throw new ArgumentNullException("Collection of Questions can not be null");

            var result = mapper.ProjectTo<QuestionDto>(curTest);

            return result;
        }

        public void Publish(TestDto test) //The test can be new and directry published or can be existing at DB and only changing status. 
        {
            var testToFind = tests.All.FirstOrDefault(t => t.Id == test.Id) ?? throw new ArgumentNullException("Test can not be null");
            if (testToFind == null)
            {
                test.StatusId = 2; //Publish

                test.Status.Name = "Publish";
                var newPublishedTest = mapper.MapTo<Test>(test);
                tests.Add(newPublishedTest);
            }
            else
            {
                testToFind.StatusId = 2;
                testToFind.Status.Name = "Publish";
                tests.Update(testToFind);
            }

            saver.SaveChanges();
        }

        public void SaveAsDraft(TestDto test)
        {
            test.StatusId = 1; //Draft
            tests.Add(mapper.MapTo<Test>(test));
            saver.SaveChanges();
            
        }

        public IEnumerable<TestDto> GetTestByAuthorId(string id)
        {
            var currentTests = tests.All.
                Where(test => test.AuthorId == id)
                .Include(q => q.Questions).ThenInclude(a => a.Answers);

            return mapper.ProjectTo<TestDto>(currentTests);
        }
        public IEnumerable<TestDto> GetAllTests()
        {
            var allTests = tests.All
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers);

            return mapper.ProjectTo<TestDto>(allTests);
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var allCategories = categories.All
                .Include(c => c.Tests)
                .ThenInclude(t => t.Questions)
                .ThenInclude(q => q.Answers);

            return mapper.ProjectTo<CategoryDto>(allCategories);
        }

        public void CreateNewTest(TestDto newTest)
        {
            var newTestEntity = mapper.MapTo<Test>(newTest) ?? throw new ArgumentNullException("Test Can Not Be Null");
            tests.Add(newTestEntity);
            saver.SaveChanges();
        }
    }
}
