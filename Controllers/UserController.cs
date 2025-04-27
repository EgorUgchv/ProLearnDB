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
public class UserController(
    IUserRepository userRepository,
    IMapper mapper,
    IUserProgressRepository userProgressRepository,
    ITestTitleRepository testTitleRepository) : Controller

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
    /// <returns>Данные о пользователе и прогресс пользователя в процентах</returns>
    [HttpGet("{phoneNumber}/userProgress")]
    [ProducesResponseType(200, Type = typeof(int))]
    [ProducesResponseType(400)]
    public IActionResult GetUserProgressByPhoneNumber(string phoneNumber)
    {
        if (!userRepository.UserExists(phoneNumber))
        {
            return NotFound();
        }

        var user = userRepository.GetUserByPhoneNumber(phoneNumber);
        if (user == null) return NotFound("User not found");
        var userProgress = new UserProgressInPercentDto
        {
            ChatId = user.ChatId,
            FullName = user.FullName,
            PhoneNumber = phoneNumber,
            UserProgressInPercent = userProgressRepository.GetUserProgressInPercent(user?.UserId)
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

    /// <summary>
    /// Помечает тест пройденным пользователем
    /// </summary>
    /// <param name="phoneNumber">Номер телефона пользователя</param>
    /// <param name="testTitle">Заголовок теста, который необходимо пометить как пройденный </param>
    /// <returns></returns>
    [HttpPatch("{phoneNumber},{testTitle}")]
    public IActionResult SetTestCompleted(string? phoneNumber, string? testTitle)
    {
        var userId = userRepository.GetUserByPhoneNumber(phoneNumber).UserId;
        var testTitleId = testTitleRepository.GetTestTitleByTitle(testTitle).TestTitleId;
        if (userProgressRepository.UserProgressExist(userId, testTitleId) == false)
        {
            ModelState.AddModelError("", "User progress not exist");
            return BadRequest(ModelState);
        }

        if (userProgressRepository.SetTestCompleted(userId, testTitleId)) return Ok("Successfully update progress");
        ModelState.AddModelError("", "Something went wrong while update progress");
        return StatusCode(500, ModelState);
    }

    /// <summary>
    /// Получение пользователя по chat id, который присваивается пользователю telegram ботом
    /// </summary>
    /// <param name="chatId">Chat id пользователя</param>
    /// <returns>Информация о пользователе</returns>
    [HttpGet("{chatId:long}/userInfo")]
    [ProducesResponseType(200, Type = typeof(UserProgressInPercentDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetUserByChatId(long chatId)
    {
        if ( chatId <= 0 )
        {
            ModelState.AddModelError("", "Chat ID must be положительным числом.");
            return BadRequest(ModelState);
        }

        var user = userRepository.GetUserByChatId(chatId);
        if ( user == null )
        {
            return NotFound("User not found");
        }

        var userProgress = new UserProgressInPercentDto
        {
            ChatId = user.ChatId,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber ?? "",
            UserProgressInPercent = userProgressRepository.GetUserProgressInPercent(user.UserId)
        };

        return Ok(userProgress);
    }




    [HttpDelete("{phoneNumber}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteUser(string phoneNumber)
    {
        if (!userRepository.UserExists(phoneNumber))
            return NotFound();

        var userToDelete = userRepository.GetUserByPhoneNumber(phoneNumber);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (userToDelete == null || userRepository.DeleteUser(userToDelete)) return Ok("Successfully deleted");
        ModelState.AddModelError("", "Something went wrong while deleting user");
        return StatusCode(500, ModelState);
    }
}