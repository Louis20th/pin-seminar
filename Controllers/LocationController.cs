using System;
using Microsoft.AspNetCore.Mvc;
using seminar_API.Data;

namespace seminar_API.Controllers
{
    [Route("api/[controller]")] // api/location
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext _appcontext;
        public LocationController(AppDbContext context)
        {
            _appcontext = context;
        }

    }
}
