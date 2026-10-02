namespace AthleteDashboard.Api.Models.Strava;

public class StravaTokenResponse
{
    public string TokenType { get; set; } = string.Empty;
    public long ExpiresAt { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public StravaAthlete? Athlete { get; set; }
}