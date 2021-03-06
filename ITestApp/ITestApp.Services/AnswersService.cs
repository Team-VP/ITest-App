﻿using ITestApp.Common.Providers;
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
            this.saver = saver ?? throw new ArgumentNullException("Saver cannot be null");
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper cannot be null");
            this.answers = answers ?? throw new ArgumentNullException("Answer repository cannot be null");
        }

        //public void Edit(AnswerDto answer)
        //{
        //    Answer answerToEdit = answers.All.
        //        FirstOrDefault(a => a.Id == answer.Id) 
        //        ?? throw new ArgumentNullException("Answer can not be null.");

        //    answerToEdit.Content = answer.Content;
        //    answerToEdit.QuestionId = answer.QuestionId;
        //    answerToEdit.IsCorrect = answer.IsCorrect;

        //    answers.Update(answerToEdit);
        //    saver.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    var answerToDelete = answers.All
        //        .FirstOrDefault(a => a.Id == id)
        //        ?? throw new ArgumentNullException("Answer can not be null.");

        //    answers.Delete(answerToDelete);
        //}

        public AnswerDto GetById(int id)
        {
            var currentAnwer = answers.All
                .FirstOrDefault(answer => answer.Id == id) 
                ?? throw new ArgumentNullException("Answer can not be Null.");

            var dto = mapper.MapTo<AnswerDto>(currentAnwer)
                 ?? throw new ArgumentNullException("AnswerDto can not be Null.");

            return dto;
        }
    }
}
