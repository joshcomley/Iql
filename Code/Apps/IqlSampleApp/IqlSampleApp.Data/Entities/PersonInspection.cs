using System;
using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    /// <summary>
    ///     If there are ANY faults, then disallow PASS
    ///     Ask: Is a design required?
    ///     If yes: Has a design been supplied?
    ///     If no: go to signature, fail with NoDesignSupplied
    ///     If yes: go to ask for design number
    ///     Enter a drawing number (required only if design is required in previous step)
    /// </summary>
    public class PersonInspection : DbObject
    {
        public int PersonId { get; set; }
        public PersonInspectionStatus InspectionStatus { get; set; }

        /// <summary>
        ///     For legal reasons store the start time
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }
        public InspectionFailReason ReasonForFailure { get; set; }
        public int SiteInspectionId { get; set; }
        public SiteInspection SiteInspection { get; set; }
        public bool IsDesignRequired { get; set; }

        public string DrawingNumber { get; set; }
        /// </summary>
        /// Allow fail even if no faults
        /// If any fault reports, only allow Fail or PassWithObservation
        /// <summary>
        //public GeoCoordinates Coordinates { get; set; }
    }
}
