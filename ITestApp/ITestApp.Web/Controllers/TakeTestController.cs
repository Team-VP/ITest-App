using System;
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
            this.resultService = resultService ?? throw new ArgumentNullException("Result service cannot be null");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            var test = this.tests.GetById(id);

            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            var takenTest = resultService.GetStartedTest(userId, id);

            if (takenTest != null)
            {
                if (takenTest.SubmittedOn != null)
                {
                    return this.RedirectToAction("All", "Dashboard");
                }
                if (takenTest.TimeExpire < DateTime.Now)
                {
                    return this.RedirectToAction("All", "Dashboard");
                }

            }
            else if (takenTest == null)
            {
                takenTest = new UserTestDto()
                {
                    UserId = userId,
                    TestId = test.Id,
                    StartOn = DateTime.Now,
                    TimeExpire = DateTime.Now.AddMinutes(test.RequiredTime).AddSeconds(5)

                };
                resultService.Add(takenTest);
            }

            var model = new IndexViewModel()
            {
                StartedOn = takenTest.StartOn,
                UserId = user.Id,
                TestId = test.Id,
                TestName = test.Title,
                Duration = TimeSpan.FromMinutes(test.RequiredTime),
                CategoryName = test.Category.Name,
                TimeLeft = TimeSpan.FromMinutes(test.RequiredTime) - (DateTime.Now.Subtract(takenTest.StartOn)),
                Questions = mapper.ProjectTo<QuestionViewModel>(test.Questions.AsQueryable()).ToList(),
            };

            return View(model);
        }
        //TODO: Refactor make some extra methods 
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IndexViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.SubmitedOn = DateTime.Now;
                var testDuration = model.SubmitedOn.Subtract(model.StartedOn);

                var testRequiredTimeSeconds = tests.GetTestDurationSeconds(model.TestId);

                if (testRequiredTimeSeconds < testDuration.TotalSeconds - 5)
                {
                    TempData["Error-Message"] = "Test submition failed, please do not turn off JS!";
                    return Json(Url.Action("All", "Dashboard"));
                }

                var currentTestEntity = resultService.GetStartedTest(model.UserId, model.TestId);
                currentTestEntity.SubmittedOn = model.SubmitedOn;
                currentTestEntity.ExecutionTime = testDuration;

                int correctAnswers = 0;
                int totalQuestions = model.Questions.Count();

                foreach (var q in model.Questions)
                {
                    if (q.AndswerId != null)
                    {
                        var answer = answers.GetById(int.Parse(q.AndswerId));
                        if (answer.IsCorrect)
                        {
                            correctAnswers++;
                        }
                    }
                }

                currentTestEntity.Points = this.resultService.CalculateTestPoints(correctAnswers, totalQuestions);

                currentTestEntity.IsPassed = (currentTestEntity.Points > 80) ? true : false;

                resultService.Submit(currentTestEntity);
                TempData["Success-Message"] = "Test submited!";
            }
            else
            {
                TempData["Error-Message"] = "Test submition failed!";
            }

            return Json(Url.Action("All", "Dashboard"));

        }
    }

}