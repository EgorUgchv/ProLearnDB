using Microsoft.EntityFrameworkCore;
using ProLearnDB.Models;

namespace ProLearnDB.Data;

public class ProLearnDbContext:DbContext
{
    protected readonly IConfiguration Configuration;

    public ProLearnDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public DbSet<TestTitle?> TestTitles { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<User?> Users { get; set; }
    public DbSet<UserProgress> UserProgresses { get; set; }
    public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       //Set up database connection
       optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ProLearnDB"));
   }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       modelBuilder.Entity<User>()
           .Property(u => u.UserId)
           .UseIdentityAlwaysColumn();
       
       modelBuilder.Entity<Question>()
           .Property(q => q.QuestionId)
           .UseIdentityAlwaysColumn();
       
       modelBuilder.Entity<TestTitle>()
           .Property(t => t.TestTitleId)
           .UseIdentityAlwaysColumn();
       
       modelBuilder.Entity<UserProgress>()
           .Property(u => u.UserProgressId)
           .UseIdentityAlwaysColumn();
       
       modelBuilder.Entity<CorrectAnswer>()
           .Property(c => c.CorrectAnswerId)
           .UseIdentityAlwaysColumn();
   }
}