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

    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}