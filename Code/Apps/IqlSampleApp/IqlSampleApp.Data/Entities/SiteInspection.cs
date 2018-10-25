using System;
using System.Collections.Generic;
using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    /// <summary>
    ///     Choose or add a client
    ///     Choose or add a site
    /// </summary>
    public class SiteInspection : DbObject
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public List<PersonInspection> PersonInspections { get; set; }
        public RiskAssessment RiskAssessment { get; set; }

        //public byte[] Signature { get; set; }
    }
}
