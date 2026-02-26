using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlanApi.Data;
using TravelPlanApi.Models;

[ApiController]
[Route("api/memories")]
[Authorize]
public class MemoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MemoriesController(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL USER MEMORIES
    // =========================
    [HttpGet]
    public async Task<IActionResult> GetMemories()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var memories = await _context.Memories
            .Where(m => m.UserProfileId == userId)
            .OrderByDescending(m => m.CreatedDate)
            .ToListAsync();

        return Ok(memories);
    }

    // =========================
    // ADD MEMORY
    // =========================
    [HttpPost]
    public async Task<IActionResult> PostMemory([FromBody] Memory memory)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        memory.UserProfileId = userId;
        memory.CreatedDate = DateTime.UtcNow;

        _context.Memories.Add(memory);
        await _context.SaveChangesAsync();

        return Ok(memory);
    }

    // =========================
    // UPDATE MEMORY
    // =========================
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMemory(int id, [FromBody] Memory memory)
    {
        if (id != memory.Id)
            return BadRequest("Memory ID mismatch");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var existing = await _context.Memories
            .FirstOrDefaultAsync(m => m.Id == id && m.UserProfileId == userId);

        if (existing == null)
            return NotFound();

        existing.TripId = memory.TripId;
        existing.Type = memory.Type;
        existing.ImageUrl = memory.ImageUrl;
        existing.Caption = memory.Caption;
        existing.Note = memory.Note;
        existing.Mood = memory.Mood;
        existing.Highlight = memory.Highlight;
        existing.DayNumber = memory.DayNumber;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // =========================
    // DELETE SINGLE MEMORY
    // =========================
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMemory(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var memory = await _context.Memories
            .FirstOrDefaultAsync(m => m.Id == id && m.UserProfileId == userId);

        if (memory == null)
            return NotFound();

        _context.Memories.Remove(memory);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // =========================
    // DELETE ALL MEMORIES OF A TRIP
    // =========================
    [HttpDelete("trip/{tripId}")]
    public async Task<IActionResult> DeleteTripMemories(int tripId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var memories = await _context.Memories
            .Where(m => m.TripId == tripId && m.UserProfileId == userId)
            .ToListAsync();

        if (!memories.Any())
            return NotFound();

        _context.Memories.RemoveRange(memories);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}