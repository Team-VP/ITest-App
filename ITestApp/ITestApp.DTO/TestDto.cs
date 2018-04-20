using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.DTO
{
    public class TestDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RequiredTime { get; set; }

        public string AuthorId { get; set; }
        public UserDto Author { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public int StatusId { get; set; }
        public StatusDto Status { get; set; }

        public ICollection<QuestionDto> Questions { get; set; }
    }
}
