using System.ComponentModel.DataAnnotations;

namespace ProLearnDB.Models;

public class User
{
    public int UserId { get; set; }
    [MaxLength(200)]
    public string? FullName { get; set; }
    [MaxLength(50)]
    public string? PhoneNumber { get; set; }
    
    public ICollection<UserProgress> UserProgresses { get; set; }
}