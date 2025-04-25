namespace ProLearnDB.Dto;

public class TestDto
{
    
    public int TestTitleId { get; set; }
    public string? Title { get; set; }
    public IEnumerable<QuestionDto> Questions { get; set; }
}