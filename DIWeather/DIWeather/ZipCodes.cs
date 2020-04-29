using System;
using System.Collections.Generic;
using System.Text;

namespace DIWeather
{
    public class ZipCodes
    {
        // Class variables
        private string _zipCode;
        private string _city;
        // Constructor method
        public ZipCodes(string[] tempCityInfo)
        {
            _zipCode = tempCityInfo[0];
            _city = tempCityInfo[1];
        }
        /// <summary>
        /// Method returns zipcode parameter from each object so getting the values from the dictionary is easier. 
        /// </summary>
        /// <returns></returns>
        public string GetZipFromCity()
        {
            return _zipCode.ToString();
        }
    }
}
