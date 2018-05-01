using System;
using System.Collections.Generic;
using System.Linq;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ITestApp.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Question> questions;
        private readonly IRepository<Answer> answers;

        public QuestionsService(ISaver saver, IMappingProvider mapper, IRepository<Question> questions, IRepository<Answer> answers)
        {
            this.saver = saver ?? throw new ArgumentNullException("Saver can not be null.");
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper can not be null.");
            this.questions = questions ?? throw new ArgumentNullException("Qestion repo can not be null.");
            this.answers = answers ?? throw new ArgumentNullException("Answers repo can not be null.");
        }

        public void Edit(QuestionDto question)
        {
            Question questionToEdit = questions.All
                .Where(q => q.Id == question.Id).Include(a => a.Answers)
                .FirstOrDefault() ?? throw new ArgumentNullException("Question can not be null.");

            questionToEdit.Content = question.Content;
            questionToEdit.TestId = question.TestId;
            

            questions.Update(questionToEdit);
            saver.SaveChanges();
        }

        public IEnumerable<AnswerDto> GetAnswers(int questionId)
        {
            var answers = questions.All
                .Where(q => q.Id == questionId)
                .Include(a => a.Answers)
                ?? throw new ArgumentNullException("Collection of answers can not be Null.");

            IQueryable<AnswerDto> result = mapper.ProjectTo<AnswerDto>(answers)
                ?? throw new ArgumentNullException("Collection of AnswerDTOs can not be null.");

            return result;
        }

        public QuestionDto GetById(int id)
        {
            Question question = questions.All
                .Where(q => q.Id == id)
                .Include(a => a.Answers).FirstOrDefault() 
                ?? throw new ArgumentNullException("Question can not be null.");

            QuestionDto result = mapper.MapTo<QuestionDto>(question) 
                ?? throw new ArgumentNullException("QuestionDTO can not be null.");

            return result;
        }

        public void Delete(int id)
        {
            Question questionToDelete = questions.All.Include(a => a.Answers)
                .FirstOrDefault(q => q.Id == id) 
                ?? throw new ArgumentNullException("Question can not be null.");

            questions.Delete(questionToDelete);

            foreach (var answer in questionToDelete.Answers)
            {
                this.answers.Delete(answer);
            }

            saver.SaveChanges();
        }
    }
}
