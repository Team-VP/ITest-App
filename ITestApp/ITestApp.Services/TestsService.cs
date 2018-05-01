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

        public void Delete(int id)
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

        public void Edit(TestDto test)
        {
            Test testToEdit = tests.All.Where(t => t.Id == test.Id)
                .Include(q => q.Questions)
                .ThenInclude(a => a.Answers).FirstOrDefault() ?? throw new ArgumentNullException("Test can not be null.");

            testToEdit.Title = test.Title;
            //testToEdit.CategoryId = test.CategoryId;
            testToEdit.RequiredTime = test.RequiredTime;

            tests.Update(testToEdit);
            saver.SaveChanges();
        }

        public IEnumerable<QuestionDto> GetQuestions(int testId)
        {
            var currTests = tests.All
                .Where(t => t.Id == testId)
                .Select(q => q.Questions) ?? throw new ArgumentNullException("Collection of Questions can not be null");

            var result = mapper.ProjectTo<QuestionDto>(currTests);

            return result;
        }

        public void Publish(TestDto test) 
        {
            if (test == null)
            {
                throw new ArgumentNullException("Test cannot be null!");
            }

            var testToFind = tests.All.FirstOrDefault(t => t.Id == test.Id);

            if (testToFind == null) //The test can be new and directry published or can be existing in the DB and only needs to change status. 
            {
                //test.StatusId = 1; //Publish
                var newPublishedTest = mapper.MapTo<Test>(test);
                tests.Add(newPublishedTest);
            }
            else
            {
                testToFind.StatusId = 2; //Draft
                tests.Update(testToFind);
            }

            saver.SaveChanges();
        }

        public void SaveAsDraft(TestDto test)
        {
            test.StatusId = 2; //Draft
            tests.Add(mapper.MapTo<Test>(test));
            saver.SaveChanges();
            
        }

        public TestDto GetById(int id)
        {
            Test testWithId = tests.All.Where(t => t.Id == id)
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Include(t => t.Author)
                .Include(q => q.Questions)
                    .ThenInclude(a => a.Answers)
                .FirstOrDefault() ?? throw new ArgumentNullException("Test can not be null");
            return mapper.MapTo<TestDto>(testWithId);
        }

        public IEnumerable<TestDto> GetByAuthorId(string id)
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

        /// <summary>
        /// Saves a newly created test to the database.
        /// </summary>
        /// <param name="test">DTO test to be saved.</param>
        public void SaveToDb(TestDto test)
        {
            var newTestEntity = mapper.MapTo<Test>(test) ?? throw new ArgumentNullException("Test Can Not Be Null");
            tests.Add(newTestEntity);
            saver.SaveChanges();
        }

        /// <summary>
        /// Gets a new test, converts it to DB entity, saves it to the database and returns the newly created entity test as DTO.
        /// </summary>
        /// <param name="test">DTO test to be saved.</param>
        /// <returns>DTO test with id from the database.</returns>
        public TestDto CreateNew(TestDto test)
        {
            var newTestEntity = mapper.MapTo<Test>(test) ?? throw new ArgumentNullException("Test Can Not Be Null");
            tests.Add(newTestEntity);
            saver.SaveChanges();

            return GetById(newTestEntity.Id);
        }

        public int GetTestDuratonSeconds(int id)
        { 
            int seconds = tests.All.FirstOrDefault(t => t.Id == id).RequiredTime * 60;

            return seconds;
        }
    }
}
