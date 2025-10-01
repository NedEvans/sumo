using System.ComponentModel.DataAnnotations;

namespace Sumo.Models;

public class WinMethod
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty; //english name

    [Required]
    [MaxLength(50)]
    public string NameJapanese { get; set; } = string.Empty; //japanese name

    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty; // e.g., "Throw", "Push", "Pull", "Special"

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    public ICollection<Fight> Fights { get; set; } = [];
}