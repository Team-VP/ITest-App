using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Web.Models.DashboardViewModels;

namespace ITestApp.Web.Models.TakeTestViewModels
{
    public class IndexViewModel
    {
        public string UserId { get; set; }

        public int TestId { get; set; } // Id

        public string TestName { get; set; }

        public TimeSpan Duration { get; set; }


        public string CategoryName { get; set; } // not from Test

        public IList<QuestionViewModel> Questions { get; set; }

        public DateTime StartedOn { get; set; }

    }
}
