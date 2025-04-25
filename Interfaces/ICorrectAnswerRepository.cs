using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface ICorrectAnswerRepository
{
    CorrectAnswer GetCorrectAnswerId(string correctAnswer);
}