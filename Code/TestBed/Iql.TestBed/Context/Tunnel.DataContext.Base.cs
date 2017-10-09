using Iql.Queryable.Operations;
using Iql.OData.Data;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.DataStores;
using System;
using Iql.Queryable;
public class TunnelDataContextBase : DataContext {
	public TunnelDataContextBase(IDataStore dataStore) : base(dataStore) {
		this.Users = this.AsDbSet<ApplicationUser, string>();		
		this.Clients = this.AsDbSet<Client, int>();		
		this.ClientTypes = this.AsDbSet<ClientType, int>();		
		this.DocumentCategories = this.AsDbSet<DocumentCategory, int>();		
		this.SiteDocuments = this.AsDbSet<SiteDocument, int>();		
		this.ReportActionsTaken = this.AsDbSet<ReportActionsTaken, int>();		
		this.ReportCategories = this.AsDbSet<ReportCategory, int>();		
		this.ReportDefaultRecommendations = this.AsDbSet<ReportDefaultRecommendation, int>();		
		this.ReportRecommendations = this.AsDbSet<ReportRecommendation, int>();		
		this.ReportTypes = this.AsDbSet<ReportType, int>();		
		this.Projects = this.AsDbSet<Project, int>();		
		this.ReportReceiverEmailAddresses = this.AsDbSet<ReportReceiverEmailAddress, int>();		
		this.RiskAssessments = this.AsDbSet<RiskAssessment, int>();		
		this.RiskAssessmentAnswers = this.AsDbSet<RiskAssessmentAnswer, int>();		
		this.RiskAssessmentQuestions = this.AsDbSet<RiskAssessmentQuestion, int>();		
		this.People = this.AsDbSet<Person, int>();		
		this.PersonInspections = this.AsDbSet<PersonInspection, int>();		
		this.PersonLoadings = this.AsDbSet<PersonLoading, int>();		
		this.PersonTypes = this.AsDbSet<PersonType, int>();		
		this.PersonTypesMap = this.AsDbSet<PersonTypeMap, CompositeKey>();		
		this.PersonReports = this.AsDbSet<PersonReport, int>();		
		this.Sites = this.AsDbSet<Site, int>();		
		this.SiteInspections = this.AsDbSet<SiteInspection, int>();		
		this.UserSites = this.AsDbSet<UserSite, CompositeKey>();		
		this.RegisterConfiguration<ODataConfiguration>(this.ODataConfiguration);
		this.ODataConfiguration.RegisterEntitySet<ApplicationUser>("Users");
		this.ODataConfiguration.RegisterEntitySet<Client>("Clients");
		this.ODataConfiguration.RegisterEntitySet<ClientType>("ClientTypes");
		this.ODataConfiguration.RegisterEntitySet<DocumentCategory>("DocumentCategories");
		this.ODataConfiguration.RegisterEntitySet<SiteDocument>("SiteDocuments");
		this.ODataConfiguration.RegisterEntitySet<ReportActionsTaken>("ReportActionsTaken");
		this.ODataConfiguration.RegisterEntitySet<ReportCategory>("ReportCategories");
		this.ODataConfiguration.RegisterEntitySet<ReportDefaultRecommendation>("ReportDefaultRecommendations");
		this.ODataConfiguration.RegisterEntitySet<ReportRecommendation>("ReportRecommendations");
		this.ODataConfiguration.RegisterEntitySet<ReportType>("ReportTypes");
		this.ODataConfiguration.RegisterEntitySet<Project>("Projects");
		this.ODataConfiguration.RegisterEntitySet<ReportReceiverEmailAddress>("ReportReceiverEmailAddresses");
		this.ODataConfiguration.RegisterEntitySet<RiskAssessment>("RiskAssessments");
		this.ODataConfiguration.RegisterEntitySet<RiskAssessmentAnswer>("RiskAssessmentAnswers");
		this.ODataConfiguration.RegisterEntitySet<RiskAssessmentQuestion>("RiskAssessmentQuestions");
		this.ODataConfiguration.RegisterEntitySet<Person>("People");
		this.ODataConfiguration.RegisterEntitySet<PersonInspection>("PersonInspections");
		this.ODataConfiguration.RegisterEntitySet<PersonLoading>("PersonLoadings");
		this.ODataConfiguration.RegisterEntitySet<PersonType>("PersonTypes");
		this.ODataConfiguration.RegisterEntitySet<PersonTypeMap>("PersonTypesMap");
		this.ODataConfiguration.RegisterEntitySet<PersonReport>("PersonReports");
		this.ODataConfiguration.RegisterEntitySet<Site>("Sites");
		this.ODataConfiguration.RegisterEntitySet<SiteInspection>("SiteInspections");
		this.ODataConfiguration.RegisterEntitySet<UserSite>("UserSites");
	}
	public ODataConfiguration ODataConfiguration { get; set; } = new ODataConfiguration();
	
	public override void Configure(EntityConfigurationBuilder builder) {
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
			.DefineProperty(p => p.TypeId)			
			.DefineProperty(p => p.LoadingId)			
			.DefineProperty(p => p.Id)			
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
			.DefineCollectionProperty(p => p.Types)			
			.DefineCollectionProperty(p => p.Reports);		
		
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
			.DefineProperty(p => p.CreatedByUser)			
			.DefineCollectionProperty(p => p.PeopleMap);		
		
		builder.DefineEntity<PersonType>()		
			.HasOne(p => p.CreatedByUser)			
			.WithMany(p => p.PersonTypesCreated)			
			.WithConstraint(p => p.CreatedByUserId, p => p.Id);		
		
		builder.DefineEntity<PersonTypeMap>()		
			.HasCompositeKey(p => p.PersonId, p => p.TypeId)			
			.DefineProperty(p => p.PersonId)			
			.DefineProperty(p => p.TypeId)			
			.DefineProperty(p => p.Notes)			
			.DefineProperty(p => p.Guid)			
			.DefineProperty(p => p.CreatedDate)			
			.DefineProperty(p => p.Person)			
			.DefineProperty(p => p.Type);		
		
		builder.DefineEntity<PersonTypeMap>()		
			.HasOne(p => p.Person)			
			.WithMany(p => p.Types)			
			.WithConstraint(p => p.PersonId, p => p.Id);		
		
		builder.DefineEntity<PersonTypeMap>()		
			.HasOne(p => p.Type)			
			.WithMany(p => p.PeopleMap)			
			.WithConstraint(p => p.TypeId, p => p.Id);		
		
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
			.WithMany(p => p.Reports)			
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
			.HasCompositeKey(p => p.SiteId, p => p.UserId)			
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
	public DbSet<PersonTypeMap, CompositeKey> PersonTypesMap { get; set; }
	public DbSet<PersonReport, int> PersonReports { get; set; }
	public DbSet<Site, int> Sites { get; set; }
	public DbSet<SiteInspection, int> SiteInspections { get; set; }
	public DbSet<UserSite, CompositeKey> UserSites { get; set; }
}

