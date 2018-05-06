using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ITestApp.Common.Constants;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class Question : BaseEntity
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        [Required]
        [StringLength(maximumLength: ModelConstants.MaxQuestionContentLength)]
        public string Content { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
