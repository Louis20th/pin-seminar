using System;

namespace seminar_API.Models
{
    public record Ticket
    {
        public Guid Id { get; init; }
        public Guid Location { get; init; }
        public string Type { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}