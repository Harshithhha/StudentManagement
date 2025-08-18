using Microsoft.EntityFrameworkCore;
namespace StudentManagement.Api.Data;
using StudentManagement.Api.Entities;
public class StudentManagementContext(DbContextOptions<StudentManagementContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Major> Majors => Set<Major>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Major>()
            .HasData(
                new { ID = 1, Name = "Computer Science" },
                new { ID = 2, Name = "Information Technology" },
                new { ID = 3, Name = "Electrical Engineering" }
            );

    }
}