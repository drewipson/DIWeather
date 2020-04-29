using System;
using System.Collections.Generic;
using System.Text;

namespace DIWeather
{
    public static class WeatherGV
    {
        /// <summary>
        /// Public class variables to store values from JSON and display them for user.
        /// </summary>
        public static string CityName { get; set; }
        public static string CurTemp { get; set; }
        public static string HighTemp { get; set; }
        public static string LowTemp { get; set; }
        public static string WindSpeed { get; set; }
        public static string WindDirection { get; set; }
        public static string Pressure { get; set; }
        public static string Humidity { get; set; }
        public static string SunriseTime { get; set; }
        public static string SunsetTime { get; set; }

    }
}
