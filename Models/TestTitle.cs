using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProLearnDB.Models;

public class TestTitle
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TestTitleId { get; set; } 
    
    [MaxLength(500)]
    public string Title { get; set; }
    public ICollection<Question> Questions { get; set; }
    public ICollection<UserProgress> UserProgresses { get; set; }
}