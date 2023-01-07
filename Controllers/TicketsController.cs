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
    [Route("api/[controller]")] // api/tickets
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _dbTickets;
        private readonly IMapper _mapper;
        public TicketsController(ITicketRepository dbTickets, IMapper mapper)
        {
            _dbTickets = dbTickets;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<TicketDTO>>> GetTicketsAsync()
        {
            var items = await _dbTickets.GetAllAsync();
            var result = _mapper.Map<List<TicketDTO>>(items);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "Get Ticket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketDTO>> GetOneTicketAsync(Guid id)
        {
            var item = _mapper.Map<TicketDTO>(await _dbTickets.GetOneAsync(id));
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TicketDTO>> AddTicketAsync([FromBody] CreateTicketDTO newTicket)
        {
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
                var createdDto = _mapper.Map<TicketDTO>(ticket);

                return CreatedAtRoute("Get Ticket", new { id = ticket.TicketId }, createdDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("location/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TicketDTO>>> GetTicketsByLocationId(Guid id)
        {
            var items = await _dbTickets.GetByLocationId(id);
            var result = _mapper.Map<List<TicketDTO>>(items);
            if (result.Count == 0)
                return NotFound();

            return Ok(result);
        }
    }
}
