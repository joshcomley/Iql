using System.Collections.Generic;
using Iql.Tests.Tests.OData;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Data.Context
{
    public class InMemoryDataBase
    {
        public IList<ApplicationLog> ApplicationLogs { get; set; } = new List<ApplicationLog>();
        public IList<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public IList<DocumentCategory> DocumentCategories { get; set; } = new List<DocumentCategory>();
        public IList<MyCustomReport> MyCustomReports { get; set; } = new List<MyCustomReport>();
        public IList<ClientType> ClientTypes { get; set; } = new List<ClientType>();
        public IList<Client> Clients { get; set; } = new List<Client>();
        public IList<Project> Projects { get; set; } = new List<Project>();
        public IList<Site> Sites { get; set; } = new List<Site>();
        public IList<SiteInspection> SiteInspections { get; set; } = new List<SiteInspection>();
        public IList<SiteDocument> SiteDocuments { get; set; } = new List<SiteDocument>();
        public IList<PersonReport> PersonReports { get; set; } = new List<PersonReport>();
        public IList<PersonLoading> PersonLoadings { get; set; } = new List<PersonLoading>();
        public IList<SiteArea> SiteAreas { get; set; } = new List<SiteArea>();
        public IList<RiskAssessmentQuestion> RiskAssessmentQuestions { get; set; } = new List<RiskAssessmentQuestion>();
        public IList<RiskAssessmentAnswer> RiskAssessmentAnswers { get; set; } = new List<RiskAssessmentAnswer>();
        public IList<ReportReceiverEmailAddress> ReportReceiverEmailAddresses { get; set; } = new List<ReportReceiverEmailAddress>();
        public IList<ReportActionsTaken> ReportActionsTaken { get; set; } = new List<ReportActionsTaken>();
        public IList<ReportDefaultRecommendation> ReportDefaultRecommendations { get; set; } = new List<ReportDefaultRecommendation>();
        public IList<ReportRecommendation> ReportRecommendations { get; set; } = new List<ReportRecommendation>();
        public IList<ReportType> ReportTypes { get; set; } = new List<ReportType>();
        public IList<UserSetting> UserSettings { get; set; } = new List<UserSetting>();
        public IList<UserSite> UserSites { get; set; } = new List<UserSite>();
        public IList<Person> People { get; set; } = new List<Person>();
        public IList<PersonType> PeopleTypes { get; set; } = new List<PersonType>();
        public IList<PersonTypeMap> PeopleTypeMap { get; set; } = new List<PersonTypeMap>();
        public IList<PersonInspection> PersonInspections { get; set; } = new List<PersonInspection>();
        public IList<ReportCategory> ReportCategories { get; set; } = new List<ReportCategory>();
        public IList<RiskAssessment> RiskAssessments { get; set; } = new List<RiskAssessment>();
        public IList<RiskAssessmentSolution> RiskAssessmentSolutions { get; set; } = new List<RiskAssessmentSolution>();
    }
}
