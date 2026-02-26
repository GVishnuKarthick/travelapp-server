using System;

namespace TravelPlanApi.Models;

public class Memory
{
    public int Id { get; set; }

    // 🔥 Link to Itinerary
    public int TripId { get; set; }

    public string Type { get; set; } = "photo"; 
    // "photo" OR "journal"

    public string? ImageUrl { get; set; }
    public string? Caption { get; set; }

    public string? Note { get; set; }
    public string? Mood { get; set; }
    public string? Highlight { get; set; }

    public int? DayNumber { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public string? UserProfileId { get; set; }

    public UserProfile? UserProfile { get; set; }
}