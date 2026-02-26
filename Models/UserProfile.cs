using Microsoft.AspNetCore.Identity;

namespace TravelPlanApi.Models;

public class UserProfile : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public List<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    public List<Memory> Memories { get; set; } = new List<Memory>();
}