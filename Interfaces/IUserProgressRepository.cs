using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IUserProgressRepository
{
    int GetUserProgressInPercent(int? userId);
    bool CreateUserProgress(User user);
    bool Save();
}