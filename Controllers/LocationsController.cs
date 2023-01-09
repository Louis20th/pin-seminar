using System;
using System.Collections.Generic;
using System.Net;
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
        private ApiResponse _response;
        public LocationsController(ILocationRepository dbLocation, IMapper mapper)
        {
            _dbLocation = dbLocation;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<LocationDTO>>> GetLocationsAsync()
        {
            try
            {
                var items = await _dbLocation.GetLocationsAsync();
                _response.Result = _mapper.Map<List<LocationDTO>>(items);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Success = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("{id}", Name = "Get Location")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationDTO>> GetLocationAsync(Guid id)
        {
            try
            {
                var item = await _dbLocation.GetLocationAsync(id);
                _response.Result = _mapper.Map<LocationDTO>(item);
                _response.Success = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
            }
            if (_response.Result == null)
            {
                _response.Success = false;
                if (_response.ErrorMessages.Count != 0)
                {
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    return StatusCode(StatusCodes.Status500InternalServerError, _response);
                }
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            return Ok(value: _response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationDTO>> AddLocationAsync([FromBody] CreateLocationDTO newLocation)
        {
            if (newLocation == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }
            try
            {
                Location location = new()
                {
                    LocationId = Guid.NewGuid(),
                    Name = newLocation.Name,
                    Coordinates = newLocation.Coordinates,
                    CreatedDate = DateTimeOffset.UtcNow
                };
                await _dbLocation.AddLocationAsync(location);

                _response.Result = _mapper.Map<LocationDTO>(location);
                _response.StatusCode = HttpStatusCode.Created;
                _response.Success = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Success = false;
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteLocation(Guid id)
        {
            try
            {
                Location item = await _dbLocation.GetLocationAsync(id);
                await _dbLocation.RemoveLocationAsync(item);
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateLocationDto>> UpdateLocationAsync([FromBody] UpdateLocationDto location, Guid id)
        {
            var item = await _dbLocation.GetLocationAsync(id);

            if (item == null)
            {
                _response.Result = location;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Success = false;
                return NotFound(_response);
            }

            item.Name = location.Name;
            item.Coordinates = location.Coordinates;
            try
            {
                await _dbLocation.UpdateLocationAsync(item);
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}