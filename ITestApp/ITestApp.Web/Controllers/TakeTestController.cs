using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Services.Contracts;
using ITestApp.Web.Models.TakeTestViewModels;
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
        private readonly UserManager<User> userManager;

        public TakeTestController(IMappingProvider mapper, ITestsService tests, IQuestionsService questions, IAnswersService answers, UserManager<User> userManager)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.questions = questions ?? throw new ArgumentNullException("Questions service cannot be null");
            this.answers = answers ?? throw new ArgumentNullException("Answers service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var test = this.tests.GetById(id);
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var mod = new IndexViewModel();

            var model = new IndexViewModel()
            {
                UserId = user.Id,
                TestId = test.Id,
                TestName = test.Title,
                Duration = TimeSpan.FromMinutes(test.RequiredTime),
                CategoryName = test.Category.Name,
                Questions = mapper.ProjectTo<QuestionViewModel>(test.Questions.AsQueryable()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index (IndexViewModel model)
        {
            var c = model;

            return Ok();
        }
    }

}