using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ITestApp.Common.Constants;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class Answer : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: ModelConstants.MaxAnswerContentLength)]
        public string Content { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public bool IsCorrect { get; set; }
    }
}
