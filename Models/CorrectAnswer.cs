using System.ComponentModel.DataAnnotations;

namespace ProLearnDB.Models;

public class CorrectAnswer
{
    public int CorrectAnswerId { get; set; }

    public string Answer { get; set; }
    
    public ICollection<Question> Questions { get; set; }
}