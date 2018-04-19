using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
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
    }
}
