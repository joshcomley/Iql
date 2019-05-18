using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IqlSampleApp.Data.Entities.Bases;
using Microsoft.AspNetCore.OData.NetTopology.Conversion;
using Microsoft.Spatial;
using NetTopologySuite.Geometries;

namespace IqlSampleApp.Data.Entities
{
    /// <summary>
    ///     Scan a person tag
    ///     Choose a person category
    ///     Choose a scaffold type
    ///     Choose a person loading type
    /// </summary>
    public class Person : DbObject
    {
        public string PhotoUrl { get; set; }
        public string PhotoRevisionKey { get; set; }
        public int? InferredFromUserClientId { get; set; }
        public Client InferredFromUserClient { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public Site Site { get; set; }
        public int? SiteId { get; set; }
        public SiteArea SiteArea { get; set; }
        public int? SiteAreaId { get; set; }
        public string Key { get; set; }
        public string InferredWhenKeyChanges { get; set; }
        //public string Location { get; set; }

        //[Required(ErrorMessage = "Please enter a title")]
        public bool IsComplete { get; set; }
        public bool? HasPaid { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public PersonSkills Skills { get; set; }

        public PersonCategory Category { get; set; }

        public PersonType Type { get; set; }
        public int? TypeId { get; set; }

        public List<PersonTypeMap> Types { get; set; }

        public PersonLoading Loading { get; set; }
        public int? LoadingId { get; set; }

        public List<PersonReport> Reports { get; set; }

//        public List<PersonInspection> PersonInspections { get; set; }
        public Client Client { get; set; }
        public int? ClientId { get; set; }

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
    }
}
