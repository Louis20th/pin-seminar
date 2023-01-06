using System;

namespace seminar_API.Models.DTOs
{
    public record UpdateLocationDto
    {
        public string Name { get; init; }
        public string Coordinates { get; init; }
    }
}
