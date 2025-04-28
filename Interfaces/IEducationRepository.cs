using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IEducationRepository
{
    List<Education?> GetEducationMaterialsByTheme(string? theme);
    bool CreateEducation(Education education);
    public bool Save();
}