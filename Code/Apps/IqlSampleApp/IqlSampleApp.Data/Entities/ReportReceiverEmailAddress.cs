using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    public class ReportReceiverEmailAddress : DbObject
    {
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public string EmailAddress { get; set; }
    }
}
