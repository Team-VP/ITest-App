using ITestApp.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITestApp.Web.Areas.Administration.Models.MangeTestsViewModels
{
    public class CreateTestViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MinTestTitleLength)]
        [MaxLength(ModelConstants.MaxTestTitleLength)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Range(ModelConstants.MinRequiredTestTime, ModelConstants.MaxRequiredTestTime, ErrorMessage = ModelConstants.RequiredTestTimeErrorMsg)]
        public int RequiredTime { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.Text)]
        public string Author { get; set; }
        
        public string Category { get; set; }

        [DataType(DataType.Text)]
        public string Status { get; set; }

        public ICollection<CreateQuestionViewModel> Questions { get; set; }
    }
}
