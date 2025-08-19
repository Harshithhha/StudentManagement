using StudentManagement.Api.Endpoints;
using StudentManagement.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("StudentManagement");
builder.Services.AddSqlite<StudentManagementContext>(connectionString);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI();

// Root redirects to Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Map endpoints and run migrations
app.MapStudentEndpoints();
app.MigrateDatabase();

app.Run();
