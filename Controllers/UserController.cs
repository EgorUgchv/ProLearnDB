using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProLearnDB.Dto;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController(
    IUserRepository userRepository,
    IMapper mapper,
    IUserProgressRepository userProgressRepository) : Controller
{
    /// <summary>
    /// Получение списка пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(400)]
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
    ///  Получение пользователя по номеру телефона
    /// </summary>
    /// <param name="phoneNumber">Номер телефона</param>
    /// <returns></returns>
    [HttpGet("{phoneNumber}")]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(400)]
    public IActionResult GetUserByPhoneNumber(string phoneNumber)
    {
        if (!userRepository.UserExists(phoneNumber))
        {
            return NotFound();
        }

        var user = mapper.Map<UserDto>(userRepository.GetUserByPhoneNumber(phoneNumber));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(user);
    }

    /// <summary>
    /// Получение прогресса пользователя в процентах
    /// </summary>
    /// <param name="phoneNumber">Номер телефона пользователя</param>
    /// <returns>Номер телефона и прогресс пользователя в процентах</returns>
    [HttpGet("{phoneNumber}/userProgress")]
    [ProducesResponseType(200, Type = typeof(int))]
    [ProducesResponseType(400)]
    public IActionResult GetUserProgressByPhoneNumber(string phoneNumber)
    {
        if (!userRepository.UserExists(phoneNumber))
        {
            return NotFound();
        }

        var userId = userRepository.GetUserByPhoneNumber(phoneNumber)?.UserId;
        UserProgressInPercentDto userProgress = new UserProgressInPercentDto
        {
            PhoneNumber = phoneNumber,
            UserProgressInPercent = userProgressRepository.GetUserProgressInPercent(userId)
        };
        return Ok(userProgress);
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
            .GetUsers().FirstOrDefault(u => u.PhoneNumber == userCreate.PhoneNumber);
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


    // [HttpDelete("{phoneNumber}")]
    // [ProducesResponseType(400)]
    // [ProducesResponseType(204)]
    // [ProducesResponseType(404)]
    // public IActionResult DeleteUser(string phoneNumber)
    // {
    //     if (!userRepository.UserExists(phoneNumber))
    //         return NotFound();
    //
    //     var userToDelete = userRepository.GetUserByPhoneNumber(phoneNumber);
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     if (userRepository.DeleteUser(userToDelete))
    //     {
    //        ModelState.AddModelError("","Something went wrong while deleting user"); 
    //     }
    //
    //     return NoContent();
    // }
}