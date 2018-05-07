using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using ITestApp.Web.Areas.Private.Models.TakeTestViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Areas.Private.Controllers
{
    [Area("Private")]
    public class TakeTestController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ITestsService tests;
        private readonly IAnswersService answers;
        private readonly IResultService resultService;
        private readonly UserManager<User> userManager;
        private readonly IMemoryCache cache;

        public TakeTestController
            (
            IResultService resultService, 
            IMappingProvider mapper, 
            ITestsService tests, 
            IAnswersService answers, 
            UserManager<User> userManager,
            IMemoryCache memoryCache)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.answers = answers ?? throw new ArgumentNullException("Answers service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
            this.cache = memoryCache ?? throw new ArgumentNullException("MemoryCache cannot be null"); ;
            this.resultService = resultService ?? throw new ArgumentNullException("Result service cannot be null");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            string key = string.Format("TestId {0}", id);
            if (!cache.TryGetValue(key, out TestDto test))
            {
                test = this.tests.GetById(id);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                cache.Set(key, test, cacheEntryOptions);
            }
           // var test = this.tests.GetById(id);

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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IndexViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                string key = string.Format("TestId {0}", model.TestId);
                this.cache.Remove(key);

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

                int correctAnswers = this.CalculateCorrectAnswers(model.Questions);
                int totalQuestions = model.Questions.Count();

                currentTestEntity.Points = this.resultService.CalculateTestPoints(correctAnswers, totalQuestions);
                currentTestEntity.IsPassed = (currentTestEntity.Points > 80) ? true : false;

                resultService.Submit(currentTestEntity);
                this.cache.Remove("TestResults");
                TempData["Success-Message"] = "Test submited!";
            }
            else
            {
                TempData["Error-Message"] = "Test submition failed!";
            }

            return Json(Url.Action("All", "Dashboard"));

        }

        private int CalculateCorrectAnswers(ICollection<QuestionViewModel> questions)
        {
            int correctAnswers = 0;

            foreach (var q in questions)
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

            return correctAnswers;
        }
    }

}