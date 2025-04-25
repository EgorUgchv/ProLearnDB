using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProLearnDB.Models;

public class User
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    [MaxLength(200)]
    public string? FullName { get; set; }
    [MaxLength(50)]
    public string? PhoneNumber { get; set; }
    
    public ICollection<UserProgress> UserProgresses { get; set; }
}