namespace AthleteDashboard.Api.Models.Strava;

public class StravaAthlete
{
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Profile { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? Sex { get; set; }

    public int? FollowerCount { get; set; }

    public int? FriendCount { get; set; }
}