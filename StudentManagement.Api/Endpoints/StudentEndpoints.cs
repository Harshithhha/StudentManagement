namespace StudentManagement.Api.Endpoints;
using StudentManagement.Api.Dtos;
public static class StudentEndpoints
{
    // This class can be used to define additional endpoints related to student management.
    // Currently, the endpoints are defined in the Program.cs file.
    // Sample in-memory student list
    private static readonly List<StudentDto> students = new List<StudentDto>
{

    new("CS101", "Alice Johnson", "Computer Science", new DateOnly(2003, 5, 12), 9.1),
    new("ME101", "Bob Smith", "Mechanical Engineering", new DateOnly(2002, 8, 20), 8.5),
    new("EE101", "Charlie Brown", "Electrical Engineering", new DateOnly(2003, 1, 30), 8.8),
    new("CS102", "Diana Prince", "Computer Science", new DateOnly(2003, 3, 18), 9.3),
    new("IT101", "Ethan Hunt", "Information Technology", new DateOnly(2002, 12, 5), 8.9),
    new("CE101", "Fiona Gallagher", "Civil Engineering", new DateOnly(2003, 7, 22), 8.6),
    new("ME102", "George Martin", "Mechanical Engineering", new DateOnly(2002, 11, 14), 8.7),
    new("CS103", "Hannah Lee", "Computer Science", new DateOnly(2003, 6, 30), 9.0),
    new("EE101", "Ian Curtis", "Electrical Engineering", new DateOnly(2002, 9, 10), 8.4),
    new("IT102", "Julia Roberts", "Information Technology", new DateOnly(2003, 4, 2), 9.2)

};

    public static RouteGroupBuilder MapStudentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/students");
        // GET all students
        group.MapGet("/", () =>
        {
            return students;
        });

     

        // GET student by ID
        group.MapGet("/{rollno}", (string rollno) =>
        {
            var student = students.Find(s => s.RollNo == rollno);
            return student is not null ? Results.Ok(student) : Results.NotFound();
        });

        // POST new student
        // Dictionary to track last number used for each major
        Dictionary<string, int> majorCounters = new Dictionary<string, int>();

        group.MapPost("/", (CreateStudentDto studentDto) =>
        {
            // Generate major code (first letter of each word, uppercase)
            if (string.IsNullOrWhiteSpace(studentDto.Major))
                return Results.BadRequest("Major cannot be empty.");
            if (string.IsNullOrWhiteSpace(studentDto.Name))
                return Results.BadRequest("Name cannot be empty.");
            if (studentDto.DOB == default)
                return Results.BadRequest("DOB cannot be empty.");

            foreach (var s in students)
            {
                // Generate major code from student's Major
                string existingMajorCode = string.Join("", s.Major
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => char.ToUpper(word[0])));

                // Extract numeric part from RollNo
                string numericPart = new string(s.RollNo.SkipWhile(c => !char.IsDigit(c)).ToArray());
                if (int.TryParse(numericPart, out int number))
                {
                    if (!majorCounters.ContainsKey(existingMajorCode))
                        majorCounters[existingMajorCode] = number;
                    else
                        majorCounters[existingMajorCode] = Math.Max(majorCounters[existingMajorCode], number);
                }
            }
            string majorCode = string.Join("", studentDto.Major
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => char.ToUpper(word[0])));

            // Get next serial number for this major
            if (!majorCounters.ContainsKey(majorCode))
                majorCounters[majorCode] = 101; // starting number
            else
                majorCounters[majorCode]++;

            string rollNo = $"{majorCode}{majorCounters[majorCode]}";

            var newStudent = new StudentDto(
                rollNo,
                studentDto.Name,
                studentDto.Major,
                studentDto.DOB,
                studentDto.GPA
            );

            students.Add(newStudent);
            return Results.Created($"/students/{newStudent.RollNo}", newStudent);
        })
        .WithParameterValidation();


        // PUT update student
        group.MapPut("/{rollNo}", (string rollNo, UpdateStudentDto studentDto) =>
        {
            var student = students.Find(s => s.RollNo == rollNo);
            if (student is null)
                return Results.NotFound();

            var updatedStudent = new StudentDto(
                rollNo,
                studentDto.Name,
                student.Major, // Major remains unchanged
                studentDto.DOB,
                studentDto.GPA
            );

            students[students.FindIndex(s => s.RollNo == rollNo)] = updatedStudent;
            return Results.Ok(updatedStudent);
        })
        .WithParameterValidation();

        // DELETE student
        group.MapDelete("/{rollNo}", (string rollNo) =>
        {
            var student = students.Find(s => s.RollNo == rollNo);
            if (student is null)
                return Results.NotFound();

            students.Remove(student);
            return Results.NoContent();
        });
        return group;
    }

}
