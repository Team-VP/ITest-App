using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Models.CreateTestViewModels
{
    public class CreateQuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public ICollection<CreateAnswerViewModel> Answers { get; set; }
    }
}
