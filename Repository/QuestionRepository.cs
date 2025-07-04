using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProLearnDB.Data;
using ProLearnDB.Dto;
using ProLearnDB.Exception;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class QuestionRepository(ProLearnDbContext context, IMapper mapper) : IQuestionRepository
{
    /// <summary>
    /// Все вопросы в тестах с верными ответами
    /// </summary>
    /// <returns>Коллекция всех вопросов с верными ответами</returns>
    public ICollection<QuestionDto> GetQuestionsWithCorrectAnswers()
    {
        return context.Questions
            .Include(q => q.CorrectAnswer)
            .OrderBy(q => q.QuestionId)
            .Select(q => new QuestionDto
            {
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
        return context.Questions
            .Include(q => q.CorrectAnswer)
            .Where(q => q.QuestionId == questionId)
            .Select(q => new QuestionDto
            {
                Issue = q.Issue,
                IssueChoice1 = q.IssueChoice1,
                IssueChoice2 = q.IssueChoice2,
                IssueChoice3 = q.IssueChoice3,
                IssueChoice4 = q.IssueChoice4,
                CorrectAnswer = q.CorrectAnswer.Answer
            })
            .FirstOrDefault();
    }

    public bool CreateQuestions(IEnumerable<Question> questions)
    {
        
       context.Questions.AddRange(questions);
       return Save();
    }

    /// <summary>
    ///  Все вопросы, которые относятся к одному тесту
    /// </summary>
    /// <param name="testTitleId">Id теста</param>
    /// <returns>Коллекция вопросов, которая относится к переданному Id теста</returns>
    public ICollection<QuestionDto> GetQuestionsByTestTitleId(int testTitleId)
    {
        return context.Questions.Where(q => q.TestTitleId == testTitleId)
            .Include(q => q.CorrectAnswer)
            .OrderBy(q => q.QuestionId)
            .Select(q => new QuestionDto
            {
                Issue = q.Issue,
                IssueChoice1 = q.IssueChoice1,
                IssueChoice2 = q.IssueChoice2,
                IssueChoice3 = q.IssueChoice3,
                IssueChoice4 = q.IssueChoice4,
                CorrectAnswer = q.CorrectAnswer.Answer
            }).ToList();
    }

    public ICollection<QuestionDto> GetQuestionsByTestTitle(string testTitle)
    {
        int testTitleId = context.TestTitles.FirstOrDefault(t => t != null && t.Title.Equals(testTitle)).TestTitleId;
        return GetQuestionsByTestTitleId(testTitleId);
    }

    public bool QuestionExists(int questionId)
    {
        return context.Questions.Any(q => q.QuestionId == questionId);
    }

    public bool TestTitleExists(int testTitleId)
    {
        return context.Questions.Any(q => q.TestTitleId == testTitleId);
    }

    public bool CreateQuestion(Question question)
    {
        context.Questions.Add(question);
        
        return Save();
    }

    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}