using System;

namespace seminar_API.Models.DTOs
{
    public record TicketDTO
    {
        public Guid Id { get; init; }
        public Guid Location { get; init; }
        public string Type { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
