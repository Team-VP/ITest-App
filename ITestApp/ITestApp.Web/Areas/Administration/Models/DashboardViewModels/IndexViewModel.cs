using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Data.Models;

namespace ITestApp.Web.Areas.Administration.Models.DashboardViewModels
{
    public class IndexViewModel
    {
        public IList <Test> Tests { get; set; }

        public IList<UserTest> UserResults { get; set; }
    }
}
