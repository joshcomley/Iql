using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    /// <summary>
    ///     Online only
    /// </summary>
    public class SiteDocument : DbObject
    {
        public int CategoryId { get; set; }
        public DocumentCategory Category { get; set; }
        public int SiteId { get; set; }
        public Site Site { get; set; }

        public string Title { get; set; }
        //public byte[] Data { get; set; }
    }
}
