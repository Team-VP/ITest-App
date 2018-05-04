using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Models.TakeTestViewModels
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
