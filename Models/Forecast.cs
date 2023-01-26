using System;
using System.Collections.Generic;

namespace seminar_API.Models.Forecast
{
    public class Astro
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
    }

    public class Condition
    {
        public int code { get; set; }
    }

    public class Current
    {
        public double temp_c { get; set; }
        public int is_day { get; set; }
        public Condition condition { get; set; }
        public double wind_kph { get; set; }
        public double precip_mm { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
    }

    public class Day
    {
        public double maxtemp_c { get; set; }
        public double mintemp_c { get; set; }
        public double maxwind_kph { get; set; }
        public double totalprecip_mm { get; set; }
        public int daily_will_it_rain { get; set; }
        public int daily_chance_of_rain { get; set; }
        public int daily_will_it_snow { get; set; }
        public int daily_chance_of_snow { get; set; }
        public Condition condition { get; set; }
    }

    public class Forecast
    {
        public List<Forecastday> forecastday { get; set; }
    }

    public class Forecastday
    {
        public string date { get; set; }
        public Day day { get; set; }
        public Astro astro { get; set; }
    }


    public class Location
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class DeserializeForecast
    {
        public Location location { get; set; }
        public Current current { get; set; }
        public Forecast forecast { get; set; }
    }
}
