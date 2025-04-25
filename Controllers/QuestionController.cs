using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProLearnDB.Dto;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;
using ProLearnDB.Repository;

namespace ProLearnDB.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class QuestionController(
    IQuestionRepository questionRepository,
    ITestTitleRepository testTitleRepository,
    IMapper mapper,
    ICorrectAnswerRepository correctAnswerRepository) : Controller
{
    /// <summary>
    /// Все вопросы в тестах с верными ответами
    /// </summary>
    /// <returns>Все вопросы с верными ответами</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
    public IActionResult GetQuestionsWithCorrectAnswers()
    {
        var questions = mapper.Map<List<QuestionDto>>(questionRepository.GetQuestionsWithCorrectAnswers());
        if (!ModelState.IsValid)
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
        if (!questionRepository.QuestionExists(questionId))
            return NotFound();
        var question = mapper.Map<QuestionDto>(questionRepository.GetQuestion(questionId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(question);
    }

    /// <summary>
    /// Создание вопроса
    /// </summary>
    /// <param name="testTitle"></param>
    /// <param name="questionCreate">Данные вопроса, которого необходимо создать</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateQuestion([FromQuery] string? testTitle, [FromBody] QuestionDto questionCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var questionMap = mapper.Map<Question>(questionCreate);
        if (testTitle != null && questionCreate.CorrectAnswer != null)
        {
            FillTestTitleAndCorrectAnswerInQuestionMap(questionMap, testTitle, questionCreate);
            if (!questionRepository.CreateQuestion(questionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        ModelState.AddModelError("", "Input testTitle or correctAnswer field");
        return BadRequest(ModelState);
    }

    private void FillTestTitleAndCorrectAnswerInQuestionMap(Question questionMap, string testTitle,
        QuestionDto questionCreate)
    {
        var testTitleForQuestion = testTitleRepository.GetTestTitleByTitle(testTitle);
        if (questionCreate.CorrectAnswer == null) throw new ArgumentException("Input correctAnswer field");
        var correctAnswer = correctAnswerRepository.GetCorrectAnswer(questionCreate.CorrectAnswer);

        questionMap.TestTitle = testTitleForQuestion;
        if (testTitleForQuestion != null) questionMap.TestTitleId = testTitleForQuestion.TestTitleId;

        questionMap.CorrectAnswer = correctAnswer;
        questionMap.CorrectAnswerId = correctAnswer.CorrectAnswerId;
    }
}