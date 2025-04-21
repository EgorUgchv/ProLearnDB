using AutoMapper;
using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Helper;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Question, QuestionDto>();
    } 
}