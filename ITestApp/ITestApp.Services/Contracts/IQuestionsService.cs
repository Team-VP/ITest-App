 using ITestApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Services.Contracts
{
    public interface IQuestionsService
    {
        QuestionDto GetById(int id);

        IEnumerable<AnswerDto> GetAnswers(int questionId);

        void Edit(QuestionDto question);
    }
}
