using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TravelPlanApi.Models;

namespace TravelPlanApi.Controllers;

[ApiController]
[Route("api/profile")]
//[Authorize]
public class ProfileController : ControllerBase
{
    private readonly UserManager<UserProfile> _userManager;

    public ProfileController(UserManager<UserProfile> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<UserProfile>> GetProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        return user;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UserProfile updatedProfile)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        user.Name = updatedProfile.Name;
        user.Bio = updatedProfile.Bio;
        user.Location = updatedProfile.Location;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}
