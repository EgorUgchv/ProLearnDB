using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IUserProgressRepository
{
    bool CreateUserProgress(User user);
    bool Save();
}