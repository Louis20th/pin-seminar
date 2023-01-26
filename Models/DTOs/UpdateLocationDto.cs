using System;

namespace seminar_API.Models.DTOs
{
    public record UpdateLocationDto
    {
        public string Name { get; init; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
