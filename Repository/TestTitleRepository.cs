using ProLearnDB.Data;
using ProLearnDB.Dto;
using ProLearnDB.Exception;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class TestTitleRepository(ProLearnDbContext context, IQuestionRepository questionRepository)
    : ITestTitleRepository
{
    public ICollection<TestTitle> GetTestTitles()
    {
        return context.TestTitles.OrderBy(p => p.TestTitleId).ToList();
    }

    /// <summary>
    ///  Тест соответствующий переданному Id
    /// </summary>
    /// <param name="testTitleId">Id теста</param>
    /// <returns>Коллекция вопросов, которая относится к переданному Id теста</returns>
    public ICollection<QuestionDto> GetTestByTestTitleId(int testTitleId)
    {
        return questionRepository.GetQuestionsByTestTitleId(testTitleId);
    }

    public bool TestTitleExists(int testTitleId)
    {
        return context.Questions.Any(t => t.TestTitleId == testTitleId);
    }
}