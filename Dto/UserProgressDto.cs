namespace ProLearnDB.Dto;

public class UserProgressDto
{
    public int UserProgressId { get; set; }
    public int UserId { get; set; }
    public int TestTitleId { get; set; }
    
    public bool IsCompleted { get; set; }
}