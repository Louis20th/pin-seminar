using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using seminar_API.Models;

namespace seminar_API.Repositories.IRepository
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket> GetOneAsync(Guid id);
        Task<List<Ticket>> GetByLocationId(Guid id);
        Task AddAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task RemoveAsync(Ticket ticket);
    }
}
