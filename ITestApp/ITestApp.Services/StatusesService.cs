using Bytes2you.Validation;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.DTO;
using ITestApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITestApp.Services
{
    public class StatusesService : IStatusesService
    {
        private readonly IMappingProvider mapper;
        private readonly IRepository<Status> statuses;

        public StatusesService(IMappingProvider mapper, IRepository<Status> statuses)
        {
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper cannot be null");
            this.statuses = statuses ?? throw new ArgumentNullException("Status repository cannot be null");
        }

        public StatusDto GetStatusByName(string name)
        {
            Guard.WhenArgument(name, "Status Name").IsNullOrEmpty().Throw();

            var status = this.statuses.All.Where(s => s.Name == name).FirstOrDefault() ?? throw new ArgumentNullException("Status not found!");

            var dto = this.mapper.MapTo<StatusDto>(status);

            return dto;
        }
    }
}
