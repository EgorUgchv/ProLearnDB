using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProLearnDB.Models;

public class Education
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EducationId { get; set; }
    [MaxLength(500)] public string? Theme { get; set; }
    [MaxLength(500)] public string? EducationLink { get; set; }
    
}