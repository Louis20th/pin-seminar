using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Geolocation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using seminar_API.Models;
using seminar_API.Models.Forecast;

namespace seminar_API.Utilities
{
    static class Endpoints
    {
        public const string forecastUrl = "http://api.weatherapi.com/v1/forecast.json?key=3436482989274ed8b53141744232601&q=";
        public const string routeUrl = "https://api.radar.io/v1/route/distance?origin=";
    }
    public class HelperFunctions
    {
        public static Coordinate getLocationCoordinates(Models.Location location)
        {
            return new Coordinate(location.latitude, location.longitude);
        }

        private static bool isWeekend(DateTimeOffset date)
        {
            List<String> weekendDays = new() { "saturday", "sunday" };
            return weekendDays.Contains(date.DayOfWeek.ToString().ToLower());
        }

        public static List<Forecastday> getWeekendForecast(Models.Location location)
        {
            string coordStr = location.latitude.ToString("F4") + "," + location.longitude.ToString("F4");
            HttpClient client = new();
            var url = Endpoints.forecastUrl + coordStr + "&days=7";

            var result = client.GetAsync(url).Result;
            result.EnsureSuccessStatusCode();
            var content = result.Content.ReadAsStringAsync().Result;
            var forecast = JsonSerializer.Deserialize<DeserializeForecast>(content);

            List<Forecastday> days = new();
            for (int i = 0; i < forecast.forecast.forecastday.Count; ++i)
            {
                var date = DateTimeOffset.Parse(forecast.forecast.forecastday[i].date);
                if (isWeekend(date))
                {
                    days.Add(forecast.forecast.forecastday[i]);
                }
            }
            return days;
        }
        public static Route getLocationRoute(Coordinate origin, Coordinate destination)
        {
            var originCoordStr = origin.Latitude.ToString(CultureInfo.InvariantCulture) + "," + origin.Longitude.ToString(CultureInfo.InvariantCulture);
            var destinCoordStr = destination.Latitude.ToString(CultureInfo.InvariantCulture) + "," + destination.Longitude.ToString(CultureInfo.InvariantCulture);
            var url = Endpoints.routeUrl + originCoordStr + "&destination=" + destinCoordStr + "&modes=car&units=metric";

            HttpClient client = new();
            HttpRequestMessage requestMessage = new();
            requestMessage.RequestUri = new(url);
            requestMessage.Method = HttpMethod.Get;
            requestMessage.Headers.Add("Authorization", "prj_live_pk_3f46d29b47e82b64d56d8f6faf0e75a51806cb33");
            var response = client.SendAsync(requestMessage).Result;

            var content = response.Content.ReadAsStringAsync().Result;
            var route = JsonSerializer.Deserialize<Route>(content);

            return route;
        }

        public static double getLocationScore(Forecastday forecast, Route route)
        {
            var score = 0.0;
            if (route.routes.car.duration.value > 90)
            {
                score += (100 - (route.routes.car.duration.value - 90) * (100 / 180)) * 0.3;
            }
            else score += 0.3;

            var rainScore = 100 - (forecast.day.daily_will_it_rain * 100) * 0.5;
            var tempScore = (100 - (Math.Abs((forecast.day.maxtemp_c + forecast.day.mintemp_c) / 2 - 20) * (100 / 10))) * 0.5;
            score += (rainScore + tempScore) * 0.7;

            return score;
        }
    }
}
