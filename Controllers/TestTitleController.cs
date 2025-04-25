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
public class TestTitleController(ITestTitleRepository testTitleRepository, IMapper mapper,IQuestionRepository questionRepository, ICorrectAnswerRepository correctAnswerRepository) : Controller
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
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
[SwaggerRequestExample(typeof(TestDto),typeof(TestDtoRequestExample))]
    public IActionResult CreateTest([FromBody] TestDto testCreate)
    {
        var test = testTitleRepository.GetTestTitles()
            .FirstOrDefault(u => u.Title.Equals(testCreate.Title));
        if (test != null)
        {
            ModelState.AddModelError("", "Test already exists");
            return StatusCode(422, ModelState);
        }
    
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    if (testCreate.Questions == null || !testCreate.Questions.Any())
    {
        ModelState.AddModelError("Questions", "At least one question is required");
        return BadRequest(ModelState);
    }
        TestTitleDto testTitleDto = new TestTitleDto
        {
            TestTitleId = testCreate.TestTitleId,
            Title = testCreate.Title
        };
        var testTitleMap = mapper.Map<TestTitle>(testTitleDto);
        if (!testTitleRepository.CreateTestTitle(testTitleMap))
        {
            ModelState.AddModelError("","Something went wrong while saving test title");
            return StatusCode(500, ModelState);
        }
        IEnumerable<Question> questions = testCreate.Questions
            .Select(question =>
            {
                if (question.CorrectAnswer != null)
                    return new Question
                    {
                        QuestionId = question.QuestionId,
                        TestTitleId = question.TestTitleId,
                        CorrectAnswerId = question.CorrectAnswerId,
                        Issue = question.Issue,
                        IssueChoice1 = question.IssueChoice1,
                        IssueChoice2 = question.IssueChoice2,
                        IssueChoice3 = question.IssueChoice3,
                        IssueChoice4 = question.IssueChoice4,
                        CorrectAnswer = correctAnswerRepository.GetCorrectAnswerId(question.CorrectAnswer),
                        TestTitle = testTitleMap
                    };
                throw new ArgumentNullException("", "Correct answer field is null");
                //TODO: finalize the method
            });
        
        if (!questionRepository.CreateQuestions(questions))
        {
            ModelState.AddModelError("","Something went wrong while saving questions");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}