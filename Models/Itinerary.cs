using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TravelPlanApi.Models;

namespace TravelPlanApi.Models;
public class Itinerary
{
    public int Id { get; set; }

    public string Destination { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Budget { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public int Activities { get; set; }

    public int Days { get; set; }

    public List<DayPlan> DayPlans { get; set; } = new();

    public List<Memory> Memories { get; set; } = new();

    public string UserProfileId { get; set; } = string.Empty;

    [JsonIgnore]   // ⭐ ADD THIS
    public UserProfile? UserProfile { get; set; }
}