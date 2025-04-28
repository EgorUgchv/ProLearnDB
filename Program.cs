using Microsoft.EntityFrameworkCore;
using ProLearnDB.Data;
using ProLearnDB.Interfaces;
using ProLearnDB.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ITestTitleRepository, TestTitleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserProgressRepository, UserProgressRepository>();
builder.Services.AddScoped<ICorrectAnswerRepository, CorrectAnswerRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Register DbContext
var connectionString = builder.Configuration.GetConnectionString("ProLearnDB");
builder.Services.AddDbContext<ProLearnDbContext>(options => options.UseNpgsql
    (connectionString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
