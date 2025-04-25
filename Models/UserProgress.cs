using System.ComponentModel.DataAnnotations.Schema;

namespace ProLearnDB.Models;

public class UserProgress
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserProgressId { get; set; }
    public int UserId { get; set; }
    public int TestTitleId { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public User User { get; set; }
    
    public TestTitle TestTitle { get; set; }
}