using Iql.Queryable.Data.EntityConfiguration;
using Tunnel.Sets;
using Tunnel.ApiContext.Base;
using Tunnel.App.Data.Entities;
using Iql.OData;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores;
using System;
using System.Collections.Generic;
using Iql.OData.Methods;
using Iql;
namespace Tunnel.ApiContext.Base
{
	public class TunnelDataContextBase : DataContext
	{
		public TunnelDataContextBase(IDataStore dataStore) : base(dataStore)
		{
			this.Users = this.AsCustomDbSet<ApplicationUser, String, ApplicationUserSet>();
			
			this.Clients = this.AsCustomDbSet<Client, int, ClientSet>();
			
			this.ClientTypes = this.AsCustomDbSet<ClientType, int, ClientTypeSet>();
			
			this.DocumentCategories = this.AsCustomDbSet<DocumentCategory, int, DocumentCategorySet>();
			
			this.SiteDocuments = this.AsCustomDbSet<SiteDocument, int, SiteDocumentSet>();
			
			this.ReportActionsTaken = this.AsCustomDbSet<ReportActionsTaken, int, ReportActionsTakenSet>();
			
			this.ReportCategories = this.AsCustomDbSet<ReportCategory, int, ReportCategorySet>();
			
			this.ReportDefaultRecommendations = this.AsCustomDbSet<ReportDefaultRecommendation, int, ReportDefaultRecommendationSet>();
			
			this.ReportRecommendations = this.AsCustomDbSet<ReportRecommendation, int, ReportRecommendationSet>();
			
			this.ReportTypes = this.AsCustomDbSet<ReportType, int, ReportTypeSet>();
			
			this.Projects = this.AsCustomDbSet<Project, int, ProjectSet>();
			
			this.ReportReceiverEmailAddresses = this.AsCustomDbSet<ReportReceiverEmailAddress, int, ReportReceiverEmailAddressSet>();
			
			this.RiskAssessments = this.AsCustomDbSet<RiskAssessment, int, RiskAssessmentSet>();
			
			this.RiskAssessmentSolutions = this.AsCustomDbSet<RiskAssessmentSolution, int, RiskAssessmentSolutionSet>();
			
			this.RiskAssessmentAnswers = this.AsCustomDbSet<RiskAssessmentAnswer, int, RiskAssessmentAnswerSet>();
			
			this.RiskAssessmentQuestions = this.AsCustomDbSet<RiskAssessmentQuestion, int, RiskAssessmentQuestionSet>();
			
			this.People = this.AsCustomDbSet<Person, int, PersonSet>();
			
			this.PersonInspections = this.AsCustomDbSet<PersonInspection, int, PersonInspectionSet>();
			
			this.PersonLoadings = this.AsCustomDbSet<PersonLoading, int, PersonLoadingSet>();
			
			this.PersonTypes = this.AsCustomDbSet<PersonType, int, PersonTypeSet>();
			
			this.PersonTypesMap = this.AsCustomDbSet<PersonTypeMap, CompositeKey, PersonTypeMapSet>();
			
			this.PersonReports = this.AsCustomDbSet<PersonReport, int, PersonReportSet>();
			
			this.Sites = this.AsCustomDbSet<Site, int, SiteSet>();
			
			this.SiteInspections = this.AsCustomDbSet<SiteInspection, int, SiteInspectionSet>();
			
			this.UserSites = this.AsCustomDbSet<UserSite, CompositeKey, UserSiteSet>();
			
			this.RegisterConfiguration<ODataConfiguration>(this.ODataConfiguration);
			this.ODataConfiguration.RegisterEntitySet<ApplicationUser>(nameof(Users));
			this.ODataConfiguration.RegisterEntitySet<Client>(nameof(Clients));
			this.ODataConfiguration.RegisterEntitySet<ClientType>(nameof(ClientTypes));
			this.ODataConfiguration.RegisterEntitySet<DocumentCategory>(nameof(DocumentCategories));
			this.ODataConfiguration.RegisterEntitySet<SiteDocument>(nameof(SiteDocuments));
			this.ODataConfiguration.RegisterEntitySet<ReportActionsTaken>(nameof(ReportActionsTaken));
			this.ODataConfiguration.RegisterEntitySet<ReportCategory>(nameof(ReportCategories));
			this.ODataConfiguration.RegisterEntitySet<ReportDefaultRecommendation>(nameof(ReportDefaultRecommendations));
			this.ODataConfiguration.RegisterEntitySet<ReportRecommendation>(nameof(ReportRecommendations));
			this.ODataConfiguration.RegisterEntitySet<ReportType>(nameof(ReportTypes));
			this.ODataConfiguration.RegisterEntitySet<Project>(nameof(Projects));
			this.ODataConfiguration.RegisterEntitySet<ReportReceiverEmailAddress>(nameof(ReportReceiverEmailAddresses));
			this.ODataConfiguration.RegisterEntitySet<RiskAssessment>(nameof(RiskAssessments));
			this.ODataConfiguration.RegisterEntitySet<RiskAssessmentSolution>(nameof(RiskAssessmentSolutions));
			this.ODataConfiguration.RegisterEntitySet<RiskAssessmentAnswer>(nameof(RiskAssessmentAnswers));
			this.ODataConfiguration.RegisterEntitySet<RiskAssessmentQuestion>(nameof(RiskAssessmentQuestions));
			this.ODataConfiguration.RegisterEntitySet<Person>(nameof(People));
			this.ODataConfiguration.RegisterEntitySet<PersonInspection>(nameof(PersonInspections));
			this.ODataConfiguration.RegisterEntitySet<PersonLoading>(nameof(PersonLoadings));
			this.ODataConfiguration.RegisterEntitySet<PersonType>(nameof(PersonTypes));
			this.ODataConfiguration.RegisterEntitySet<PersonTypeMap>(nameof(PersonTypesMap));
			this.ODataConfiguration.RegisterEntitySet<PersonReport>(nameof(PersonReports));
			this.ODataConfiguration.RegisterEntitySet<Site>(nameof(Sites));
			this.ODataConfiguration.RegisterEntitySet<SiteInspection>(nameof(SiteInspections));
			this.ODataConfiguration.RegisterEntitySet<UserSite>(nameof(UserSites));
		
		}

		public ODataConfiguration ODataConfiguration { get; set; }		 = new ODataConfiguration();
		
		public override void Configure(EntityConfigurationBuilder builder)
		{
			builder.EntityType<ApplicationUser>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.String)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.ClientId, true, IqlType.Integer)
				.DefineProperty(p => p.Email, true, IqlType.String)
				.DefineProperty(p => p.FullName, false, IqlType.String)
				.DefineProperty(p => p.EmailConfirmed, false, IqlType.Boolean)
				.DefineProperty(p => p.UserType, false, IqlType.Enum)
				.DefineProperty(p => p.IsLockedOut, false, IqlType.Boolean)
				.DefineProperty(p => p.Permissions, false, IqlType.Enum)
				.DefineProperty(p => p.Client, true, IqlType.Unknown)
				.DefineCollectionProperty(p => p.ClientsCreated, p => p.ClientsCreatedCount)
				.DefineCollectionProperty(p => p.DocumentCategoriesCreated, p => p.DocumentCategoriesCreatedCount)
				.DefineCollectionProperty(p => p.SiteDocumentsCreated, p => p.SiteDocumentsCreatedCount)
				.DefineCollectionProperty(p => p.FaultActionsTakenCreated, p => p.FaultActionsTakenCreatedCount)
				.DefineCollectionProperty(p => p.FaultCategoriesCreated, p => p.FaultCategoriesCreatedCount)
				.DefineCollectionProperty(p => p.FaultDefaultRecommendationsCreated, p => p.FaultDefaultRecommendationsCreatedCount)
				.DefineCollectionProperty(p => p.FaultRecommendationsCreated, p => p.FaultRecommendationsCreatedCount)
				.DefineCollectionProperty(p => p.FaultTypesCreated, p => p.FaultTypesCreatedCount)
				.DefineCollectionProperty(p => p.ProjectCreated, p => p.ProjectCreatedCount)
				.DefineCollectionProperty(p => p.ReportReceiverEmailAddressesCreated, p => p.ReportReceiverEmailAddressesCreatedCount)
				.DefineCollectionProperty(p => p.RiskAssessmentsCreated, p => p.RiskAssessmentsCreatedCount)
				.DefineCollectionProperty(p => p.RiskAssessmentAnswersCreated, p => p.RiskAssessmentAnswersCreatedCount)
				.DefineCollectionProperty(p => p.RiskAssessmentQuestionsCreated, p => p.RiskAssessmentQuestionsCreatedCount)
				.DefineCollectionProperty(p => p.PeopleCreated, p => p.PeopleCreatedCount)
				.DefineCollectionProperty(p => p.PersonInspectionsCreated, p => p.PersonInspectionsCreatedCount)
				.DefineCollectionProperty(p => p.PersonLoadingsCreated, p => p.PersonLoadingsCreatedCount)
				.DefineCollectionProperty(p => p.PersonTypesCreated, p => p.PersonTypesCreatedCount)
				.DefineCollectionProperty(p => p.FaultReportsCreated, p => p.FaultReportsCreatedCount)
				.DefineCollectionProperty(p => p.SitesCreated, p => p.SitesCreatedCount)
				.DefineCollectionProperty(p => p.SiteInspectionsCreated, p => p.SiteInspectionsCreatedCount)
				.DefineCollectionProperty(p => p.Sites, p => p.SitesCount);
			
			builder.EntityType<ApplicationUser>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Users)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Client>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.TypeId, false, IqlType.Integer)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Name, false, IqlType.String)
				.DefineProperty(p => p.Description, true, IqlType.String)
				.DefineProperty(p => p.Discount, false, IqlType.Decimal)
				.DefineProperty(p => p.AverageSales, false, IqlType.Decimal)
				.DefineProperty(p => p.AverageIncome, false, IqlType.Decimal)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.Users, p => p.UsersCount)
				.DefineProperty(p => p.Type, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount)
				.DefineCollectionProperty(p => p.Sites, p => p.SitesCount);
			
			builder.EntityType<Client>()
				.HasOne(p => p.Type)
				.WithMany(p => p.Clients)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<Client>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ClientsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ClientType>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Name, true, IqlType.String)
				.DefineCollectionProperty(p => p.Clients, p => p.ClientsCount);
			
			builder.EntityType<DocumentCategory>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Name, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount);
			
			builder.EntityType<DocumentCategory>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.DocumentCategoriesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<SiteDocument>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.CategoryId, false, IqlType.Integer)
				.DefineProperty(p => p.SiteId, false, IqlType.Integer)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Title, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Category, false, IqlType.Unknown)
				.DefineProperty(p => p.Site, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<SiteDocument>()
				.HasOne(p => p.Category)
				.WithMany(p => p.Documents)
				.WithConstraint(p => p.CategoryId, p => p.Id);
			
			builder.EntityType<SiteDocument>()
				.HasOne(p => p.Site)
				.WithMany(p => p.Documents)
				.WithConstraint(p => p.SiteId, p => p.Id);
			
			builder.EntityType<SiteDocument>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.SiteDocumentsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportActionsTaken>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.FaultReportId, false, IqlType.Integer)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Notes, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.PersonReport, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefinePropertyValidation(p => p.Notes, entity => (((entity.Notes == null ? null : entity.Notes.ToUpper()) != null) || ((entity.Notes == null ? null : entity.Notes.ToUpper()) != ("" == null ? null : "".ToUpper()))), "e83e2b21-fad0-4571-bd96-a49d16937790", "Please enter some actions taken notes")
				.DefinePropertyValidation(p => p.Notes, entity => (entity.Notes.Length > 5), "c3bea70f-ac11-49b2-b3fe-d7fa60f37579", "Please enter at least five characters for notes");
			
			builder.EntityType<ReportActionsTaken>()
				.HasOne(p => p.PersonReport)
				.WithMany(p => p.ActionsTaken)
				.WithConstraint(p => p.FaultReportId, p => p.Id);
			
			builder.EntityType<ReportActionsTaken>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultActionsTakenCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportCategory>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Name, true, IqlType.String)
				.ConfigureProperty(p => p.Name, p => {
					p.Nullable = false;
				})
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.ReportTypes, p => p.ReportTypesCount);
			
			builder.EntityType<ReportCategory>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultCategoriesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportDefaultRecommendation>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Name, true, IqlType.String)
				.DefineProperty(p => p.Text, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount);
			
			builder.EntityType<ReportDefaultRecommendation>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultDefaultRecommendationsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportRecommendation>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.ReportId, false, IqlType.Integer)
				.DefineProperty(p => p.RecommendationId, false, IqlType.Integer)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Notes, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.PersonReport, false, IqlType.Unknown)
				.DefineProperty(p => p.Recommendation, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<ReportRecommendation>()
				.HasOne(p => p.PersonReport)
				.WithMany(p => p.Recommendations)
				.WithConstraint(p => p.ReportId, p => p.Id);
			
			builder.EntityType<ReportRecommendation>()
				.HasOne(p => p.Recommendation)
				.WithMany(p => p.Recommendations)
				.WithConstraint(p => p.RecommendationId, p => p.Id);
			
			builder.EntityType<ReportRecommendation>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultRecommendationsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportType>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CategoryId, false, IqlType.Integer)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Name, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Category, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.FaultReports, p => p.FaultReportsCount);
			
			builder.EntityType<ReportType>()
				.HasOne(p => p.Category)
				.WithMany(p => p.ReportTypes)
				.WithConstraint(p => p.CategoryId, p => p.Id);
			
			builder.EntityType<ReportType>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultTypesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Project>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Title, false, IqlType.String)
				.DefineProperty(p => p.Description, true, IqlType.String)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<Project>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ProjectCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ReportReceiverEmailAddress>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.SiteId, false, IqlType.Integer)
				.DefineProperty(p => p.EmailAddress, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Site, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<ReportReceiverEmailAddress>()
				.HasOne(p => p.Site)
				.WithMany(p => p.AdditionalSendReportsTo)
				.WithConstraint(p => p.SiteId, p => p.Id);
			
			builder.EntityType<ReportReceiverEmailAddress>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ReportReceiverEmailAddressesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<RiskAssessment>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.SiteInspectionId, false, IqlType.Integer)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.SiteInspection, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.RiskAssessmentSolution, false, IqlType.Unknown);
			
			builder.EntityType<RiskAssessment>()
				.HasOne(p => p.SiteInspection)
				.WithOne(p => p.RiskAssessment)
				.WithConstraint(p => p.SiteInspectionId, p => p.Id);
			
			builder.EntityType<RiskAssessment>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.RiskAssessmentsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<RiskAssessmentSolution>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.RiskAssessmentId, false, IqlType.Integer)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.RiskAssessment, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<RiskAssessmentSolution>()
				.HasOne(p => p.RiskAssessment)
				.WithOne(p => p.RiskAssessmentSolution)
				.WithConstraint(p => p.RiskAssessmentId, p => p.Id);
			
			builder.EntityType<RiskAssessmentAnswer>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.QuestionId, false, IqlType.Integer)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.SpecificHazard, true, IqlType.String)
				.DefineProperty(p => p.PrecautionsToControlHazard, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Question, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<RiskAssessmentAnswer>()
				.HasOne(p => p.Question)
				.WithMany(p => p.Answers)
				.WithConstraint(p => p.QuestionId, p => p.Id);
			
			builder.EntityType<RiskAssessmentAnswer>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.RiskAssessmentAnswersCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<RiskAssessmentQuestion>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Name, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.Answers, p => p.AnswersCount)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<RiskAssessmentQuestion>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.RiskAssessmentQuestionsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.TypeId, true, IqlType.Integer)
				.ConfigureProperty(p => p.TypeId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.LoadingId, true, IqlType.Integer)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Key, true, IqlType.String)
				.DefineProperty(p => p.Title, true, IqlType.String)
				.ConfigureProperty(p => p.Title, p => {
					p.Description = "Some key2";
					p.FriendlyName = "Key me2";
					p.Hints = new List<string>(new [] { "!2", "Open2" });
					p.Title = "__key2";
				})
				.DefineProperty(p => p.Description, true, IqlType.String)
				.ConfigureProperty(p => p.Description, p => {
					p.Nullable = false;
				})
				.DefineProperty(p => p.Category, false, IqlType.Enum)
				.DefineProperty(p => p.ClientId, true, IqlType.Integer)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Client, true, IqlType.Unknown)
				.DefineProperty(p => p.Type, true, IqlType.Unknown)
				.DefineProperty(p => p.Loading, true, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.Types, p => p.TypesCount)
				.DefineCollectionProperty(p => p.Reports, p => p.ReportsCount)
				.DefineEntityValidation(entity => ((((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()))) && (((entity.Description == null ? null : entity.Description.ToUpper()) == null) || ((entity.Description.Trim() == null ? null : entity.Description.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())))), "NoTitleOrDescription", "Please enter either a title or a description")
				.DefineEntityValidation(entity => (((entity.Title == null ? null : entity.Title.ToUpper()) == ("Josh" == null ? null : "Josh".ToUpper())) && ((entity.Description == null ? null : entity.Description.ToUpper()) != ("Josh" == null ? null : "Josh".ToUpper()))), "JoshCheck", "If the name is 'Josh' please match it in the description")
				.DefineDisplayFormatter(entity => entity.Title, "Default")
				.DefineDisplayFormatter(entity => (((entity.Title + " (") + entity.Id) + ")"), "Report")
				.DefinePropertyValidation(p => p.Title, entity => (((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()))), "EmptyTitle", "Please enter a person title")
				.DefinePropertyValidation(p => p.Title, entity => (!((((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())))) && (entity.Title.Trim().Length > 50)), "TitleMaxLength", "Please enter less than fifty characters")
				.DefinePropertyValidation(p => p.Title, entity => (!((((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())))) && (entity.Title.Trim().Length < 3)), "TitleMinLength", "Please enter at least three characters for the person's title")
				.DefinePropertyValidation(p => p.Description, entity => (((entity.Description == null ? null : entity.Description.ToUpper()) == null) || ((entity.Description.Trim() == null ? null : entity.Description.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()))), "EmptyDescription", "Please enter a person description")
				.Configure(p => {
					p.Description = "Hello";
					p.FriendlyName = "A person";
					p.Hints = new List<string>(new [] { "%", "Print" });
					p.Title = "PERSON";
				});
			
			builder.EntityType<Person>()
				.HasOne(p => p.Client)
				.WithMany(p => p.People)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasOne(p => p.Type)
				.WithMany(p => p.People)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasOne(p => p.Loading)
				.WithMany(p => p.People)
				.WithConstraint(p => p.LoadingId, p => p.Id);
			
			builder.EntityType<Person>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PeopleCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonInspection>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.SiteInspectionId, false, IqlType.Integer)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.PersonId, false, IqlType.Integer)
				.DefineProperty(p => p.InspectionStatus, false, IqlType.Enum)
				.DefineProperty(p => p.StartTime, false, IqlType.Date)
				.DefineProperty(p => p.EndTime, false, IqlType.Date)
				.DefineProperty(p => p.ReasonForFailure, false, IqlType.Enum)
				.DefineProperty(p => p.IsDesignRequired, false, IqlType.Boolean)
				.DefineProperty(p => p.DrawingNumber, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.SiteInspection, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<PersonInspection>()
				.HasOne(p => p.SiteInspection)
				.WithMany(p => p.PersonInspections)
				.WithConstraint(p => p.SiteInspectionId, p => p.Id);
			
			builder.EntityType<PersonInspection>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PersonInspectionsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonLoading>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Name, true, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefinePropertyValidation(p => p.Name, entity => (((entity.Name == null ? null : entity.Name.ToUpper()) != null) && ((entity.Name == null ? null : entity.Name.ToUpper()) != ("" == null ? null : "".ToUpper()))), "2a784166-a6ee-4d30-a20f-489cdae3591d", "Please enter a loading name");
			
			builder.EntityType<PersonLoading>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PersonLoadingsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonType>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Title, false, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.People, p => p.PeopleCount)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.PeopleMap, p => p.PeopleMapCount);
			
			builder.EntityType<PersonType>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.PersonTypesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<PersonTypeMap>()
				.HasCompositeKey(p => p.PersonId, p => p.TypeId)
				.DefineProperty(p => p.PersonId, false, IqlType.Integer)
				.DefineProperty(p => p.TypeId, false, IqlType.Integer)
				.DefineProperty(p => p.Notes, false, IqlType.String)
				.DefineProperty(p => p.Description, false, IqlType.String)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Person, false, IqlType.Unknown)
				.DefineProperty(p => p.Type, false, IqlType.Unknown);
			
			builder.EntityType<PersonTypeMap>()
				.HasOne(p => p.Person)
				.WithMany(p => p.Types)
				.WithConstraint(p => p.PersonId, p => p.Id);
			
			builder.EntityType<PersonTypeMap>()
				.HasOne(p => p.Type)
				.WithMany(p => p.PeopleMap)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<PersonReport>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.PersonId, false, IqlType.Integer)
				.DefineProperty(p => p.TypeId, false, IqlType.Integer)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Title, true, IqlType.String)
				.DefineProperty(p => p.Status, false, IqlType.Enum)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.ActionsTaken, p => p.ActionsTakenCount)
				.DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount)
				.DefineProperty(p => p.Person, false, IqlType.Unknown)
				.DefineProperty(p => p.Type, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefinePropertyValidation(p => p.Title, entity => (((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()))), "9ad102da-7b91-4459-bc99-5947735f7da8", "Please enter a valid report title")
				.DefinePropertyValidation(p => p.Title, entity => (!((((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())))) && (entity.Title.Trim().Length > 5)), "3a5dbfb0-aeb1-414b-82de-8f4740baf69e", "Please enter less than five characters");
			
			builder.EntityType<PersonReport>()
				.HasOne(p => p.Person)
				.WithMany(p => p.Reports)
				.WithConstraint(p => p.PersonId, p => p.Id);
			
			builder.EntityType<PersonReport>()
				.HasOne(p => p.Type)
				.WithMany(p => p.FaultReports)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<PersonReport>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.FaultReportsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Site>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.ParentId, true, IqlType.Integer)
				.DefineProperty(p => p.ClientId, true, IqlType.Integer)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Address, true, IqlType.String)
				.DefineProperty(p => p.PostCode, true, IqlType.String)
				.DefineProperty(p => p.Name, true, IqlType.String)
				.DefineProperty(p => p.Left, false, IqlType.Integer)
				.DefineProperty(p => p.Right, false, IqlType.Integer)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount)
				.DefineCollectionProperty(p => p.AdditionalSendReportsTo, p => p.AdditionalSendReportsToCount)
				.DefineProperty(p => p.Parent, true, IqlType.Unknown)
				.DefineCollectionProperty(p => p.Children, p => p.ChildrenCount)
				.DefineProperty(p => p.Client, true, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				})
				.DefineCollectionProperty(p => p.SiteInspections, p => p.SiteInspectionsCount)
				.DefineCollectionProperty(p => p.Users, p => p.UsersCount);
			
			builder.EntityType<Site>()
				.HasOne(p => p.Parent)
				.WithMany(p => p.Children)
				.WithConstraint(p => p.ParentId, p => p.Id);
			
			builder.EntityType<Site>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Sites)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Site>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.SitesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<SiteInspection>()
				.HasKey(p => p.Id, IqlType.Unknown)
				.DefineProperty(p => p.Id, false, IqlType.Integer)
				.ConfigureProperty(p => p.Id, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.SiteId, false, IqlType.Integer)
				.DefineProperty(p => p.CreatedByUserId, true, IqlType.String)
				.ConfigureProperty(p => p.CreatedByUserId, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.StartTime, false, IqlType.Date)
				.DefineProperty(p => p.EndTime, false, IqlType.Date)
				.DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.Guid, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.CreatedDate, false, IqlType.Date)
				.ConfigureProperty(p => p.CreatedDate, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.Version, false, IqlType.Integer)
				.ConfigureProperty(p => p.Version, p => {
					p.ReadOnly = true;
				})
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String)
				.ConfigureProperty(p => p.PersistenceKey, p => {
					p.ReadOnly = true;
				})
				.DefineProperty(p => p.RiskAssessment, false, IqlType.Unknown)
				.DefineCollectionProperty(p => p.PersonInspections, p => p.PersonInspectionsCount)
				.DefineProperty(p => p.Site, false, IqlType.Unknown)
				.DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown)
				.ConfigureProperty(p => p.CreatedByUser, p => {
					p.ReadOnly = true;
				});
			
			builder.EntityType<SiteInspection>()
				.HasOne(p => p.Site)
				.WithMany(p => p.SiteInspections)
				.WithConstraint(p => p.SiteId, p => p.Id);
			
			builder.EntityType<SiteInspection>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.SiteInspectionsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<UserSite>()
				.HasCompositeKey(p => p.SiteId, p => p.UserId)
				.DefineProperty(p => p.SiteId, false, IqlType.Integer)
				.DefineProperty(p => p.UserId, true, IqlType.String)
				.DefineProperty(p => p.User, false, IqlType.Unknown)
				.DefineProperty(p => p.Site, false, IqlType.Unknown);
			
			builder.EntityType<UserSite>()
				.HasOne(p => p.User)
				.WithMany(p => p.Sites)
				.WithConstraint(p => p.UserId, p => p.Id);
			
			builder.EntityType<UserSite>()
				.HasOne(p => p.Site)
				.WithMany(p => p.Users)
				.WithConstraint(p => p.SiteId, p => p.Id);
		
		}
		
		
		public ApplicationUserSet Users { get; set; }
		
		public ClientSet Clients { get; set; }
		
		public ClientTypeSet ClientTypes { get; set; }
		
		public DocumentCategorySet DocumentCategories { get; set; }
		
		public SiteDocumentSet SiteDocuments { get; set; }
		
		public ReportActionsTakenSet ReportActionsTaken { get; set; }
		
		public ReportCategorySet ReportCategories { get; set; }
		
		public ReportDefaultRecommendationSet ReportDefaultRecommendations { get; set; }
		
		public ReportRecommendationSet ReportRecommendations { get; set; }
		
		public ReportTypeSet ReportTypes { get; set; }
		
		public ProjectSet Projects { get; set; }
		
		public ReportReceiverEmailAddressSet ReportReceiverEmailAddresses { get; set; }
		
		public RiskAssessmentSet RiskAssessments { get; set; }
		
		public RiskAssessmentSolutionSet RiskAssessmentSolutions { get; set; }
		
		public RiskAssessmentAnswerSet RiskAssessmentAnswers { get; set; }
		
		public RiskAssessmentQuestionSet RiskAssessmentQuestions { get; set; }
		
		public PersonSet People { get; set; }
		
		public PersonInspectionSet PersonInspections { get; set; }
		
		public PersonLoadingSet PersonLoadings { get; set; }
		
		public PersonTypeSet PersonTypes { get; set; }
		
		public PersonTypeMapSet PersonTypesMap { get; set; }
		
		public PersonReportSet PersonReports { get; set; }
		
		public SiteSet Sites { get; set; }
		
		public SiteInspectionSet SiteInspections { get; set; }
		
		public UserSiteSet UserSites { get; set; }
		public virtual ODataDataMethodRequest<string> SendHi(string name)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(name, typeof(string), "name", false));
			return ((ODataDataStore)this.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Global,
				"Tunnel",
				"SendHi",
				null,
				typeof(String));
		}
	
	}
}

