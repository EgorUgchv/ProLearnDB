using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProLearnDB.Dto;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController(IUserRepository userRepository, IMapper mapper) : Controller
{
    /// <summary>
    /// Получение списка пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var users = mapper.Map<List<UserDto>>(userRepository.GetUsers());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(users);
    }
/// <summary>
/// Создание пользователя и его прогресса
/// </summary>
/// <param name="userCreate">Данные пользователя</param>
/// <returns></returns>
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateUser([FromBody] UserDto userCreate)
    {
        var user = userRepository
            .GetUsers().FirstOrDefault(u => u.UserId == userCreate.UserId);
        if (user != null)
        {
            ModelState.AddModelError("", "User already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userMap = mapper.Map<User>(userCreate);
        if (!userRepository.CreateUser(userMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}