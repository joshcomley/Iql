using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IqlSampleApp.Data.Entities.Bases;
using Microsoft.AspNetCore.OData.NetTopology.Conversion;
using Microsoft.Spatial;
using NetTopologySuite.Geometries;

namespace IqlSampleApp.Data.Entities
{
    //public class SiteContact
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public int SiteId { get; set; }
    //    public Site Site { get; set; }
    //}

    public class Site : DbObject
    {
        private LineStringWrapper _line;
        public LineString Line
        {
            get => _line;
            set => _line = value;
        }

        [NotMapped]
        public GeographyLineString EdmLine
        {
            get => _line;
            set => _line = value;
        }

        private PointWrapper _location;
        public Point Location
        {
            get => _location;
            set => _location = value;
        }

        [NotMapped]
        public GeographyPoint EdmLocation
        {
            get => _location;
            set => _location = value;
        }

        private PolygonWrapper _area;
        public Polygon Area
        {
            get => _area;
            set => _area = value;
        }

        [NotMapped]
        public GeographyPolygon EdmArea
        {
            get => _area;
            set => _area = value;
        }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public Site Parent { get; set; }
        public List<Site> Children { get; set; }
        public List<UserSite> Users { get; set; }
        public List<SiteInspection> SiteInspections { get; set; }
        public int? ParentId { get; set; }

        /// <summary>
        ///     Also send them to all super users, and all users who are allocated at this site and below
        /// </summary>
        public List<ReportReceiverEmailAddress> AdditionalSendReportsTo { get; set; }

        public List<SiteDocument> Documents { get; set; }

        public string Name { get; set; }

        /// <summary>
        ///     Left in Modified Pre-Order Tree Traversal
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        ///     Right in Modified Pre-Order Tree Traversal
        /// </summary>
        public int Right { get; set; }
    }
}
