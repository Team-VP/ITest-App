using System.Collections.Generic;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;

namespace ITestApp.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Question> questions;

        public QuestionsService(ISaver saver, IMappingProvider mapper, IRepository<Question> questions)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.questions = questions;
        }

        public void Edit(QuestionDto question)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AnswerDto> GetAnswers(int questionId)
        {
            throw new System.NotImplementedException();
        }

        public QuestionDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
