using Iql.OData.Data;
using Iql.Queryable.Data.Validation;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
public enum FaultReportStatus {
	Fail = 0,
	PassWithObservations = 1
}

public enum InspectionFailReason {
	None = 0,
	UnableToAccess = 1,
	PersistentFaults = 2,
	FailuresInFaultReports = 3,
	TooManyMinorObservations = 4,
	NoDesignSupplied = 5
}

public enum PersonInspectionStatus {
	Pass = 0,
	Fail = 1,
	PassWithObservations = 2
}

public enum PersonCategory {
	System = 0,
	Conventional = 1
}

public enum UserType {
	Super = 1,
	Client = 2,
	Candidate = 3
}

public class UserSite : UserSiteBase, IEntity {
	public int SiteId { get; set; }
	public string UserId { get; set; }
	public ApplicationUser User { get; set; }
	public Site Site { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class SiteInspection : SiteInspectionBase, IEntity {
	public int Id { get; set; }
	public int RiskAssessmentId { get; set; }
	public int SiteId { get; set; }
	public string CreatedByUserId { get; set; }
	public DateTimeOffset StartTime { get; set; }
	public DateTimeOffset EndTime { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public List<PersonInspection> PersonInspections { get; set; }
	public RiskAssessment RiskAssessment { get; set; }
	public Site Site { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Site : SiteBase, IEntity {
	public int Id { get; set; }
	public int? ParentId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Address { get; set; }
	public string PostCode { get; set; }
	public int? ClientId { get; set; }
	public string Name { get; set; }
	public int Left { get; set; }
	public int Right { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public List<SiteDocument> Documents { get; set; }
	public List<ReportReceiverEmailAddress> AdditionalSendReportsTo { get; set; }
	public Site Parent { get; set; }
	public List<Site> Children { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public Client Client { get; set; }
	public List<SiteInspection> SiteInspections { get; set; }
	public List<UserSite> Users { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class PersonReport : PersonReportBase, IEntity {
	public int Id { get; set; }
	public int PersonId { get; set; }
	public int TypeId { get; set; }
	public string CreatedByUserId { get; set; }
	public FaultReportStatus Status { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public List<ReportActionsTaken> ActionsTaken { get; set; }
	public List<ReportRecommendation> Recommendations { get; set; }
	public Person Person { get; set; }
	public ReportType Type { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class PersonTypeMap : PersonTypeMapBase, IEntity {
	public int PersonId { get; set; }
	public int TypeId { get; set; }
	public string Notes { get; set; }
	public Person Person { get; set; }
	public PersonType Type { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class PersonType : PersonTypeBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public List<Person> People { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<PersonTypeMap> PeopleMap { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class PersonLoading : PersonLoadingBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public List<Person> People { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class PersonInspection : PersonInspectionBase, IEntity {
	public int SiteInspectionId { get; set; }
	public string CreatedByUserId { get; set; }
	public int PersonId { get; set; }
	public PersonInspectionStatus InspectionStatus { get; set; }
	public DateTimeOffset StartTime { get; set; }
	public DateTimeOffset EndTime { get; set; }
	public InspectionFailReason ReasonForFailure { get; set; }
	public bool IsDesignRequired { get; set; }
	public string DrawingNumber { get; set; }
	public string Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public SiteInspection SiteInspection { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Person : PersonBase, IEntity {
	public int? TypeId { get; set; }
	public int? LoadingId { get; set; }
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Key { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public PersonCategory Category { get; set; }
	public int? ClientId { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public Client Client { get; set; }
	public PersonType Type { get; set; }
	public PersonLoading Loading { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<PersonTypeMap> Types { get; set; }
	public List<PersonReport> Reports { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	
	public async virtual Task<ODataResult<string>> IncrementVersion() {
		// Call API somehow
		var parameters = new JObject();
		
		return await this.GetODataDataStore().PostOnEntityInstance<Person, string>(this, parameters);
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		if(!(true)) {
			validationResult.AddFailure("Please enter either a title or a description");
		}
		if(!(true)) {
			validationResult.AddFailure("If the name is 'Josh' please match it in the description");
		}
		validationResult.AddPropertyValidationResult(this.ValidateTitle());
		return validationResult;
	}
	public PropertyValidationResult ValidateTitle() {
		var validationResult = new PropertyValidationResult(this.GetType(), "Title");
		var entity = this;
		if(!(true)) {
			validationResult.AddFailure("Please enter a valid title");
		}
		if(!(true)) {
			validationResult.AddFailure("Please enter less than fifty characters");
		}
		return validationResult;
	}
}

public class RiskAssessmentQuestion : RiskAssessmentQuestionBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public List<RiskAssessmentAnswer> Answers { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class RiskAssessmentAnswer : RiskAssessmentAnswerBase, IEntity {
	public int QuestionId { get; set; }
	public string CreatedByUserId { get; set; }
	public string SpecificHazard { get; set; }
	public string PrecautionsToControlHazard { get; set; }
	public string Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public RiskAssessmentQuestion Question { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class RiskAssessment : RiskAssessmentBase, IEntity {
	public int SiteInspectionId { get; set; }
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public SiteInspection SiteInspection { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportReceiverEmailAddress : ReportReceiverEmailAddressBase, IEntity {
	public string CreatedByUserId { get; set; }
	public int SiteId { get; set; }
	public string EmailAddress { get; set; }
	public string Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public Site Site { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Project : ProjectBase, IEntity {
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string CreatedByUserId { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportType : ReportTypeBase, IEntity {
	public int Id { get; set; }
	public int CategoryId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public ReportCategory Category { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<PersonReport> FaultReports { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportRecommendation : ReportRecommendationBase, IEntity {
	public int FaultReportId { get; set; }
	public int RecommendationId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Notes { get; set; }
	public string Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public PersonReport PersonReport { get; set; }
	public ReportDefaultRecommendation Recommendation { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportDefaultRecommendation : ReportDefaultRecommendationBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public string Text { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<ReportRecommendation> Recommendations { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportCategory : ReportCategoryBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<ReportType> FaultTypes { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportActionsTaken : ReportActionsTakenBase, IEntity {
	public int FaultReportId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Notes { get; set; }
	public string Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public PersonReport PersonReport { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class SiteDocument : SiteDocumentBase, IEntity {
	public int CategoryId { get; set; }
	public int SiteId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Title { get; set; }
	public string Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public DocumentCategory Category { get; set; }
	public Site Site { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class DocumentCategory : DocumentCategoryBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public string Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<SiteDocument> Documents { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ClientType : ClientTypeBase, IEntity {
	public int Id { get; set; }
	public string Name { get; set; }
	public List<Client> Clients { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Client : ClientBase, IEntity {
	public int TypeId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public int Version { get; set; }
	public List<ApplicationUser> Users { get; set; }
	public ClientType Type { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<Person> People { get; set; }
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ApplicationUser : ApplicationUserBase, IEntity {
	public string Id { get; set; }
	public int? ClientId { get; set; }
	public string Email { get; set; }
	public string FullName { get; set; }
	public bool EmailConfirmed { get; set; }
	public UserType UserType { get; set; }
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
	public override ODataDataStore GetODataDataStore() {
		return null;
	}
	
	public async virtual Task<ODataResult<string>> GeneratePasswordResetLink() {
		// Call API somehow
		var parameters = new JObject();
		
		return await this.GetODataDataStore().GetOnEntityInstance<ApplicationUser, string>(this, parameters);
	}
	public async virtual Task<ODataResult<string>> AccountConfirm() {
		// Call API somehow
		var parameters = new JObject();
		
		return await this.GetODataDataStore().PostOnEntityInstance<ApplicationUser, string>(this, parameters);
	}
	public async virtual Task<ODataResult<string>> SendAccountConfirmationEmail() {
		// Call API somehow
		var parameters = new JObject();
		
		return await this.GetODataDataStore().PostOnEntityInstance<ApplicationUser, string>(this, parameters);
	}
	public async virtual Task<ODataResult<string>> SendPasswordResetEmail() {
		// Call API somehow
		var parameters = new JObject();
		
		return await this.GetODataDataStore().PostOnEntityInstance<ApplicationUser, string>(this, parameters);
	}
	public async virtual Task<ODataResult<string>> ReinstateUser() {
		// Call API somehow
		var parameters = new JObject();
		
		return await this.GetODataDataStore().PostOnEntityInstance<ApplicationUser, string>(this, parameters);
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}



public class UserSiteBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class SiteInspectionBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class SiteBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonReportBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonTypeMapBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonTypeBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonLoadingBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonInspectionBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class RiskAssessmentQuestionBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class RiskAssessmentAnswerBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class RiskAssessmentBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportReceiverEmailAddressBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ProjectBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportTypeBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportRecommendationBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportDefaultRecommendationBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportCategoryBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportActionsTakenBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class SiteDocumentBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class DocumentCategoryBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ClientTypeBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ClientBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ApplicationUserBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public virtual ODataDataStore GetODataDataStore() {
		return null;
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


