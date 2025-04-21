namespace ProLearnDB.Models;

public class TestTitle
{
    public int TestTitleId { get; set; } 
    public string Title { get; set; }
    public ICollection<Question> Questions { get; set; }
    public ICollection<UserProgress> UserProgresses { get; set; }
}