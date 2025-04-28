using System.Xml.Linq;
using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class EducationRepository(ProLearnDbContext context): IEducationRepository
{
    public Education? GetEducationMaterialByTheme(string? theme)
    {
        return context.Education.FirstOrDefault(e =>  e != null && e.Theme != null && e.Theme.Equals(theme));
    }


    public bool CreateEducation(Education education)
    {
        context.Education.Add(education);
        
        return Save();
    }
    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}