using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IQuestionRepository
{
   
    ICollection<QuestionDto> GetQuestionsWithCorrectAnswers();

    QuestionDto? GetQuestion(int questionId);

   
    ICollection<QuestionDto> GetQuestionsByTestTitleId(int testTitleId);
    
    bool QuestionExists(int questionId);
    bool TestTitleExists(int testTitleId);
}