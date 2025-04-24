using System.Runtime.Serialization;
using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class UserProgressRepository(ProLearnDbContext context, ITestTitleRepository testTitleRepository) :
    IUserProgressRepository
{
    public bool CreateUserProgress(User user)
    {
        if (user.UserId == 0)
            throw new ArgumentException("User ID cannot be zero");
        var tests = testTitleRepository.GetTestTitles()
            .ToList();
        var userProgresses = tests
            .Select(test => new UserProgress
            {
                UserId = user.UserId,
                TestTitleId = test.TestTitleId,
                IsCompleted = false,
                User = user,
                TestTitle = test
            });
        context.UserProgresses.AddRange(userProgresses);
        return Save();
    }

    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}

