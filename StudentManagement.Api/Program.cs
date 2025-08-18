using StudentManagement.Api.Endpoints;
using StudentManagement.Api.Data;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("StudentManagement");
builder.Services.AddSqlite<StudentManagementContext>(connectionString);
var app = builder.Build();

app.MapStudentEndpoints();
app.MigrateDatabase();
app.Run();
