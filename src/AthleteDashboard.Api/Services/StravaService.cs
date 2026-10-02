using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using AthleteDashboard.Api.Models.Strava;
using Microsoft.Extensions.Options;

namespace AthleteDashboard.Api.Services;

public class StravaService
{
    private readonly HttpClient _httpClient;
    private readonly StravaOptions _options;
    public StravaService(HttpClient httpClient, IOptions<StravaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }
    public string GetAuthorizationUrl()
    {
        var query = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["response_type"] = "code",
            ["redirect_uri"] = _options.RedirectUri,
            ["approval_prompt"] = "auto",
            // Start with read access. We can expand this later. 
            ["scope"] = "read,activity:read_all"
        };
        var queryString = string.Join("&", query.Select(pair => $"{Uri.EscapeDataString(pair.Key)}=" + $"{Uri.EscapeDataString(pair.Value)}"));
        return $"https://www.strava.com/oauth/authorize?{queryString}";
    }
    public async Task<StravaTokenResponse> ExchangeCodeAsync(string code)
    {
        var request = new FormUrlEncodedContent([new KeyValuePair<string, string>("client_id", _options.ClientId),
        new KeyValuePair<string, string>("client_secret", _options.ClientSecret),
        new KeyValuePair<string, string>("code", code),
        new KeyValuePair<string, string>("grant_type", "authorization_code")]);
        var response = await _httpClient.PostAsync("https://www.strava.com/oauth/token", request);
        response.EnsureSuccessStatusCode();
        var tokenResponse = await response.Content.ReadFromJsonAsync<StravaTokenResponse>();
        if (tokenResponse is null)
        { throw new InvalidOperationException("Strava returned an empty token response."); }
        return tokenResponse;
    }
    public async Task<StravaAthlete?> GetAthleteAsync(string accessToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://www.strava.com/api/v3/athlete");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.SendAsync(request); response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<StravaAthlete>();
    }
}