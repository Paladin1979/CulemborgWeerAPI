using CulemborgWeerAPI.Data.Entities;
using System;
using System.Net.Http;

namespace CulemborgWeerAPI.WttrIn
{
    public class WttrInClient : IWttrInClient
    {
        private const string WttrInURL = "https://wttr.in";
        private const string JsonFormat = "Culemborg?format=j1";

        public WeatherInformation GetCurrentWeatherForCulemborg()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(WttrInURL);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(JsonFormat).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var wttrInResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<WttrInResponse>(content);
                WttrInCurrentCondition currentCondition = wttrInResponse.current_condition[0];

                return new WeatherInformation() {
                    CloudPercentage = currentCondition.cloudcover,
                    Date = currentCondition.localObsDateTime,
                    HumidityPercentage = currentCondition.humidity,
                    Temperature = currentCondition.temp_C,
                    TemperatureFeelsLike = currentCondition.FeelsLikeC,
                    WindDirection = currentCondition.winddirDegree,
                    WindSpeed = currentCondition.windspeedKmph };
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
