using Microsoft.EntityFrameworkCore;
namespace StudentManagement.Api.Data;
using StudentManagement.Api.Entities;
public class StudentManagementContext(DbContextOptions<StudentManagementContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Major> Majors => Set<Major>();

}