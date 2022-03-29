using System;
using System.ComponentModel.DataAnnotations;

namespace CulemborgWeerAPI.Data.Entities
{
    public class WeatherInformation
    {
        [Key]
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature in degrees Celsius.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// What the temperature actually feels like in degrees Celsius.
        /// </summary>
        public double TemperatureFeelsLike { get; set; }

        /// <summary>
        /// Wind speed in km/h.
        /// </summary>
        public double WindSpeed { get; set; }

        /// <summary>
        /// Wind direction in degrees. 0 is north, 90 is east, 180 is south, 270 is west.
        /// </summary>
        public double WindDirection { get; set; }

        public double CloudPercentage { get; set; }

        public double HumidityPercentage { get; set; }
    }
}
