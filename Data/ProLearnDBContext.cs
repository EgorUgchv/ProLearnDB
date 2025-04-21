using Microsoft.EntityFrameworkCore;
using ProLearnDB.Models;

namespace ProLearnDB.Data;

internal class ProLearnDBContext:DbContext
{
    protected readonly IConfiguration Configuration;

    public ProLearnDBContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public DbSet<TestTitle> TestTitles { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       //Set up database connection
       optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ProLearnDB"));
   }
}