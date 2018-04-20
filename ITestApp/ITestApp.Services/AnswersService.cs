using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ITestApp.Services
{
    public class AnswersService : IAnswersService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Answer> answers;

        public AnswersService(ISaver saver, IMappingProvider mapper, IRepository<Answer> answers)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.answers = answers;
        }

        public void Edit(AnswerDto answer)
        {
            throw new NotImplementedException();
        }

        public AnswerDto GetById(int id)
        {
            var currentAnwer = answers.All.FirstOrDefault(answer => answer.Id == id);
            return mapper.MapTo<AnswerDto>(currentAnwer);
        }

        public void DeleteAnswer(int id)
        {
            var answerToDelete = answers.All.FirstOrDefault(a => a.Id == id) ?? throw new ArgumentNullException("Answer can not be null.");

            answers.Delete(answerToDelete);
        }

    }
}
