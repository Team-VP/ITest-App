using ITestApp.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Areas.Administration.Models.MangeTestsViewModels
{
    public class CreateAnswerViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MinAnswerContentLength)]
        [MaxLength(ModelConstants.MaxAnswerContentLength)]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsCorrect { get; set; }
    }
}
