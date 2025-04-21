using System.ComponentModel.DataAnnotations;

namespace ProLearnDB.Models;

public class Question
{
    public int QuestionId { get; set; }
    public int TestTitleId { get; set; }
    public int CorrectAnswerId { get; set; }
    [MaxLength(500)]
    public string Issue { get; set; }
    [MaxLength(500)]
    public string? IssueChoice1 { get; set; }
    [MaxLength(500)]
    public string? IssueChoice2 { get; set; }
    [MaxLength(500)]
    public string? IssueChoice3 { get; set; }
    [MaxLength(500)]
    public string? IssueChoice4 { get; set; }
    
    public TestTitle TestTitle { get; set; }
    
    public CorrectAnswer CorrectAnswer { get; set; }
}