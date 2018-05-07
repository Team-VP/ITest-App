using ITestApp.Common.Exceptions;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly IRepository<UserTest> userTests;

        public TestsService(ISaver saver, 
            IMappingProvider mapper, 
            IRepository<Test> tests, 
            IRepository<Question> questions, 
            IRepository<Answer> answers, 
            IRepository<UserTest> userTests)
        {
            this.saver = saver ?? throw new ArgumentNullException("Saver can not be null");
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests repo can not be null");
            this.answers = answers ?? throw new ArgumentNullException("Answers repo can not be null");
            this.questions = questions ?? throw new ArgumentNullException("Questions repo can not be null");
            this.userTests = userTests ?? throw new ArgumentNullException("UserTests repo can not be null"); ;
        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid id provided");
            }

            var testToDelete = tests.All.Include(q => q.Questions).ThenInclude(a => a.Answers)
                .FirstOrDefault(t => t.Id == id) ?? throw new TestNotFoundException($"No test with id {id} found!");

            var testsUser = userTests.All;

            if (testToDelete != null && !(testsUser.Any(ut => ut.TestId == testToDelete.Id)))
            {
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
            else
            {
                throw new InvalidTestException("Cannot delete test it has been taked by users.");
            }
        }

        public void Edit(TestDto testDto)
        {
            if (testDto == null)
            {
                throw new ArgumentNullException("Test dto cannot be null!");
            }

            var test = GetTestWithoutDeletedQuestionsAndAnswers(testDto.Id);
            
            if (test == null)
            {
                throw new ArgumentNullException("Test not found!");
            }

            test.Title = testDto.Title;
            test.CategoryId = testDto.CategoryId;
            test.RequiredTime = testDto.RequiredTime;
            test.StatusId = testDto.StatusId;
            var newlyCreatedQuestions = new List<Question>();

            foreach (var questionDto in testDto.Questions)
            {
                var questionEntity = test.Questions.FirstOrDefault(q => q.Id == questionDto.Id);

                if (questionEntity != null)
                {
                    if (questionDto.IsDeleted)
                    {
                        questions.Delete(questionEntity);
                    }
                    else
                    {
                        questionEntity.Content = questionDto.Content;
                        questions.Update(questionEntity);
                    }
                }
                else
                {
                    questionEntity = new Question()
                    {
                        Content = questionDto.Content
                    };

                    newlyCreatedQuestions.Add(questionEntity);
                }

                var newlyCreatedAnswers = new List<Answer>();

                foreach (var answerDto in questionDto.Answers)
                {
                    var answerEntity = questionEntity.Answers.FirstOrDefault(a => a.Id == answerDto.Id);

                    if (answerEntity != null)
                    {
                        if (answerDto.IsDeleted)
                        {
                            answers.Delete(answerEntity);
                        }
                        else
                        {
                            answerEntity.Content = answerDto.Content;
                            answerEntity.IsCorrect = answerDto.IsCorrect;
                            answers.Update(answerEntity);
                        }
                    }
                    else
                    {
                        answerEntity = new Answer()
                        {
                            Content = answerDto.Content,
                            IsCorrect = answerDto.IsCorrect
                        };

                        newlyCreatedAnswers.Add(answerEntity);
                    }
                }

                foreach(var a in newlyCreatedAnswers)
                {
                    questionEntity.Answers.Add(a);
                }
            }

            foreach (var q in newlyCreatedQuestions)
            {
                test.Questions.Add(q);
            }

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
                throw new ArgumentNullException("Test dto cannot be null!");
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
            var userTests = this.userTests.All.Where(ut => ut.TimeExpire > DateTime.Now.AddSeconds(5));

            if (test != null && !(userTests.Any(ut => ut.TestId == id && ut.SubmittedOn == null)))
            {
                test.StatusId = 2; //Draft

                tests.Update(test);
                saver.SaveChanges();
            }
            else
            {
                throw new InvalidTestException("Cannot set test status as Draft cause it been used right now by users.");
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
            var test = GetTestWithoutDeletedQuestionsAndAnswers(id);
            //var test = tests.All.Include(c => c.Category).Include(q => q.Questions).ThenInclude(a => a.Answers).Where(t => t.Id == id).FirstOrDefault();
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

        public TestDto GetRandomTestByCategory(string name, string user)
        {
            var filteredTests = tests.All.Include(c => c.Category).Include(ut => ut.UserTests)
                .Where(t => t.StatusId != 2 && t.Category.Name == name);

            var userTest = userTests.All.Include(t => t.Test).ThenInclude(c => c.Category)
                .Where(ut => ut.UserId == user && ut.TimeExpire > DateTime.Now.AddSeconds(5) 
                && ut.SubmittedOn == null && ut.Test.Category.Name == name).FirstOrDefault();
            
            if (userTest != null)
            {
                var test = tests.All.FirstOrDefault(t => t.Id == userTest.TestId);
                var dto = mapper.MapTo<TestDto>(test);
                dto.TakinStatus = "Ongoing";
                return dto;
            }

            else if(filteredTests.Count() != 0)
            {
                var curenttests = new List<TestDto>();

                foreach (var item in filteredTests)
                {
                    if (!(item.UserTests.Any(tst => tst.UserId == user)))
                    {
                        curenttests.Add(mapper.MapTo<TestDto>(item));
                    }
                }
                if (curenttests.Count > 1)
                {
                    var randoninstance = new Random();
                    int testCount = curenttests.Count();
                    int random = randoninstance.Next(testCount);

                    var test = curenttests[random];
                    return test;
                }
                else
                {
                    return curenttests.FirstOrDefault();
                }
            }
            else
            {
                return null;
            }
        }

        private Test GetTestWithoutDeletedQuestionsAndAnswers(int id)
        {
            var test = tests.All.Where(t => t.Id == id)
               .Include(t => t.Status)
               .Include(t => t.Category)
               .Include(t => t.Author)
               .FirstOrDefault();

            if (test == null)
            {
                throw new ArgumentNullException($"Test with id {id} not found!");
            }

            var testQuestions = this.questions.All.Where(q => q.TestId == test.Id).ToList();

            foreach (var question in testQuestions)
            {
                question.Answers = this.answers.All.Where(a => a.QuestionId == question.Id).ToList();
            }

            test.Questions = testQuestions.ToList();

            return test;
        }
    }
}
