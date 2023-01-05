using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using seminar_API.Models;
using seminar_API.Models.DTOs;

namespace seminar_API.Repositories.IRepository
{
    public interface ILocationRepository : IDisposable
    {
        Task<IEnumerable<Location>> GetLocationsAsync();
        Task<Location> GetLocationAsync(Guid id);
        Task AddLocationAsync(LocationDTO location);
        Task RemoveLocationAsync(Guid id);
        Task UpdateLocationAsync(Guid id);
    }
}