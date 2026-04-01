using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelPlanApi.Models;
namespace TravelPlanApi.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<UserProfile> _userManager;
    private readonly SignInManager<UserProfile> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new UserProfile { UserName = model.Email, Email = model.Email, Name = model.FullName };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded) return Ok();
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        if (!result.Succeeded) return Unauthorized();

        // Use FindByNameAsync since UserName and Email are consistent in this app
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null) return Unauthorized();

        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

private string GenerateJwtToken(UserProfile user)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id), // IMPORTANT
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
    );

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
}

public class RegisterModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}