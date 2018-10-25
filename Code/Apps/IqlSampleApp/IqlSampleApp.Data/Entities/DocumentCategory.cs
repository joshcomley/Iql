using System.Collections.Generic;
using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    /// <summary>
    ///     Design
    ///     Handing over certificate
    ///     Anchor pool testing
    ///     Compliancy sheet
    ///     Qualifications
    ///     Other
    /// </summary>
    public class DocumentCategory : DbObject
    {
        public List<SiteDocument> Documents { get; set; }
        public string Name { get; set; }
    }
}
