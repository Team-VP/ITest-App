using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Models.CreateTestViewModels
{
    public class CreateSubmitViewModel
    {
        public CreateTestViewModel Test { get; set; }

        public IList<CreateQuestionViewModel> Questions { get; set; }

        public IList<CreateAnswerViewModel> Answers { get; set; }
    }
}
