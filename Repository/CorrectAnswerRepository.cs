using System.Xml.Linq;
using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class CorrectAnswerRepository(ProLearnDbContext context): ICorrectAnswerRepository
{
   public CorrectAnswer GetCorrectAnswer(string correctAnswer)
   {
       return context.CorrectAnswers.FirstOrDefault(a => a.Answer.Equals(correctAnswer));
   }

   public bool DeleteAnswer(CorrectAnswer correctAnswer)
   {
       context.Remove(correctAnswer);
       return Save();
   }

   public bool Save()
   {
       var saved = context.SaveChanges();
       return saved > 0 ? true : false;
   }
}