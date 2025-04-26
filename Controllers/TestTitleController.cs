using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProLearnDB.Dto;
using ProLearnDB.Helper;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;
using ProLearnDB.Repository;
using Swashbuckle.AspNetCore.Filters;

namespace ProLearnDB.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TestTitleController(
    ITestTitleRepository testTitleRepository,
    IMapper mapper,
    IQuestionRepository questionRepository,
    ICorrectAnswerRepository correctAnswerRepository) : Controller
{
    /// <summary>
    /// Все заголовки тестов 
    /// </summary>
    /// <returns>Все заголовки тестов и их Id</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
    public IActionResult GetTestTitles()
    {
        var titles = mapper.Map<List<TestTitleDto>>(testTitleRepository.GetTestTitles());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(titles);
    }

    /// <summary>
    /// Тест, который соответсвует заданному Id
    /// </summary>
    /// <param name="testTitleId">Id требуемого теста</param>
    /// <returns>Тест, который соответствует заданному Id</returns>
    [HttpGet("{testTitleId:int}")]
    [ProducesResponseType(200, Type = typeof(Question))]
    [ProducesResponseType(400)]
    public IActionResult GetTestByTestTitleId(int testTitleId)
    {
        if (!testTitleRepository.TestTitleExists(testTitleId))
        {
            return NotFound();
        }

        var test = mapper.Map<List<QuestionDto>>(testTitleRepository.GetTestByTestTitleId(testTitleId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(test);
    }

    /// <summary>
    /// Тест, который соответсвует заданному test title
    /// </summary>
    /// <param name="testTitle"></param>
    /// <returns>Тест, который соответствует заданному Id</returns>
    [HttpGet("{testTitle}")]
    [ProducesResponseType(200, Type = typeof(Question))]
    [ProducesResponseType(400)]
    public IActionResult GetTestByTestTitle(string? testTitle)
    {
        if (testTitle == null)
        {
            ModelState.AddModelError("", "Input test Title");
            return BadRequest(ModelState);
        }

        var test = mapper.Map<List<QuestionDto>>(questionRepository.GetQuestionsByTestTitle(testTitle));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(test);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateTest([FromBody] TestDto testCreate)
    {
        var test = testTitleRepository.GetTestTitles()
            .FirstOrDefault(u => u.Title.Equals(testCreate.Title));
        if (test != null)
        {
            ModelState.AddModelError("", "Test already exists");
            return StatusCode(422, ModelState);
        }

        if (!testCreate.Questions.Any())
        {
            ModelState.AddModelError("Questions", "At least one question is required");
            return BadRequest(ModelState);
        }

        if (testCreate.Title == null)
        {
            ModelState.AddModelError("Title", "Input title");
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var testTitleDto = new TestTitleDto
        {
            Title = testCreate.Title
        };
        var testTitleMap = mapper.Map<TestTitle>(testTitleDto);
        if (!testTitleRepository.CreateTestTitle(testTitleMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving test title");
            return StatusCode(500, ModelState);
        }

        var questions = testCreate.Questions
            .Select(question =>
            {
                if (question.CorrectAnswer != null)
                {
                    var correctAnswer = correctAnswerRepository.GetCorrectAnswer(question.CorrectAnswer);
                    return new Question
                    {
                        CorrectAnswerId = correctAnswer.CorrectAnswerId,
                        TestTitleId = testTitleMap.TestTitleId,
                        Issue = question.Issue,
                        IssueChoice1 = question.IssueChoice1,
                        IssueChoice2 = question.IssueChoice2,
                        IssueChoice3 = question.IssueChoice3,
                        IssueChoice4 = question.IssueChoice4,
                        CorrectAnswer = correctAnswer,
                        TestTitle = testTitleMap
                    };
                }

                throw new ArgumentNullException(nameof(testCreate), "Correct answer field is null");
            });

        if (!questionRepository.CreateQuestions(questions))
        {
            ModelState.AddModelError("", "Something went wrong while saving questions");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}