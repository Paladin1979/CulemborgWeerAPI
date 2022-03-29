using CulemborgWeerAPI.Data.Entities;

namespace CulemborgWeerAPI.WttrIn
{
    public interface IWttrInClient
    {
        WeatherInformation GetCurrentWeatherForCulemborg();
    }
}