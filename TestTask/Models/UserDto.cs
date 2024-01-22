namespace TestTask.Models;

public class UserDto
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int Age { get; set; }

    public Job Job { get; set; } = new Job();

    public Phone Phones { get; set; } = new Phone();
}
