using System.Collections.Generic;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Data.Context
{
    public class InMemoryDataBase
    {
        public IList<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public IList<ClientType> ClientTypes { get; set; } = new List<ClientType>();
        public IList<Client> Clients { get; set; } = new List<Client>();
        public IList<ClientCategory> ClientCategories { get; set; } = new List<ClientCategory>();
        public IList<Site> Sites { get; set; } = new List<Site>();
        public IList<SiteInspection> SiteInspections { get; set; } = new List<SiteInspection>();
        public IList<Person> People { get; set; } = new List<Person>();
        public IList<PersonType> PeopleTypes { get; set; } = new List<PersonType>();
        public IList<PersonTypeMap> PeopleTypeMap { get; set; } = new List<PersonTypeMap>();
        public IList<PersonInspection> PersonInspections { get; set; } = new List<PersonInspection>();
        public IList<ReportCategory> ReportCategories { get; set; } = new List<ReportCategory>();
        public IList<RiskAssessment> RiskAssessments { get; set; } = new List<RiskAssessment>();
        public IList<RiskAssessmentSolution> RiskAssessmentSolutions { get; set; } = new List<RiskAssessmentSolution>();
    }
}
