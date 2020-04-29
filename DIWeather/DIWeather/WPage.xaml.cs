using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WPage : ContentPage
    {
        public WPage()
        {
            InitializeComponent();
            LBLCityName.Text = $"{WeatherGV.CityName}";
            LBLCurTemp.Text = $"{WeatherGV.CurTemp}°F";
            LBLLowTemp.Text = $"{WeatherGV.LowTemp}°F";
            LBLHighTemp.Text = $"{WeatherGV.HighTemp}°F";
            LBLHumidity.Text = $"{WeatherGV.Humidity}%";
            LBLPressure.Text = $"{WeatherGV.Pressure} atm";
            LBLWindDirection.Text = $"{WeatherGV.WindDirection}";
            LBLWindSpeed.Text = $"{WeatherGV.WindSpeed} m.p.h";
            LBLSunrise.Text = $"{WeatherGV.SunriseTime}";
            LBLSunset.Text = $"{WeatherGV.SunsetTime}";
            SetBackgroundTempColor(WeatherGV.CurTemp);
        }

        /// <summary>
        /// Method to determine the background color of the WPage based on temperature. 
        /// </summary>
        /// <param name="currentTemp"></param>
        private void SetBackgroundTempColor(string currentTemp)
        {
            float tempInt = float.Parse(currentTemp);
            if (tempInt >= 90.0)
            {
                WeatherBackgroundColor.BackgroundColor = Color.IndianRed;
            }
            if (tempInt >= 75.0 && tempInt < 90.0)
            {
                WeatherBackgroundColor.BackgroundColor = Color.Gold;
            }
            if (tempInt < 75.0)
            {
                WeatherBackgroundColor.BackgroundColor = Color.SkyBlue;
            }
        }

    }
}