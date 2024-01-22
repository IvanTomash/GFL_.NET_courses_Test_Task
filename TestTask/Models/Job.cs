using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestTask.Models;

public class Job
{
    public int Id { get; set; }

    public string? Position { get; set; }

    public int Experience { get; set; }
}
