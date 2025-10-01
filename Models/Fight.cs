using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sumo.Models;

public class Fight
{
    public int Id { get; set; }

    [Required]
    public int FighterEastId { get; set; }

    [ForeignKey(nameof(FighterEastId))]
    public Fighter FighterEast { get; set; } = null!;

    [Required]
    public int FighterWestId { get; set; }

    [ForeignKey(nameof(FighterWestId))]
    public Fighter FighterWest { get; set; } = null!;

    [Required]
    public int WinnerId { get; set; }

    [ForeignKey(nameof(WinnerId))]
    public Fighter Winner { get; set; } = null!;

    [Required]
    public int WinMethodId { get; set; }
    public WinMethod Method { get; set; } = null!;

    [Required]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Required]
    [Range(1, 15)]
    public int DayInEvent { get; set; }
}
