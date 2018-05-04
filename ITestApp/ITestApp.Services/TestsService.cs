using ITestApp.Common.Exceptions;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
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

        public void Edit(TestDto testDto)
        {
            var test = tests.All
                .Where(t => t.Id == testDto.Id)
                .Include(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .FirstOrDefault();


            var testToEditFrom = this.mapper.MapTo<Test>(testDto);

            if (test == null)
            {
                throw new ArgumentNullException("Test not found!");
            }

            test.Title = testToEditFrom.Title;
            test.CategoryId = testToEditFrom.CategoryId;
            test.RequiredTime = testToEditFrom.RequiredTime;
            test.Questions = testToEditFrom.Questions;

            tests.Update(test);
            saver.SaveChanges();
        }

        /// <summary>
        /// Creates a new test and directly publishes it.
        /// </summary>
        /// <param name="testDto">Test dto to publish.</param>
        public void Publish(TestDto testDto)
        {
            if (testDto == null)
            {
                throw new ArgumentNullException("Test cannot be null!");
            }

            var test = mapper.MapTo<Test>(testDto);
            test.StatusId = 1; // Published
            tests.Add(test);
            saver.SaveChanges();
        }

        /// <summary>
        /// Creates a new test and saves it as draft.
        /// </summary>
        /// <param name="testDto">Test dto to save.</param>
        public void SaveAsDraft(TestDto testDto)
        {
            if (testDto == null)
            {
                throw new ArgumentNullException("Test cannot be null!");
            }

            var test = mapper.MapTo<Test>(testDto);
            test.StatusId = 2; // Draft
            tests.Add(test);
            saver.SaveChanges();
        }

        public void DisableTest(int id)
        {
            var test = tests.All.Where(t => t.Id == id && t.StatusId != 2).FirstOrDefault();
            if (test != null)
            {
                test.StatusId = 2; //Draft

                tests.Update(test);
                saver.SaveChanges();
            }

        }

        public void PublishExistingTest(int id)
        {
            var test = tests.All
                .Where(t => t.Id == id && t.StatusId != 1)
                .Include(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .FirstOrDefault();

            if (!test.Questions.Any())
            {
                throw new InvalidTestException("Cannot publish a test with no questions!");
            }
            else
            {
                foreach (var question in test.Questions)
                {
                    if (question.Answers.Count < 2)
                    {
                        throw new InvalidTestException("Cannot publish a test with a question with less than 2 answers!");
                    }
                }
            }

            if (test != null)
            {
                test.StatusId = 1; //Published

                tests.Update(test);
                saver.SaveChanges();
            }

        }

        public TestDto GetById(int id)
        {
            var test = tests.All.Where(t => t.Id == id)
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Include(t => t.Author)
                .FirstOrDefault() ?? throw new ArgumentNullException("Test not found!");

            var testQuestions = this.questions.All.Where(q => q.TestId == test.Id).ToList();

            foreach (var question in testQuestions)
            {
                question.Answers = this.answers.All.Where(a => a.QuestionId == question.Id).ToList();
            }

            test.Questions = testQuestions;

            return mapper.MapTo<TestDto>(test);
        }

        public int GetTestDurationSeconds(int id)
        {
            int seconds = tests.All.FirstOrDefault(t => t.Id == id).RequiredTime * 60;

            return seconds;
        }

        public string GetCategoryNameByTestId(int id)
        {
            var name = tests.All.
                Where(t => t.Id == id).Include(c => c.Category).
                FirstOrDefault().Category.Name;

            return name;
        }

        public int GetTestRequestedTime(int id)
        {
            int time = tests.All.FirstOrDefault(t => t.Id == id).RequiredTime;

            return time;
        }

        public string GetStatusNameByTestId(int id)
        {
            string name = tests.All
                .Where(t => t.Id == id).Include(st => st.Status)
                .FirstOrDefault().Status.Name ?? throw new ArgumentNullException("Status name cannot be null or empty");

            return name;
        }
    }
}
