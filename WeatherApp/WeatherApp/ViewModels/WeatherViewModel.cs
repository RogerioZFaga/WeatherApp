using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Services;
using WeatherApp.Models;

namespace WeatherApp.ViewModels;

public partial class WeatherViewModel : ObservableObject
{
    private readonly WeatherService _weatherService;

    [ObservableProperty]
    private string temperature = "Carregando...";

    [ObservableProperty]
    private string description = "Carregando...";

    [ObservableProperty]
    private bool isBusy;

    public WeatherViewModel()
    {
        _weatherService = new WeatherService(new HttpClient());
    }

    [RelayCommand]
    public async Task GetWeatherAsync()
    {
        if (IsBusy) return;

        IsBusy = true;
        try
        {
            var location = await Geolocation.Default.GetLocationAsync();
            if (location != null)
            {
                var weather = await _weatherService.GetWeatherAsync(location.Latitude, location.Longitude);
                if (weather != null)
                {
                    Temperature = $"{weather.Main.Temp}°C";
                    Description = weather.Weather.FirstOrDefault()?.Description ?? "Sem descrição";
                }
                else
                {
                    Temperature = "Erro ao obter clima.";
                    Description = "Tente novamente.";
                }
            }
            else
            {
                Temperature = "Localização não encontrada.";
                Description = "Verifique as permissões.";
            }
        }
        catch (Exception ex)
        {
            Temperature = "Erro ao buscar dados.";
            Description = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
