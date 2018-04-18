using AutoMapper;
using ITestApp.Data.Models;
using ITestApp.DTO;
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
            //this.CreateMap<PostDto, PostViewModel>()
            //       .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email));

            //this.CreateMap<CommentDto, CommentViewModel>()
            //       .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email));

            //this.CreateMap<PostViewModel, PostDto>(MemberList.Source);
            this.CreateMap<AnswerDto, Answer>(MemberList.Source);
            this.CreateMap<CategoryDto, Category>(MemberList.Source);
            this.CreateMap<QuestionDto, Question>(MemberList.Source);
            this.CreateMap<StatusDto, Status>(MemberList.Source);
            this.CreateMap<TestDto, Test>(MemberList.Source);
        }
    }
}
