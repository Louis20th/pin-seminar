using System;

namespace seminar_API.Models.DTOs
{
    public record CreateTicketDTO
    {
        public Guid LocationID { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
    }
}
