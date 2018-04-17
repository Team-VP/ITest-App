using System;
using System.Collections.Generic;
using System.Text;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class UserTest : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public float Points { get; set; }

        public bool IsPassed { get; set; }
    }
}
