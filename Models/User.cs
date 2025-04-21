namespace ProLearnDB.Models;

public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    
    public ICollection<UserProgress> UserProgresses { get; set; }
}