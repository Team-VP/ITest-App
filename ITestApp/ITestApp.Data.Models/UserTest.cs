using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class UserTest : BaseEntity
    {
        public int ExecutionTime { get; set; }

        [Required]
        public float Points { get; set; }

        [Required]
        public bool IsPassed { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
