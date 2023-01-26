using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Geolocation;
using Microsoft.IdentityModel.Protocols;
using seminar_API.Models.Forecast;

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
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset ModifiedDate { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}