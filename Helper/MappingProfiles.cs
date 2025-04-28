using AutoMapper;
using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Helper;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Question, QuestionDto>();
        CreateMap<QuestionDto, Question>()
            .ForMember(x => x.CorrectAnswer, opt =>opt.Ignore());
        CreateMap<TestTitle,TestTitleDto>();
        CreateMap<TestTitleDto,TestTitle>();
        CreateMap<User,UserDto>();
        CreateMap<UserDto,User>();
        CreateMap<EducationDto,Education>();
        CreateMap<Education,EducationDto>();
    } 
}