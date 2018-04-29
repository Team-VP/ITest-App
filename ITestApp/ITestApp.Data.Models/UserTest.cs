using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class UserTest : BaseEntity
    {
        public TimeSpan ExecutionTime { get; set; }

        public float? Points { get; set; }

        [Required]
        public bool IsPassed { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public DateTime StartOn { get; set; }

        public DateTime? SubmittedOn { get; set; }

        public DateTime? TimeExpire { get; set; }
    }
}
