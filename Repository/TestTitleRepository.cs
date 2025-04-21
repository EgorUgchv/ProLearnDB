using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class TestTitleRepository : ITestTitleRepository
{
    private readonly ProLearnDbContext _context;

    public TestTitleRepository(ProLearnDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Все заголовки тестов
    /// </summary>
    /// <returns>Коллекция всех заголовков тестов</returns>
    public ICollection<TestTitle> GetTestTitles()
    {
        return _context.TestTitles.OrderBy(p => p.TestTitleId).ToList();
    }
}