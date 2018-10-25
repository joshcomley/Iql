using System.Collections.Generic;
using Tunnel.App.Data.Models;

namespace Tunnel.App.Data.Entities
{
    public class ReportDefaultRecommendation : DbObject
    {
        public List<ReportRecommendation> Recommendations { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
