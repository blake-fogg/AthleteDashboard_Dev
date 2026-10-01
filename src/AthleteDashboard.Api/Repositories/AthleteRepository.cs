using AthleteDashboard.Api.Data;
using AthleteDashboard.Api.Models;
using Dapper;

namespace AthleteDashboard.Api.Repositories;

public class AthleteRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public AthleteRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Athlete?> GetAthleteAsync(int athleteId)
    {
        const string sql = """
            SELECT
                AthleteID,
                FirstName,
                LastName
            FROM dbo.Athletes
            WHERE AthleteID = @AthleteID;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Athlete?>(sql, new { AthleteID = athleteId });
    }

    public async Task<IEnumerable<Athlete>> GetAllAthletesAsync()
    {
        const string sql = """
            SELECT
                AthleteID,
                FirstName,
                LastName
            FROM dbo.Athlete;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryAsync<Athlete>(sql);
    }
}