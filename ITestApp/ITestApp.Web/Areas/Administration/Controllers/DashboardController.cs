using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Common.Exceptions;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using ITestApp.Web.Areas.Administration.Models.DashboardViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ITestApp.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    //[Route("Admin/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ITestsService tests;
        private readonly IResultService resultService;
        private readonly IAdminService adminService;
        private readonly IMemoryCache cache;
        private readonly UserManager<User> userManager;

        public DashboardController(IAdminService adminService, 
            IMappingProvider mapper, 
            ITestsService tests, 
            IResultService resultService, 
            UserManager<User> userManager, 
            IMemoryCache memoryCache)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
            this.resultService = resultService ?? throw new ArgumentNullException("Result service cannot be null");
            this.adminService = adminService ?? throw new ArgumentNullException("Admin service can not be null.");
            this.cache = memoryCache ?? throw new ArgumentNullException("MemoryCache cannot be null!");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var admin = await this.userManager.GetUserAsync(HttpContext.User);
            var adminId = admin.Id;

            if (!cache.TryGetValue(adminId, out IEnumerable<TestDto> authorTests))
            {
                authorTests = adminService.GetTestsByAuthor(adminId).ToList();
               
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(20)); 
                
                cache.Set(adminId, authorTests, cacheEntryOptions);
            }

            if (!cache.TryGetValue("TestResults", out IEnumerable<UserTestDto> userResults))
            {
                userResults = adminService.GetUserResults().ToList();
                
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(20)); ;

                cache.Set("TestResults", userResults, cacheEntryOptions);
            }

            // Model creating
            var userResultsList = new List<UserTestViewModel>();
            var authorTestsList = new List<TestViewModel>();

            // UserTestViewModels creating
            foreach (var userResult in userResults)
            {
                var currentModel = new UserTestViewModel()
                {
                    TestName = userResult.Test.Title,
                    UserName = userResult.User.UserName,
                    Category = tests.GetCategoryNameByTestId(userResult.TestId),
                    RequestedTime = tests.GetTestRequestedTime(userResult.TestId),
                    ExecutionTime = (int)userResult.ExecutionTime.TotalMinutes,
                    Result = (userResult.IsPassed) ? "Passed" : "Failed"
                };

                userResultsList.Add(currentModel);
            }

            // TestViewModels creating
            foreach (var authorTest in authorTests)
            {
                var currentModel = new TestViewModel()
                {
                    Id = authorTest.Id.ToString(),
                    TestName = authorTest.Title,
                    CategoryName = authorTest.Category.Name,
                    Status = tests.GetStatusNameByTestId(authorTest.Id),
                    CreatedOn = authorTest.CreatedOn
                };

                authorTestsList.Add(currentModel);
            }

            // IndexViewModel creating
            var model = new IndexViewModel()
            {
                AdminName = admin.UserName,
                UserResults = userResultsList,
                Tests = authorTestsList
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Disable(int id)
        {
            try
            {
                tests.DisableTest(id);
                TempData["Success-Message"] = "You successfully set the test status as Draft!";
            }
            catch (InvalidTestException ex)
            {
                TempData["Error-Message"] = string.Format("Disable test failed! {0}", ex.Message);
            }

            return Json(Url.Action("Index", "Dashboard", new { area = "Administration" }));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Publish(int id)
        {
            try
            {
                tests.PublishExistingTest(id);
                TempData["Success-Message"] = "You successfully published a test!";
            }
            catch (InvalidTestException ex)
            {
                TempData["Error-Message"] = string.Format("Publishing test failed! {0}", ex.Message);
            }

            return Json(Url.Action("Index", "Dashboard", new { area = "Administration" }));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                tests.Delete(id);
                TempData["Success-Message"] = "You successfully deleted a test!";

            }
            catch (InvalidTestException ex)
            {
                TempData["Error-Message"] = string.Format("Deleting test failed! {0}", ex.Message);
            }

            return Json(Url.Action("Index", "Dashboard", new { area = "Administration" }));
        }
    }
}