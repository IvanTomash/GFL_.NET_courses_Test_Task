using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestTask.Models;

public class Phone
{
    public int Id { get; set; }

    public string? Mobile { get; set; }

    public string? Home { get; set; }
}
