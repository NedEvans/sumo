using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sumo.Models;

public class Fighter
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Birthdate { get; set; }

    [Required]
    [MaxLength(100)]
    public string Stable { get; set; } = string.Empty;

    [Required]
    public bool Active { get; set; } = true;

    public byte[]? Image { get; set; }

    [InverseProperty(nameof(Fight.FighterEast))]
    public ICollection<Fight> FightsAsFighterEast { get; set; } = [];

    [InverseProperty(nameof(Fight.FighterWest))]
    public ICollection<Fight> FightsAsFighterWest { get; set; } = [];

    [InverseProperty(nameof(Fight.Winner))]
    public ICollection<Fight> Wins { get; set; } = [];

    [InverseProperty(nameof(FighterRank.Fighter))]
    public ICollection<FighterRank> FighterRanks { get; set; } = [];
}
