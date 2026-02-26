namespace TravelPlanApi.Models;

public class DayPlan
{
    public int Id { get; set; }
    public int Day { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> Activities { get; set; } = new List<string>();
    public int ItineraryId { get; set; } // Foreign key
    public Itinerary? Itinerary { get; set; } // Navigation
}