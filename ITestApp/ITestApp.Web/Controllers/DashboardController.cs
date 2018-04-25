using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Services.Contracts;
using ITestApp.Web.Models.DashboardViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly ITestsService tests;
        private readonly IQuestionsService questions;
        private readonly IAnswersService answers;
        private readonly UserManager<User> userManager;

        public DashboardController(IMappingProvider mapper, ITestsService tests, IQuestionsService questions, IAnswersService answers, UserManager<User> userManager)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.questions = questions ?? throw new ArgumentNullException("Questions service cannot be null");
            this.answers = answers ?? throw new ArgumentNullException("Answers service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
        }

        public IActionResult All()
        {
            var categories = this.tests.GetAllCategories();

            var model = new DashboardViewModel()
            {
                Categories = this.mapper.ProjectTo<CategoryViewModel>(categories).ToList()
            };

            return View(model);
        }

        public  IActionResult BeginTest(int id)
        {
            var testTobegin = this.tests.GetById(id);

            var model = mapper.MapTo<TestViewModel>(testTobegin);

            return View(model);
            
        }
    }
}
