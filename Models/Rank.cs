using System.ComponentModel.DataAnnotations;

namespace Sumo.Models;

public class Rank
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int HierarchyLevel { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    public ICollection<FighterRank> FighterRanks { get; set; } = [];
}