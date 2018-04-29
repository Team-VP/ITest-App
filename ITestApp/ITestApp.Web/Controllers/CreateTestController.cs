using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using ITestApp.Web.Models.DashboardViewModels;
using ITestApp.Web.Models.CreateTestViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Controllers
{
    public class CreateTestController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ITestsService tests;
        private readonly ICategoryService categories;
        private readonly IQuestionsService questions;
        private readonly IAnswersService answers;
        private readonly UserManager<User> userManager;

        public CreateTestController(IMappingProvider mapper, ITestsService tests, ICategoryService categories, IQuestionsService questions, IAnswersService answers, UserManager<User> userManager)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.categories = categories ?? throw new ArgumentNullException("Categories service cannot be null");
            this.questions = questions ?? throw new ArgumentNullException("Questions service cannot be null");
            this.answers = answers ?? throw new ArgumentNullException("Answers service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
        }

        [HttpGet]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public IActionResult New()
        {
            var allCategories = categories.GetAllCategories();
            ViewData["Categories"] = mapper.ProjectTo<CreateCategoryViewModel>(allCategories).ToList();
            return View();
        }

        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public IActionResult New([FromBody] CreateTestViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var dto = this.mapper.MapTo<TestDto>(model);
                dto.AuthorId = this.userManager.GetUserId(this.HttpContext.User);
                dto.CategoryId = this.categories.GetCategoryByName(model.Category).Id;

                this.tests.Publish(dto);
                
                return this.RedirectToAction("All", "Dashboard");
            }

            var allCategories = categories.GetAllCategories();
            ViewData["Categories"] = mapper.ProjectTo<CreateCategoryViewModel>(allCategories).ToList();

            return View(model);
        }

        [HttpGet]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public IActionResult AddQuestion(CreateQuestionViewModel model)
        {
            return PartialView("_CreateQuestionPartialView", model);
        }

        [HttpGet]
        public IActionResult AddAnswer(CreateAnswerViewModel model)
        {
            return PartialView("_CreateAnswerPartialView", model);
        }
        
        [HttpPost]
        public JsonResult PostJson([FromBody] CreateTestViewModel model)
        {
            return Json(new
            {
                state = 0,
                msg = string.Empty
            });
        }
    }
}
