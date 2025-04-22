using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface ITestTitleRepository
{
    /// <summary>
    /// Все заголовки тестов
    /// </summary>
    /// <returns>Коллекция всех заголовков тестов</returns>
    ICollection<TestTitle> GetTestTitles();
    ICollection<QuestionDto> GetTestByTestTitleId(int testTitleId);
    bool TestTitleExists(int testTitleId);
}