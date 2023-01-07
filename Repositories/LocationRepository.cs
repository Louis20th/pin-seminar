using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using seminar_API.Data;
using seminar_API.Models;
using seminar_API.Models.DTOs;
using seminar_API.Repositories.IRepository;

namespace seminar_API.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public LocationRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            var items = await _db.Locations.ToListAsync();
            return items;
        }

        public async Task AddLocationAsync(Location location)
        {
            await _db.Locations.AddAsync(location);
            await _db.SaveChangesAsync();
        }

        async Task<Location> ILocationRepository.GetLocationAsync(Guid id)
        {
            var item = await _db.Locations.FirstOrDefaultAsync(res => res.LocationId == id);
            return item;
        }

        async Task ILocationRepository.RemoveLocationAsync(Location location)
        {
            var relatedTickets = _db.Tickets.Where(t => t.LocationID == location.LocationId).ToList();
            _db.Tickets.RemoveRange(relatedTickets);
            _db.Locations.Remove(location);
            await _db.SaveChangesAsync();
        }

        async Task ILocationRepository.UpdateLocationAsync(Location location)
        {
            location.ModifiedDate = DateTimeOffset.UtcNow;
            _db.Locations.Update(location);
            await _db.SaveChangesAsync();
        }
    }
}