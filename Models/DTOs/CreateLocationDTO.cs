using System;
using System.Diagnostics.CodeAnalysis;

namespace seminar_API.Models.DTOs
{
    public record CreateLocationDTO
    {
        public string Name { get; init; }
        public string Coordinates { get; init; }
    }
}
