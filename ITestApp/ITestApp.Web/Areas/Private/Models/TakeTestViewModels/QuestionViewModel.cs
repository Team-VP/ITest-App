using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITestApp.Web.Areas.Private.Models.TakeTestViewModels
{
    public class QuestionViewModel
    {
        [Required]
        public string Id { get; set; }

        public string Content { get; set; }

        public IList<AnswerViewModel> Answers { get; set; }

        [Required]
        public string AndswerId { get; set; }
    }
}
