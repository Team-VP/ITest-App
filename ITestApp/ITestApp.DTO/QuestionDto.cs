using System.Collections.Generic;

namespace ITestApp.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public int TestId { get; set; }
        public TestDto Test { get; set; }

        public ICollection<AnswerDto> Answers { get; set; }
    }
}
