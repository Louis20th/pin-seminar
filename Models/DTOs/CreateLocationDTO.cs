using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace seminar_API.Models.DTOs
{
    public record CreateLocationDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
