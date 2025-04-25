namespace ProLearnDB.Dto;

public class TestDto
{
    
    public string? Title { get; set; }
    public IEnumerable<QuestionDto> Questions { get; set; }
}