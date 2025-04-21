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

    public Question? GetQuestion(int questionId)
    {
        return _context.Questions.FirstOrDefault(p => p.QuestionId == questionId);
    }
}