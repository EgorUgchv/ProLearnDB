using ProLearnDB.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace ProLearnDB.Helper;

public class TestDtoRequestExample : IExamplesProvider<TestDto>
{
    public TestDto GetExamples()
    {
               return new TestDto
                    {
           TestTitleId = 0,
            Title  = "",
             Questions = {}
        };
    }


}