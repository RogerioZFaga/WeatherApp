namespace WeatherApp.Models;

public class WeatherResponse
{
    public MainWeather Main { get; set; } = new MainWeather();
    public List<WeatherDescription> Weather { get; set; } = new List<WeatherDescription>();
}

public class MainWeather
{
    public double Temp { get; set; }
}

public class WeatherDescription
{
    public string Description { get; set; } = string.Empty;
}
