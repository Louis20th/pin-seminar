using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account.Manage;
using Microsoft.EntityFrameworkCore;
using seminar_API.Data;
using seminar_API.Models;
using seminar_API.Repositories.IRepository;

namespace seminar_API.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public TicketRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<Ticket>> GetByLocationId(Guid id)
        {
            var items = await _db.Tickets.Where(t => t.LocationID == id).ToListAsync();
            return items;
        }

        async Task ITicketRepository.AddAsync(Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();
        }

        async Task<List<Ticket>> ITicketRepository.GetAllAsync()
        {
            var items = await _db.Tickets.ToListAsync();
            return items;
        }

        async Task<Ticket> ITicketRepository.GetOneAsync(Guid id)
        {
            var item = await _db.Tickets.FirstOrDefaultAsync(t => t.TicketId == id);
            return item;
        }

        async Task ITicketRepository.RemoveAsync(Ticket ticket)
        {
            _db.Tickets.Remove(ticket);
            await _db.SaveChangesAsync();
        }

        async Task ITicketRepository.UpdateAsync(Ticket ticket)
        {
            _db.Tickets.Update(ticket);
            await _db.SaveChangesAsync();
        }
    }
}
