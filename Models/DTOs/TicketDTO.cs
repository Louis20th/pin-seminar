using System;

namespace seminar_API.Models.DTOs
{
    public record TicketDTO
    {
        public Guid TicketId { get; init; }
        public Guid LocationID { get; set; }
        public string Type { get; set; }
        public uint Price { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
