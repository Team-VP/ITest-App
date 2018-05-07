using AutoMapper;
using ITestApp.Data.Models;
using ITestApp.DTO;
using ITestApp.Web.Areas.Administration.Models.MangeTestsViewModels;
using ITestApp.Web.Areas.Private.Models.DashboardViewModels;

namespace ITestApp.Web.Properties
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            this.CreateMap<Test, TestDto>()
                   .ForMember(dto => dto.Questions, options => options.MapFrom(source => source.Questions))
                   .ForMember(dto => dto.Status, options => options.MapFrom(source => source.Status))
                   .ForMember(dto => dto.Category, options => options.MapFrom(source => source.Category))
                   .ReverseMap().MaxDepth(3);

            this.CreateMap<TestDto, Test>()
                   .ForMember(m => m.Questions, options => options.MapFrom(dto => dto.Questions))
                   .ForMember(m => m.Status, options => options.MapFrom(dto => dto.Status))
                   .ForMember(m => m.Category, options => options.MapFrom(dto => dto.Category))
                   .ReverseMap().MaxDepth(3);

            this.CreateMap<TestDto, TestViewModel>()
                   .ForMember(vm => vm.Author, options => options.MapFrom(dto => dto.Author.Email))
                   .ForMember(vm => vm.Status, options => options.MapFrom(dto => dto.Status.Name))
                   .ReverseMap().MaxDepth(3);

            this.CreateMap<Question, QuestionDto>()
                   .ForMember(dto => dto.Answers, options => options.MapFrom(source => source.Answers))
                   .ReverseMap().MaxDepth(3);

            this.CreateMap<QuestionDto, Question>()
                .ForMember(m => m.Test, options => options.MapFrom(dto => dto.Test))
                .ForMember(m => m.Answers, options => options.MapFrom(dto => dto.Answers));

            this.CreateMap<QuestionDto, QuestionViewModel>()
                .ForMember(dto => dto.Answers, opt => opt.MapFrom(src => src.Answers))
                .ReverseMap().MaxDepth(3);

            this.CreateMap<Answer, AnswerDto>()
                .ReverseMap().MaxDepth(3);

            this.CreateMap<AnswerDto, Answer>();

            this.CreateMap<AnswerDto, AnswerViewModel>()
                .ReverseMap().MaxDepth(3);

            this.CreateMap<Category, CategoryDto>()
                .ForMember(dto => dto.Tests, opt => opt.MapFrom(src => src.Tests))
                .ReverseMap().MaxDepth(3);

            //this.CreateMap<CategoryDto, CategoryViewModel>()
            //    .ForMember(vm => vm.Tests, opt => opt.MapFrom(src => src.Tests));

            this.CreateMap<UserTest, UserTestDto>().ReverseMap().MaxDepth(3);

            this.CreateMap<Status, StatusDto>().ReverseMap();

            this.CreateMap<CreateTestViewModel, TestDto>(MemberList.Destination)
                .ForMember(dto => dto.Category, options => options.Ignore())
                .ForMember(dto => dto.Status, options => options.Ignore());

            this.CreateMap<TestDto, CreateTestViewModel>()
                .ForMember(vm => vm.Author, options => options.MapFrom(dto => dto.Author.Email))
                .ForMember(vm => vm.Category, options => options.MapFrom(dto => dto.Category.Name))
                .ForMember(vm => vm.Status, options => options.MapFrom(dto => dto.Status.Name));

            this.CreateMap<QuestionDto, CreateQuestionViewModel>(MemberList.Source);
            this.CreateMap<AnswerDto, CreateAnswerViewModel>(MemberList.Source);

            this.CreateMap<CreateQuestionViewModel, QuestionDto>();
            this.CreateMap<CreateAnswerViewModel, AnswerDto>();
            this.CreateMap<CreateCategoryViewModel, CategoryDto>();
        }
    }
}
