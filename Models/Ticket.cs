using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace seminar_API.Models
{
    public record Ticket
    {
        [Required]
        [NotNull]
        public Guid TicketId { get; init; }
        public string Type { get; init; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset ModifiedDate { get; set; }

        public Location Location { get; set; }
    }
}