namespace ITestApp.DTO
{
    public class AnswerDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int QuestionId { get; set; }
        public QuestionDto Question { get; set; }

        public bool IsCorrect { get; set; }

        public bool IsDeleted { get; set; }
    }
}
