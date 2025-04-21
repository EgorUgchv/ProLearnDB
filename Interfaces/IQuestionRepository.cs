using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IQuestionRepository
{
    ICollection<Question> GetQuestionsWithCorrectAnswers();
}