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
using ITestApp.Common.Constants;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache cache;

        public ManageTestController(IMappingProvider mapper,
            ITestsService tests,
            ICategoryService categories,
            IStatusesService statuses,
            UserManager<User> userManager,
            IMemoryCache memoryCache)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.categories = categories ?? throw new ArgumentNullException("Categories service cannot be null");
            this.statuses = statuses ?? throw new ArgumentNullException("Statuses service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
            this.cache = memoryCache ?? throw new ArgumentNullException("Cache cannot be null!");
        }

        [HttpGet]
        [Authorize]
        [Route("administration/create/new")]
        [Route("administration/create")]
        public IActionResult New()
        {
            // Look for cache key.
            if (!cache.TryGetValue("categories", out IEnumerable<CreateCategoryViewModel> allCategories))
            {
                // Key not in cache, so get data.
                var allCategoriesDto = categories.GetAllCategories();
                allCategories = mapper.ProjectTo<CreateCategoryViewModel>(allCategoriesDto).ToList();
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(300)); //5 mins

                // Save data in cache.
                cache.Set("Categories", allCategories, cacheEntryOptions);
            }

            //var allCategories = categories.GetAllCategories();
            ViewData["Categories"] = allCategories;
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("administration/create/new")]
        [Route("administration/create")]
        public IActionResult New([FromBody]CreateTestViewModel model)
        {
            bool isValid = ValidateTestModel(model);

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

            if (!cache.TryGetValue("categories", out IEnumerable<CreateCategoryViewModel> allCategories))
            {
                // Key not in cache, so get data.
                var allCategoriesDto = categories.GetAllCategories();
                allCategories = mapper.ProjectTo<CreateCategoryViewModel>(allCategoriesDto).ToList();
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(300)); //5 mins

                // Save data in cache.
                cache.Set("Categories", allCategories, cacheEntryOptions);
            }
            
            ViewData["Categories"] = allCategories;
            TempData["Error-Message"] = "Test creation failed!";

            return Json(Url.Action("Index", "Dashboard", new { area = "Administration" }));
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

            bool isValid = ValidateTestModel(model);

            if (isValid && this.ModelState.IsValid)
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

        [NonAction]
        private bool ValidateTestModel(CreateTestViewModel model)
        {
            bool isValid = true;

            if (!model.Questions.Any())
            {
                isValid = false;
            }
            else
            {
                foreach (var question in model.Questions)
                {
                    var qLength = question.ContentWithoutTags.Length;
                    if (ModelConstants.MinQuestionContentLength > qLength || qLength > ModelConstants.MaxQuestionContentLength)
                    {
                        isValid = false;
                        break;
                    }

                    if (question.Answers.Count < 2)
                    {
                        isValid = false;
                        break;
                    }
                    
                    var correctAnswers = 0;
                    foreach (var answer in question.Answers)
                    {
                        var aLength = answer.ContentWithoutTags.Length;
                        if (ModelConstants.MinAnswerContentLength > aLength || aLength > ModelConstants.MaxAnswerContentLength)
                        {
                            isValid = false;
                            break;
                        }

                        if (answer.IsCorrect)
                        {
                            correctAnswers++;

                            if (correctAnswers > 1)
                            {
                                break;
                            }
                        }
                    }

                    if (correctAnswers != 1)
                    {
                        isValid = false;
                        break;
                    }

                    if (!isValid)
                    {
                        break;
                    }
                }
            }

            return isValid;
        }
    }
}
