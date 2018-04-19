using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.Services.Contracts;

namespace ITestApp.Services
{
    public class StatusesService : IStatusesService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Status> statuses;

        public StatusesService(ISaver saver, IMappingProvider mapper, IRepository<Status> statuses)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.statuses = statuses;
        }
    }
}
