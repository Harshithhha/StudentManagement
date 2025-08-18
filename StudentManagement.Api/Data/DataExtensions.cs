using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; 
namespace StudentManagement.Api.Data;

public static class DataExtensions
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StudentManagementContext>();
        dbContext.Database.Migrate();
    }
}
