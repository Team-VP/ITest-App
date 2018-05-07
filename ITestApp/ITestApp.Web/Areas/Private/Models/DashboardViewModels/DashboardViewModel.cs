using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Areas.Private.Models.DashboardViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<UserTestViewModel> UserTests { get; set; }
    }
}
