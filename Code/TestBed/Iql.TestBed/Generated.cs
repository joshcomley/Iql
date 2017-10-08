using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.OData.Data;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Validation;
using Newtonsoft.Json.Linq;

namespace GeneratedCode
{
    public class TunnelDataContextBase : DataContext
    {
        public TunnelDataContextBase(IDataStore dataStore) : base(dataStore)
        {
            Users = AsDbSet<ApplicationUser, string>();
            Clients = AsDbSet<Client, int>();
            ClientTypes = AsDbSet<ClientType, int>();
            DocumentCategories = AsDbSet<DocumentCategory, int>();
            SiteDocuments = AsDbSet<SiteDocument, int>();
            ReportActionsTaken = AsDbSet<ReportActionsTaken, int>();
            ReportCategories = AsDbSet<ReportCategory, int>();
            ReportDefaultRecommendations = AsDbSet<ReportDefaultRecommendation, int>();
            ReportRecommendations = AsDbSet<ReportRecommendation, int>();
            ReportTypes = AsDbSet<ReportType, int>();
            Projects = AsDbSet<Project, int>();
            ReportReceiverEmailAddresses = AsDbSet<ReportReceiverEmailAddress, int>();
            RiskAssessments = AsDbSet<RiskAssessment, int>();
            RiskAssessmentAnswers = AsDbSet<RiskAssessmentAnswer, int>();
            RiskAssessmentQuestions = AsDbSet<RiskAssessmentQuestion, int>();
            People = AsDbSet<Person, int>();
            PersonInspections = AsDbSet<PersonInspection, int>();
            PersonLoadings = AsDbSet<PersonLoading, int>();
            PersonTypes = AsDbSet<PersonType, int>();
            PersonReports = AsDbSet<PersonReport, int>();
            Sites = AsDbSet<Site, int>();
            SiteInspections = AsDbSet<SiteInspection, int>();
            UserSites = AsDbSet<UserSite, int>();
            RegisterConfiguration(ODataConfiguration);
            ODataConfiguration.RegisterEntitySet<ApplicationUser>("Users");
            ODataConfiguration.RegisterEntitySet<Client>("Clients");
            ODataConfiguration.RegisterEntitySet<ClientType>("ClientTypes");
            ODataConfiguration.RegisterEntitySet<DocumentCategory>("DocumentCategories");
            ODataConfiguration.RegisterEntitySet<SiteDocument>("SiteDocuments");
            ODataConfiguration.RegisterEntitySet<ReportActionsTaken>("ReportActionsTaken");
            ODataConfiguration.RegisterEntitySet<ReportCategory>("ReportCategories");
            ODataConfiguration.RegisterEntitySet<ReportDefaultRecommendation>("ReportDefaultRecommendations");
            ODataConfiguration.RegisterEntitySet<ReportRecommendation>("ReportRecommendations");
            ODataConfiguration.RegisterEntitySet<ReportType>("ReportTypes");
            ODataConfiguration.RegisterEntitySet<Project>("Projects");
            ODataConfiguration.RegisterEntitySet<ReportReceiverEmailAddress>("ReportReceiverEmailAddresses");
            ODataConfiguration.RegisterEntitySet<RiskAssessment>("RiskAssessments");
            ODataConfiguration.RegisterEntitySet<RiskAssessmentAnswer>("RiskAssessmentAnswers");
            ODataConfiguration.RegisterEntitySet<RiskAssessmentQuestion>("RiskAssessmentQuestions");
            ODataConfiguration.RegisterEntitySet<Person>("People");
            ODataConfiguration.RegisterEntitySet<PersonInspection>("PersonInspections");
            ODataConfiguration.RegisterEntitySet<PersonLoading>("PersonLoadings");
            ODataConfiguration.RegisterEntitySet<PersonType>("PersonTypes");
            ODataConfiguration.RegisterEntitySet<PersonReport>("PersonReports");
            ODataConfiguration.RegisterEntitySet<Site>("Sites");
            ODataConfiguration.RegisterEntitySet<SiteInspection>("SiteInspections");
            ODataConfiguration.RegisterEntitySet<UserSite>("UserSites");
        }

        public ODataConfiguration ODataConfiguration { get; set; } = new ODataConfiguration();

        public DbSet<ApplicationUser, string> Users { get; set; }
        public DbSet<Client, int> Clients { get; set; }
        public DbSet<ClientType, int> ClientTypes { get; set; }
        public DbSet<DocumentCategory, int> DocumentCategories { get; set; }
        public DbSet<SiteDocument, int> SiteDocuments { get; set; }
        public DbSet<ReportActionsTaken, int> ReportActionsTaken { get; set; }
        public DbSet<ReportCategory, int> ReportCategories { get; set; }
        public DbSet<ReportDefaultRecommendation, int> ReportDefaultRecommendations { get; set; }
        public DbSet<ReportRecommendation, int> ReportRecommendations { get; set; }
        public DbSet<ReportType, int> ReportTypes { get; set; }
        public DbSet<Project, int> Projects { get; set; }
        public DbSet<ReportReceiverEmailAddress, int> ReportReceiverEmailAddresses { get; set; }
        public DbSet<RiskAssessment, int> RiskAssessments { get; set; }
        public DbSet<RiskAssessmentAnswer, int> RiskAssessmentAnswers { get; set; }
        public DbSet<RiskAssessmentQuestion, int> RiskAssessmentQuestions { get; set; }
        public DbSet<Person, int> People { get; set; }
        public DbSet<PersonInspection, int> PersonInspections { get; set; }
        public DbSet<PersonLoading, int> PersonLoadings { get; set; }
        public DbSet<PersonType, int> PersonTypes { get; set; }
        public DbSet<PersonReport, int> PersonReports { get; set; }
        public DbSet<Site, int> Sites { get; set; }
        public DbSet<SiteInspection, int> SiteInspections { get; set; }
        public DbSet<UserSite, int> UserSites { get; set; }

        public override void Configure(EntityConfigurationBuilder builder)
        {
            builder.DefineEntity<ApplicationUser>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.ClientId)
                .DefineProperty(p => p.Email)
                .DefineProperty(p => p.FullName)
                .DefineProperty(p => p.EmailConfirmed)
                .DefineProperty(p => p.UserType)
                .DefineProperty(p => p.IsLockedOut)
                .DefineProperty(p => p.Client)
                .DefineCollectionProperty(p => p.ClientsCreated)
                .DefineCollectionProperty(p => p.DocumentCategoriesCreated)
                .DefineCollectionProperty(p => p.SiteDocumentsCreated)
                .DefineCollectionProperty(p => p.FaultActionsTakenCreated)
                .DefineCollectionProperty(p => p.FaultCategoriesCreated)
                .DefineCollectionProperty(p => p.FaultDefaultRecommendationsCreated)
                .DefineCollectionProperty(p => p.FaultRecommendationsCreated)
                .DefineCollectionProperty(p => p.FaultTypesCreated)
                .DefineCollectionProperty(p => p.ProjectCreated)
                .DefineCollectionProperty(p => p.ReportReceiverEmailAddressesCreated)
                .DefineCollectionProperty(p => p.RiskAssessmentsCreated)
                .DefineCollectionProperty(p => p.RiskAssessmentAnswersCreated)
                .DefineCollectionProperty(p => p.RiskAssessmentQuestionsCreated)
                .DefineCollectionProperty(p => p.PeopleCreated)
                .DefineCollectionProperty(p => p.PersonInspectionsCreated)
                .DefineCollectionProperty(p => p.PersonLoadingsCreated)
                .DefineCollectionProperty(p => p.PersonTypesCreated)
                .DefineCollectionProperty(p => p.FaultReportsCreated)
                .DefineCollectionProperty(p => p.SitesCreated)
                .DefineCollectionProperty(p => p.SiteInspectionsCreated)
                .DefineCollectionProperty(p => p.Sites);

            builder.DefineEntity<ApplicationUser>()
                .HasOne(p => p.Client)
                .WithMany(p => p.Users)
                .WithConstraint(p => p.ClientId, p => p.Id);

            builder.DefineEntity<Client>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.TypeId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Name)
                .DefineProperty(p => p.Description)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineCollectionProperty(p => p.Users)
                .DefineProperty(p => p.Type)
                .DefineProperty(p => p.CreatedByUser)
                .DefineCollectionProperty(p => p.People);

            builder.DefineEntity<Client>()
                .HasOne(p => p.Type)
                .WithMany(p => p.Clients)
                .WithConstraint(p => p.TypeId, p => p.Id);

            builder.DefineEntity<Client>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.ClientsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<ClientType>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.Name)
                .DefineCollectionProperty(p => p.Clients);

            builder.DefineEntity<DocumentCategory>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Name)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.CreatedByUser)
                .DefineCollectionProperty(p => p.Documents);

            builder.DefineEntity<DocumentCategory>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.DocumentCategoriesCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<SiteDocument>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.CategoryId)
                .DefineProperty(p => p.SiteId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Title)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.Category)
                .DefineProperty(p => p.Site)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<SiteDocument>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Documents)
                .WithConstraint(p => p.CategoryId, p => p.Id);

            builder.DefineEntity<SiteDocument>()
                .HasOne(p => p.Site)
                .WithMany(p => p.Documents)
                .WithConstraint(p => p.SiteId, p => p.Id);

            builder.DefineEntity<SiteDocument>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.SiteDocumentsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<ReportActionsTaken>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.FaultReportId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Notes)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.PersonReport)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<ReportActionsTaken>()
                .HasOne(p => p.PersonReport)
                .WithMany(p => p.ActionsTaken)
                .WithConstraint(p => p.FaultReportId, p => p.Id);

            builder.DefineEntity<ReportActionsTaken>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.FaultActionsTakenCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<ReportCategory>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Name)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.CreatedByUser)
                .DefineCollectionProperty(p => p.FaultTypes);

            builder.DefineEntity<ReportCategory>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.FaultCategoriesCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<ReportDefaultRecommendation>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Name)
                .DefineProperty(p => p.Text)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.CreatedByUser)
                .DefineCollectionProperty(p => p.Recommendations);

            builder.DefineEntity<ReportDefaultRecommendation>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.FaultDefaultRecommendationsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<ReportRecommendation>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.FaultReportId)
                .DefineProperty(p => p.RecommendationId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Notes)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.PersonReport)
                .DefineProperty(p => p.Recommendation)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<ReportRecommendation>()
                .HasOne(p => p.PersonReport)
                .WithMany(p => p.Recommendations)
                .WithConstraint(p => p.FaultReportId, p => p.Id);

            builder.DefineEntity<ReportRecommendation>()
                .HasOne(p => p.Recommendation)
                .WithMany(p => p.Recommendations)
                .WithConstraint(p => p.RecommendationId, p => p.Id);

            builder.DefineEntity<ReportRecommendation>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.FaultRecommendationsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<ReportType>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CategoryId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Name)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.Category)
                .DefineProperty(p => p.CreatedByUser)
                .DefineCollectionProperty(p => p.FaultReports);

            builder.DefineEntity<ReportType>()
                .HasOne(p => p.Category)
                .WithMany(p => p.FaultTypes)
                .WithConstraint(p => p.CategoryId, p => p.Id);

            builder.DefineEntity<ReportType>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.FaultTypesCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<Project>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.Title)
                .DefineProperty(p => p.Description)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<Project>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.ProjectCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<ReportReceiverEmailAddress>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.SiteId)
                .DefineProperty(p => p.EmailAddress)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.Site)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<ReportReceiverEmailAddress>()
                .HasOne(p => p.Site)
                .WithMany(p => p.AdditionalSendReportsTo)
                .WithConstraint(p => p.SiteId, p => p.Id);

            builder.DefineEntity<ReportReceiverEmailAddress>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.ReportReceiverEmailAddressesCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<RiskAssessment>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.SiteInspectionId)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.CreatedByUser)
                .DefineProperty(p => p.SiteInspection);

            builder.DefineEntity<RiskAssessment>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.RiskAssessmentsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<RiskAssessment>()
                .HasOne(p => p.SiteInspection)
                .WithOne(p => p.RiskAssessment)
                .WithConstraint(p => p.Id, p => p.RiskAssessmentId);

            builder.DefineEntity<RiskAssessmentAnswer>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.QuestionId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.SpecificHazard)
                .DefineProperty(p => p.PrecautionsToControlHazard)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.Question)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<RiskAssessmentAnswer>()
                .HasOne(p => p.Question)
                .WithMany(p => p.Answers)
                .WithConstraint(p => p.QuestionId, p => p.Id);

            builder.DefineEntity<RiskAssessmentAnswer>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.RiskAssessmentAnswersCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<RiskAssessmentQuestion>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Name)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineCollectionProperty(p => p.Answers)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<RiskAssessmentQuestion>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.RiskAssessmentQuestionsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<Person>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.TypeId)
                .DefineProperty(p => p.LoadingId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Key)
                .DefineProperty(p => p.Title)
                .DefineProperty(p => p.Description)
                .DefineProperty(p => p.Category)
                .DefineProperty(p => p.ClientId)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.Client)
                .DefineProperty(p => p.Type)
                .DefineProperty(p => p.Loading)
                .DefineProperty(p => p.CreatedByUser)
                .DefineCollectionProperty(p => p.FaultReports);

            builder.DefineEntity<Person>()
                .HasOne(p => p.Client)
                .WithMany(p => p.People)
                .WithConstraint(p => p.ClientId, p => p.Id);

            builder.DefineEntity<Person>()
                .HasOne(p => p.Type)
                .WithMany(p => p.People)
                .WithConstraint(p => p.TypeId, p => p.Id);

            builder.DefineEntity<Person>()
                .HasOne(p => p.Loading)
                .WithMany(p => p.People)
                .WithConstraint(p => p.LoadingId, p => p.Id);

            builder.DefineEntity<Person>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.PeopleCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<PersonInspection>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.SiteInspectionId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.PersonId)
                .DefineProperty(p => p.InspectionStatus)
                .DefineProperty(p => p.StartTime)
                .DefineProperty(p => p.EndTime)
                .DefineProperty(p => p.ReasonForFailure)
                .DefineProperty(p => p.IsDesignRequired)
                .DefineProperty(p => p.DrawingNumber)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineProperty(p => p.SiteInspection)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<PersonInspection>()
                .HasOne(p => p.SiteInspection)
                .WithMany(p => p.PersonInspections)
                .WithConstraint(p => p.SiteInspectionId, p => p.Id);

            builder.DefineEntity<PersonInspection>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.PersonInspectionsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<PersonLoading>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Name)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineCollectionProperty(p => p.People)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<PersonLoading>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.PersonLoadingsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<PersonType>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Title)
                .DefineProperty(p => p.Description)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineCollectionProperty(p => p.People)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<PersonType>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.PersonTypesCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<PersonReport>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.PersonId)
                .DefineProperty(p => p.TypeId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Status)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineCollectionProperty(p => p.ActionsTaken)
                .DefineCollectionProperty(p => p.Recommendations)
                .DefineProperty(p => p.Person)
                .DefineProperty(p => p.Type)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<PersonReport>()
                .HasOne(p => p.Person)
                .WithMany(p => p.FaultReports)
                .WithConstraint(p => p.PersonId, p => p.Id);

            builder.DefineEntity<PersonReport>()
                .HasOne(p => p.Type)
                .WithMany(p => p.FaultReports)
                .WithConstraint(p => p.TypeId, p => p.Id);

            builder.DefineEntity<PersonReport>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.FaultReportsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<Site>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.ParentId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.Address)
                .DefineProperty(p => p.PostCode)
                .DefineProperty(p => p.ClientId)
                .DefineProperty(p => p.Name)
                .DefineProperty(p => p.Left)
                .DefineProperty(p => p.Right)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineCollectionProperty(p => p.Documents)
                .DefineCollectionProperty(p => p.AdditionalSendReportsTo)
                .DefineProperty(p => p.Parent)
                .DefineCollectionProperty(p => p.Children)
                .DefineProperty(p => p.CreatedByUser)
                .DefineProperty(p => p.Client)
                .DefineCollectionProperty(p => p.SiteInspections)
                .DefineCollectionProperty(p => p.Users);

            builder.DefineEntity<Site>()
                .HasOne(p => p.Parent)
                .WithMany(p => p.Children)
                .WithConstraint(p => p.ParentId, p => p.Id);

            builder.DefineEntity<Site>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.SitesCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<SiteInspection>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Id)
                .DefineProperty(p => p.RiskAssessmentId)
                .DefineProperty(p => p.SiteId)
                .DefineProperty(p => p.CreatedByUserId)
                .DefineProperty(p => p.StartTime)
                .DefineProperty(p => p.EndTime)
                .DefineProperty(p => p.Guid)
                .DefineProperty(p => p.CreatedDate)
                .DefineProperty(p => p.Version)
                .DefineCollectionProperty(p => p.PersonInspections)
                .DefineProperty(p => p.RiskAssessment)
                .DefineProperty(p => p.Site)
                .DefineProperty(p => p.CreatedByUser);

            builder.DefineEntity<SiteInspection>()
                .HasOne(p => p.RiskAssessment)
                .WithOne(p => p.SiteInspection)
                .WithConstraint(p => p.Id, p => p.SiteInspectionId);

            builder.DefineEntity<SiteInspection>()
                .HasOne(p => p.Site)
                .WithMany(p => p.SiteInspections)
                .WithConstraint(p => p.SiteId, p => p.Id);

            builder.DefineEntity<SiteInspection>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(p => p.SiteInspectionsCreated)
                .WithConstraint(p => p.CreatedByUserId, p => p.Id);

            builder.DefineEntity<UserSite>()
                .HasKey(p => p.SiteId)
                .DefineProperty(p => p.SiteId)
                .DefineProperty(p => p.UserId)
                .DefineProperty(p => p.User)
                .DefineProperty(p => p.Site);

            builder.DefineEntity<UserSite>()
                .HasOne(p => p.User)
                .WithMany(p => p.Sites)
                .WithConstraint(p => p.UserId, p => p.Id);

            builder.DefineEntity<UserSite>()
                .HasOne(p => p.Site)
                .WithMany(p => p.Users)
                .WithConstraint(p => p.SiteId, p => p.Id);
        }
    }

    public enum FaultReportStatus
    {
        Fail = 0,
        PassWithObservations = 1
    }

    public enum InspectionFailReason
    {
        None = 0,
        UnableToAccess = 1,
        PersistentFaults = 2,
        FailuresInFaultReports = 3,
        TooManyMinorObservations = 4,
        NoDesignSupplied = 5
    }

    public enum PersonInspectionStatus
    {
        Pass = 0,
        Fail = 1,
        PassWithObservations = 2
    }

    public enum PersonCategory
    {
        System = 0,
        Conventional = 1
    }

    public enum UserType
    {
        Super = 1,
        Client = 2,
        Candidate = 3
    }

    public class UserSite : UserSiteBase, IEntity
    {
        public int SiteId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Site Site { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class SiteInspection : SiteInspectionBase, IEntity
    {
        public int Id { get; set; }
        public int RiskAssessmentId { get; set; }
        public int SiteId { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public List<PersonInspection> PersonInspections { get; set; }
        public RiskAssessment RiskAssessment { get; set; }
        public Site Site { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class Site : SiteBase, IEntity
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string CreatedByUserId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public List<SiteDocument> Documents { get; set; }
        public List<ReportReceiverEmailAddress> AdditionalSendReportsTo { get; set; }
        public Site Parent { get; set; }
        public List<Site> Children { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public Client Client { get; set; }
        public List<SiteInspection> SiteInspections { get; set; }
        public List<UserSite> Users { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class PersonReport : PersonReportBase, IEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int TypeId { get; set; }
        public string CreatedByUserId { get; set; }
        public int Status { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public List<ReportActionsTaken> ActionsTaken { get; set; }
        public List<ReportRecommendation> Recommendations { get; set; }
        public Person Person { get; set; }
        public ReportType Type { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class PersonType : PersonTypeBase, IEntity
    {
        public int Id { get; set; }
        public string CreatedByUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public List<Person> People { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class PersonLoading : PersonLoadingBase, IEntity
    {
        public int Id { get; set; }
        public string CreatedByUserId { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public List<Person> People { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class PersonInspection : PersonInspectionBase, IEntity
    {
        public int SiteInspectionId { get; set; }
        public string CreatedByUserId { get; set; }
        public int PersonId { get; set; }
        public int InspectionStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ReasonForFailure { get; set; }
        public bool IsDesignRequired { get; set; }
        public string DrawingNumber { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public SiteInspection SiteInspection { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class Person : PersonBase, IEntity
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int LoadingId { get; set; }
        public string CreatedByUserId { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public int ClientId { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public Client Client { get; set; }
        public PersonType Type { get; set; }
        public PersonLoading Loading { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public List<PersonReport> FaultReports { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            if (!true)
            {
                validationResult.AddFailure("Please enter either a title or a description");
            }
            if (!true)
            {
                validationResult.AddFailure("If the name is 'Josh' please match it in the description");
            }
            validationResult.AddPropertyValidationResult(ValidateTitle());
            return validationResult;
        }

        public virtual async Task<ODataResult<string>> IncrementVersion()
        {
            // Call API somehow
            var parameters = new JObject();

            return await GetODataDataStore().PostOnEntityInstance<Person, string>(this, parameters);
        }

        public PropertyValidationResult ValidateTitle()
        {
            var validationResult = new PropertyValidationResult(GetType(), "Title");
            var entity = this;
            if (!true)
            {
                validationResult.AddFailure("Please enter a valid title");
            }
            if (!true)
            {
                validationResult.AddFailure("Please enter less than fifty characters");
            }
            return validationResult;
        }
    }

    public class RiskAssessmentQuestion : RiskAssessmentQuestionBase, IEntity
    {
        public int Id { get; set; }
        public string CreatedByUserId { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public List<RiskAssessmentAnswer> Answers { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class RiskAssessmentAnswer : RiskAssessmentAnswerBase, IEntity
    {
        public int QuestionId { get; set; }
        public string CreatedByUserId { get; set; }
        public string SpecificHazard { get; set; }
        public string PrecautionsToControlHazard { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public RiskAssessmentQuestion Question { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class RiskAssessment : RiskAssessmentBase, IEntity
    {
        public int SiteInspectionId { get; set; }
        public int Id { get; set; }
        public string CreatedByUserId { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public SiteInspection SiteInspection { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class ReportReceiverEmailAddress : ReportReceiverEmailAddressBase, IEntity
    {
        public string CreatedByUserId { get; set; }
        public int SiteId { get; set; }
        public string EmailAddress { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public Site Site { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class Project : ProjectBase, IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedByUserId { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class ReportType : ReportTypeBase, IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CreatedByUserId { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public ReportCategory Category { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public List<PersonReport> FaultReports { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class ReportRecommendation : ReportRecommendationBase, IEntity
    {
        public int FaultReportId { get; set; }
        public int RecommendationId { get; set; }
        public string CreatedByUserId { get; set; }
        public string Notes { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public PersonReport PersonReport { get; set; }
        public ReportDefaultRecommendation Recommendation { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class ReportDefaultRecommendation : ReportDefaultRecommendationBase, IEntity
    {
        public int Id { get; set; }
        public string CreatedByUserId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public List<ReportRecommendation> Recommendations { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class ReportCategory : ReportCategoryBase, IEntity
    {
        public int Id { get; set; }
        public string CreatedByUserId { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public List<ReportType> FaultTypes { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class ReportActionsTaken : ReportActionsTakenBase, IEntity
    {
        public int FaultReportId { get; set; }
        public string CreatedByUserId { get; set; }
        public string Notes { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public PersonReport PersonReport { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class SiteDocument : SiteDocumentBase, IEntity
    {
        public int CategoryId { get; set; }
        public int SiteId { get; set; }
        public string CreatedByUserId { get; set; }
        public string Title { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public DocumentCategory Category { get; set; }
        public Site Site { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class DocumentCategory : DocumentCategoryBase, IEntity
    {
        public int Id { get; set; }
        public string CreatedByUserId { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public List<SiteDocument> Documents { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class ClientType : ClientTypeBase, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Client> Clients { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class Client : ClientBase, IEntity
    {
        public int TypeId { get; set; }
        public string CreatedByUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Version { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public ClientType Type { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public List<Person> People { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }
    }

    public class ApplicationUser : ApplicationUserBase, IEntity
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool EmailConfirmed { get; set; }
        public int UserType { get; set; }
        public bool IsLockedOut { get; set; }
        public Client Client { get; set; }
        public List<Client> ClientsCreated { get; set; }
        public List<DocumentCategory> DocumentCategoriesCreated { get; set; }
        public List<SiteDocument> SiteDocumentsCreated { get; set; }
        public List<ReportActionsTaken> FaultActionsTakenCreated { get; set; }
        public List<ReportCategory> FaultCategoriesCreated { get; set; }
        public List<ReportDefaultRecommendation> FaultDefaultRecommendationsCreated { get; set; }
        public List<ReportRecommendation> FaultRecommendationsCreated { get; set; }
        public List<ReportType> FaultTypesCreated { get; set; }
        public List<Project> ProjectCreated { get; set; }
        public List<ReportReceiverEmailAddress> ReportReceiverEmailAddressesCreated { get; set; }
        public List<RiskAssessment> RiskAssessmentsCreated { get; set; }
        public List<RiskAssessmentAnswer> RiskAssessmentAnswersCreated { get; set; }
        public List<RiskAssessmentQuestion> RiskAssessmentQuestionsCreated { get; set; }
        public List<Person> PeopleCreated { get; set; }
        public List<PersonInspection> PersonInspectionsCreated { get; set; }
        public List<PersonLoading> PersonLoadingsCreated { get; set; }
        public List<PersonType> PersonTypesCreated { get; set; }
        public List<PersonReport> FaultReportsCreated { get; set; }
        public List<Site> SitesCreated { get; set; }
        public List<SiteInspection> SiteInspectionsCreated { get; set; }
        public List<UserSite> Sites { get; set; }

        public override ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public override EntityValidationResult ValidateEntity()
        {
            var entity = this;
            var validationResult = new EntityValidationResult(GetType());
            return validationResult;
        }

        public virtual async Task<ODataResult<string>> GeneratePasswordResetLink()
        {
            // Call API somehow
            var parameters = new JObject();

            return await GetODataDataStore().GetOnEntityInstance<ApplicationUser, string>(this, parameters);
        }

        public virtual async Task<ODataResult<string>> AccountConfirm()
        {
            // Call API somehow
            var parameters = new JObject();

            return await GetODataDataStore().PostOnEntityInstance<ApplicationUser, string>(this, parameters);
        }

        public virtual async Task<ODataResult<string>> SendAccountConfirmationEmail()
        {
            // Call API somehow
            var parameters = new JObject();

            return await GetODataDataStore().PostOnEntityInstance<ApplicationUser, string>(this, parameters);
        }

        public virtual async Task<ODataResult<string>> SendPasswordResetEmail()
        {
            // Call API somehow
            var parameters = new JObject();

            return await GetODataDataStore().PostOnEntityInstance<ApplicationUser, string>(this, parameters);
        }

        public virtual async Task<ODataResult<string>> ReinstateUser()
        {
            // Call API somehow
            var parameters = new JObject();

            return await GetODataDataStore().PostOnEntityInstance<ApplicationUser, string>(this, parameters);
        }
    }

    public class UserSiteBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class SiteInspectionBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class SiteBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class PersonReportBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class PersonTypeBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class PersonLoadingBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class PersonInspectionBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class PersonBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class RiskAssessmentQuestionBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class RiskAssessmentAnswerBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class RiskAssessmentBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ReportReceiverEmailAddressBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ProjectBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ReportTypeBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ReportRecommendationBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ReportDefaultRecommendationBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ReportCategoryBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ReportActionsTakenBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class SiteDocumentBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class DocumentCategoryBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ClientTypeBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ClientBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }


    public class ApplicationUserBase : IEntity
    {
        public virtual bool OnSaving()
        {
            return true;
        }

        public virtual bool OnDeleting()
        {
            return true;
        }

        public virtual ODataDataStore GetODataDataStore()
        {
            return null;
        }

        public virtual EntityValidationResult ValidateEntity()
        {
            return new EntityValidationResult(GetType());
        }
    }
}