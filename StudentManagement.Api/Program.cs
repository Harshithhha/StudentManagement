using StudentManagement.Api.Endpoints;
using StudentManagement.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("StudentManagement");
builder.Services.AddSqlite<StudentManagementContext>(connectionString);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.MapStudentEndpoints();
app.MigrateDatabase();

app.Run();
