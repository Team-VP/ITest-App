using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITestApp.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    //[Route("Admin/[controller]")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hi from Admin area :)");
        }
    }
}