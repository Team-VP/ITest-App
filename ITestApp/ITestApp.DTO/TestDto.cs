using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.DTO
{
    public class TestDto
    {
        public string Name { get; set; }

        public int RequiredTime { get; set; }

        //public int AuthorId { get; set; }
        //public User Author { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public int StatusId { get; set; }
        public StatusDto Status { get; set; }

        public ICollection<QuestionDto> Questions { get; set; }
    }
}
