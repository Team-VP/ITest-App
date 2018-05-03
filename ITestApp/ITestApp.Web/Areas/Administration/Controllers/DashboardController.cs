using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Services.Contracts;
using ITestApp.Web.Areas.Administration.Models.DashboardViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        private readonly UserManager<User> userManager;

        public DashboardController(IAdminService adminService, IMappingProvider mapper, ITestsService tests, IResultService resultService, UserManager<User> userManager)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
            this.resultService = resultService ?? throw new ArgumentNullException("Result service cannot be null");
            this.adminService = adminService ?? throw new ArgumentNullException("Admin service can not be null.");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var admin = await this.userManager.GetUserAsync(HttpContext.User);
            var adminId = admin.Id;

            var userResults = adminService.GetUserResults();
            var authorTests = adminService.GetTestsByAuthor(admin.UserName);
            //Model creating
            var userResultsList = new List<UserTestViewModel>();
            var authorTestsList = new List<TestViewModel>();
            //UserTestViewModels creating
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
            //TestViewModels creating
            foreach (var authorTest in authorTests)
            {
                var cur = new TestViewModel()
                {
                    TestName = authorTest.Title,
                    CategoryName = authorTest.Category.Name
                };
                authorTestsList.Add(cur);
            }
            //IndexViewModel creating
            var model = new IndexViewModel()
            {
                AdminName = admin.UserName,
                UserResults = userResultsList,
                Tests = authorTestsList
            };

            return View(model);
        }
    }
}