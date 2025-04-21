namespace ProLearnDB.Models;

public class Question
{
    public int QuestionId { get; set; }
    public int TestTitleId { get; set; }
    public int CorrectAnswerId { get; set; }
    public string Issue { get; set; }
    public string IssueChoice1 { get; set; }
    public string IssueChoice2 { get; set; }
    public string IssueChoice3 { get; set; }
    
    public TestTitle TestTitle { get; set; }
    
    public CorrectAnswer CorrectAnswer { get; set; }
}