namespace ITestApp.DTO
{
    public class AnswerDto
    {
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public QuestionDto Question { get; set; }

        public bool IsCorrect { get; set; }
    }
}
