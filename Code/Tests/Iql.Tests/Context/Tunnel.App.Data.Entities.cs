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


public class PersonReportBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "PersonReport";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonTypeMapBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "PersonTypeMap";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonTypeBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "PersonType";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonLoadingBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "PersonLoading";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonInspectionBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "PersonInspection";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class PersonBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "Person";
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


public class RiskAssessmentSolutionBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "RiskAssessmentSolution";
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


public class ReportTypeBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ReportType";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportRecommendationBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ReportRecommendation";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportDefaultRecommendationBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ReportDefaultRecommendation";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportCategoryBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ReportCategory";
	}
	public virtual EntityValidationResult ValidateEntity() {
		return new EntityValidationResult(this.GetType());
	}
}


public class ReportActionsTakenBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
	public virtual bool OnSaving() {
		return true;
	}
	public virtual bool OnDeleting() {
		return true;
	}
	public static string ClassName() {
		return "ReportActionsTaken";
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

	
	public Int64 PersonInspectionsCount { get; set; }
	private RelatedList<SiteInspection,PersonInspection> _personInspections;
	public RelatedList<SiteInspection,PersonInspection> PersonInspections
	{
		get
		{
			this._personInspections = _personInspections ?? new RelatedList<SiteInspection,PersonInspection>(this, nameof(PersonInspections));
			return _personInspections;
		}
		set
		{
			var oldValue = _personInspections;
			this.PropertyChanging.Emit(new PropertyChangeEvent<SiteInspection>(nameof(PersonInspections), this, oldValue, value));
			_personInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>(nameof(PersonInspections), this, oldValue, value));
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
			this._documents = _documents ?? new RelatedList<Site,SiteDocument>(this, nameof(Documents));
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
			this._additionalSendReportsTo = _additionalSendReportsTo ?? new RelatedList<Site,ReportReceiverEmailAddress>(this, nameof(AdditionalSendReportsTo));
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
			this._children = _children ?? new RelatedList<Site,Site>(this, nameof(Children));
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
			this._siteInspections = _siteInspections ?? new RelatedList<Site,SiteInspection>(this, nameof(SiteInspections));
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
			this._users = _users ?? new RelatedList<Site,UserSite>(this, nameof(Users));
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

public class PersonReport : PersonReportBase, IEntity {
	private int _personId;
	public int PersonId
	{
		get => _personId;
		set
		{
			var oldValue = _personId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(PersonId), this, oldValue, value));
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(PersonId), this, oldValue, value));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			var oldValue = _typeId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(TypeId), this, oldValue, value));
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(TypeId), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(Title), this, oldValue, value));
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(Title), this, oldValue, value));
		}
	}

	private FaultReportStatus _status;
	public FaultReportStatus Status
	{
		get => _status;
		set
		{
			var oldValue = _status;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(Status), this, oldValue, value));
			_status = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(Status), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 ActionsTakenCount { get; set; }
	private RelatedList<PersonReport,ReportActionsTaken> _actionsTaken;
	public RelatedList<PersonReport,ReportActionsTaken> ActionsTaken
	{
		get
		{
			this._actionsTaken = _actionsTaken ?? new RelatedList<PersonReport,ReportActionsTaken>(this, nameof(ActionsTaken));
			return _actionsTaken;
		}
		set
		{
			var oldValue = _actionsTaken;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(ActionsTaken), this, oldValue, value));
			_actionsTaken = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(ActionsTaken), this, oldValue, value));
		}
	}

	
	public Int64 RecommendationsCount { get; set; }
	private RelatedList<PersonReport,ReportRecommendation> _recommendations;
	public RelatedList<PersonReport,ReportRecommendation> Recommendations
	{
		get
		{
			this._recommendations = _recommendations ?? new RelatedList<PersonReport,ReportRecommendation>(this, nameof(Recommendations));
			return _recommendations;
		}
		set
		{
			var oldValue = _recommendations;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(Recommendations), this, oldValue, value));
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(Recommendations), this, oldValue, value));
		}
	}

	private Person _person;
	public Person Person
	{
		get => _person;
		set
		{
			var oldValue = _person;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(Person), this, oldValue, value));
			_person = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(Person), this, oldValue, value));
		}
	}

	private ReportType _type;
	public ReportType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(Type), this, oldValue, value));
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(Type), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonReport>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class PersonTypeMap : PersonTypeMapBase, IEntity {
	private int _personId;
	public int PersonId
	{
		get => _personId;
		set
		{
			var oldValue = _personId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(PersonId), this, oldValue, value));
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(PersonId), this, oldValue, value));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			var oldValue = _typeId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(TypeId), this, oldValue, value));
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(TypeId), this, oldValue, value));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			var oldValue = _notes;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Notes), this, oldValue, value));
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Notes), this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Description), this, oldValue, value));
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Description), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private Person _person;
	public Person Person
	{
		get => _person;
		set
		{
			var oldValue = _person;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Person), this, oldValue, value));
			_person = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Person), this, oldValue, value));
		}
	}

	private PersonType _type;
	public PersonType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Type), this, oldValue, value));
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>(nameof(Type), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class PersonType : PersonTypeBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(Title), this, oldValue, value));
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(Title), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 PeopleCount { get; set; }
	private RelatedList<PersonType,Person> _people;
	public RelatedList<PersonType,Person> People
	{
		get
		{
			this._people = _people ?? new RelatedList<PersonType,Person>(this, nameof(People));
			return _people;
		}
		set
		{
			var oldValue = _people;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(People), this, oldValue, value));
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(People), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 PeopleMapCount { get; set; }
	private RelatedList<PersonType,PersonTypeMap> _peopleMap;
	public RelatedList<PersonType,PersonTypeMap> PeopleMap
	{
		get
		{
			this._peopleMap = _peopleMap ?? new RelatedList<PersonType,PersonTypeMap>(this, nameof(PeopleMap));
			return _peopleMap;
		}
		set
		{
			var oldValue = _peopleMap;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonType>(nameof(PeopleMap), this, oldValue, value));
			_peopleMap = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>(nameof(PeopleMap), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class PersonLoading : PersonLoadingBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(Name), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	
	public Int64 PeopleCount { get; set; }
	private RelatedList<PersonLoading,Person> _people;
	public RelatedList<PersonLoading,Person> People
	{
		get
		{
			this._people = _people ?? new RelatedList<PersonLoading,Person>(this, nameof(People));
			return _people;
		}
		set
		{
			var oldValue = _people;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(People), this, oldValue, value));
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(People), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonLoading>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>(nameof(CreatedByUser), this, oldValue, value));
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
			validationResult.AddFailure("Please enter a loading name");
		}
		return validationResult;
	}
}

public class PersonInspection : PersonInspectionBase, IEntity {
	private int _siteInspectionId;
	public int SiteInspectionId
	{
		get => _siteInspectionId;
		set
		{
			var oldValue = _siteInspectionId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(SiteInspectionId), this, oldValue, value));
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(SiteInspectionId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private int _personId;
	public int PersonId
	{
		get => _personId;
		set
		{
			var oldValue = _personId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(PersonId), this, oldValue, value));
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(PersonId), this, oldValue, value));
		}
	}

	private PersonInspectionStatus _inspectionStatus;
	public PersonInspectionStatus InspectionStatus
	{
		get => _inspectionStatus;
		set
		{
			var oldValue = _inspectionStatus;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(InspectionStatus), this, oldValue, value));
			_inspectionStatus = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(InspectionStatus), this, oldValue, value));
		}
	}

	private DateTimeOffset _startTime;
	public DateTimeOffset StartTime
	{
		get => _startTime;
		set
		{
			var oldValue = _startTime;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(StartTime), this, oldValue, value));
			_startTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(StartTime), this, oldValue, value));
		}
	}

	private DateTimeOffset _endTime;
	public DateTimeOffset EndTime
	{
		get => _endTime;
		set
		{
			var oldValue = _endTime;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(EndTime), this, oldValue, value));
			_endTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(EndTime), this, oldValue, value));
		}
	}

	private InspectionFailReason _reasonForFailure;
	public InspectionFailReason ReasonForFailure
	{
		get => _reasonForFailure;
		set
		{
			var oldValue = _reasonForFailure;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(ReasonForFailure), this, oldValue, value));
			_reasonForFailure = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(ReasonForFailure), this, oldValue, value));
		}
	}

	private bool _isDesignRequired;
	public bool IsDesignRequired
	{
		get => _isDesignRequired;
		set
		{
			var oldValue = _isDesignRequired;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(IsDesignRequired), this, oldValue, value));
			_isDesignRequired = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(IsDesignRequired), this, oldValue, value));
		}
	}

	private string _drawingNumber;
	public string DrawingNumber
	{
		get => _drawingNumber;
		set
		{
			var oldValue = _drawingNumber;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(DrawingNumber), this, oldValue, value));
			_drawingNumber = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(DrawingNumber), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private SiteInspection _siteInspection;
	public SiteInspection SiteInspection
	{
		get => _siteInspection;
		set
		{
			var oldValue = _siteInspection;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(SiteInspection), this, oldValue, value));
			_siteInspection = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(SiteInspection), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<PersonInspection>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class Person : PersonBase, IEntity {
	private int? _typeId;
	public int? TypeId
	{
		get => _typeId;
		set
		{
			var oldValue = _typeId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(TypeId), this, oldValue, value));
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(TypeId), this, oldValue, value));
		}
	}

	private int? _loadingId;
	public int? LoadingId
	{
		get => _loadingId;
		set
		{
			var oldValue = _loadingId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(LoadingId), this, oldValue, value));
			_loadingId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(LoadingId), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _key;
	public string Key
	{
		get => _key;
		set
		{
			var oldValue = _key;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Key), this, oldValue, value));
			_key = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Key), this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Title), this, oldValue, value));
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Title), this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Description), this, oldValue, value));
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Description), this, oldValue, value));
		}
	}

	private PersonCategory _category;
	public PersonCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Category), this, oldValue, value));
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Category), this, oldValue, value));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			var oldValue = _clientId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(ClientId), this, oldValue, value));
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(ClientId), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			var oldValue = _client;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Client), this, oldValue, value));
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Client), this, oldValue, value));
		}
	}

	private PersonType _type;
	public PersonType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Type), this, oldValue, value));
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Type), this, oldValue, value));
		}
	}

	private PersonLoading _loading;
	public PersonLoading Loading
	{
		get => _loading;
		set
		{
			var oldValue = _loading;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Loading), this, oldValue, value));
			_loading = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Loading), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 TypesCount { get; set; }
	private RelatedList<Person,PersonTypeMap> _types;
	public RelatedList<Person,PersonTypeMap> Types
	{
		get
		{
			this._types = _types ?? new RelatedList<Person,PersonTypeMap>(this, nameof(Types));
			return _types;
		}
		set
		{
			var oldValue = _types;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Types), this, oldValue, value));
			_types = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Types), this, oldValue, value));
		}
	}

	
	public Int64 ReportsCount { get; set; }
	private RelatedList<Person,PersonReport> _reports;
	public RelatedList<Person,PersonReport> Reports
	{
		get
		{
			this._reports = _reports ?? new RelatedList<Person,PersonReport>(this, nameof(Reports));
			return _reports;
		}
		set
		{
			var oldValue = _reports;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Person>(nameof(Reports), this, oldValue, value));
			_reports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>(nameof(Reports), this, oldValue, value));
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
		validationResult.AddPropertyValidationResult(this.ValidateDescription());
		return validationResult;
	}
	public PropertyValidationResult ValidateTitle() {
		var validationResult = new PropertyValidationResult(this.GetType(), "Title");
		var entity = this;
		if(!(true)) {
			validationResult.AddFailure("Please enter a valid person title");
		}
		if(!(true)) {
			validationResult.AddFailure("Please enter less than fifty characters");
		}
		if(!(true)) {
			validationResult.AddFailure("Please enter at least three characters for the person's title");
		}
		if(!(true)) {
			validationResult.AddFailure("Please enter a valid report title");
		}
		if(!(true)) {
			validationResult.AddFailure("Please enter less than five characters");
		}
		return validationResult;
	}
	public PropertyValidationResult ValidateDescription() {
		var validationResult = new PropertyValidationResult(this.GetType(), "Description");
		var entity = this;
		if(!(true)) {
			validationResult.AddFailure("Please enter a valid person description");
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
			this._answers = _answers ?? new RelatedList<RiskAssessmentQuestion,RiskAssessmentAnswer>(this, nameof(Answers));
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

public class RiskAssessmentSolution : RiskAssessmentSolutionBase, IEntity {
	private int _riskAssessmentId;
	public int RiskAssessmentId
	{
		get => _riskAssessmentId;
		set
		{
			var oldValue = _riskAssessmentId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(RiskAssessmentId), this, oldValue, value));
			_riskAssessmentId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(RiskAssessmentId), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private RiskAssessment _riskAssessment;
	public RiskAssessment RiskAssessment
	{
		get => _riskAssessment;
		set
		{
			var oldValue = _riskAssessment;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(RiskAssessment), this, oldValue, value));
			_riskAssessment = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(RiskAssessment), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class RiskAssessment : RiskAssessmentBase, IEntity {
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

	private RiskAssessmentSolution _riskAssessmentSolution;
	public RiskAssessmentSolution RiskAssessmentSolution
	{
		get => _riskAssessmentSolution;
		set
		{
			var oldValue = _riskAssessmentSolution;
			this.PropertyChanging.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(RiskAssessmentSolution), this, oldValue, value));
			_riskAssessmentSolution = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>(nameof(RiskAssessmentSolution), this, oldValue, value));
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

public class ReportType : ReportTypeBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(Id), this, oldValue, value));
		}
	}

	private int _categoryId;
	public int CategoryId
	{
		get => _categoryId;
		set
		{
			var oldValue = _categoryId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(CategoryId), this, oldValue, value));
			_categoryId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(CategoryId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(Name), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(Version), this, oldValue, value));
		}
	}

	private ReportCategory _category;
	public ReportCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(Category), this, oldValue, value));
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(Category), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 FaultReportsCount { get; set; }
	private RelatedList<ReportType,PersonReport> _faultReports;
	public RelatedList<ReportType,PersonReport> FaultReports
	{
		get
		{
			this._faultReports = _faultReports ?? new RelatedList<ReportType,PersonReport>(this, nameof(FaultReports));
			return _faultReports;
		}
		set
		{
			var oldValue = _faultReports;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportType>(nameof(FaultReports), this, oldValue, value));
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>(nameof(FaultReports), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportRecommendation : ReportRecommendationBase, IEntity {
	private int _reportId;
	public int ReportId
	{
		get => _reportId;
		set
		{
			var oldValue = _reportId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(ReportId), this, oldValue, value));
			_reportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(ReportId), this, oldValue, value));
		}
	}

	private int _recommendationId;
	public int RecommendationId
	{
		get => _recommendationId;
		set
		{
			var oldValue = _recommendationId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(RecommendationId), this, oldValue, value));
			_recommendationId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(RecommendationId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			var oldValue = _notes;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Notes), this, oldValue, value));
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Notes), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private PersonReport _personReport;
	public PersonReport PersonReport
	{
		get => _personReport;
		set
		{
			var oldValue = _personReport;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(PersonReport), this, oldValue, value));
			_personReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(PersonReport), this, oldValue, value));
		}
	}

	private ReportDefaultRecommendation _recommendation;
	public ReportDefaultRecommendation Recommendation
	{
		get => _recommendation;
		set
		{
			var oldValue = _recommendation;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Recommendation), this, oldValue, value));
			_recommendation = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(Recommendation), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportDefaultRecommendation : ReportDefaultRecommendationBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Name), this, oldValue, value));
		}
	}

	private string _text;
	public string Text
	{
		get => _text;
		set
		{
			var oldValue = _text;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Text), this, oldValue, value));
			_text = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Text), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 RecommendationsCount { get; set; }
	private RelatedList<ReportDefaultRecommendation,ReportRecommendation> _recommendations;
	public RelatedList<ReportDefaultRecommendation,ReportRecommendation> Recommendations
	{
		get
		{
			this._recommendations = _recommendations ?? new RelatedList<ReportDefaultRecommendation,ReportRecommendation>(this, nameof(Recommendations));
			return _recommendations;
		}
		set
		{
			var oldValue = _recommendations;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Recommendations), this, oldValue, value));
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Recommendations), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportCategory : ReportCategoryBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(Id), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(Name), this, oldValue, value));
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(Name), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(Guid), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	
	public Int64 ReportTypesCount { get; set; }
	private RelatedList<ReportCategory,ReportType> _reportTypes;
	public RelatedList<ReportCategory,ReportType> ReportTypes
	{
		get
		{
			this._reportTypes = _reportTypes ?? new RelatedList<ReportCategory,ReportType>(this, nameof(ReportTypes));
			return _reportTypes;
		}
		set
		{
			var oldValue = _reportTypes;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportCategory>(nameof(ReportTypes), this, oldValue, value));
			_reportTypes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>(nameof(ReportTypes), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

public class ReportActionsTaken : ReportActionsTakenBase, IEntity {
	private int _faultReportId;
	public int FaultReportId
	{
		get => _faultReportId;
		set
		{
			var oldValue = _faultReportId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(FaultReportId), this, oldValue, value));
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(FaultReportId), this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedByUserId), this, oldValue, value));
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedByUserId), this, oldValue, value));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			var oldValue = _notes;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(Notes), this, oldValue, value));
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(Notes), this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(Guid), this, oldValue, value));
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(Guid), this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(Id), this, oldValue, value));
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(Id), this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedDate), this, oldValue, value));
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedDate), this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(Version), this, oldValue, value));
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(Version), this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(PersistenceKey), this, oldValue, value));
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(PersistenceKey), this, oldValue, value));
		}
	}

	private PersonReport _personReport;
	public PersonReport PersonReport
	{
		get => _personReport;
		set
		{
			var oldValue = _personReport;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(PersonReport), this, oldValue, value));
			_personReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(PersonReport), this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedByUser), this, oldValue, value));
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedByUser), this, oldValue, value));
		}
	}

	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		validationResult.AddPropertyValidationResult(this.ValidateNotes());
		return validationResult;
	}
	public PropertyValidationResult ValidateNotes() {
		var validationResult = new PropertyValidationResult(this.GetType(), "Notes");
		var entity = this;
		if(!(true)) {
			validationResult.AddFailure("Please enter some actions taken notes");
		}
		if(!(true)) {
			validationResult.AddFailure("Please enter at least five characters for notes");
		}
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
			this._documents = _documents ?? new RelatedList<DocumentCategory,SiteDocument>(this, nameof(Documents));
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
			this._clients = _clients ?? new RelatedList<ClientType,Client>(this, nameof(Clients));
			return _clients;
		}
		set
		{
			var oldValue = _clients;
		    if (value.Owner != this)
		    {
		        throw new Exception("Bad owner");
		    }
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
			this._users = _users ?? new RelatedList<Client,ApplicationUser>(this, nameof(Users));
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

	
	public Int64 PeopleCount { get; set; }
	private RelatedList<Client,Person> _people;
	public RelatedList<Client,Person> People
	{
		get
		{
			this._people = _people ?? new RelatedList<Client,Person>(this, nameof(People));
			return _people;
		}
		set
		{
			var oldValue = _people;
			this.PropertyChanging.Emit(new PropertyChangeEvent<Client>(nameof(People), this, oldValue, value));
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>(nameof(People), this, oldValue, value));
		}
	}

	
	public Int64 SitesCount { get; set; }
	private RelatedList<Client,Site> _sites;
	public RelatedList<Client,Site> Sites
	{
		get
		{
			this._sites = _sites ?? new RelatedList<Client,Site>(this, nameof(Sites));
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
			this._clientsCreated = _clientsCreated ?? new RelatedList<ApplicationUser,Client>(this, nameof(ClientsCreated));
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
			this._documentCategoriesCreated = _documentCategoriesCreated ?? new RelatedList<ApplicationUser,DocumentCategory>(this, nameof(DocumentCategoriesCreated));
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
			this._siteDocumentsCreated = _siteDocumentsCreated ?? new RelatedList<ApplicationUser,SiteDocument>(this, nameof(SiteDocumentsCreated));
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
	private RelatedList<ApplicationUser,ReportActionsTaken> _faultActionsTakenCreated;
	public RelatedList<ApplicationUser,ReportActionsTaken> FaultActionsTakenCreated
	{
		get
		{
			this._faultActionsTakenCreated = _faultActionsTakenCreated ?? new RelatedList<ApplicationUser,ReportActionsTaken>(this, nameof(FaultActionsTakenCreated));
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
	private RelatedList<ApplicationUser,ReportCategory> _faultCategoriesCreated;
	public RelatedList<ApplicationUser,ReportCategory> FaultCategoriesCreated
	{
		get
		{
			this._faultCategoriesCreated = _faultCategoriesCreated ?? new RelatedList<ApplicationUser,ReportCategory>(this, nameof(FaultCategoriesCreated));
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
	private RelatedList<ApplicationUser,ReportDefaultRecommendation> _faultDefaultRecommendationsCreated;
	public RelatedList<ApplicationUser,ReportDefaultRecommendation> FaultDefaultRecommendationsCreated
	{
		get
		{
			this._faultDefaultRecommendationsCreated = _faultDefaultRecommendationsCreated ?? new RelatedList<ApplicationUser,ReportDefaultRecommendation>(this, nameof(FaultDefaultRecommendationsCreated));
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
	private RelatedList<ApplicationUser,ReportRecommendation> _faultRecommendationsCreated;
	public RelatedList<ApplicationUser,ReportRecommendation> FaultRecommendationsCreated
	{
		get
		{
			this._faultRecommendationsCreated = _faultRecommendationsCreated ?? new RelatedList<ApplicationUser,ReportRecommendation>(this, nameof(FaultRecommendationsCreated));
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

	
	public Int64 FaultTypesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,ReportType> _faultTypesCreated;
	public RelatedList<ApplicationUser,ReportType> FaultTypesCreated
	{
		get
		{
			this._faultTypesCreated = _faultTypesCreated ?? new RelatedList<ApplicationUser,ReportType>(this, nameof(FaultTypesCreated));
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
			this._projectCreated = _projectCreated ?? new RelatedList<ApplicationUser,Project>(this, nameof(ProjectCreated));
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
			this._reportReceiverEmailAddressesCreated = _reportReceiverEmailAddressesCreated ?? new RelatedList<ApplicationUser,ReportReceiverEmailAddress>(this, nameof(ReportReceiverEmailAddressesCreated));
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
			this._riskAssessmentsCreated = _riskAssessmentsCreated ?? new RelatedList<ApplicationUser,RiskAssessment>(this, nameof(RiskAssessmentsCreated));
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
			this._riskAssessmentAnswersCreated = _riskAssessmentAnswersCreated ?? new RelatedList<ApplicationUser,RiskAssessmentAnswer>(this, nameof(RiskAssessmentAnswersCreated));
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
			this._riskAssessmentQuestionsCreated = _riskAssessmentQuestionsCreated ?? new RelatedList<ApplicationUser,RiskAssessmentQuestion>(this, nameof(RiskAssessmentQuestionsCreated));
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

	
	public Int64 PeopleCreatedCount { get; set; }
	private RelatedList<ApplicationUser,Person> _peopleCreated;
	public RelatedList<ApplicationUser,Person> PeopleCreated
	{
		get
		{
			this._peopleCreated = _peopleCreated ?? new RelatedList<ApplicationUser,Person>(this, nameof(PeopleCreated));
			return _peopleCreated;
		}
		set
		{
			var oldValue = _peopleCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(PeopleCreated), this, oldValue, value));
			_peopleCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(PeopleCreated), this, oldValue, value));
		}
	}

	
	public Int64 PersonInspectionsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,PersonInspection> _personInspectionsCreated;
	public RelatedList<ApplicationUser,PersonInspection> PersonInspectionsCreated
	{
		get
		{
			this._personInspectionsCreated = _personInspectionsCreated ?? new RelatedList<ApplicationUser,PersonInspection>(this, nameof(PersonInspectionsCreated));
			return _personInspectionsCreated;
		}
		set
		{
			var oldValue = _personInspectionsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(PersonInspectionsCreated), this, oldValue, value));
			_personInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(PersonInspectionsCreated), this, oldValue, value));
		}
	}

	
	public Int64 PersonLoadingsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,PersonLoading> _personLoadingsCreated;
	public RelatedList<ApplicationUser,PersonLoading> PersonLoadingsCreated
	{
		get
		{
			this._personLoadingsCreated = _personLoadingsCreated ?? new RelatedList<ApplicationUser,PersonLoading>(this, nameof(PersonLoadingsCreated));
			return _personLoadingsCreated;
		}
		set
		{
			var oldValue = _personLoadingsCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(PersonLoadingsCreated), this, oldValue, value));
			_personLoadingsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(PersonLoadingsCreated), this, oldValue, value));
		}
	}

	
	public Int64 PersonTypesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,PersonType> _personTypesCreated;
	public RelatedList<ApplicationUser,PersonType> PersonTypesCreated
	{
		get
		{
			this._personTypesCreated = _personTypesCreated ?? new RelatedList<ApplicationUser,PersonType>(this, nameof(PersonTypesCreated));
			return _personTypesCreated;
		}
		set
		{
			var oldValue = _personTypesCreated;
			this.PropertyChanging.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(PersonTypesCreated), this, oldValue, value));
			_personTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>(nameof(PersonTypesCreated), this, oldValue, value));
		}
	}

	
	public Int64 FaultReportsCreatedCount { get; set; }
	private RelatedList<ApplicationUser,PersonReport> _faultReportsCreated;
	public RelatedList<ApplicationUser,PersonReport> FaultReportsCreated
	{
		get
		{
			this._faultReportsCreated = _faultReportsCreated ?? new RelatedList<ApplicationUser,PersonReport>(this, nameof(FaultReportsCreated));
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

	
	public Int64 SitesCreatedCount { get; set; }
	private RelatedList<ApplicationUser,Site> _sitesCreated;
	public RelatedList<ApplicationUser,Site> SitesCreated
	{
		get
		{
			this._sitesCreated = _sitesCreated ?? new RelatedList<ApplicationUser,Site>(this, nameof(SitesCreated));
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
			this._siteInspectionsCreated = _siteInspectionsCreated ?? new RelatedList<ApplicationUser,SiteInspection>(this, nameof(SiteInspectionsCreated));
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
			this._sites = _sites ?? new RelatedList<ApplicationUser,UserSite>(this, nameof(Sites));
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

