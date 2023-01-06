using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace seminar_API.Models
{
    public record Location
    {
        [Required]
        public Guid LocationId { get; init; }
        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Name { get; set; }
        [NotNull]
        public string Coordinates { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}