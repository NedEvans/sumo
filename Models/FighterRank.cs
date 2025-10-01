using System.ComponentModel.DataAnnotations;

namespace Sumo.Models;

public class FighterRank
{
    public int Id { get; set; }

    [Required]
    public int FighterId { get; set; }
    public Fighter Fighter { get; set; } = null!;

    [Required]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Required]
    public int RankId { get; set; }
    public Rank Rank { get; set; } = null!;

    [Required]
    public int Weight { get; set; }
}
