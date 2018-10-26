using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class ReportDefaultRecommendation : DbObject
    {
        public List<ReportRecommendation> Recommendations { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
