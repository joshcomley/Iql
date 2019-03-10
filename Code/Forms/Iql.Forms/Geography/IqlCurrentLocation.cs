using System;

namespace Iql.Forms.Geography
{
    public class IqlCurrentLocation
    {
        public bool Success { get; }
        public IqlGeographicCoordinates Coordinates { get; }
        public float? Altitude { get; }
        public float? Speed { get; }
        public float? Direction { get; }
        public IqlLocationAccuracy? HorizontalAccuracy { get; }
        public IqlLocationAccuracy? VerticalAccuracy { get; }
        public DateTimeOffset Time { get; }

        public IqlCurrentLocation(IqlGeographicCoordinates coordinates, 
            bool success = true,
            float? altitude = null, 
            float? speed = null, 
            float? direction = null, 
            IqlLocationAccuracy? horizontalAccuracy = null, 
            IqlLocationAccuracy? verticalAccuracy = null)
        {
            Coordinates = coordinates;
            Success = success;
            Altitude = altitude;
            Speed = speed;
            Direction = direction;
            HorizontalAccuracy = horizontalAccuracy;
            VerticalAccuracy = verticalAccuracy;
            Time = DateTimeOffset.Now;
        }
    }
}