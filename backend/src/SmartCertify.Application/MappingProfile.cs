using AutoMapper;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.DTOs.Courses;
using SmartCertify.Application.DTOs.Questions;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

            //CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<CreateQuestionDto, Question>();
            CreateMap<UpdateQuestionDto, Question>();

            CreateMap<Choice, ChoiceDto>().ReverseMap();
            CreateMap<CreateChoiceDto, Choice>();
            CreateMap<UpdateChoiceDto, Choice>();

            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

            CreateMap<QuestionDto, Question>()
                .ForMember(dest => dest.Choices, opt => opt.Ignore()); // Ignore to handle manually


        }
    }
}
