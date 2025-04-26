using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IUserProgressRepository
{
    int GetUserProgressInPercent(int? userId);
    bool CreateUserProgress(User user);
    bool SetTestCompleted(int userId, int testTitleId);
    bool UserProgressExist(int userId, int testTitleId);
    bool Save();
}