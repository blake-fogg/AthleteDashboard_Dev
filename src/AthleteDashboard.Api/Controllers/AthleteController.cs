using AthleteDashboard.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AthleteDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AthleteController : ControllerBase
{
    private readonly AthleteService _service;

    public AthleteController(AthleteService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAthletes()
    {
        var athletes = await _service.GetAthletesAsync();

        return Ok(athletes);
    }
}