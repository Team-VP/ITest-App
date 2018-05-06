using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using ITestApp.Web.Areas.Administration.Models.MangeTestsViewModels;

namespace ITestApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    public class ManageTestController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ITestsService tests;
        private readonly ICategoryService categories;
        private readonly IStatusesService statuses;
        private readonly UserManager<User> userManager;

        public ManageTestController(IMappingProvider mapper,
            ITestsService tests,
            ICategoryService categories,
            IStatusesService statuses,
            UserManager<User> userManager)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.categories = categories ?? throw new ArgumentNullException("Categories service cannot be null");
            this.statuses = statuses ?? throw new ArgumentNullException("Statuses service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
        }

        [HttpGet]
        [Authorize]
        [Route("administration/create/new")]
        [Route("administration/create")]
        public IActionResult New()
        {
            var allCategories = categories.GetAllCategories();
            ViewData["Categories"] = mapper.ProjectTo<CreateCategoryViewModel>(allCategories).ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("administration/create/new")]
        [Route("administration/create")]
        public IActionResult New([FromBody]CreateTestViewModel model)
        {
            bool isValid = true;

            if (model.Status == "Published")
            {
                if (!model.Questions.Any())
                {
                    isValid = false;
                }

                foreach (var question in model.Questions)
                {
                    if (question.Answers.Count < 2)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid && this.ModelState.IsValid)
            {
                var dto = this.mapper.MapTo<TestDto>(model);
                dto.AuthorId = this.userManager.GetUserId(this.HttpContext.User);
                dto.CategoryId = this.categories.GetCategoryByName(model.Category).Id;
                //dto.StatusId = this.statuses.GetStatusByName(model.Status).Id;

                if (model.Status == "Published")
                {
                    TempData["Success-Message"] = "You successfully published a new test!";
                    this.tests.Publish(dto);
                }
                else if (model.Status == "Draft")
                {
                    TempData["Success-Message"] = "You successfully created a new test!";
                    this.tests.SaveAsDraft(dto);
                }
                
                return Json(Url.Action("Index", "Dashboard", new { area = "Administration" }));
            }

            var allCategories = categories.GetAllCategories();
            ViewData["Categories"] = mapper.ProjectTo<CreateCategoryViewModel>(allCategories).ToList();
            TempData["Error-Message"] = "Test publish failed!";

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddQuestion()
        {
            return PartialView("_CreateQuestionPartialView");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddAnswer()
        {
            return PartialView("_CreateAnswerPartialView");
        }

        [HttpGet("administration/edit/{id}")]
        [Authorize]
        public IActionResult Edit(int id)
        {
            TestDto testToEdit;

            try
            {
                testToEdit = this.tests.GetById(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }

            var testVm = this.mapper.MapTo<CreateTestViewModel>(testToEdit);
            var allCategories = categories.GetAllCategories();
            ViewData["Categories"] = mapper.ProjectTo<CreateCategoryViewModel>(allCategories).ToList();

            return View(testVm);
        }

        [HttpPost("administration/edit/{id}")]
        [Authorize]
        public IActionResult Edit([FromBody]CreateTestViewModel model, int id)
        {
            model.Id = id;

            //TODO validate answer and question count

            if (this.ModelState.IsValid)
            {
                var dto = this.mapper.MapTo<TestDto>(model);
                dto.AuthorId = this.userManager.GetUserId(this.HttpContext.User);
                dto.CategoryId = this.categories.GetCategoryByName(model.Category).Id;
                dto.StatusId = this.statuses.GetStatusByName(model.Status).Id;

                TempData["Success-Message"] = "You successfully editted the test!";
                this.tests.Edit(dto);

                return Json(Url.Action("Index", "Dashboard", new { area = "Administration" }));
            }

            var allCategories = categories.GetAllCategories();
            ViewData["Categories"] = mapper.ProjectTo<CreateCategoryViewModel>(allCategories).ToList();
            TempData["Error-Message"] = "Test editting failed!";

            return View(model);
        }
    }
}
