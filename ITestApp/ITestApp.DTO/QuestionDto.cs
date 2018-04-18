using System.Collections.Generic;

namespace ITestApp.DTO
{
    public class QuestionDto
    {
        public string Text { get; set; }

        public int TestId { get; set; }
        public TestDto Test { get; set; }

        public ICollection<AnswerDto> Answers { get; set; }
    }
}
