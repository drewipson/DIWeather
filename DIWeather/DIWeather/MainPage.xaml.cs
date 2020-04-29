using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using DIWeather.CityInfo;
using System.Globalization;

namespace DIWeather
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        // Instantiate dictionary to store city zipcodes.
        Dictionary<string, ZipCodes> cityDict = new Dictionary<string, ZipCodes>();
        public MainPage()
        {
            // Load content from CSV file into Class OBjects and store in dictionary. Add keys to the picker list to display to user. 
            InitializeComponent();
            LoadContent content = new LoadContent();
            cityDict = content.GetCSVContent();
            List<string> pickerItems = cityDict.Keys.ToList<string>();
            pckrCity.ItemsSource = pickerItems;
        }

        /// <summary>
        /// Check user input before sending GET request to open weather API. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        private void btnShowTemp_Clicked(object sender, EventArgs e)
        {
            if (pckrCity.SelectedIndex > -1)
            {
                iptZipCode.Text = "";
                cityDict.TryGetValue(pckrCity.SelectedItem.ToString(), out ZipCodes zip);
                OpenWeatherAPICall(zip.GetZipFromCity());
            }
            if (iptZipCode.Text != "")
            {
                pckrCity.SelectedIndex = -1;
                OpenWeatherAPICall(iptZipCode.Text.ToString());
            }
        }

        /// <summary>
        /// Method converts unix time returned from the API into formatted data string.
        /// </summary>
        /// <param name="timeValueUTC"></param>
        /// <returns></returns>
        private string DateTimeFromUnix(string timeValueUTC)
        {
            int tempUnix = int.Parse(timeValueUTC);
            return DateTimeOffset.FromUnixTimeSeconds(tempUnix).ToLocalTime().ToString("h:mm tt", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Method converts degree returned to cardinal directions: N, W, E, S.
        /// </summary>
        /// <param name="jsonDegree"></param>
        /// <returns></returns>
        private string GetCardinalDirections(string jsonDegree)
        {
            float degree = float.Parse(jsonDegree);
            string windDirection = "";

            if (degree >= 315 || degree <= 45)
            {
                windDirection = "N";
            }
            if (degree < 315 && degree >= 225)
            {
                windDirection = "W";
            }
            if (degree >= 135 && degree < 225)
            {
                windDirection = "S";
            }
            else
            {
                windDirection = "E";
            }

            return windDirection;
        }
        /// <summary>
        /// Method passes API Key and ZipCode to the OpenWeatherAPI and parses JSON string returned into varias methods and logic to determine display to the user. 
        /// </summary>
        /// <param name="zipCodeValue"></param>
        private void OpenWeatherAPICall(string zipCodeValue)
        {
            //  API KEY and CALL Values for the call using WebClient.
            string API_KEY = "70fa07a0d7e1b93955f1111dd935c600";
            string API_CALL = $"http://api.openweathermap.org/data/2.5/weather?zip={zipCodeValue}&appid={API_KEY}&units=imperial";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string JSON = wc.DownloadString(API_CALL);
                    // Parses string into JSON objects needed for dispaly.
                    JObject jo = JObject.Parse(JSON);
                    JObject main = JObject.Parse(jo["main"].ToString());
                    JObject wind = JObject.Parse(jo["wind"].ToString());
                    JObject sun = JObject.Parse(jo["sys"].ToString());
                    // Assigns JSON objects to class variables. 
                    WeatherGV.HighTemp = main["temp_max"].ToString();
                    WeatherGV.LowTemp = main["temp_min"].ToString();
                    WeatherGV.CurTemp = main["temp"].ToString();
                    WeatherGV.CityName = jo["name"].ToString();
                    WeatherGV.Pressure = main["pressure"].ToString();
                    WeatherGV.Humidity = main["humidity"].ToString();
                    WeatherGV.WindSpeed = wind["speed"].ToString();
                    // If statement to determine if wind degree parameter is returned as sometimes it is not.
                    if (wind["deg"] != null)
                    {
                        WeatherGV.WindDirection = GetCardinalDirections(wind["deg"].ToString());
                    }
                    else
                    {
                        WeatherGV.WindDirection = "No wind direction detected.";
                    }
                    // Sets sunrise time from string returned from method call.
                    WeatherGV.SunriseTime = DateTimeFromUnix(sun["sunrise"].ToString());
                    WeatherGV.SunsetTime = DateTimeFromUnix(sun["sunset"].ToString());
                    // Resets input values from user to default and displays information to the end user. 
                    iptZipCode.Text = "";
                    pckrCity.SelectedIndex = -1;
                    Navigation.PushAsync(new WPage());
                }
                catch (Exception ex)
                {
                    // Display error if API call cannot be completed.
                    DisplayAlert("Error Message", $"{ex.Message} : This zipcode is not supported by the OpenWeather API. Check it again.", "Close");
                }

            }
        }
    }
}
