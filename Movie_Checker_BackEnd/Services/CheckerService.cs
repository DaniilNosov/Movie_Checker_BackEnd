using System.Net;
using System.Net.Http;

namespace Movie_Checker_BackEnd.Services;

public class CheckerService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CheckerService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> CheckMovieAsync(string movieName)
    {
        var apiKey = _configuration["ApiKey"];
        var encodedName = WebUtility.UrlEncode(movieName);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://ott-details.p.rapidapi.com/search?title={encodedName}&page=1"),
            Headers =
    {
        { "X-RapidAPI-Key", $"{apiKey}" },
        { "X-RapidAPI-Host", "ott-details.p.rapidapi.com" },
    },
        };
        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
