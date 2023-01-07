using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace seminar_API.Models
{
    public record Location
    {
        [Key]
        public Guid LocationId { get; init; }
        [NotNull]
        [MaxLength(50)]
        public string Name { get; set; }
        [NotNull]
        public string Coordinates { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset ModifiedDate { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}