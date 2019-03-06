using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities.Events;

namespace Iql.Forms.Geography
{
    public class GeographicCoordinates
    {
        private float _latitude;

        private float _longitude;

        public EventEmitter<GeographicCoordinatesChangedEvent> Changed =
            new EventEmitter<GeographicCoordinatesChangedEvent>();

        public object Entity;
        public string LatitudeDegrees;
        public string LongitudeDegrees;

        public GeographicCoordinates(float? latitude = null, float? longitude = null, object entity = null)
        {
            Latitude = latitude ?? 0;
            Longitude = longitude ?? 0;
            Entity = entity;
        }

        public float Longitude
        {
            get => _longitude;
            set
            {
                var oldValue = _longitude;
                _longitude = value;
                Update(null, oldValue);
            }
        }

        public float Latitude
        {
            get => _latitude;
            set
            {
                var oldValue = _latitude;
                _latitude = value;
                Update(null, oldValue);
            }
        }

        public bool HasValue => HasLatitude && HasLongitude;
        public bool HasLatitude => ValueHasValue(Latitude);
        public bool HasLongitude => ValueHasValue(Longitude);

        public static bool AreEquivalent(GeographicCoordinates[] left, GeographicCoordinates[] right)
        {
            var areDifferent = left == null && right != null ||
                               left != null && right == null;
            var lastCoordinatesNormalised = NormalizeCoordinates(left);
            var currentCoordinatesNormalised = NormalizeCoordinates(right);
            if (lastCoordinatesNormalised.Length != currentCoordinatesNormalised.Length)
            {
                areDifferent = true;
            }
            else
            {
                for (var i = 0; i < lastCoordinatesNormalised.Length; i++)
                {
                    if (lastCoordinatesNormalised[i].Latitude != currentCoordinatesNormalised[i].Latitude)
                    {
                        areDifferent = true;
                        break;
                    }

                    if (lastCoordinatesNormalised[i].Longitude != currentCoordinatesNormalised[i].Longitude)
                    {
                        areDifferent = true;
                        break;
                    }
                }
            }

            return !areDifferent;
        }

        private static GeographicCoordinates[] NormalizeCoordinates(GeographicCoordinates[] coordinates)
        {
            if (coordinates == null)
            {
                return new GeographicCoordinates[] { };
            }

            // Technically a length of less than four is invalid but for our purposes of checking
            // the status this is fine
            if (coordinates.Length < 2)
            {
                return new GeographicCoordinates[] { };
            }

            var first = coordinates[0];
            var last = coordinates[coordinates.Length - 1];
            if (first.Latitude == last.Latitude && first.Longitude == last.Longitude)
            {
                var newList = coordinates.ToList();
                newList.RemoveAt(coordinates.Length - 1);
                return newList.ToArray();
            }

            return coordinates;
        }

        public static IqlRingExpression ToIqlRing(GeographicCoordinates[] coordinates)
        {
            var ring = new IqlRingExpression();
            ring.Points = new List<IqlPointExpression>();
            for (var i = 0; i < coordinates.Length; i++)
            {
                ring.Points.Add(coordinates[i].ToIqlPoint());
            }

            if (coordinates.Length > 0)
            {
                ring.Points.Add(coordinates[0].ToIqlPoint());
            }

            return ring;
        }

        public static IqlPolygonExpression ToIqlPolygon(GeographicCoordinates[] coordinates)
        {
            var polygon = new IqlPolygonExpression(
                ToIqlRing(coordinates)
            );
            return polygon;
        }

        public IqlPointExpression ToIqlPoint()
        {
            return new IqlPointExpression(Longitude, Latitude);
        }

        private void Update(float? oldLatitude = null, float? oldLongitude = null)
        {
            if (oldLatitude != null || oldLongitude != null)
            {
                Changed.Emit(() => new GeographicCoordinatesChangedEvent(
                    oldLatitude ?? Latitude,
                    oldLongitude ?? Longitude,
                    Latitude,
                    Longitude
                ));
            }

            var lat = Latitude;
            var lon = Longitude;
            var latDir = lat >= 0 ? "N" : "S";
            lat = Math.Abs(lat);
            var latMinPart = (lat - Math.Truncate(lat) / 1) * 60;
            var latSecPart = (latMinPart - Math.Truncate(latMinPart) / 1) * 60;

            var lonDir = lon >= 0 ? "E" : "W";
            lon = Math.Abs(lon);
            var lonMinPart = (lon - Math.Truncate(lon) / 1) * 60;
            var lonSecPart = (lonMinPart - Math.Truncate(lonMinPart) / 1) * 60;
            LatitudeDegrees =
                $"{Math.Truncate(lat)}° {Math.Truncate(latMinPart)}' {Math.Truncate(latSecPart)}\" {latDir}";
            LongitudeDegrees =
                $"{Math.Truncate(lon)}° {Math.Truncate(lonMinPart)}' {Math.Truncate(lonSecPart)}\" {lonDir}";
        }

        public void UpdateWith(GeographicCoordinates coordinates)
        {
            UpdateTo(coordinates.Latitude, coordinates.Longitude);
        }

        public void UpdateTo(float latitude, float longitude)
        {
            var oldLatitude = Latitude;
            var oldLongitude = Longitude;
            _latitude = latitude;
            _longitude = longitude;
            Update(oldLatitude, oldLongitude);
        }

        private bool ValueHasValue(float? value)
        {
            return value != null && value != 0;
        }
    }
}