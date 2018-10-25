using System.Collections.Generic;
using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    public class ReportCategory : DbObject
    {
        public string Name { get; set; }
        public List<ReportType> ReportTypes { get; set; }
    }
}
