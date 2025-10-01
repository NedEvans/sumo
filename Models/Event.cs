using System.ComponentModel.DataAnnotations;

namespace Sumo.Models;

public class Event
{
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    public ICollection<Fight> Fights { get; set; } = [];
}