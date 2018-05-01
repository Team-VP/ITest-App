using System;
using System.Collections;
using System.Collections.Generic;

namespace ITestApp.Web.Models.DashboardViewModels
{
    public class UserTestViewModel
    {
        public string Id { get; set; }

        public int TestId { get; set; }

        public DateTime? SubmittedOn { get; set; }

        public TimeSpan TimeLeft { get; set; }

        public DateTime? TimeExpire { get; set; }

    }
}