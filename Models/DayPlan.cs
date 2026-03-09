using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelPlanApi.Models;

public class DayPlan
{
    public int Id { get; set; }
    public int Day { get; set; }
    public string Title { get; set; } = string.Empty;

    public List<string> Activities { get; set; } = new();

    public int ItineraryId { get; set; }

    [JsonIgnore]
    public Itinerary? Itinerary { get; set; }
}