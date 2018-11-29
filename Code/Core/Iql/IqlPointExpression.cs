using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlPointExpression : IqlSridExpression
    {
        public double X { get; set; }
        public double Y { get; set; }

        public IqlPointExpression(double xOrLongitude, double yOrLatitude, IqlType type = IqlType.GeographyPoint, int? srid = null) : base(srid, type)
        {
            X = xOrLongitude;
            Y = yOrLatitude;
        }

        public IqlPointExpression() : base(null, IqlType.GeographyPoint)
        {

        }

        public bool Intersects(IqlPolygonExpression polygon)
        {
            return IntersectsPolygon(X, Y, polygon);
        }

        public double DistanceFrom(IqlPointExpression point)
        {
            return DistanceBetween(point.X, point.Y, X, Y);
        }

        public static double DistanceBetween(double xOrLongitude1, double yOrLatitude1, double xOrLongitude2, double yOrLatitude2,
            IqlDistanceKind unit = IqlDistanceKind.Meters)
        {
            var rlat1 = Math.PI * yOrLatitude1 / 180;
            var rlat2 = Math.PI * yOrLatitude2 / 180;
            var theta = xOrLongitude1 - xOrLongitude2;
            var rtheta = Math.PI * theta / 180;
            var dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case IqlDistanceKind.Meters: //Meters -> default
                    return dist * 1000;
                case IqlDistanceKind.Kilometers: //Kilometers
                    return dist;
                case IqlDistanceKind.NauticalMiles: //Nautical Miles 
                    return dist * 0.8684;
                case IqlDistanceKind.Miles: //Miles
                    return dist * 0.621369647819236;
            }

            return dist;
        }

        /// <summary>
        /// Determines if this point is inside the polygon
        /// </summary>
        /// <param name="xOrLongitude">The Y coordinate.</param>
        /// <param name="yOrLatitude">The X coordinate.</param>
        /// <param name="polygon">The polygon.</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        public static bool IntersectsPolygon(double xOrLongitude, double yOrLatitude, IqlPolygonExpression polygon)
        {
            if (polygon == null || polygon.OuterRing == null)
            {
                return false;
            }
            var inOuter = IqlPointExpression.IntersectsRing(xOrLongitude, yOrLatitude, polygon.OuterRing);
            if (!inOuter)
            {
                return false;
            }

            if (polygon.InnerRings == null)
            {
                return true;
            }

            return !polygon.InnerRings.Any(_ => IntersectsRing(xOrLongitude, yOrLatitude, _));
        }

        /// <summary>
        /// Determines if this point is inside the ring
        /// </summary>
        /// <param name="yOrLatitude">The X coordinate.</param>
        /// <param name="xOrLongitude">The Y coordinate.</param>
        /// <param name="ring">The ring.</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        public static bool IntersectsRing(double xOrLongitude, double yOrLatitude, IqlRingExpression ring)
        {
            if (ring == null || ring.Points == null)
            {
                return false;
            }
            var result = false;
            var j = ring.Points.Count - 1;
            for (var i = 0; i < ring.Points.Count; i++)
            {
                if (ring.Points[i].Y < yOrLatitude && ring.Points[j].Y >= yOrLatitude || ring.Points[j].Y < yOrLatitude && ring.Points[i].Y >= yOrLatitude)
                {
                    if (ring.Points[i].X + (yOrLatitude - ring.Points[i].Y) / (ring.Points[j].Y - ring.Points[i].Y) * (ring.Points[j].X - ring.Points[i].X) < xOrLongitude)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

            var expression = new IqlPointExpression(0, 0);
            expression.Srid = Srid;
            expression.Key = Key;
            expression.Kind = Kind;
            expression.ReturnType = ReturnType;
            expression.Parent = Parent?.Clone();
            return expression;

            // #CloneEnd
        }

        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

            context.Flatten(Parent);

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

            Parent = context.Replace(this, nameof(Parent), null, Parent);
            var replaced = context.Replacer(context, this);
            if (replaced != this)
            {
                return replaced;
            }
            return this;

            // #ReplaceEnd
        }
    }
}
