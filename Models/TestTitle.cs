using System.ComponentModel.DataAnnotations;

namespace ProLearnDB.Models;

public class TestTitle
{
    public int TestTitleId { get; set; } 
    
    [MaxLength(500)]
    public string Title { get; set; }
    public ICollection<Question> Questions { get; set; }
    public ICollection<UserProgress> UserProgresses { get; set; }
}