using System;

namespace Iql
{
    public abstract class IqlPointExpression : IqlExpression
    {
        public double X { get; }
        public double Y { get; }

        protected IqlPointExpression(double x, double y, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            X = x;
            Y = y;
        }

        public bool Intersects(IqlPolygonExpression polygon)
        {
            return IntersectsPolygon(X, Y, polygon);
        }

        public double DistanceFrom(IqlPointExpression point, IqlDistanceKind unit = IqlDistanceKind.Kilometers)
        {
            return DistanceBetween(point.X, point.Y, X, Y, unit);
        }

        public static double DistanceBetween(double lat1, double lon1, double lat2, double lon2, IqlDistanceKind unit = IqlDistanceKind.Kilometers)
        {
            var rlat1 = Math.PI * lat1 / 180;
            var rlat2 = Math.PI * lat2 / 180;
            var theta = lon1 - lon2;
            var rtheta = Math.PI * theta / 180;
            var dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case IqlDistanceKind.Kilometers: //Kilometers -> default
                    return dist * 1.609344;
                case IqlDistanceKind.NauticalMiles: //Nautical Miles 
                    return dist * 0.8684;
                case IqlDistanceKind.Miles: //Miles
                    return dist;
            }

            return dist;
        }

        /// <summary>
        /// Determines if this point is inside the polygon
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="polygon">The polygon.</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        public static bool IntersectsPolygon(double x, double y, IqlPolygonExpression polygon)
        {
            if (polygon.Points == null)
            {
                return false;
            }
            var result = false;
            var j = polygon.Points.Count - 1;
            for (var i = 0; i < polygon.Points.Count; i++)
            {
                if (polygon.Points[i].Y < y && polygon.Points[j].Y >= y || polygon.Points[j].Y < y && polygon.Points[i].Y >= y)
                {
                    if (polygon.Points[i].X + (y - polygon.Points[i].Y) / (polygon.Points[j].Y - polygon.Points[i].Y) * (polygon.Points[j].X - polygon.Points[i].X) < x)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
    }
}