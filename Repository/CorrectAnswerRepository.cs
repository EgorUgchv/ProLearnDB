using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class CorrectAnswerRepository(ProLearnDbContext context): ICorrectAnswerRepository
{
   public CorrectAnswer GetCorrectAnswerId(string correctAnswer)
   {
       return context.CorrectAnswers.FirstOrDefault(a => a.Answer.Equals(correctAnswer));
   }
}