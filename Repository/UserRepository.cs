using ProLearnDB.Data;
using ProLearnDB.Dto;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class UserRepository(ProLearnDbContext context, IUserProgressRepository userProgressRepository) : IUserRepository
{
    public ICollection<User> GetUsers()
    {
        return context.Users.OrderBy(u => u.UserId).ToList();
    }

    public User? GetUserByPhoneNumber(string phoneNumber)
    {
        return context.Users.FirstOrDefault(u =>u != null && u.PhoneNumber != null && u.PhoneNumber.Equals(phoneNumber));
    }

    public bool CreateUser(User user)
    {
        context.Add(user);
        if (Save() == false)
        {
            throw new InvalidOperationException("Failed to create user");
        }
        if (!userProgressRepository.CreateUserProgress(user))
        {
            throw new InvalidOperationException("Failed to create user progress");
        };
        return true;
    }

    public bool DeleteUser(User user)
    {
        context.Remove(user);
        return Save();
    }
    public bool UserExists(string phoneNumber)
    {
        return context.Users.Any(q => q.PhoneNumber != null && q.PhoneNumber.Equals(phoneNumber));
    }

    public bool CheckChatIdExists(int chatId)
    {
        return context.Users.Any(u => u != null && u.ChatId == chatId);
    }

    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }

}