using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelPlanApi.Models;

namespace TravelPlanApi.Data;

public class AppDbContext : IdentityDbContext<UserProfile>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Itinerary> Itineraries { get; set; }
    public DbSet<DayPlan> DayPlans { get; set; }
    public DbSet<Memory> Memories { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Itinerary>()
        .HasMany(i => i.DayPlans)
        .WithOne(dp => dp.Itinerary)
        .HasForeignKey(dp => dp.ItineraryId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Itinerary>()
        .HasOne(i => i.UserProfile)
        .WithMany(u => u.Itineraries)
        .HasForeignKey(i => i.UserProfileId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Memory>()
        .HasOne(m => m.UserProfile)
        .WithMany(u => u.Memories)
        .HasForeignKey(m => m.UserProfileId)
        .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<DayPlan>()
        .Property(dp => dp.Activities)
        .HasColumnType("text[]");
}
}