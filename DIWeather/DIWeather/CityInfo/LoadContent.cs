using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
namespace DIWeather.CityInfo
{
   public class LoadContent
    {
        Dictionary<string, ZipCodes> cityDict = new Dictionary<string, ZipCodes>();
        public Dictionary<string, ZipCodes> GetCSVContent()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string NAME = "DIWeather.CityInfo.utahzipcodes.csv";
            using (Stream stream = assembly.GetManifestResourceStream(NAME))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string[] tempCityInfo;

                    while (!reader.EndOfStream)
                    {
                        tempCityInfo = reader.ReadLine().Split(',');
                        cityDict.Add(tempCityInfo[1], new ZipCodes(tempCityInfo));
                    }
                    return cityDict;
                }
            }
        }
    }
}
