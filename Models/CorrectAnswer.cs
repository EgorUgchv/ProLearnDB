using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProLearnDB.Models;

public class CorrectAnswer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CorrectAnswerId { get; set; }

    [MaxLength(500)] public string? Answer { get; set; }

    public ICollection<Question> Questions { get; set; }
}