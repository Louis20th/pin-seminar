using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using seminar_API.Models;
using seminar_API.Models.DTOs;

namespace seminar_API.Repositories.IRepository
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetLocationsAsync();
        Task<Location> GetLocationAsync(Guid id);
        Task AddLocationAsync(Location location);
        Task RemoveLocationAsync(Location location);
        Task UpdateLocationAsync(Location location);
    }
}