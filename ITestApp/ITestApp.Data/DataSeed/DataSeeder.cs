using ITestApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITestApp.Data.DataSeed
{
    public class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ITestAppDbContext>();
                //context.Database.EnsureCreated();

               
                if (!context.Test.Any())
                {
                    var user = new User
                    {
                        IsDeleted = false,
                        IsAdmin = false,
                        Email = "someName@someDomain.test",
                        UserName = "Test",
                        
                    };

                    var statusPublished = new Status { IsDeleted = false, Name = "Published" };
                    var statusDraft = new Status { IsDeleted = false, Name = "Draft" };

                    var categoryDotNet = new Category { IsDeleted = false, Name = ".Net" };
                    var categoryJava = new Category { IsDeleted = false, Name = "Java" };
                    var categorySql = new Category { IsDeleted = false, Name = "SQL" };
                    var categoryJs = new Category { IsDeleted = false, Name = "JavaScript" };

                    var answers = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers2 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers3 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers4 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers5 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers6 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers7 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers8 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers9 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers10 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers11 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers12 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers13 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers14 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers15 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var answers16 = new List<Answer>()
                    {
                        new Answer { IsDeleted = false, Content = "Answer 1", IsCorrect = true },
                        new Answer { IsDeleted = false, Content = "Answer 2", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 3", IsCorrect = false },
                        new Answer { IsDeleted = false, Content = "Answer 4", IsCorrect = false }
                    };

                    var questions = new List<Question>()
                    {
                        new Question { IsDeleted = false, Content = "Question 1", Answers = answers },
                        new Question { IsDeleted = false, Content = "Question 2", Answers = answers2 },
                        new Question { IsDeleted = false, Content = "Question 3", Answers = answers3 },
                        new Question { IsDeleted = false, Content = "Question 4", Answers = answers4 },
                    };

                    var questions2 = new List<Question>()
                    {
                        new Question { IsDeleted = false, Content = "Question 1", Answers = answers5 },
                        new Question { IsDeleted = false, Content = "Question 2", Answers = answers6 },
                        new Question { IsDeleted = false, Content = "Question 3", Answers = answers7 },
                        new Question { IsDeleted = false, Content = "Question 4", Answers = answers8 },
                    };

                    var questions3 = new List<Question>()
                    {
                        new Question { IsDeleted = false, Content = "Question 1", Answers = answers9 },
                        new Question { IsDeleted = false, Content = "Question 2", Answers = answers10 },
                        new Question { IsDeleted = false, Content = "Question 3", Answers = answers11 },
                        new Question { IsDeleted = false, Content = "Question 4", Answers = answers12 },
                    };

                    var questions4 = new List<Question>()
                    {
                        new Question { IsDeleted = false, Content = "Question 1", Answers = answers13 },
                        new Question { IsDeleted = false, Content = "Question 2", Answers = answers14 },
                        new Question { IsDeleted = false, Content = "Question 3", Answers = answers15 },
                        new Question { IsDeleted = false, Content = "Question 4", Answers = answers16 },
                    };

                    var tests = new List<Test>
                    {
                        new Test { IsDeleted = false, Title = "Test 1", RequiredTime = 60, Author=user, Category = categoryDotNet, Status = statusPublished, Questions = questions},
                        new Test { IsDeleted = false, Title = "Test 2", RequiredTime = 50, Author=user, Category = categoryJava, Status = statusPublished, Questions = questions2},
                        new Test { IsDeleted = false, Title = "Test 3", RequiredTime = 40, Author=user, Category = categorySql, Status = statusDraft, Questions = questions3},
                        new Test { IsDeleted = false, Title = "Test 4", RequiredTime = 30, Author=user, Category = categoryJs, Status = statusDraft, Questions = questions4}
                    };

                    context.AddRange(tests);
                    context.SaveChanges();
                }
            }
        }
    }
}
