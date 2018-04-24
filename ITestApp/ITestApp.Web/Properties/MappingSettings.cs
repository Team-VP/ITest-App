using AutoMapper;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Web.Models.DashboardViewModels;
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
            /////
            this.CreateMap<Test, TestDto>()
                   .ForMember(dto => dto.Questions, options => options.MapFrom(source => source.Questions)).ReverseMap().MaxDepth(3);

            this.CreateMap<TestDto, TestViewModel>()
                   .ForMember(vm => vm.Author, options => options.MapFrom(dto => dto.Author.Email))
                   //.ForMember(vm => vm.Category, options => options.MapFrom(dto => dto.Category.Name))
                   .ForMember(vm => vm.Status, options => options.MapFrom(dto => dto.Status.Name)).MaxDepth(3).ReverseMap();

            this.CreateMap<Question, QuestionDto>()
                   .ForMember(dto => dto.Answers, options => options.MapFrom(source => source.Answers)).ReverseMap().MaxDepth(3);

            this.CreateMap<QuestionDto, QuestionViewModel>()
                .ForMember(dto => dto.Answers, opt => opt.MapFrom(src => src.Answers)).ReverseMap().MaxDepth(3);

            this.CreateMap<Answer, AnswerDto>().ReverseMap().MaxDepth(3);

            this.CreateMap<AnswerDto, AnswerViewModel>().ReverseMap().MaxDepth(3);

            this.CreateMap<Category, CategoryDto>()
                .ForMember(dto => dto.Tests, opt => opt.MapFrom(src => src.Tests)).ReverseMap().MaxDepth(3);

            this.CreateMap<CategoryDto, CategoryViewModel>()
                .ForMember(vm => vm.Tests, opt => opt.MapFrom(src => src.Tests));

            ///
        }
    }
}
