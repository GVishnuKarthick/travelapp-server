using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlanApi.Data;
using TravelPlanApi.Models;
using System.Security.Claims;

namespace TravelPlanApi.Controllers
{
    [ApiController]
    [Route("api/itineraries")]
    [Authorize]
    public class ItinerariesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItinerariesController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================
        // GET: api/itineraries
        // ==========================
       [HttpGet]
public async Task<ActionResult<IEnumerable<Itinerary>>> GetItineraries()
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null)
        return Unauthorized();

  var itineraries = await _context.Itineraries.ToListAsync();

    return Ok(itineraries);
}

        // ==========================
        // GET: api/itineraries/{id}
        // ==========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Itinerary>> GetItinerary(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var itinerary = await _context.Itineraries
                .Include(i => i.DayPlans)
                .FirstOrDefaultAsync(i => i.Id == id && i.UserProfileId == userId);

            if (itinerary == null)
                return NotFound();

            return Ok(itinerary);
        }

        // ==========================
        // POST: api/itineraries
        // ==========================
    [HttpPost]
public async Task<ActionResult<Itinerary>> PostItinerary(Itinerary itinerary)
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null)
        return Unauthorized();

    // Assign current user
    itinerary.UserProfileId = userId;

    // FIX: convert DateTimes to UTC
    itinerary.StartDate = DateTime.SpecifyKind(itinerary.StartDate, DateTimeKind.Utc);
    itinerary.EndDate = DateTime.SpecifyKind(itinerary.EndDate, DateTimeKind.Utc);

    // Calculate days automatically
    itinerary.Days = Math.Max(1, (itinerary.EndDate - itinerary.StartDate).Days);

    _context.Itineraries.Add(itinerary);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetItinerary), new { id = itinerary.Id }, itinerary);
}

        // ==========================
        // PUT: api/itineraries/{id}
        // ==========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItinerary(int id, Itinerary itinerary)
        {
            if (id != itinerary.Id)
                return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var existing = await _context.Itineraries
                .FirstOrDefaultAsync(i => i.Id == id && i.UserProfileId == userId);

            if (existing == null)
                return NotFound();

            // Update fields
            existing.Destination = itinerary.Destination;
            existing.Description = itinerary.Description;
            existing.Budget = itinerary.Budget;
            existing.StartDate = DateTime.SpecifyKind(itinerary.StartDate, DateTimeKind.Utc);
            existing.EndDate = DateTime.SpecifyKind(itinerary.EndDate, DateTimeKind.Utc);
            existing.Image = itinerary.Image;
            existing.Activities = itinerary.Activities;

            // Automatically calculate days
            existing.Days = Math.Max(1, (existing.EndDate - existing.StartDate).Days);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ==========================
        // DELETE: api/itineraries/{id}
        // ==========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItinerary(int id)
        {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

var itinerary = await _context.Itineraries
    .FirstOrDefaultAsync(i => i.Id == id && i.UserProfileId == userId);

            if (itinerary == null)
                return NotFound();

            _context.Itineraries.Remove(itinerary);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}