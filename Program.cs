using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace UklonTest_Weather_Forecsat
{
    class Program
    {
        static public string Get_Forecst(string City)
        {
            string url = "https://goweather.herokuapp.com/weather/";
            WebRequest request = WebRequest.Create(String.Concat(url, City));
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                WebResponse response = request.GetResponse();

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    response.Close();
                    Weather_Forecast current_forecast = new Weather_Forecast();
                    current_forecast = JsonSerializer.Deserialize<Weather_Forecast>(responseFromServer);
                    return current_forecast.temperature;
                }
            }
            catch (WebException)
            {
                return "server  error";
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Kyiv weather is:");
            Console.WriteLine(Get_Forecst("Kyiv"));
            Console.WriteLine("Odesa weather is:");
            Console.WriteLine(Get_Forecst("Odesa"));
            Console.ReadKey();
        }
    }
}
