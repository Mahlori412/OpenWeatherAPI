using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        string apiKey = "13e9780f1958326f930e787cfb499741";
        string[] cities = { "Limpopo", "Mpumalanga", "Gauteng", "Alberton North, ZA" };
        List<WeatherData> weatherDataList = new List<WeatherData>();

        HttpClient client = new HttpClient();

        foreach (string city in cities)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(json);

                WeatherData weatherData = new WeatherData
                {
                    City = city,
                    Temperature = data.main.temp,
                    Humidity = data.main.humidity,
                };

                weatherDataList.Add(weatherData);
            }
        }

        string jsonOutput = JsonConvert.SerializeObject(weatherDataList, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(@"C:\Users\manga\3D Objects\C# Practice\weather.json", jsonOutput);
    }
}

class WeatherData
{
    public string City { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
}