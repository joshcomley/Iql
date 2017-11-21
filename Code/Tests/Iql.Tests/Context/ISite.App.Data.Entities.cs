using Iql.OData.Data;
using Iql.Queryable.Data.Validation;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


public class UserSiteBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "UserSite";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "SiteInspection";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "Site";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ScaffoldTypeBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ScaffoldType";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ScaffoldLoadingBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ScaffoldLoading";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ScaffoldInspectionBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ScaffoldInspection";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ScaffoldBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "Scaffold";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "RiskAssessmentQuestion";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "RiskAssessmentAnswer";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "RiskAssessment";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "ReportReceiverEmailAddress";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "Project";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultTypeBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultType";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultReportBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultReport";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultRecommendationBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultRecommendation";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultDefaultRecommendationBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultDefaultRecommendation";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultCategoryBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultCategory";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultActionsTakenBase : IEntity {
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultActionsTaken";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "SiteDocument";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "DocumentCategory";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "ClientType";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "Client";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public static string ClassName() {
		return "ApplicationUser";
	}
	public virtual ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public enum InspectionFailReason {
	None = 0,
	UnableToAccess = 1,
	PersistentFaults = 2,
	FailuresInFaultReports = 3,
	TooManyMinorObservations = 4,
	NoDesignSupplied = 5
}

public enum ScaffoldInspectionStatus {
	Pass = 0,
	Fail = 1,
	PassWithObservations = 2
}

public enum ScaffoldCategory {
	System = 0,
	Conventional = 1
}

public enum FaultReportStatus {
	Fail = 0,
	PassWithObservations = 1
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
		throw new Exception("Not implemented.");
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
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<ScaffoldInspection> ScaffoldInspections { get; set; }
	public long? ScaffoldInspectionsCount { get; set; }
	public RiskAssessment RiskAssessment { get; set; }
	public Site Site { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<SiteDocument> Documents { get; set; }
	public long? DocumentsCount { get; set; }
	public List<ReportReceiverEmailAddress> AdditionalSendReportsTo { get; set; }
	public long? AdditionalSendReportsToCount { get; set; }
	public Site Parent { get; set; }
	public List<Site> Children { get; set; }
	public long? ChildrenCount { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public Client Client { get; set; }
	public List<SiteInspection> SiteInspections { get; set; }
	public long? SiteInspectionsCount { get; set; }
	public List<UserSite> Users { get; set; }
	public long? UsersCount { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ScaffoldType : ScaffoldTypeBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<Scaffold> Scaffolds { get; set; }
	public long? ScaffoldsCount { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ScaffoldLoading : ScaffoldLoadingBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<Scaffold> Scaffolds { get; set; }
	public long? ScaffoldsCount { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ScaffoldInspection : ScaffoldInspectionBase, IEntity {
	public int SiteInspectionId { get; set; }
	public string CreatedByUserId { get; set; }
	public int ScaffoldId { get; set; }
	public ScaffoldInspectionStatus InspectionStatus { get; set; }
	public DateTimeOffset StartTime { get; set; }
	public DateTimeOffset EndTime { get; set; }
	public InspectionFailReason ReasonForFailure { get; set; }
	public bool IsDesignRequired { get; set; }
	public string DrawingNumber { get; set; }
	public Guid Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public SiteInspection SiteInspection { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Scaffold : ScaffoldBase, IEntity {
	public int Id { get; set; }
	public int TypeId { get; set; }
	public int LoadingId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Key { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public ScaffoldCategory Category { get; set; }
	public int ClientId { get; set; }
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<FaultReport> FaultReports { get; set; }
	public long? FaultReportsCount { get; set; }
	public Client Client { get; set; }
	public ScaffoldType Type { get; set; }
	public ScaffoldLoading Loading { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	
	public async virtual Task<ODataResult<string>> IncrementVersion() {
		// Call API somehow
		var parameters = new JObject();
		
		return await this.GetODataDataStore().PostOnEntityInstance<Scaffold, string>(this, parameters);
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
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<RiskAssessmentAnswer> Answers { get; set; }
	public long? AnswersCount { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public Guid Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public RiskAssessmentQuestion Question { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public SiteInspection SiteInspection { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public Guid Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public Site Site { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultType : FaultTypeBase, IEntity {
	public int Id { get; set; }
	public int CategoryId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<FaultReport> FaultReports { get; set; }
	public long? FaultReportsCount { get; set; }
	public FaultCategory Category { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		validationResult.AddPropertyValidationResult(this.ValidateName());
		return validationResult;
	}
	public PropertyValidationResult ValidateName() {
		var validationResult = new PropertyValidationResult(this.GetType(), "Name");
		var entity = this;
		if(!(true)) {
			validationResult.AddFailure("Please enter a valid fault type title");
		}
		return validationResult;
	}
}

public class FaultReport : FaultReportBase, IEntity {
	public int Id { get; set; }
	public int ScaffoldId { get; set; }
	public int TypeId { get; set; }
	public string CreatedByUserId { get; set; }
	public FaultReportStatus Status { get; set; }
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<FaultActionsTaken> ActionsTaken { get; set; }
	public long? ActionsTakenCount { get; set; }
	public List<FaultRecommendation> Recommendations { get; set; }
	public long? RecommendationsCount { get; set; }
	public Scaffold Scaffold { get; set; }
	public FaultType Type { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultRecommendation : FaultRecommendationBase, IEntity {
	public int FaultReportId { get; set; }
	public int RecommendationId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Notes { get; set; }
	public Guid Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public FaultReport FaultReport { get; set; }
	public FaultDefaultRecommendation Recommendation { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultDefaultRecommendation : FaultDefaultRecommendationBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public string Text { get; set; }
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<FaultRecommendation> Recommendations { get; set; }
	public long? RecommendationsCount { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultCategory : FaultCategoryBase, IEntity {
	public int Id { get; set; }
	public string CreatedByUserId { get; set; }
	public string Name { get; set; }
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<FaultType> FaultTypes { get; set; }
	public long? FaultTypesCount { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
	}
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultActionsTaken : FaultActionsTakenBase, IEntity {
	public int FaultReportId { get; set; }
	public string CreatedByUserId { get; set; }
	public string Notes { get; set; }
	public Guid Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public FaultReport FaultReport { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public Guid Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public DocumentCategory Category { get; set; }
	public Site Site { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public Guid Guid { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<SiteDocument> Documents { get; set; }
	public long? DocumentsCount { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public long? ClientsCount { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public Guid Guid { get; set; }
	public int Id { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public long Version { get; set; }
	public Guid PersistenceKey { get; set; }
	public List<ApplicationUser> Users { get; set; }
	public long? UsersCount { get; set; }
	public ClientType Type { get; set; }
	public ApplicationUser CreatedByUser { get; set; }
	public List<Scaffold> Scaffolds { get; set; }
	public long? ScaffoldsCount { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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
	public long? ClientsCreatedCount { get; set; }
	public List<DocumentCategory> DocumentCategoriesCreated { get; set; }
	public long? DocumentCategoriesCreatedCount { get; set; }
	public List<SiteDocument> SiteDocumentsCreated { get; set; }
	public long? SiteDocumentsCreatedCount { get; set; }
	public List<FaultActionsTaken> FaultActionsTakenCreated { get; set; }
	public long? FaultActionsTakenCreatedCount { get; set; }
	public List<FaultCategory> FaultCategoriesCreated { get; set; }
	public long? FaultCategoriesCreatedCount { get; set; }
	public List<FaultDefaultRecommendation> FaultDefaultRecommendationsCreated { get; set; }
	public long? FaultDefaultRecommendationsCreatedCount { get; set; }
	public List<FaultRecommendation> FaultRecommendationsCreated { get; set; }
	public long? FaultRecommendationsCreatedCount { get; set; }
	public List<FaultReport> FaultReportsCreated { get; set; }
	public long? FaultReportsCreatedCount { get; set; }
	public List<FaultType> FaultTypesCreated { get; set; }
	public long? FaultTypesCreatedCount { get; set; }
	public List<Project> ProjectCreated { get; set; }
	public long? ProjectCreatedCount { get; set; }
	public List<ReportReceiverEmailAddress> ReportReceiverEmailAddressesCreated { get; set; }
	public long? ReportReceiverEmailAddressesCreatedCount { get; set; }
	public List<RiskAssessment> RiskAssessmentsCreated { get; set; }
	public long? RiskAssessmentsCreatedCount { get; set; }
	public List<RiskAssessmentAnswer> RiskAssessmentAnswersCreated { get; set; }
	public long? RiskAssessmentAnswersCreatedCount { get; set; }
	public List<RiskAssessmentQuestion> RiskAssessmentQuestionsCreated { get; set; }
	public long? RiskAssessmentQuestionsCreatedCount { get; set; }
	public List<Scaffold> ScaffoldsCreated { get; set; }
	public long? ScaffoldsCreatedCount { get; set; }
	public List<ScaffoldInspection> ScaffoldInspectionsCreated { get; set; }
	public long? ScaffoldInspectionsCreatedCount { get; set; }
	public List<ScaffoldLoading> ScaffoldLoadingsCreated { get; set; }
	public long? ScaffoldLoadingsCreatedCount { get; set; }
	public List<ScaffoldType> ScaffoldTypesCreated { get; set; }
	public long? ScaffoldTypesCreatedCount { get; set; }
	public List<Site> SitesCreated { get; set; }
	public long? SitesCreatedCount { get; set; }
	public List<SiteInspection> SiteInspectionsCreated { get; set; }
	public long? SiteInspectionsCreatedCount { get; set; }
	public List<UserSite> Sites { get; set; }
	public long? SitesCount { get; set; }
	public override ODataDataStore GetODataDataStore() {
		throw new Exception("Not implemented.");
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

