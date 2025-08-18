using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.Entities;

public class Student
{
[Key]    public string RollNo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string MajorId { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public DateOnly DOB { get; set; }
    public double GPA { get; set; }

    
       private Student() { }
}