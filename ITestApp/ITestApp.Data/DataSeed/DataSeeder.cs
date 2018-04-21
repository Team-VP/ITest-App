using ITestApp.Data.Models;
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
        public static void SeedTests(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ITestAppDbContext>();
                context.Database.EnsureCreated();

                if (!context.Test.Any())
                {
                    var user = new User
                    {
                        IsDeleted = false,
                        IsAdmin = false,
                        Email = "someName@someDomain.test",
                        UserName = "Test"
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

                    var questions = new List<Question>()
                    {
                        new Question { IsDeleted = false, Content = "Question 1", Answers = answers },
                        new Question { IsDeleted = false, Content = "Question 2", Answers = answers },
                        new Question { IsDeleted = false, Content = "Question 3", Answers = answers },
                        new Question { IsDeleted = false, Content = "Question 4", Answers = answers },
                    };

                    var tests = new List<Test>
                    {
                        new Test { IsDeleted = false, Title = "Test 1", RequiredTime = 60, Author=user, Category = categoryDotNet, Status = statusPublished, Questions = questions},
                        new Test { IsDeleted = false, Title = "Test 2", RequiredTime = 50, Author=user, Category = categoryJava, Status = statusPublished, Questions = questions},
                        new Test { IsDeleted = false, Title = "Test 3", RequiredTime = 40, Author=user, Category = categorySql, Status = statusDraft, Questions = questions},
                        new Test { IsDeleted = false, Title = "Test 4", RequiredTime = 30, Author=user, Category = categoryJs, Status = statusDraft, Questions = questions}
                    };

                    context.AddRange(tests);
                    context.SaveChanges();
                }
            }
        }
    }
}
