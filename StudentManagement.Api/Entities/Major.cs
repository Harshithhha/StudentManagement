namespace StudentManagement.Api.Entities;

public class Major
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    private Major() { }
    public Major(int code, string name)
    {
        ID = code;
        Name = name;
    }

    public override string ToString()
    {
        return $"{ID} - {Name}";
    }
}