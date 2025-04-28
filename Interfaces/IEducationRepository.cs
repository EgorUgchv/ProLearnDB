using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IEducationRepository
{
    Education? GetEducationMaterialByTheme(string? theme);
    bool CreateEducation(Education education);
    public bool Save();
}