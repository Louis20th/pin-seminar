using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Diagnostics;
using seminar_API.Models.DTOs;
using seminar_API.Models.Forecast;

namespace seminar_API.Models
{
    public class LocationRecommendation
    {
        public LocationDTO location { get; set; }
        public double score { get; set; }
        public Forecastday forecast { get; set; }
        public Route route { get; set; }
        public List<TicketDTO> tickets { get; set; }

    }

    public class RecommendationResponse
    {
        public List<LocationRecommendation> recommendations { get; set; }
    }
}
