using System;

namespace seminar_API.Models.DTOs
{
    public record LocationDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Coordinates { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
