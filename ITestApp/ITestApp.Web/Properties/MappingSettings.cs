using AutoMapper;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Web.Models.DashboardViewModels;
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
                   .ForMember(vm => vm.Author, options => options.MapFrom(dto => dto.Author.Email))
                   //.ForMember(vm => vm.Category, options => options.MapFrom(dto => dto.Category.Name))
                   .ForMember(vm => vm.Status, options => options.MapFrom(dto => dto.Status.Name));

            this.CreateMap<CategoryDto, CategoryViewModel>();
            this.CreateMap<QuestionDto, QuestionViewModel>();
            this.CreateMap<AnswerDto, AnswerViewModel>();
            //
            this.CreateMap<TestViewModel, TestDto>(MemberList.Source);
            this.CreateMap<QuestionViewModel, QuestionDto>(MemberList.Source);
            this.CreateMap<AnswerViewModel, AnswerDto>(MemberList.Source);
            this.CreateMap<CategoryViewModel, CategoryDto>(MemberList.Source);
            //
            this.CreateMap<CategoryDto, Category>(MemberList.Source);
            this.CreateMap<TestDto, Test>(MemberList.Source);
            this.CreateMap<StatusDto, Status>(MemberList.Source);
            this.CreateMap<QuestionDto, Question>(MemberList.Source);
            this.CreateMap<AnswerDto, Answer>(MemberList.Source);
            //
            this.CreateMap<PostTestViewModel, TestDto>(MemberList.Source)
                .ForMember(dto => dto.Category, options => options.MapFrom(vm => new CategoryDto() { Name = vm.Category }));

            this.CreateMap<PostQuestionViewModel, QuestionDto>(MemberList.Source);
            this.CreateMap<PostAnswerViewModel, AnswerDto>(MemberList.Source);
        }
    }
}
