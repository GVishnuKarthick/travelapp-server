using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelPlanApi.Models;

public class DayPlan
{
    public int Id { get; set; }

    public int Day { get; set; }

    public string Title { get; set; } = string.Empty;

    // Stored in DB
    public string ActivitiesJson { get; set; } = "[]";

    // Used in API
    [NotMapped]
    public List<string> Activities
    {
        get => JsonSerializer.Deserialize<List<string>>(ActivitiesJson) ?? new();
        set => ActivitiesJson = JsonSerializer.Serialize(value);
    }

    public int ItineraryId { get; set; }

    [JsonIgnore]
    public Itinerary? Itinerary { get; set; }
}