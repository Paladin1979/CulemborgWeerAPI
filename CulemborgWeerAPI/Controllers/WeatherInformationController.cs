using CulemborgWeerAPI.Data;
using CulemborgWeerAPI.Data.Entities;
using CulemborgWeerAPI.WttrIn;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CulemborgWeerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherInformationController : ControllerBase
    {
        private readonly ILogger<WeatherInformationController> _logger;
        private readonly ICulemborgWeerRepository _repository;
        private readonly IWttrInClient _wttrClient;
        private readonly IConfiguration _config;
        private readonly Random _randomizer;

        public WeatherInformationController(ILogger<WeatherInformationController> logger, ICulemborgWeerRepository repository, IWttrInClient wttrClient, IConfiguration config)
        {
            _logger = logger;
            _repository = repository;
            _wttrClient = wttrClient;
            _config = config;
            _randomizer = new Random();
        }

        [HttpGet("lunch")]
        public ActionResult<bool> IsTheWeatherNiceInCulemborgForLunchOutsideToday()
        {
            try
            {
                var minimumTemperature = ReadDoubleFromNiceWeatherSection("MinimumTemperature");
                var maximumHumidityPercentage = ReadDoubleFromNiceWeatherSection("MaximumHumidityPercentage");
                var maximumCloudPercentage = ReadDoubleFromNiceWeatherSection("MaximumCloudPercentage");
                var maximumWindSpeed = ReadDoubleFromNiceWeatherSection("MaximumWindSpeed");

                var weather = _wttrClient.GetCurrentWeatherForCulemborg();
                if (weather.TemperatureFeelsLike < minimumTemperature ||
                    weather.HumidityPercentage > maximumHumidityPercentage ||
                    weather.CloudPercentage > maximumCloudPercentage ||
                    weather.WindSpeed > maximumWindSpeed)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to determine if it was nice weather: {ex}");
                return BadRequest("Failed to determine if it was nice weather");
            }
        }

        private double ReadDoubleFromNiceWeatherSection(string propertyName)
        {
            return double.Parse(_config.GetSection("NiceWeatherForLunchOutside")[propertyName], System.Globalization.CultureInfo.InvariantCulture);
        }

        [HttpGet("from-until")]
        public ActionResult<IEnumerable<WeatherInformation>> GetWeatherHistoryForCulemborgBetweenDates(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return BadRequest("startDate and endDate should be specified");
            }

            try
            {
                // temporary hack to generate some random data
                var data = _repository.GetHistoryBetweenDates(startDate.Value, endDate.Value);
                if (!data.Any())
                {
                    GenerateRandomDataBetweenDates(startDate.Value, endDate.Value);
                    data = _repository.GetHistoryBetweenDates(startDate.Value, endDate.Value);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get history for period: {ex}");
                return BadRequest("Failed to get history for period");
            }
        }

        private void GenerateRandomDataBetweenDates(DateTime startDate, DateTime endDate)
        {
            var date = startDate.Date.AddHours(startDate.Hour);
            if (startDate.Minute > 0) date = date.AddMinutes(30);
            if (startDate.Minute > 30) date = date.AddMinutes(30);

            while (date < endDate)
            {
                if (IsWorkingHour(date))
                {
                    var data = GenerateRandomDataForDate(date);
                    _repository.StoreHistoricalData(data);
                }

                date = date.AddMinutes(30);
            }
        }

        private WeatherInformation GenerateRandomDataForDate(DateTime date)
        {
            var data = new WeatherInformation()
            {
                CloudPercentage = _randomizer.NextDouble() * 100,
                Date = date,
                HumidityPercentage = _randomizer.NextDouble() * 100,
                Temperature = _randomizer.NextDouble() * 40 - 5,
                WindDirection = _randomizer.NextDouble() * 360,
                WindSpeed = _randomizer.NextDouble() * 80
            };
            data.TemperatureFeelsLike = data.Temperature - data.WindSpeed / 10;
            return data;
        }

        private bool IsWorkingHour(DateTime dateTime)
        {
            if (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday) return false;
            if (dateTime.TimeOfDay < new TimeSpan(6, 0, 0)) return false;
            if (dateTime.TimeOfDay > new TimeSpan(19, 0, 0)) return false;
            return true;
        }

        [HttpGet("fullhistory")]
        public ActionResult<IEnumerable<WeatherInformation>> GetFullWeatherHistoryForCulemborg()
        {
            try
            {
                return Ok(_repository.GetFullHistory());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get data dump: {ex}");
                return BadRequest("Failed to get data dump");
            }
        }
    }
}
