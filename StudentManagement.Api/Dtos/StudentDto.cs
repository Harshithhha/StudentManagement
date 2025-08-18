using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.Dtos;

public record StudentDto(
    [Required]string RollNo,
    [Required][StringLength(50)] string Name,  
    [Required][StringLength(20)]string Major,
    [Required]DateOnly DOB,
    [Required][Range(0,10)]double GPA
);