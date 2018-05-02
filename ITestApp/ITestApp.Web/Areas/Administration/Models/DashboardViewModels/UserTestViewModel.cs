using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Areas.Administration.Models.DashboardViewModels
{
    public class UserTestViewModel
    {
        public string TestName { get; set; }

        public string UserName { get; set; }

        public string Category { get; set; }

        public int RequestedTime { get; set; }

        public int ExecutionTime { get; set; }

        public string Result { get; set; }
    }
}
