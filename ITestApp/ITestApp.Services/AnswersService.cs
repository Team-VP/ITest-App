using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
