using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface ITestTitleRepository
{
    ICollection<TestTitle> GetTestTitles();
    ICollection<QuestionDto> GetTestByTestTitleId(int testTitleId);
    TestTitle? GetTestTitleByTitle(string testTitle);
    public bool CreateTestTitle(TestTitle testTitle);
    bool TestTitleExists(int testTitleId);
    bool TestTitleExistsByTestTitle(string testTitle);
    public bool Save();
    bool DeleteTest(TestTitle testToDelete);
}