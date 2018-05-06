using ITestApp.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Models.DashboardViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MinTestTitleLength)]
        [MaxLength(ModelConstants.MaxTestTitleLength)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Range(ModelConstants.MinRequiredTestTime, ModelConstants.MaxRequiredTestTime, ErrorMessage = ModelConstants.RequiredTestTimeErrorMsg)]
        public int RequiredTime { get; set; }

        [DataType(DataType.Text)]
        public string Author { get; set; }
        
        [MinLength(ModelConstants.MinTestCategoryLength)]
        [MaxLength(ModelConstants.MaxTestCategoryLength)]
        [DataType(DataType.Text)]
        public string TestCategory { get; set; }

        [DataType(DataType.Text)]
        public string Status { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}
