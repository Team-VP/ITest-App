using ITestApp.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Areas.Administration.Models.MangeTestsViewModels
{
    public class CreateQuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MinQuestionContentLength)]
        [MaxLength(ModelConstants.MaxQuestionContentLength)]
        [DataType(DataType.Text)]
        public string Content { get; set; }
        
        [DataType(DataType.Text)]
        public string ContentWithoutTags { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<CreateAnswerViewModel> Answers { get; set; }
    }
}
