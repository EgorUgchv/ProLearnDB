using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IQuestionRepository
{
   
    ICollection<QuestionDto> GetQuestionsWithCorrectAnswers();

    QuestionDto? GetQuestion(int questionId);

   
    ICollection<QuestionDto> GetQuestionsByTestTitleId(int testTitleId);
    ICollection<QuestionDto> GetQuestionsByTestTitle(string testTitle);
    
    bool QuestionExists(int questionId);
    bool TestTitleExists(int testTitleId);

    bool CreateQuestion(Question question);
    bool CreateQuestions(IEnumerable<Question> questions);
    bool Save();
}