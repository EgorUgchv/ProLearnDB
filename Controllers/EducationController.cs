using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProLearnDB.Dto;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class EducationController(
    IEducationRepository educationRepository,
    IMapper mapper) : Controller
{
    [HttpGet("{theme}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult GetEducationData(string? theme)
    {
        var educationMap = mapper.Map<List<EducationDto>>(educationRepository.GetEducationMaterialsByTheme(theme));
        if (educationMap == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(educationMap);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateEducation([FromBody] EducationDto educationCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var educationMap = mapper.Map<Education>(educationCreate);
        if (educationRepository.CreateEducation(educationMap)) return Ok("Successfully created");
        ModelState.AddModelError("", "Something went wrong while saving");
        return StatusCode(500, ModelState);

    }
}