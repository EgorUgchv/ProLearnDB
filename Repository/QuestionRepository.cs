using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class QuestionRepository : IQuestionRepository
{
    private readonly ProLearnDbContext _context;

    public QuestionRepository(ProLearnDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Все вопросы в тестах с верными ответами
    /// </summary>
    /// <returns>Коллекция всех вопросов с верными ответами</returns>
    public ICollection<Question> GetQuestionsWithCorrectAnswers()
    {
        return _context.Questions.OrderBy(p => p.QuestionId).ToList();
    }

    /// <summary>
    /// Вопрос по id 
    /// </summary>
    /// <param name="questionId">Id вопроса</param>
    /// <returns>Вопрос, соответствующий переданному id</returns>
    public Question? GetQuestion(int questionId)
    {
        return _context.Questions.FirstOrDefault(q => q.QuestionId == questionId);
    }

    public bool QuestionExists(int questionId)
    {
        return _context.Questions.Any(q => q.QuestionId == questionId);
    }

}