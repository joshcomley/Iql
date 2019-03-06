using System;

namespace Iql.Forms.Geography
{
    public class CurrentLocation
    {
        public GeographicCoordinates Coordinates { get; }
        public float? Altitude { get; }
        public float? Speed { get; }
        public float? Direction { get; }
        public IqlLocationAccuracy? HorizontalAccuracy { get; }
        public IqlLocationAccuracy? VerticalAccuracy { get; }
        public DateTimeOffset Time { get; }

        public CurrentLocation(GeographicCoordinates coordinates, float? altitude, float? speed, float? direction, IqlLocationAccuracy? horizontalAccuracy, IqlLocationAccuracy? verticalAccuracy)
        {
            Coordinates = coordinates;
            Altitude = altitude;
            Speed = speed;
            Direction = direction;
            HorizontalAccuracy = horizontalAccuracy;
            VerticalAccuracy = verticalAccuracy;
            Time = DateTimeOffset.Now;
        }
    }
}