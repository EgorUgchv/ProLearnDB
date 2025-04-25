using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface ICorrectAnswerRepository
{
    CorrectAnswer GetCorrectAnswer(string correctAnswer);
    bool DeleteAnswer(CorrectAnswer correctAnswer);
    bool Save();
}