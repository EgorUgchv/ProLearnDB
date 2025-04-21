using Microsoft.AspNetCore.Mvc;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Controller;

[Route("api/v1/[controller]")]
[ApiController]
public class QuestionController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IQuestionRepository _questionRepository;
    public QuestionController(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
    public IActionResult GetQuestionsWithCorrectAnswers()
    {
        var questions = _questionRepository.GetQuestionsWithCorrectAnswers();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(questions);
    }
}