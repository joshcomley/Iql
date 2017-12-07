using System;
using System.Collections.Generic;
using System.Text;

namespace Iql.Tests.Context
{
    public class InMemoryDataBase
    {
        public IList<ClientType> ClientTypes { get; set; } = new List<ClientType>();
        public IList<Client> Clients { get; set; } = new List<Client>();
        public IList<Site> Sites { get; set; } = new List<Site>();
        public IList<SiteInspection> SiteInspections { get; set; } = new List<SiteInspection>();
        public IList<RiskAssessment> RiskAssessments { get; set; } = new List<RiskAssessment>();
        public IList<RiskAssessmentSolution> RiskAssessmentSolutions { get; set; } = new List<RiskAssessmentSolution>();
    }
}
