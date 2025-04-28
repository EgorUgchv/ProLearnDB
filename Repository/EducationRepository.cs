using System.Xml.Linq;
using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Models;

namespace ProLearnDB.Repository;

public class EducationRepository(ProLearnDbContext context): IEducationRepository
{
    public List<Education?> GetEducationMaterialsByTheme(string? theme)
    {
        return context.Education.Where(e =>  e != null && e.Theme != null && e.Theme.Equals(theme)).ToList();
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