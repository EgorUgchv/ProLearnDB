using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IQuestionRepository
{
    ICollection<Question> GetQuestionsWithCorrectAnswers();
    Question? GetQuestion(int questionId);
     bool QuestionExists(int questionId);
}