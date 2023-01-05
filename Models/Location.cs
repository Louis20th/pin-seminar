using System;

namespace seminar_API.Models
{
    public record Location
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Coordinates { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}