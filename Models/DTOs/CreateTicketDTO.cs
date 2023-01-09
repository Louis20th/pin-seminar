using System;
using System.ComponentModel.DataAnnotations;

namespace seminar_API.Models.DTOs
{
    public record CreateTicketDTO
    {
        [Required]
        public Guid LocationID { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
