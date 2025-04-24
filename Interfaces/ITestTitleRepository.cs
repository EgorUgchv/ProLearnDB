using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface ITestTitleRepository
{
    ICollection<TestTitle> GetTestTitles();
    ICollection<QuestionDto> GetTestByTestTitleId(int testTitleId);
    public bool CreateTestTitle(TestTitle testTitle);
    bool TestTitleExists(int testTitleId);
    public bool Save();
}