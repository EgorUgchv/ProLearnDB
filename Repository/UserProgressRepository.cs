using System.Runtime.Serialization;
using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class UserProgressRepository(ProLearnDbContext context, ITestTitleRepository testTitleRepository) :
    IUserProgressRepository
{
    public int GetUserProgressInPercent(int? userId)
    {
        if (userId is null or <= 0)
        {
            throw new ArgumentException("User ID must be a positive non-null value.");
        }

        var totalTest = context.UserProgresses.Count(u => u.UserId == userId);
        if (totalTest == 0)
        {
            return 0;
        }

        var countCompletedTests = context.UserProgresses.Count(u => u.UserId == userId && u.IsCompleted == true);
        var progress = (double)countCompletedTests / totalTest * 100;
        return (int)Math.Round(progress, 0);
    }

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

    public bool SetTestCompleted(int userId, int testTitleId)
    {
        var userProgress =
            context.UserProgresses.FirstOrDefault(p => p.UserId == userId && p.TestTitleId == testTitleId);
        if (userProgress == null || userProgress.IsCompleted) return false;
        userProgress.IsCompleted = true;
        return Save();
    }

    public bool UserProgressExist(int userId, int testTitleId)
    {
        return context.UserProgresses.Any(p => p.TestTitleId == testTitleId && p.UserId == userId);
    }

    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}