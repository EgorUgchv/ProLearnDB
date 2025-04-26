using System.Xml.Linq;
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

    /// <summary>
    ///  Тест соответствующий переданному заголовку теста
    /// </summary>
    /// <param name="testTitle">заголовок теста</param>
    /// <returns>Коллекция вопросов, которая относится к переданному заголовку теста</returns>
    public ICollection<QuestionDto> GetTestByTestTitle(string testTitle)
    {
        return questionRepository.GetQuestionsByTestTitle(testTitle);
    }
    
    public TestTitle? GetTestTitleByTitle(string testTitle)
    {
        return context.TestTitles.FirstOrDefault(t => t != null && t.Title.Equals(testTitle));
    }

    public bool CreateTestTitle(TestTitle testTitle)
    {
        context.Add(testTitle);
        return Save();
    }
    
    
    
    /// <summary>
    /// Проверяет существует ли тест с переданным Id
    /// </summary>
    /// <param name="testTitleId">Id теста</param>
    /// <returns></returns>
    public bool TestTitleExists(int testTitleId)
    {
        return context.Questions.Any(t => t.TestTitleId == testTitleId);
    }

    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}