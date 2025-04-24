using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProLearnDB.Dto;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class QuestionController : Controller
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;
    public QuestionController(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Все вопросы в тестах с верными ответами
    /// </summary>
    /// <returns>Все вопросы с верными ответами</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
    public IActionResult GetQuestionsWithCorrectAnswers()
    {
        var questions = _mapper.Map<List<QuestionDto>>( _questionRepository.GetQuestionsWithCorrectAnswers()); if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(questions);
    }
/// <summary>
/// Получение вопроса по Id
/// </summary>
/// <param name="questionId">Id необходимого вопроса</param>
/// <returns></returns>
    [HttpGet("{questionId}")]
    [ProducesResponseType(200, Type = typeof(Question))]
    [ProducesResponseType(400)]
    public IActionResult GetQuestion(int questionId)
    {
        if (!_questionRepository.QuestionExists(questionId))
            return NotFound();
        var question =_mapper.Map<QuestionDto>(_questionRepository.GetQuestion(questionId)) ;

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(question);
    }
/// <summary>
/// Создание вопроса
/// </summary>
/// <param name="questionCreate">Данные вопроса, которого необходимо создать</param>
/// <returns></returns>
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateQuestion([FromBody] QuestionDto questionCreate)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!_questionRepository.CreateQuestion(questionCreate))
        {
            ModelState.AddModelError("","Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}