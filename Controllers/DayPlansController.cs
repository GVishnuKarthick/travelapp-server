using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlanApi.Models;
using TravelPlanApi.Data;

namespace TravelPlanApi.Controllers
{
    [ApiController]
    [Route("api/itineraries/{itineraryId}/dayplans")]
    public class DayPlansController : ControllerBase
    {
        private readonly AppDbContext _context;   // ✅ FIXED

        public DayPlansController(AppDbContext context)   // ✅ FIXED
        {
            _context = context;
        }

        // ✅ CREATE DAY PLAN
        [HttpPost]
        public async Task<IActionResult> CreateDayPlan(int itineraryId, [FromBody] DayPlan dayPlan)
        {
            var itinerary = await _context.Itineraries
                .Include(i => i.DayPlans)
                .FirstOrDefaultAsync(i => i.Id == itineraryId);

            if (itinerary == null)
                return NotFound("Itinerary not found");

            dayPlan.ItineraryId = itineraryId;

            _context.DayPlans.Add(dayPlan);
            await _context.SaveChangesAsync();

            return Ok(dayPlan);
        }

        // ✅ DELETE DAY PLAN
        [HttpDelete("{dayPlanId}")]
        public async Task<IActionResult> DeleteDayPlan(int itineraryId, int dayPlanId)
        {
            var dayPlan = await _context.DayPlans
                .FirstOrDefaultAsync(dp =>
                    dp.Id == dayPlanId &&
                    dp.ItineraryId == itineraryId);

            if (dayPlan == null)
                return NotFound();

            _context.DayPlans.Remove(dayPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{dayPlanId}")]
public async Task<IActionResult> UpdateDayPlan(int itineraryId, int dayPlanId, [FromBody] DayPlan updatedPlan)
{
    var dayPlan = await _context.DayPlans
        .FirstOrDefaultAsync(dp => dp.Id == dayPlanId && dp.ItineraryId == itineraryId);

    if (dayPlan == null)
        return NotFound();

    dayPlan.Title = updatedPlan.Title;
    dayPlan.Activities = updatedPlan.Activities;

    await _context.SaveChangesAsync();

    return Ok(dayPlan);
}
        // ✅ GET ALL DAY PLANS
        [HttpGet]
        public async Task<IActionResult> GetDayPlans(int itineraryId)
        {
            var dayPlans = await _context.DayPlans
                .Where(dp => dp.ItineraryId == itineraryId)
                .OrderBy(dp => dp.Day)
                .ToListAsync();

            return Ok(dayPlans);
        }
    }
}