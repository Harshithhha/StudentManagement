using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.Dtos;

public record UpdateStudentDto(
     [Required][StringLength(50)] string Name,
    [Required] DateOnly DOB,
    [Required][Range(0, 10)] double GPA
);