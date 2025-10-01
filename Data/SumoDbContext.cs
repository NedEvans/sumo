using Microsoft.EntityFrameworkCore;
using Sumo.Models;

namespace Sumo.Data;

public class SumoDbContext : DbContext
{
    public SumoDbContext(DbContextOptions<SumoDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Fighter> Fighters { get; set; }
    public DbSet<Fight> Fights { get; set; }
    public DbSet<Rank> Ranks { get; set; }
    public DbSet<WinMethod> WinMethods { get; set; }
    public DbSet<FighterRank> FighterRanks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var fightBuilder = modelBuilder.Entity<Fight>();

        fightBuilder.HasOne(f => f.FighterEast)
            .WithMany(fighter => fighter.FightsAsFighterEast)
            .HasForeignKey(f => f.FighterEastId)
            .HasConstraintName("FK_Fights_Fighters_FighterEastId")
            .OnDelete(DeleteBehavior.Restrict);

        fightBuilder.HasOne(f => f.FighterWest)
            .WithMany(fighter => fighter.FightsAsFighterWest)
            .HasForeignKey(f => f.FighterWestId)
            .HasConstraintName("FK_Fights_Fighters_FighterWestId")
            .OnDelete(DeleteBehavior.Restrict);

        fightBuilder.HasOne(f => f.Winner)
            .WithMany(fighter => fighter.Wins)
            .HasForeignKey(f => f.WinnerId)
            .HasConstraintName("FK_Fights_Fighters_WinnerId")
            .OnDelete(DeleteBehavior.Restrict);

        fightBuilder.HasOne(f => f.Event)
            .WithMany(e => e.Fights)
            .HasForeignKey(f => f.EventId)
            .HasConstraintName("FK_Fights_Events_EventId")
            .OnDelete(DeleteBehavior.Restrict);

        // Fixed naming: Fight.Method (not Fight.WinMethod)
        fightBuilder.HasOne(f => f.Method)
            .WithMany(wm => wm.Fights)
            .HasForeignKey(f => f.WinMethodId)
            .HasConstraintName("FK_Fights_WinMethods_WinMethodId")
            .OnDelete(DeleteBehavior.Restrict);

        // --- Unique indexes ---
        modelBuilder.Entity<Rank>()
            .HasIndex(r => r.Name)
            .IsUnique();

        modelBuilder.Entity<WinMethod>()
            .HasIndex(wm => wm.Name)
            .IsUnique();

        // --- FighterRank relationships ---
        modelBuilder.Entity<FighterRank>()
            .HasOne(fr => fr.Fighter)
            .WithMany(f => f.FighterRanks)
            .HasForeignKey(fr => fr.FighterId)
            .OnDelete(DeleteBehavior.Restrict); // switched from Cascade

        modelBuilder.Entity<FighterRank>()
            .HasOne(fr => fr.Event)
            .WithMany() // no collection navigation on Event
            .HasForeignKey(fr => fr.EventId)
            .OnDelete(DeleteBehavior.Restrict); // switched from Cascade

        modelBuilder.Entity<FighterRank>()
            .HasOne(fr => fr.Rank)
            .WithMany(r => r.FighterRanks)
            .HasForeignKey(fr => fr.RankId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ensure unique fighter-event combination
        modelBuilder.Entity<FighterRank>()
            .HasIndex(fr => new { fr.FighterId, fr.EventId })
            .IsUnique();

        // --- Additional performance indexes ---
        modelBuilder.Entity<Fight>()
            .HasIndex(f => f.EventId);

        modelBuilder.Entity<Fight>()
            .HasIndex(f => f.WinnerId);

        modelBuilder.Entity<FighterRank>()
            .HasIndex(fr => fr.EventId);
    }
}
