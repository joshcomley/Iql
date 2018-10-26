using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
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
