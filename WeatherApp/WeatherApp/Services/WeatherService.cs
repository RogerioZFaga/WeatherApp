using System.Net.Http.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherService
{
    private const string ApiKey = "e74f4c6280d9832d1699ccba88c336b2";
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse?> GetWeatherAsync(double latitude, double longitude, string units = "metric", string lang = "pt")
    {
        var url = $"{BaseUrl}?lat={latitude}&lon={longitude}&units={units}&lang={lang}&appid={ApiKey}";
        try
        {
            return await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar dados: {ex.Message}");
            return null;
        }
    }
}

