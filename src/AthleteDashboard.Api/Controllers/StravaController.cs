using AthleteDashboard.Api.Repositories;
using AthleteDashboard.Api.Services;
using AthleteDashboard.Api.Models.Strava;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AthleteDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StravaController : ControllerBase
{
    private readonly StravaService _stravaService;
    private readonly AthleteRepository _athleteRepository;

    public StravaController(StravaService stravaService, AthleteRepository athleteRepository)
    {
        _stravaService = stravaService;
        _athleteRepository = athleteRepository;
    }

    [HttpGet("athlete")]
    public async Task<IActionResult> GetAthlete([FromQuery] int athleteId)
    {
        if (athleteId <= 0)
        {
            return BadRequest(new { Error = "Athlete ID is required." });
        }

        var athlete = await _athleteRepository.GetAthleteAsync(athleteId);

        if (athlete is null)
        {
            return NotFound(new { Error = $"Athlete {athleteId} was not found." });
        }

        if (string.IsNullOrWhiteSpace(athlete.StravaToken))
        {
            return BadRequest(new { Error = $"No Strava token has been stored for athlete {athleteId}." });
        }

        var stravaAthlete = await _stravaService.GetAthleteAsync(athlete.StravaToken);

        return Ok(stravaAthlete);
    }

    [HttpGet("connect")]
    public IActionResult Connect()
    {
        var authorizationUrl = _stravaService.GetAuthorizationUrl();
        return Redirect(authorizationUrl);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(
        [FromQuery] string? code,
        [FromQuery] string? scope,
        [FromQuery] string? error)
    {
        if (!string.IsNullOrWhiteSpace(error))
        { return BadRequest(new { Error = error }); }

        if (string.IsNullOrWhiteSpace(code))
        { return BadRequest(new { Error = "No authorization code was returned by Strava." }); }

        var tokenResponse = await _stravaService.ExchangeCodeAsync(code);

        return Ok(new
        {
            tokenResponse.Athlete,
            tokenResponse.ExpiresAt,
            tokenResponse.ExpiresIn,
            tokenResponse.RefreshToken,
            tokenResponse.AccessToken,
            Scope = scope
        });
    }
}