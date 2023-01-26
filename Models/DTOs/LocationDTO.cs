using System;

namespace seminar_API.Models.DTOs
{
    public record LocationDTO
    {
        public Guid LocationID { get; init; }
        public string Name { get; init; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}
