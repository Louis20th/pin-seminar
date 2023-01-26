using System;

namespace seminar_API.Models
{
    public class Car
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
    }

    public class Distance
    {
        public double value { get; set; }
    }

    public class Duration
    {
        public double value { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
    }

    public class Route
    {
        public Meta meta { get; set; }
        public Routes routes { get; set; }
    }

    public class Routes
    {
        public Car car { get; set; }
    }
}
