using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestTask.Models;

public class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int Age { get; set; }

    public int? JobId { get; set; }

    public Job Job { get; set; } = new Job();

    public int? PhonesId { get; set; }

    public Phone Phones { get; set; } = new Phone();
}
