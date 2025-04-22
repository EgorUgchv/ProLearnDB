using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProLearnDB.Data;
using ProLearnDB.Dto;
using ProLearnDB.Exception;
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
    public ICollection<QuestionDto> GetQuestionsWithCorrectAnswers()
    {
        // return _context.Questions.OrderBy(p => p.QuestionId).Join().ToList();
        return _context.Questions
            .Include(q => q.CorrectAnswer)
            .OrderBy(q => q.QuestionId)
            .Select(q => new QuestionDto
            {
                QuestionId = q.QuestionId,
                TestTitleId = q.TestTitleId,
                CorrectAnswerId = q.CorrectAnswerId,
                Issue = q.Issue,
                IssueChoice1 = q.IssueChoice1,
                IssueChoice2 = q.IssueChoice2,
                IssueChoice3 = q.IssueChoice3,
                IssueChoice4 = q.IssueChoice4,
                CorrectAnswer = q.CorrectAnswer.Answer
            }).ToList();
    }


    /// <summary>
    /// Вопрос по id 
    /// </summary>
    /// <param name="questionId">Id вопроса</param>
    /// <returns>Вопрос, соответствующий переданному id</returns>
    public QuestionDto? GetQuestion(int questionId)
    {
        return _context.Questions
            .Include(q => q.CorrectAnswer)
            .Where(q => q.QuestionId == questionId)
            .Select(q => new QuestionDto
            {
                QuestionId = q.QuestionId,
                TestTitleId = q.TestTitleId,
                CorrectAnswerId = q.CorrectAnswerId,
                Issue = q.Issue,
                IssueChoice1 = q.IssueChoice1,
                IssueChoice2 = q.IssueChoice2,
                IssueChoice3 = q.IssueChoice3,
                IssueChoice4 = q.IssueChoice4,
                CorrectAnswer = q.CorrectAnswer.Answer
            })
            .FirstOrDefault();
    }

    /// <summary>
    ///  Все вопросы, которые относятся к одному тесту
    /// </summary>
    /// <param name="testTitleId">Id теста</param>
    /// <returns>Коллекция вопросов, которая относится к переданному Id теста</returns>
    public ICollection<QuestionDto> GetQuestionsByTestTitleId(int testTitleId)
    {
        return _context.Questions.Where(q => q.TestTitleId == testTitleId)
            .Include(q => q.CorrectAnswer)
            .OrderBy(q => q.QuestionId)
            .Select(q => new QuestionDto
            {
                QuestionId = q.QuestionId,
                TestTitleId = q.TestTitleId,
                CorrectAnswerId = q.CorrectAnswerId,
                Issue = q.Issue,
                IssueChoice1 = q.IssueChoice1,
                IssueChoice2 = q.IssueChoice2,
                IssueChoice3 = q.IssueChoice3,
                IssueChoice4 = q.IssueChoice4,
                CorrectAnswer = q.CorrectAnswer.Answer
            }).ToList();
    }

    public bool QuestionExists(int questionId)
    {
        return _context.Questions.Any(q => q.QuestionId == questionId);
    }

    public bool TestTitleExists(int testTitleId)
    {
        return _context.Questions.Any(q => q.TestTitleId == testTitleId);
    }
}