using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Geolocation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Localization;
using seminar_API.Models;
using seminar_API.Models.DTOs;
using seminar_API.Models.Forecast;
using seminar_API.Repositories;
using seminar_API.Repositories.IRepository;
using seminar_API.Utilities;

namespace seminar_API.Controllers
{
    [Route("api/[controller]")] // api/Recommendations
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly ITicketRepository _tickets;
        private readonly ILocationRepository _locations;
        private readonly IMapper _mapper;
        private ApiResponse _response;
        public RecommendationsController(ITicketRepository tickets,
                                        ILocationRepository locations,
                                        IMapper mapper)
        {
            _tickets = tickets;
            _locations = locations;
            _mapper = mapper;
            _response = new();
        }

        private List<Models.Location> getCandidates(List<Models.Location> locations, Coordinate origin)
        {
            List<Models.Location> result = new();

            for (int i = 0; i < locations.Count; ++i)
            {
                var destination = new Coordinate(locations[i].latitude, locations[i].longitude);

                if (GeoCalculator.GetDistance(origin, destination, 0, DistanceUnit.Kilometers) < 350)
                {
                    result.Add(locations[i]);
                }
            }

            return result;
        }


        [HttpGet("lat={lat},lon={lon}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        async public Task<ActionResult<ApiResponse>> GetRecommendation(double lat, double lon)
        {

            var allLocations = await _locations.GetLocationsAsync();
            Coordinate origin = new(lat, lon);
            var candidates = getCandidates(allLocations, origin);


            RecommendationResponse recResponse = new();
            recResponse.recommendations = new List<LocationRecommendation>();

            for (int i = 0; i < candidates.Count; ++i)
            {
                var tempLoc = candidates[i];
                LocationRecommendation temp = new();

                Coordinate destination = HelperFunctions.getLocationCoordinates(tempLoc);
                var locForecast = HelperFunctions.getWeekendForecast(tempLoc);
                var locRoute = HelperFunctions.getLocationRoute(origin, destination);

                temp.route = locRoute;
                var tickets = await _tickets.GetByLocationId(tempLoc.LocationId);
                temp.tickets = _mapper.Map<List<TicketDTO>>(tickets);
                temp.location = _mapper.Map<LocationDTO>(tempLoc);

                for (int j = 0; j < locForecast.Count; ++j)
                {
                    var score = HelperFunctions.getLocationScore(locForecast[j], locRoute);
                    temp.forecast = locForecast[j];
                    temp.score = score;
                }
                recResponse.recommendations.Add(temp);
            }

            _response.Result = recResponse;
            _response.Success = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);

        }
    }
}
