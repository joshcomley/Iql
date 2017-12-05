using Iql.Queryable;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Events;
using System;


public class UserSiteBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "UserSite";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class SiteInspectionBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "SiteInspection";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class SiteBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "Site";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ScaffoldTypeBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ScaffoldType";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ScaffoldLoadingBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ScaffoldLoading";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ScaffoldInspectionBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ScaffoldInspection";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ScaffoldBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "Scaffold";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class RiskAssessmentQuestionBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "RiskAssessmentQuestion";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class RiskAssessmentAnswerBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "RiskAssessmentAnswer";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class RiskAssessmentBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "RiskAssessment";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportReceiverEmailAddressBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ReportReceiverEmailAddress";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ProjectBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "Project";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultTypeBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultType";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultReportBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultReport";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultRecommendationBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultRecommendation";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultDefaultRecommendationBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultDefaultRecommendation";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultCategoryBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultCategory";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class FaultActionsTakenBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "FaultActionsTaken";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class SiteDocumentBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "SiteDocument";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class DocumentCategoryBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "DocumentCategory";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ClientTypeBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ClientType";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ClientBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "Client";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ApplicationUserBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ApplicationUser";
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
	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			var oldValue = _siteId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<UserSite>(nameof(SiteId), this, oldValue, value));
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>(nameof(SiteId), this, oldValue, value));
		}
	}

	private string _userId;
	public string UserId
	{
		get => _userId;
		set
		{
			var oldValue = _userId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<UserSite>(nameof(UserId), this, oldValue, value));
			_userId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>(nameof(UserId), this, oldValue, value));
		}
	}

	private ApplicationUser _user;
	public ApplicationUser User
	{
		get => _user;
		set
		{
			var oldValue = _user;
			this.PropertyChanging.Emit(new PropertyChangeEvent<UserSite>(nameof(User), this, oldValue, value));
			_user = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>(nameof(User), this, oldValue, value));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			var oldValue = _site;
			this.PropertyChanging.Emit(new PropertyChangeEvent<UserSite>(nameof(Site), this, oldValue, value));
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>(nameof(Site), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class SiteInspection : SiteInspectionBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(Id), this, oldValue, value));
		}
	}

	private int? _riskAssessmentId;
	public int? RiskAssessmentId
	{
		get => _riskAssessmentId;
		set
		{
			var oldValue = _riskAssessmentId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(RiskAssessmentId), this, oldValue, value));
			_riskAssessmentId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(RiskAssessmentId), this, oldValue, value));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			var oldValue = _siteId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(SiteId), this, oldValue, value));
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(SiteId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private DateTimeOffset _startTime;
	public DateTimeOffset StartTime
	{
		get => _startTime;
		set
		{
			var oldValue = _startTime;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(StartTime), this, oldValue, value));
			_startTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(StartTime), this, oldValue, value));
		}
	}

	private DateTimeOffset _endTime;
	public DateTimeOffset EndTime
	{
		get => _endTime;
		set
		{
			var oldValue = _endTime;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(EndTime), this, oldValue, value));
			_endTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(EndTime), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 ScaffoldInspectionsCount { get; set; }
	private RelatedList<SiteInspection,ScaffoldInspection> _scaffoldInspections;
	public RelatedList<SiteInspection,ScaffoldInspection> ScaffoldInspections
	{
		get
		{
			this._scaffoldInspections = _scaffoldInspections ?? new RelatedList<SiteInspection,ScaffoldInspection>(this);
			return _scaffoldInspections;
		}
		set
		{
			var oldValue = _scaffoldInspections;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(ScaffoldInspections), this, oldValue, value));
			_scaffoldInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(ScaffoldInspections), this, oldValue, value));
		}
	}

	private RiskAssessment _riskAssessment;
	public RiskAssessment RiskAssessment
	{
		get => _riskAssessment;
		set
		{
			var oldValue = _riskAssessment;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(RiskAssessment), this, oldValue, value));
			_riskAssessment = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(RiskAssessment), this, oldValue, value));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			var oldValue = _site;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(Site), this, oldValue, value));
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(Site), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Site : SiteBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Id), this, oldValue, value));
		}
	}

	private int? _parentId;
	public int? ParentId
	{
		get => _parentId;
		set
		{
			var oldValue = _parentId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(ParentId), this, oldValue, value));
			_parentId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(ParentId), this, oldValue, value));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			var oldValue = _clientId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(ClientId), this, oldValue, value));
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(ClientId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _address;
	public string Address
	{
		get => _address;
		set
		{
			var oldValue = _address;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Address), this, oldValue, value));
			_address = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Address), this, oldValue, value));
		}
	}

	private string _postCode;
	public string PostCode
	{
		get => _postCode;
		set
		{
			var oldValue = _postCode;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(PostCode), this, oldValue, value));
			_postCode = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(PostCode), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Name), this, oldValue, value));
		}
	}

	private int _left;
	public int Left
	{
		get => _left;
		set
		{
			var oldValue = _left;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Left), this, oldValue, value));
			_left = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Left), this, oldValue, value));
		}
	}

	private int _right;
	public int Right
	{
		get => _right;
		set
		{
			var oldValue = _right;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Right), this, oldValue, value));
			_right = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Right), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 DocumentsCount { get; set; }
	private RelatedList<Site,SiteDocument> _documents;
	public RelatedList<Site,SiteDocument> Documents
	{
		get
		{
			this._documents = _documents ?? new RelatedList<Site,SiteDocument>(this);
			return _documents;
		}
		set
		{
			var oldValue = _documents;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Documents), this, oldValue, value));
			_documents = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Documents), this, oldValue, value));
		}
	}

	
	public Int64 AdditionalSendReportsToCount { get; set; }
	private RelatedList<Site,ReportReceiverEmailAddress> _additionalSendReportsTo;
	public RelatedList<Site,ReportReceiverEmailAddress> AdditionalSendReportsTo
	{
		get
		{
			this._additionalSendReportsTo = _additionalSendReportsTo ?? new RelatedList<Site,ReportReceiverEmailAddress>(this);
			return _additionalSendReportsTo;
		}
		set
		{
			var oldValue = _additionalSendReportsTo;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(AdditionalSendReportsTo), this, oldValue, value));
			_additionalSendReportsTo = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(AdditionalSendReportsTo), this, oldValue, value));
		}
	}

	private Site _parent;
	public Site Parent
	{
		get => _parent;
		set
		{
			var oldValue = _parent;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Parent), this, oldValue, value));
			_parent = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Parent), this, oldValue, value));
		}
	}

	
	public Int64 ChildrenCount { get; set; }
	private RelatedList<Site,Site> _children;
	public RelatedList<Site,Site> Children
	{
		get
		{
			this._children = _children ?? new RelatedList<Site,Site>(this);
			return _children;
		}
		set
		{
			var oldValue = _children;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Children), this, oldValue, value));
			_children = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Children), this, oldValue, value));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			var oldValue = _client;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Client), this, oldValue, value));
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Client), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 SiteInspectionsCount { get; set; }
	private RelatedList<Site,SiteInspection> _siteInspections;
	public RelatedList<Site,SiteInspection> SiteInspections
	{
		get
		{
			this._siteInspections = _siteInspections ?? new RelatedList<Site,SiteInspection>(this);
			return _siteInspections;
		}
		set
		{
			var oldValue = _siteInspections;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(SiteInspections), this, oldValue, value));
			_siteInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(SiteInspections), this, oldValue, value));
		}
	}

	
	public Int64 UsersCount { get; set; }
	private RelatedList<Site,UserSite> _users;
	public RelatedList<Site,UserSite> Users
	{
		get
		{
			this._users = _users ?? new RelatedList<Site,UserSite>(this);
			return _users;
		}
		set
		{
			var oldValue = _users;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Site>(nameof(Users), this, oldValue, value));
			_users = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>(nameof(Users), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ScaffoldType : ScaffoldTypeBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Title), this, oldValue, value));
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Title), this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Description), this, oldValue, value));
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Description), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 ScaffoldsCount { get; set; }
	private RelatedList<ScaffoldType,Scaffold> _scaffolds;
	public RelatedList<ScaffoldType,Scaffold> Scaffolds
	{
		get
		{
			this._scaffolds = _scaffolds ?? new RelatedList<ScaffoldType,Scaffold>(this);
			return _scaffolds;
		}
		set
		{
			var oldValue = _scaffolds;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Scaffolds), this, oldValue, value));
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(Scaffolds), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ScaffoldLoading : ScaffoldLoadingBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Name), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 ScaffoldsCount { get; set; }
	private RelatedList<ScaffoldLoading,Scaffold> _scaffolds;
	public RelatedList<ScaffoldLoading,Scaffold> Scaffolds
	{
		get
		{
			this._scaffolds = _scaffolds ?? new RelatedList<ScaffoldLoading,Scaffold>(this);
			return _scaffolds;
		}
		set
		{
			var oldValue = _scaffolds;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Scaffolds), this, oldValue, value));
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(Scaffolds), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ScaffoldInspection : ScaffoldInspectionBase, IEntity {
	private int _siteInspectionId;
	public int SiteInspectionId
	{
		get => _siteInspectionId;
		set
		{
			var oldValue = _siteInspectionId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(SiteInspectionId), this, oldValue, value));
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(SiteInspectionId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private int _scaffoldId;
	public int ScaffoldId
	{
		get => _scaffoldId;
		set
		{
			var oldValue = _scaffoldId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(ScaffoldId), this, oldValue, value));
			_scaffoldId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(ScaffoldId), this, oldValue, value));
		}
	}

	private ScaffoldInspectionStatus _inspectionStatus;
	public ScaffoldInspectionStatus InspectionStatus
	{
		get => _inspectionStatus;
		set
		{
			var oldValue = _inspectionStatus;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(InspectionStatus), this, oldValue, value));
			_inspectionStatus = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(InspectionStatus), this, oldValue, value));
		}
	}

	private DateTimeOffset _startTime;
	public DateTimeOffset StartTime
	{
		get => _startTime;
		set
		{
			var oldValue = _startTime;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(StartTime), this, oldValue, value));
			_startTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(StartTime), this, oldValue, value));
		}
	}

	private DateTimeOffset _endTime;
	public DateTimeOffset EndTime
	{
		get => _endTime;
		set
		{
			var oldValue = _endTime;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(EndTime), this, oldValue, value));
			_endTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(EndTime), this, oldValue, value));
		}
	}

	private InspectionFailReason _reasonForFailure;
	public InspectionFailReason ReasonForFailure
	{
		get => _reasonForFailure;
		set
		{
			var oldValue = _reasonForFailure;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(ReasonForFailure), this, oldValue, value));
			_reasonForFailure = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(ReasonForFailure), this, oldValue, value));
		}
	}

	private bool _isDesignRequired;
	public bool IsDesignRequired
	{
		get => _isDesignRequired;
		set
		{
			var oldValue = _isDesignRequired;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(IsDesignRequired), this, oldValue, value));
			_isDesignRequired = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(IsDesignRequired), this, oldValue, value));
		}
	}

	private string _drawingNumber;
	public string DrawingNumber
	{
		get => _drawingNumber;
		set
		{
			var oldValue = _drawingNumber;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(DrawingNumber), this, oldValue, value));
			_drawingNumber = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(DrawingNumber), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private SiteInspection _siteInspection;
	public SiteInspection SiteInspection
	{
		get => _siteInspection;
		set
		{
			var oldValue = _siteInspection;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(SiteInspection), this, oldValue, value));
			_siteInspection = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(SiteInspection), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Scaffold : ScaffoldBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Id), this, oldValue, value));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			var oldValue = _typeId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(TypeId), this, oldValue, value));
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(TypeId), this, oldValue, value));
		}
	}

	private int _loadingId;
	public int LoadingId
	{
		get => _loadingId;
		set
		{
			var oldValue = _loadingId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(LoadingId), this, oldValue, value));
			_loadingId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(LoadingId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _key;
	public string Key
	{
		get => _key;
		set
		{
			var oldValue = _key;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Key), this, oldValue, value));
			_key = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Key), this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Title), this, oldValue, value));
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Title), this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Description), this, oldValue, value));
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Description), this, oldValue, value));
		}
	}

	private ScaffoldCategory _category;
	public ScaffoldCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Category), this, oldValue, value));
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Category), this, oldValue, value));
		}
	}

	private int _clientId;
	public int ClientId
	{
		get => _clientId;
		set
		{
			var oldValue = _clientId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(ClientId), this, oldValue, value));
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(ClientId), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 FaultReportsCount { get; set; }
	private RelatedList<Scaffold,FaultReport> _faultReports;
	public RelatedList<Scaffold,FaultReport> FaultReports
	{
		get
		{
			this._faultReports = _faultReports ?? new RelatedList<Scaffold,FaultReport>(this);
			return _faultReports;
		}
		set
		{
			var oldValue = _faultReports;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(FaultReports), this, oldValue, value));
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(FaultReports), this, oldValue, value));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			var oldValue = _client;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Client), this, oldValue, value));
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Client), this, oldValue, value));
		}
	}

	private ScaffoldType _type;
	public ScaffoldType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Type), this, oldValue, value));
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Type), this, oldValue, value));
		}
	}

	private ScaffoldLoading _loading;
	public ScaffoldLoading Loading
	{
		get => _loading;
		set
		{
			var oldValue = _loading;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(Loading), this, oldValue, value));
			_loading = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(Loading), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Scaffold>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>(nameof(CreatedByUser), this, oldValue, value));
		}
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
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Name), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 AnswersCount { get; set; }
	private RelatedList<RiskAssessmentQuestion,RiskAssessmentAnswer> _answers;
	public RelatedList<RiskAssessmentQuestion,RiskAssessmentAnswer> Answers
	{
		get
		{
			this._answers = _answers ?? new RelatedList<RiskAssessmentQuestion,RiskAssessmentAnswer>(this);
			return _answers;
		}
		set
		{
			var oldValue = _answers;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Answers), this, oldValue, value));
			_answers = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Answers), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class RiskAssessmentAnswer : RiskAssessmentAnswerBase, IEntity {
	private int _questionId;
	public int QuestionId
	{
		get => _questionId;
		set
		{
			var oldValue = _questionId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(QuestionId), this, oldValue, value));
			_questionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(QuestionId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _specificHazard;
	public string SpecificHazard
	{
		get => _specificHazard;
		set
		{
			var oldValue = _specificHazard;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(SpecificHazard), this, oldValue, value));
			_specificHazard = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(SpecificHazard), this, oldValue, value));
		}
	}

	private string _precautionsToControlHazard;
	public string PrecautionsToControlHazard
	{
		get => _precautionsToControlHazard;
		set
		{
			var oldValue = _precautionsToControlHazard;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(PrecautionsToControlHazard), this, oldValue, value));
			_precautionsToControlHazard = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(PrecautionsToControlHazard), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private RiskAssessmentQuestion _question;
	public RiskAssessmentQuestion Question
	{
		get => _question;
		set
		{
			var oldValue = _question;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Question), this, oldValue, value));
			_question = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Question), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class RiskAssessment : RiskAssessmentBase, IEntity {
	private int _siteInspectionId;
	public int SiteInspectionId
	{
		get => _siteInspectionId;
		set
		{
			var oldValue = _siteInspectionId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(SiteInspectionId), this, oldValue, value));
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(SiteInspectionId), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	private SiteInspection _siteInspection;
	public SiteInspection SiteInspection
	{
		get => _siteInspection;
		set
		{
			var oldValue = _siteInspection;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(SiteInspection), this, oldValue, value));
			_siteInspection = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(SiteInspection), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportReceiverEmailAddress : ReportReceiverEmailAddressBase, IEntity {
	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			var oldValue = _siteId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(SiteId), this, oldValue, value));
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(SiteId), this, oldValue, value));
		}
	}

	private string _emailAddress;
	public string EmailAddress
	{
		get => _emailAddress;
		set
		{
			var oldValue = _emailAddress;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(EmailAddress), this, oldValue, value));
			_emailAddress = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(EmailAddress), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			var oldValue = _site;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Site), this, oldValue, value));
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Site), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Project : ProjectBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Project>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>(nameof(Id), this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Project>(nameof(Title), this, oldValue, value));
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>(nameof(Title), this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Project>(nameof(Description), this, oldValue, value));
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>(nameof(Description), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Project>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Project>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultType : FaultTypeBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(Id), this, oldValue, value));
		}
	}

	private int _categoryId;
	public int CategoryId
	{
		get => _categoryId;
		set
		{
			var oldValue = _categoryId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(CategoryId), this, oldValue, value));
			_categoryId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(CategoryId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(Name), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 FaultReportsCount { get; set; }
	private RelatedList<FaultType,FaultReport> _faultReports;
	public RelatedList<FaultType,FaultReport> FaultReports
	{
		get
		{
			this._faultReports = _faultReports ?? new RelatedList<FaultType,FaultReport>(this);
			return _faultReports;
		}
		set
		{
			var oldValue = _faultReports;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(FaultReports), this, oldValue, value));
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(FaultReports), this, oldValue, value));
		}
	}

	private FaultCategory _category;
	public FaultCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(Category), this, oldValue, value));
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(Category), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultType>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>(nameof(CreatedByUser), this, oldValue, value));
		}
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
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(Id), this, oldValue, value));
		}
	}

	private int _scaffoldId;
	public int ScaffoldId
	{
		get => _scaffoldId;
		set
		{
			var oldValue = _scaffoldId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(ScaffoldId), this, oldValue, value));
			_scaffoldId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(ScaffoldId), this, oldValue, value));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			var oldValue = _typeId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(TypeId), this, oldValue, value));
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(TypeId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private FaultReportStatus _status;
	public FaultReportStatus Status
	{
		get => _status;
		set
		{
			var oldValue = _status;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(Status), this, oldValue, value));
			_status = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(Status), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 ActionsTakenCount { get; set; }
	private RelatedList<FaultReport,FaultActionsTaken> _actionsTaken;
	public RelatedList<FaultReport,FaultActionsTaken> ActionsTaken
	{
		get
		{
			this._actionsTaken = _actionsTaken ?? new RelatedList<FaultReport,FaultActionsTaken>(this);
			return _actionsTaken;
		}
		set
		{
			var oldValue = _actionsTaken;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(ActionsTaken), this, oldValue, value));
			_actionsTaken = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(ActionsTaken), this, oldValue, value));
		}
	}

	
	public Int64 RecommendationsCount { get; set; }
	private RelatedList<FaultReport,FaultRecommendation> _recommendations;
	public RelatedList<FaultReport,FaultRecommendation> Recommendations
	{
		get
		{
			this._recommendations = _recommendations ?? new RelatedList<FaultReport,FaultRecommendation>(this);
			return _recommendations;
		}
		set
		{
			var oldValue = _recommendations;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(Recommendations), this, oldValue, value));
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(Recommendations), this, oldValue, value));
		}
	}

	private Scaffold _scaffold;
	public Scaffold Scaffold
	{
		get => _scaffold;
		set
		{
			var oldValue = _scaffold;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(Scaffold), this, oldValue, value));
			_scaffold = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(Scaffold), this, oldValue, value));
		}
	}

	private FaultType _type;
	public FaultType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(Type), this, oldValue, value));
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(Type), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultReport>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultRecommendation : FaultRecommendationBase, IEntity {
	private int _faultReportId;
	public int FaultReportId
	{
		get => _faultReportId;
		set
		{
			var oldValue = _faultReportId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(FaultReportId), this, oldValue, value));
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(FaultReportId), this, oldValue, value));
		}
	}

	private int _recommendationId;
	public int RecommendationId
	{
		get => _recommendationId;
		set
		{
			var oldValue = _recommendationId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(RecommendationId), this, oldValue, value));
			_recommendationId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(RecommendationId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			var oldValue = _notes;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Notes), this, oldValue, value));
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Notes), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private FaultReport _faultReport;
	public FaultReport FaultReport
	{
		get => _faultReport;
		set
		{
			var oldValue = _faultReport;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(FaultReport), this, oldValue, value));
			_faultReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(FaultReport), this, oldValue, value));
		}
	}

	private FaultDefaultRecommendation _recommendation;
	public FaultDefaultRecommendation Recommendation
	{
		get => _recommendation;
		set
		{
			var oldValue = _recommendation;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Recommendation), this, oldValue, value));
			_recommendation = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(Recommendation), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultDefaultRecommendation : FaultDefaultRecommendationBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Name), this, oldValue, value));
		}
	}

	private string _text;
	public string Text
	{
		get => _text;
		set
		{
			var oldValue = _text;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Text), this, oldValue, value));
			_text = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Text), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 RecommendationsCount { get; set; }
	private RelatedList<FaultDefaultRecommendation,FaultRecommendation> _recommendations;
	public RelatedList<FaultDefaultRecommendation,FaultRecommendation> Recommendations
	{
		get
		{
			this._recommendations = _recommendations ?? new RelatedList<FaultDefaultRecommendation,FaultRecommendation>(this);
			return _recommendations;
		}
		set
		{
			var oldValue = _recommendations;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Recommendations), this, oldValue, value));
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>(nameof(Recommendations), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultCategory : FaultCategoryBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(Name), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 FaultTypesCount { get; set; }
	private RelatedList<FaultCategory,FaultType> _faultTypes;
	public RelatedList<FaultCategory,FaultType> FaultTypes
	{
		get
		{
			this._faultTypes = _faultTypes ?? new RelatedList<FaultCategory,FaultType>(this);
			return _faultTypes;
		}
		set
		{
			var oldValue = _faultTypes;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultCategory>(nameof(FaultTypes), this, oldValue, value));
			_faultTypes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>(nameof(FaultTypes), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class FaultActionsTaken : FaultActionsTakenBase, IEntity {
	private int _faultReportId;
	public int FaultReportId
	{
		get => _faultReportId;
		set
		{
			var oldValue = _faultReportId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(FaultReportId), this, oldValue, value));
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(FaultReportId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			var oldValue = _notes;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(Notes), this, oldValue, value));
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(Notes), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private FaultReport _faultReport;
	public FaultReport FaultReport
	{
		get => _faultReport;
		set
		{
			var oldValue = _faultReport;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(FaultReport), this, oldValue, value));
			_faultReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(FaultReport), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class SiteDocument : SiteDocumentBase, IEntity {
	private int _categoryId;
	public int CategoryId
	{
		get => _categoryId;
		set
		{
			var oldValue = _categoryId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(CategoryId), this, oldValue, value));
			_categoryId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(CategoryId), this, oldValue, value));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			var oldValue = _siteId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(SiteId), this, oldValue, value));
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(SiteId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Title), this, oldValue, value));
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Title), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private DocumentCategory _category;
	public DocumentCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Category), this, oldValue, value));
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Category), this, oldValue, value));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			var oldValue = _site;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Site), this, oldValue, value));
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(Site), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteDocument>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class DocumentCategory : DocumentCategoryBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Name), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 DocumentsCount { get; set; }
	private RelatedList<DocumentCategory,SiteDocument> _documents;
	public RelatedList<DocumentCategory,SiteDocument> Documents
	{
		get
		{
			this._documents = _documents ?? new RelatedList<DocumentCategory,SiteDocument>(this);
			return _documents;
		}
		set
		{
			var oldValue = _documents;
			this.PropertyChanging.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Documents), this, oldValue, value));
			_documents = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>(nameof(Documents), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ClientType : ClientTypeBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ClientType>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>(nameof(Id), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ClientType>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>(nameof(Name), this, oldValue, value));
		}
	}

	
	public Int64 ClientsCount { get; set; }
	private RelatedList<ClientType,Client> _clients;
	public RelatedList<ClientType,Client> Clients
	{
		get
		{
			this._clients = _clients ?? new RelatedList<ClientType,Client>(this);
			return _clients;
		}
		set
		{
			var oldValue = _clients;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ClientType>(nameof(Clients), this, oldValue, value));
			_clients = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>(nameof(Clients), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Client : ClientBase, IEntity {
	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			var oldValue = _typeId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(TypeId), this, oldValue, value));
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(TypeId), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Name), this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Description), this, oldValue, value));
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Description), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 UsersCount { get; set; }
	private RelatedList<Client,ApplicationUser> _users;
	public RelatedList<Client,ApplicationUser> Users
	{
		get
		{
			this._users = _users ?? new RelatedList<Client,ApplicationUser>(this);
			return _users;
		}
		set
		{
			var oldValue = _users;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Users), this, oldValue, value));
			_users = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Users), this, oldValue, value));
		}
	}

	private ClientType _type;
	public ClientType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Type), this, oldValue, value));
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Type), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 ScaffoldsCount { get; set; }
	private RelatedList<Client,Scaffold> _scaffolds;
	public RelatedList<Client,Scaffold> Scaffolds
	{
		get
		{
			this._scaffolds = _scaffolds ?? new RelatedList<Client,Scaffold>(this);
			return _scaffolds;
		}
		set
		{
			var oldValue = _scaffolds;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Scaffolds), this, oldValue, value));
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Scaffolds), this, oldValue, value));
		}
	}

	
	public Int64 SitesCount { get; set; }
	private RelatedList<Client,Site> _sites;
	public RelatedList<Client,Site> Sites
	{
		get
		{
			this._sites = _sites ?? new RelatedList<Client,Site>(this);
			return _sites;
		}
		set
		{
			var oldValue = _sites;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(Sites), this, oldValue, value));
			_sites = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(Sites), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ApplicationUser : ApplicationUserBase, IEntity {
	private string _id;
	public string Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(Id), this, oldValue, value));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			var oldValue = _clientId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ClientId), this, oldValue, value));
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ClientId), this, oldValue, value));
		}
	}

	private string _email;
	public string Email
	{
		get => _email;
		set
		{
			var oldValue = _email;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(Email), this, oldValue, value));
			_email = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(Email), this, oldValue, value));
		}
	}

	private string _fullName;
	public string FullName
	{
		get => _fullName;
		set
		{
			var oldValue = _fullName;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FullName), this, oldValue, value));
			_fullName = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FullName), this, oldValue, value));
		}
	}

	private bool _emailConfirmed;
	public bool EmailConfirmed
	{
		get => _emailConfirmed;
		set
		{
			var oldValue = _emailConfirmed;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(EmailConfirmed), this, oldValue, value));
			_emailConfirmed = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(EmailConfirmed), this, oldValue, value));
		}
	}

	private UserType _userType;
	public UserType UserType
	{
		get => _userType;
		set
		{
			var oldValue = _userType;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(UserType), this, oldValue, value));
			_userType = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(UserType), this, oldValue, value));
		}
	}

	private bool _isLockedOut;
	public bool IsLockedOut
	{
		get => _isLockedOut;
		set
		{
			var oldValue = _isLockedOut;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(IsLockedOut), this, oldValue, value));
			_isLockedOut = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(IsLockedOut), this, oldValue, value));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			var oldValue = _client;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(Client), this, oldValue, value));
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(Client), this, oldValue, value));
		}
	}

	
	public Int64 ClientsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,Client> _clientsCreated;
	public RelatedList<ApplicationUser,Client> ClientsCreated
	{
		get
		{
			this._clientsCreated = _clientsCreated ?? new RelatedList<ApplicationUser,Client>(this);
			return _clientsCreated;
		}
		set
		{
			var oldValue = _clientsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ClientsCreated), this, oldValue, value));
			_clientsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ClientsCreated), this, oldValue, value));
		}
	}

	
	public Int64 DocumentCategoriesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,DocumentCategory> _documentCategoriesCreated;
	public RelatedList<ApplicationUser,DocumentCategory> DocumentCategoriesCreated
	{
		get
		{
			this._documentCategoriesCreated = _documentCategoriesCreated ?? new RelatedList<ApplicationUser,DocumentCategory>(this);
			return _documentCategoriesCreated;
		}
		set
		{
			var oldValue = _documentCategoriesCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(DocumentCategoriesCreated), this, oldValue, value));
			_documentCategoriesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(DocumentCategoriesCreated), this, oldValue, value));
		}
	}

	
	public Int64 SiteDocumentsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,SiteDocument> _siteDocumentsCreated;
	public RelatedList<ApplicationUser,SiteDocument> SiteDocumentsCreated
	{
		get
		{
			this._siteDocumentsCreated = _siteDocumentsCreated ?? new RelatedList<ApplicationUser,SiteDocument>(this);
			return _siteDocumentsCreated;
		}
		set
		{
			var oldValue = _siteDocumentsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(SiteDocumentsCreated), this, oldValue, value));
			_siteDocumentsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(SiteDocumentsCreated), this, oldValue, value));
		}
	}

	
	public Int64 FaultActionsTakenCreatedCount { get; set; }
	private RelatedList<ApplicationUser,FaultActionsTaken> _faultActionsTakenCreated;
	public RelatedList<ApplicationUser,FaultActionsTaken> FaultActionsTakenCreated
	{
		get
		{
			this._faultActionsTakenCreated = _faultActionsTakenCreated ?? new RelatedList<ApplicationUser,FaultActionsTaken>(this);
			return _faultActionsTakenCreated;
		}
		set
		{
			var oldValue = _faultActionsTakenCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultActionsTakenCreated), this, oldValue, value));
			_faultActionsTakenCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultActionsTakenCreated), this, oldValue, value));
		}
	}

	
	public Int64 FaultCategoriesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,FaultCategory> _faultCategoriesCreated;
	public RelatedList<ApplicationUser,FaultCategory> FaultCategoriesCreated
	{
		get
		{
			this._faultCategoriesCreated = _faultCategoriesCreated ?? new RelatedList<ApplicationUser,FaultCategory>(this);
			return _faultCategoriesCreated;
		}
		set
		{
			var oldValue = _faultCategoriesCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultCategoriesCreated), this, oldValue, value));
			_faultCategoriesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultCategoriesCreated), this, oldValue, value));
		}
	}

	
	public Int64 FaultDefaultRecommendationsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,FaultDefaultRecommendation> _faultDefaultRecommendationsCreated;
	public RelatedList<ApplicationUser,FaultDefaultRecommendation> FaultDefaultRecommendationsCreated
	{
		get
		{
			this._faultDefaultRecommendationsCreated = _faultDefaultRecommendationsCreated ?? new RelatedList<ApplicationUser,FaultDefaultRecommendation>(this);
			return _faultDefaultRecommendationsCreated;
		}
		set
		{
			var oldValue = _faultDefaultRecommendationsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultDefaultRecommendationsCreated), this, oldValue, value));
			_faultDefaultRecommendationsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultDefaultRecommendationsCreated), this, oldValue, value));
		}
	}

	
	public Int64 FaultRecommendationsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,FaultRecommendation> _faultRecommendationsCreated;
	public RelatedList<ApplicationUser,FaultRecommendation> FaultRecommendationsCreated
	{
		get
		{
			this._faultRecommendationsCreated = _faultRecommendationsCreated ?? new RelatedList<ApplicationUser,FaultRecommendation>(this);
			return _faultRecommendationsCreated;
		}
		set
		{
			var oldValue = _faultRecommendationsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultRecommendationsCreated), this, oldValue, value));
			_faultRecommendationsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultRecommendationsCreated), this, oldValue, value));
		}
	}

	
	public Int64 FaultReportsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,FaultReport> _faultReportsCreated;
	public RelatedList<ApplicationUser,FaultReport> FaultReportsCreated
	{
		get
		{
			this._faultReportsCreated = _faultReportsCreated ?? new RelatedList<ApplicationUser,FaultReport>(this);
			return _faultReportsCreated;
		}
		set
		{
			var oldValue = _faultReportsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultReportsCreated), this, oldValue, value));
			_faultReportsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultReportsCreated), this, oldValue, value));
		}
	}

	
	public Int64 FaultTypesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,FaultType> _faultTypesCreated;
	public RelatedList<ApplicationUser,FaultType> FaultTypesCreated
	{
		get
		{
			this._faultTypesCreated = _faultTypesCreated ?? new RelatedList<ApplicationUser,FaultType>(this);
			return _faultTypesCreated;
		}
		set
		{
			var oldValue = _faultTypesCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultTypesCreated), this, oldValue, value));
			_faultTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(FaultTypesCreated), this, oldValue, value));
		}
	}

	
	public Int64 ProjectCreatedCount { get; set; }
	private RelatedList<ApplicationUser,Project> _projectCreated;
	public RelatedList<ApplicationUser,Project> ProjectCreated
	{
		get
		{
			this._projectCreated = _projectCreated ?? new RelatedList<ApplicationUser,Project>(this);
			return _projectCreated;
		}
		set
		{
			var oldValue = _projectCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ProjectCreated), this, oldValue, value));
			_projectCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ProjectCreated), this, oldValue, value));
		}
	}

	
	public Int64 ReportReceiverEmailAddressesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,ReportReceiverEmailAddress> _reportReceiverEmailAddressesCreated;
	public RelatedList<ApplicationUser,ReportReceiverEmailAddress> ReportReceiverEmailAddressesCreated
	{
		get
		{
			this._reportReceiverEmailAddressesCreated = _reportReceiverEmailAddressesCreated ?? new RelatedList<ApplicationUser,ReportReceiverEmailAddress>(this);
			return _reportReceiverEmailAddressesCreated;
		}
		set
		{
			var oldValue = _reportReceiverEmailAddressesCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ReportReceiverEmailAddressesCreated), this, oldValue, value));
			_reportReceiverEmailAddressesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ReportReceiverEmailAddressesCreated), this, oldValue, value));
		}
	}

	
	public Int64 RiskAssessmentsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,RiskAssessment> _riskAssessmentsCreated;
	public RelatedList<ApplicationUser,RiskAssessment> RiskAssessmentsCreated
	{
		get
		{
			this._riskAssessmentsCreated = _riskAssessmentsCreated ?? new RelatedList<ApplicationUser,RiskAssessment>(this);
			return _riskAssessmentsCreated;
		}
		set
		{
			var oldValue = _riskAssessmentsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentsCreated), this, oldValue, value));
			_riskAssessmentsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentsCreated), this, oldValue, value));
		}
	}

	
	public Int64 RiskAssessmentAnswersCreatedCount { get; set; }
	private RelatedList<ApplicationUser,RiskAssessmentAnswer> _riskAssessmentAnswersCreated;
	public RelatedList<ApplicationUser,RiskAssessmentAnswer> RiskAssessmentAnswersCreated
	{
		get
		{
			this._riskAssessmentAnswersCreated = _riskAssessmentAnswersCreated ?? new RelatedList<ApplicationUser,RiskAssessmentAnswer>(this);
			return _riskAssessmentAnswersCreated;
		}
		set
		{
			var oldValue = _riskAssessmentAnswersCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentAnswersCreated), this, oldValue, value));
			_riskAssessmentAnswersCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentAnswersCreated), this, oldValue, value));
		}
	}

	
	public Int64 RiskAssessmentQuestionsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,RiskAssessmentQuestion> _riskAssessmentQuestionsCreated;
	public RelatedList<ApplicationUser,RiskAssessmentQuestion> RiskAssessmentQuestionsCreated
	{
		get
		{
			this._riskAssessmentQuestionsCreated = _riskAssessmentQuestionsCreated ?? new RelatedList<ApplicationUser,RiskAssessmentQuestion>(this);
			return _riskAssessmentQuestionsCreated;
		}
		set
		{
			var oldValue = _riskAssessmentQuestionsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentQuestionsCreated), this, oldValue, value));
			_riskAssessmentQuestionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentQuestionsCreated), this, oldValue, value));
		}
	}

	
	public Int64 ScaffoldsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,Scaffold> _scaffoldsCreated;
	public RelatedList<ApplicationUser,Scaffold> ScaffoldsCreated
	{
		get
		{
			this._scaffoldsCreated = _scaffoldsCreated ?? new RelatedList<ApplicationUser,Scaffold>(this);
			return _scaffoldsCreated;
		}
		set
		{
			var oldValue = _scaffoldsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ScaffoldsCreated), this, oldValue, value));
			_scaffoldsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ScaffoldsCreated), this, oldValue, value));
		}
	}

	
	public Int64 ScaffoldInspectionsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,ScaffoldInspection> _scaffoldInspectionsCreated;
	public RelatedList<ApplicationUser,ScaffoldInspection> ScaffoldInspectionsCreated
	{
		get
		{
			this._scaffoldInspectionsCreated = _scaffoldInspectionsCreated ?? new RelatedList<ApplicationUser,ScaffoldInspection>(this);
			return _scaffoldInspectionsCreated;
		}
		set
		{
			var oldValue = _scaffoldInspectionsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ScaffoldInspectionsCreated), this, oldValue, value));
			_scaffoldInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ScaffoldInspectionsCreated), this, oldValue, value));
		}
	}

	
	public Int64 ScaffoldLoadingsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,ScaffoldLoading> _scaffoldLoadingsCreated;
	public RelatedList<ApplicationUser,ScaffoldLoading> ScaffoldLoadingsCreated
	{
		get
		{
			this._scaffoldLoadingsCreated = _scaffoldLoadingsCreated ?? new RelatedList<ApplicationUser,ScaffoldLoading>(this);
			return _scaffoldLoadingsCreated;
		}
		set
		{
			var oldValue = _scaffoldLoadingsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ScaffoldLoadingsCreated), this, oldValue, value));
			_scaffoldLoadingsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ScaffoldLoadingsCreated), this, oldValue, value));
		}
	}

	
	public Int64 ScaffoldTypesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,ScaffoldType> _scaffoldTypesCreated;
	public RelatedList<ApplicationUser,ScaffoldType> ScaffoldTypesCreated
	{
		get
		{
			this._scaffoldTypesCreated = _scaffoldTypesCreated ?? new RelatedList<ApplicationUser,ScaffoldType>(this);
			return _scaffoldTypesCreated;
		}
		set
		{
			var oldValue = _scaffoldTypesCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ScaffoldTypesCreated), this, oldValue, value));
			_scaffoldTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(ScaffoldTypesCreated), this, oldValue, value));
		}
	}

	
	public Int64 SitesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,Site> _sitesCreated;
	public RelatedList<ApplicationUser,Site> SitesCreated
	{
		get
		{
			this._sitesCreated = _sitesCreated ?? new RelatedList<ApplicationUser,Site>(this);
			return _sitesCreated;
		}
		set
		{
			var oldValue = _sitesCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(SitesCreated), this, oldValue, value));
			_sitesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(SitesCreated), this, oldValue, value));
		}
	}

	
	public Int64 SiteInspectionsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,SiteInspection> _siteInspectionsCreated;
	public RelatedList<ApplicationUser,SiteInspection> SiteInspectionsCreated
	{
		get
		{
			this._siteInspectionsCreated = _siteInspectionsCreated ?? new RelatedList<ApplicationUser,SiteInspection>(this);
			return _siteInspectionsCreated;
		}
		set
		{
			var oldValue = _siteInspectionsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(SiteInspectionsCreated), this, oldValue, value));
			_siteInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(SiteInspectionsCreated), this, oldValue, value));
		}
	}

	
	public Int64 SitesCount { get; set; }
	private RelatedList<ApplicationUser,UserSite> _sites;
	public RelatedList<ApplicationUser,UserSite> Sites
	{
		get
		{
			this._sites = _sites ?? new RelatedList<ApplicationUser,UserSite>(this);
			return _sites;
		}
		set
		{
			var oldValue = _sites;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(Sites), this, oldValue, value));
			_sites = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(Sites), this, oldValue, value));
		}
	}

	
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

