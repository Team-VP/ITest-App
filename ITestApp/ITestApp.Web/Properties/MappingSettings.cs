using AutoMapper;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Web.Models.TestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Properties
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            this.CreateMap<TestDto, TestViewModel>()
                   .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email));

            this.CreateMap<TestViewModel, TestDto>(MemberList.Source);
            this.CreateMap<QuestionViewModel, QuestionDto>(MemberList.Source);
            this.CreateMap<AnswerViewModel, AnswerDto>(MemberList.Source);

            this.CreateMap<AnswerDto, Answer>(MemberList.Source);
            this.CreateMap<CategoryDto, Category>(MemberList.Source);
            this.CreateMap<QuestionDto, Question>(MemberList.Source);
            this.CreateMap<StatusDto, Status>(MemberList.Source);
            this.CreateMap<TestDto, Test>(MemberList.Source);
        }
    }
}
