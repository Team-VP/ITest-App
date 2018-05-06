using ITestApp.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace ITestApp.Web.Models.DashboardViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MinAnswerContentLength)]
        [MaxLength(ModelConstants.MaxAnswerContentLength)]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public bool IsCorrect { get; set; }
    }
}
