using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class Answer : BaseEntity
    {
        [Required]
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public bool IsCorrect { get; set; }
    }
}
