using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Services.Contracts;
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
        private readonly UserManager<User> userManager;

        public DashboardController(IMappingProvider mapper, ITestsService tests, IResultService resultService, UserManager<User> userManager)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null");
            this.tests = tests ?? throw new ArgumentNullException("Tests service cannot be null");
            this.userManager = userManager ?? throw new ArgumentNullException("User manager cannot be null");
            this.resultService = resultService ?? throw new ArgumentNullException("Result service cannot be null");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var admin = await this.userManager.GetUserAsync(HttpContext.User);
            var adminId = admin.Id;



            return Ok("Hi from Admin area :)");
        }
    }
}