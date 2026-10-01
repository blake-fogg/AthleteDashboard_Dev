using AthleteDashboard.Api.Models;
using AthleteDashboard.Api.Repositories;

namespace AthleteDashboard.Api.Services;

public class AthleteService
{
    private readonly AthleteRepository _athleteRepository;

    public AthleteService(AthleteRepository athleteRepository)
    {
        _athleteRepository = athleteRepository;
    }

    public Task<Athlete?> GetAthleteAsync(int athleteId)
    {
        return _athleteRepository.GetAthleteAsync(athleteId);
    }
    
    public async Task<IEnumerable<Athlete>> GetAthletesAsync()
    {
        return await _athleteRepository.GetAllAthletesAsync();
    }
}