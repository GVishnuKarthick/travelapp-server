using System;
using System.Collections.Generic;

namespace TravelPlanApi.Models
{
    public class Itinerary
    {
        public int Id { get; set; }

        public string Destination { get; set; } = string.Empty;

        // ✅ Proper date storage
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // ✅ Existing fields (DO NOT REMOVE)
        public string Description { get; set; } = string.Empty;

        public string Budget { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public int Activities { get; set; }

        public int Days { get; set; }

        // ✅ Relationships
        public List<DayPlan> DayPlans { get; set; } = new();

        public List<Memory> Memories { get; set; } = new();

        public string UserProfileId { get; set; } = string.Empty;

        public UserProfile? UserProfile { get; set; }
    }
}