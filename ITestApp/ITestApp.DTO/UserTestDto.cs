using System;
using System.Collections.Generic;
using System.Text;
using ITestApp.Data.Models;

namespace ITestApp.DTO
{
    public class UserTestDto
    {
        public int Id { get; set; }

        public float Points { get; set; }

        public int ExecutionTime { get; set; }

        public bool IsPassed { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
