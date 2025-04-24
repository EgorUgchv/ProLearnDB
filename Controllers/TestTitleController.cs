using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProLearnDB.Dto;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TestTitleController : Controller
{
    private readonly ITestTitleRepository _testTitleRepository;
    private readonly IMapper _mapper;

    public TestTitleController(ITestTitleRepository testTitleRepository, IMapper mapper)
    {
        _testTitleRepository = testTitleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Все заголовки тестов 
    /// </summary>
    /// <returns>Все заголовки тестов и их Id</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
    public IActionResult GetTestTitles()
    {
        var titles = _mapper.Map<List<TestTitleDto>>(_testTitleRepository.GetTestTitles());
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
        if (!_testTitleRepository.TestTitleExists(testTitleId))
        {
            return NotFound();
        }

        var test = _mapper.Map<List<QuestionDto>>(_testTitleRepository.GetTestByTestTitleId(testTitleId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(test);
    }
}