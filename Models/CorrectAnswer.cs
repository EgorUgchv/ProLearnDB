using System.ComponentModel.DataAnnotations;

namespace ProLearnDB.Models;

public class CorrectAnswer
{
    public int CorrectAnswerId { get; set; }

    [MaxLength(500)]
    public string? Answer { get; set; }
    
    public ICollection<Question> Questions { get; set; }
}