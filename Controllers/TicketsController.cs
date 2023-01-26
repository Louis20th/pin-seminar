using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminar_API.Models;
using seminar_API.Models.DTOs;
using seminar_API.Repositories.IRepository;

namespace seminar_API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")] // api/tickets
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _dbTickets;
        private readonly IMapper _mapper;
        private ApiResponse _response;
        public TicketsController(ITicketRepository dbTickets, IMapper mapper)
        {
            _dbTickets = dbTickets;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TicketDTO>>> GetTicketsAsync()
        {
            _response.Success = false;
            try
            {
                var items = await _dbTickets.GetAllAsync();
                var result = _mapper.Map<List<TicketDTO>>(items);

                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            if (!_response.Success)
                return NoContent();

            return Ok(_response);
        }

        [HttpGet("{id}", Name = "Get Ticket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TicketDTO>> GetOneTicketAsync(Guid id)
        {
            _response.Success = false;
            try
            {
                _response.Result = _mapper.Map<TicketDTO>(await _dbTickets.GetOneAsync(id));
                _response.Success = (_response.Result != null);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!_response.Success)
                return NotFound(_response);
            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TicketDTO>> AddTicketAsync([FromBody] CreateTicketDTO newTicket)
        {
            _response.Success = false;

            Ticket ticket = new()
            {
                TicketId = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                ModifiedDate = DateTimeOffset.UtcNow,
                Type = newTicket.Type,
                Price = (uint)(newTicket.Price * 100),
                LocationID = newTicket.LocationID
            };

            try
            {
                await _dbTickets.AddAsync(ticket);
                _response.Result = _mapper.Map<TicketDTO>(ticket);
                _response.StatusCode = HttpStatusCode.Created;
                _response.Success = true;

                return CreatedAtRoute("Get Ticket", new { id = ticket.TicketId }, _response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("location/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TicketDTO>>> GetTicketsByLocationId(Guid id)
        {
            _response.Success = false;

            try
            {
                var items = await _dbTickets.GetByLocationId(id);
                _response.Result = _mapper.Map<List<TicketDTO>>(items);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Success = (items.Count != 0);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            if (!_response.Success)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            return Ok(_response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveTicketAsync(Guid id)
        {
            try
            {
                var item = await _dbTickets.GetOneAsync(id);
                await _dbTickets.RemoveAsync(item);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Success = false;
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveOneAsync(Guid id)
        {
            try
            {
                var item = await _dbTickets.GetOneAsync(id);
                if (item == null)
                    return NotFound();

                await _dbTickets.RemoveAsync(item);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages.Add(ex.Message);
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
