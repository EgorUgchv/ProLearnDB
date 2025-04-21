using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface ITestTitleRepository
{
    
    ICollection<TestTitle> GetTestTitles();
}