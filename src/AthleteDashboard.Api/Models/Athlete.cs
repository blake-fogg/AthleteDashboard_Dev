namespace AthleteDashboard.Api.Models;

public class Athlete
{
    public int AthleteID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? StravaToken { get; set; }
}