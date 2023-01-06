using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminar_API.Models;
using seminar_API.Models.DTOs;
using seminar_API.Repositories.IRepository;

namespace seminar_API.Controllers
{
    [Route("api/[controller]")] // api/location
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _dbLocation;
        private readonly IMapper _mapper;
        public LocationsController(ILocationRepository dbLocation, IMapper mapper)
        {
            _dbLocation = dbLocation;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<LocationDTO>>> GetLocationsAsync()
        {
            var items = await _dbLocation.GetLocationsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LocationDTO>> GetLocationAsync(Guid id)
        {
            var item = await _dbLocation.GetLocationAsync(id);
            if (item != null) return Ok(item);
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationDTO>> AddLocationAsync([FromBody] CreateLocationDTO newLocation)
        {
            Location location = new()
            {
                LocationId = Guid.NewGuid(),
                Name = newLocation.Name,
                Coordinates = newLocation.Coordinates,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _dbLocation.AddLocationAsync(location);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteLocation(Guid id)
        {
            var item = await _dbLocation.GetLocationAsync(id);
            if (item == null) return NotFound();
            await _dbLocation.RemoveLocationAsync(item);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateLocationDto>> UpdateLocationAsync([FromBody] UpdateLocationDto location, Guid id)
        {
            var item = await _dbLocation.GetLocationAsync(id);
            if (item == null) return NotFound();
            item.Name = location.Name;
            item.Coordinates = location.Coordinates;
            await _dbLocation.UpdateLocationAsync(item);
            return NoContent();
        }
    }
}