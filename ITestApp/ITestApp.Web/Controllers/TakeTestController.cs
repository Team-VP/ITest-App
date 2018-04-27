using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using ITestApp.Web.Models.TakeTestViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITestApp.Web.Controllers
{
    public class TakeTestController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ITestsService tests;
        private readonly IQuestionsService questions;
        private readonly IAnswersService answers;
        private readonly IResultService resultService;
        private readonly UserManager<User> userManager;

        public TakeTestController(IResultService resultService, IMappingProvider mapper, ITestsService tests, IQuestionsService questions, IAnswersService answers, UserManager<User> userManager)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.questions = questions ?? throw new ArgumentNullException("Questions service cannot be null");
            this.answers = answers ?? throw new ArgumentNullException("Answers service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
            this.resultService = resultService ?? throw new ArgumentNullException("Result service cannot be null"); ;
        }

        [HttpGet]
        
        public async Task<IActionResult> Index(int id)
        {
            var test = this.tests.GetById(id);

            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            
            var takenTests = resultService.GetSubmitedTestByUser(userId);

            if (takenTests.Any(t => t.TestId == id && t.ExecutionTime < test.RequiredTime))
            {
                return this.RedirectToAction("All", "Dashboard");
            }

            var currentTest = new UserTestDto()
            {
                UserId = userId,
                TestId = test.Id
            };

            resultService.Add(currentTest);

            

            var model = new IndexViewModel()
            {
                StartedOn = DateTime.Now,
                UserId = user.Id,
                TestId = test.Id,
                TestName = test.Title,
                Duration = TimeSpan.FromMinutes(test.RequiredTime),
                CategoryName = test.Category.Name,
                Questions = mapper.ProjectTo<QuestionViewModel>(test.Questions.AsQueryable()).ToList()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IndexViewModel model)
        {
            model.SubmitedOn = DateTime.Now;
            var testDuration = model.SubmitedOn.Subtract(model.StartedOn);

            if (testDuration.Seconds > model.Duration.Minutes * 60)
            {
                return Ok("You are cheater. Test failed");
            }

            var testIfo = tests.GetById(model.TestId);


            var currentTest = resultService.GetStartedTest(model.TestId);
           

            int correctAnswers = 0;
            int totalQuestions = model.Questions.Count();


            foreach (var q in model.Questions)
            {
                int aId = int.Parse(q.AndswerId);

                var answer = answers.GetById(int.Parse(q.AndswerId));
                if (answer.IsCorrect)
                {
                    correctAnswers++;
                }
            }

            currentTest.Points = (float)((100.0 * correctAnswers) / totalQuestions);
            if (currentTest.Points > 80)
            {
                currentTest.IsPassed = true;
            }
            currentTest.ExecutionTime = testDuration.Minutes;

            resultService.Submit(currentTest);
            
            return this.RedirectToAction("All", "Dashboard");
        }
    }

}