using ITestApp.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Areas.Administration.Models.DashboardViewModels
{
    public class TestViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MinTestTitleLength)]
        [MaxLength(ModelConstants.MaxTestTitleLength)]
        [DataType(DataType.Text)]
        public string TestName { get; set; }

        public string CategoryName { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
