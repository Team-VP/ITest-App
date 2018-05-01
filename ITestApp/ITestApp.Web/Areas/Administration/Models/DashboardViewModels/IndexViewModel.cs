using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Data.Models;

namespace ITestApp.Web.Areas.Administration.Models.DashboardViewModels
{
    public class IndexViewModel
    {
        public string AdminName { get; set; }

        public IList<TestViewModel> Tests { get; set; }

        public IList<UserTestViewModel> UserResults { get; set; }
    }
}
