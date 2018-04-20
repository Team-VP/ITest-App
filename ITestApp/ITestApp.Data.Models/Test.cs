using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class Test : BaseEntity
    {
        public Test()
        {
            Questions = new HashSet<Question>();
            UserTests = new HashSet<UserTest>();
        }

        [Required]
        public string Title { get; set; }

        public int RequiredTime { get; set; }

        public string AuthorId { get; set; }
        [Required]
        public User Author { get; set; }

        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }

        public int StatusId { get; set; }
        [Required]
        public Status Status { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<UserTest> UserTests { get; set; }
    }
}
