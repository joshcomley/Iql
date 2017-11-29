using Iql.Queryable;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Events;
using System.Collections.ObjectModel;
using System;


public class UserSiteBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
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
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>("SiteId", this, oldValue, value));
		}
	}

	private string _userId;
	public string UserId
	{
		get => _userId;
		set
		{
			var oldValue = _userId;
			_userId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>("UserId", this, oldValue, value));
		}
	}

	private ApplicationUser _user;
	public ApplicationUser User
	{
		get => _user;
		set
		{
			var oldValue = _user;
			_user = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>("User", this, oldValue, value));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			var oldValue = _site;
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>("Site", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("Id", this, oldValue, value));
		}
	}

	private int _riskAssessmentId;
	public int RiskAssessmentId
	{
		get => _riskAssessmentId;
		set
		{
			var oldValue = _riskAssessmentId;
			_riskAssessmentId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("RiskAssessmentId", this, oldValue, value));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			var oldValue = _siteId;
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("SiteId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("CreatedByUserId", this, oldValue, value));
		}
	}

	private DateTimeOffset _startTime;
	public DateTimeOffset StartTime
	{
		get => _startTime;
		set
		{
			var oldValue = _startTime;
			_startTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("StartTime", this, oldValue, value));
		}
	}

	private DateTimeOffset _endTime;
	public DateTimeOffset EndTime
	{
		get => _endTime;
		set
		{
			var oldValue = _endTime;
			_endTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("EndTime", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<PersonInspection> _personInspections;
	public ObservableCollection<PersonInspection> PersonInspections
	{
		get => _personInspections;
		set
		{
			var oldValue = _personInspections;
			_personInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("PersonInspections", this, oldValue, value));
		}
	}

	
	public long? PersonInspectionsCount { get; set; }
	private RiskAssessment _riskAssessment;
	public RiskAssessment RiskAssessment
	{
		get => _riskAssessment;
		set
		{
			var oldValue = _riskAssessment;
			_riskAssessment = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("RiskAssessment", this, oldValue, value));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			var oldValue = _site;
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("Site", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Id", this, oldValue, value));
		}
	}

	private int? _parentId;
	public int? ParentId
	{
		get => _parentId;
		set
		{
			var oldValue = _parentId;
			_parentId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("ParentId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _address;
	public string Address
	{
		get => _address;
		set
		{
			var oldValue = _address;
			_address = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Address", this, oldValue, value));
		}
	}

	private string _postCode;
	public string PostCode
	{
		get => _postCode;
		set
		{
			var oldValue = _postCode;
			_postCode = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("PostCode", this, oldValue, value));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			var oldValue = _clientId;
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("ClientId", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Name", this, oldValue, value));
		}
	}

	private int _left;
	public int Left
	{
		get => _left;
		set
		{
			var oldValue = _left;
			_left = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Left", this, oldValue, value));
		}
	}

	private int _right;
	public int Right
	{
		get => _right;
		set
		{
			var oldValue = _right;
			_right = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Right", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<SiteDocument> _documents;
	public ObservableCollection<SiteDocument> Documents
	{
		get => _documents;
		set
		{
			var oldValue = _documents;
			_documents = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Documents", this, oldValue, value));
		}
	}

	
	public long? DocumentsCount { get; set; }
	private ObservableCollection<ReportReceiverEmailAddress> _additionalSendReportsTo;
	public ObservableCollection<ReportReceiverEmailAddress> AdditionalSendReportsTo
	{
		get => _additionalSendReportsTo;
		set
		{
			var oldValue = _additionalSendReportsTo;
			_additionalSendReportsTo = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("AdditionalSendReportsTo", this, oldValue, value));
		}
	}

	
	public long? AdditionalSendReportsToCount { get; set; }
	private Site _parent;
	public Site Parent
	{
		get => _parent;
		set
		{
			var oldValue = _parent;
			_parent = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Parent", this, oldValue, value));
		}
	}

	private ObservableCollection<Site> _children;
	public ObservableCollection<Site> Children
	{
		get => _children;
		set
		{
			var oldValue = _children;
			_children = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Children", this, oldValue, value));
		}
	}

	
	public long? ChildrenCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("CreatedByUser", this, oldValue, value));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			var oldValue = _client;
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Client", this, oldValue, value));
		}
	}

	private ObservableCollection<SiteInspection> _siteInspections;
	public ObservableCollection<SiteInspection> SiteInspections
	{
		get => _siteInspections;
		set
		{
			var oldValue = _siteInspections;
			_siteInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("SiteInspections", this, oldValue, value));
		}
	}

	
	public long? SiteInspectionsCount { get; set; }
	private ObservableCollection<UserSite> _users;
	public ObservableCollection<UserSite> Users
	{
		get => _users;
		set
		{
			var oldValue = _users;
			_users = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Users", this, oldValue, value));
		}
	}

	
	public long? UsersCount { get; set; }
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
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("PersonId", this, oldValue, value));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			var oldValue = _typeId;
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("TypeId", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Title", this, oldValue, value));
		}
	}

	private FaultReportStatus _status;
	public FaultReportStatus Status
	{
		get => _status;
		set
		{
			var oldValue = _status;
			_status = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Status", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<ReportActionsTaken> _actionsTaken;
	public ObservableCollection<ReportActionsTaken> ActionsTaken
	{
		get => _actionsTaken;
		set
		{
			var oldValue = _actionsTaken;
			_actionsTaken = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("ActionsTaken", this, oldValue, value));
		}
	}

	
	public long? ActionsTakenCount { get; set; }
	private ObservableCollection<ReportRecommendation> _recommendations;
	public ObservableCollection<ReportRecommendation> Recommendations
	{
		get => _recommendations;
		set
		{
			var oldValue = _recommendations;
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Recommendations", this, oldValue, value));
		}
	}

	
	public long? RecommendationsCount { get; set; }
	private Person _person;
	public Person Person
	{
		get => _person;
		set
		{
			var oldValue = _person;
			_person = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Person", this, oldValue, value));
		}
	}

	private ReportType _type;
	public ReportType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Type", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("CreatedByUser", this, oldValue, value));
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
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("PersonId", this, oldValue, value));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			var oldValue = _typeId;
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("TypeId", this, oldValue, value));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			var oldValue = _notes;
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Notes", this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Description", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("CreatedDate", this, oldValue, value));
		}
	}

	private Person _person;
	public Person Person
	{
		get => _person;
		set
		{
			var oldValue = _person;
			_person = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Person", this, oldValue, value));
		}
	}

	private PersonType _type;
	public PersonType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Type", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("Title", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<Person> _people;
	public ObservableCollection<Person> People
	{
		get => _people;
		set
		{
			var oldValue = _people;
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("People", this, oldValue, value));
		}
	}

	
	public long? PeopleCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<PersonTypeMap> _peopleMap;
	public ObservableCollection<PersonTypeMap> PeopleMap
	{
		get => _peopleMap;
		set
		{
			var oldValue = _peopleMap;
			_peopleMap = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("PeopleMap", this, oldValue, value));
		}
	}

	
	public long? PeopleMapCount { get; set; }
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("Name", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<Person> _people;
	public ObservableCollection<Person> People
	{
		get => _people;
		set
		{
			var oldValue = _people;
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("People", this, oldValue, value));
		}
	}

	
	public long? PeopleCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("CreatedByUser", this, oldValue, value));
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
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("SiteInspectionId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("CreatedByUserId", this, oldValue, value));
		}
	}

	private int _personId;
	public int PersonId
	{
		get => _personId;
		set
		{
			var oldValue = _personId;
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("PersonId", this, oldValue, value));
		}
	}

	private PersonInspectionStatus _inspectionStatus;
	public PersonInspectionStatus InspectionStatus
	{
		get => _inspectionStatus;
		set
		{
			var oldValue = _inspectionStatus;
			_inspectionStatus = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("InspectionStatus", this, oldValue, value));
		}
	}

	private DateTimeOffset _startTime;
	public DateTimeOffset StartTime
	{
		get => _startTime;
		set
		{
			var oldValue = _startTime;
			_startTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("StartTime", this, oldValue, value));
		}
	}

	private DateTimeOffset _endTime;
	public DateTimeOffset EndTime
	{
		get => _endTime;
		set
		{
			var oldValue = _endTime;
			_endTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("EndTime", this, oldValue, value));
		}
	}

	private InspectionFailReason _reasonForFailure;
	public InspectionFailReason ReasonForFailure
	{
		get => _reasonForFailure;
		set
		{
			var oldValue = _reasonForFailure;
			_reasonForFailure = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("ReasonForFailure", this, oldValue, value));
		}
	}

	private bool _isDesignRequired;
	public bool IsDesignRequired
	{
		get => _isDesignRequired;
		set
		{
			var oldValue = _isDesignRequired;
			_isDesignRequired = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("IsDesignRequired", this, oldValue, value));
		}
	}

	private string _drawingNumber;
	public string DrawingNumber
	{
		get => _drawingNumber;
		set
		{
			var oldValue = _drawingNumber;
			_drawingNumber = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("DrawingNumber", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("Guid", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("Id", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("PersistenceKey", this, oldValue, value));
		}
	}

	private SiteInspection _siteInspection;
	public SiteInspection SiteInspection
	{
		get => _siteInspection;
		set
		{
			var oldValue = _siteInspection;
			_siteInspection = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("SiteInspection", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("CreatedByUser", this, oldValue, value));
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
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("TypeId", this, oldValue, value));
		}
	}

	private int? _loadingId;
	public int? LoadingId
	{
		get => _loadingId;
		set
		{
			var oldValue = _loadingId;
			_loadingId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("LoadingId", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _key;
	public string Key
	{
		get => _key;
		set
		{
			var oldValue = _key;
			_key = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Key", this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Title", this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Description", this, oldValue, value));
		}
	}

	private PersonCategory _category;
	public PersonCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Category", this, oldValue, value));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			var oldValue = _clientId;
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("ClientId", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("PersistenceKey", this, oldValue, value));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			var oldValue = _client;
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Client", this, oldValue, value));
		}
	}

	private PersonType _type;
	public PersonType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Type", this, oldValue, value));
		}
	}

	private PersonLoading _loading;
	public PersonLoading Loading
	{
		get => _loading;
		set
		{
			var oldValue = _loading;
			_loading = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Loading", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<PersonTypeMap> _types;
	public ObservableCollection<PersonTypeMap> Types
	{
		get => _types;
		set
		{
			var oldValue = _types;
			_types = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Types", this, oldValue, value));
		}
	}

	
	public long? TypesCount { get; set; }
	private ObservableCollection<PersonReport> _reports;
	public ObservableCollection<PersonReport> Reports
	{
		get => _reports;
		set
		{
			var oldValue = _reports;
			_reports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Reports", this, oldValue, value));
		}
	}

	
	public long? ReportsCount { get; set; }
	
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Name", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<RiskAssessmentAnswer> _answers;
	public ObservableCollection<RiskAssessmentAnswer> Answers
	{
		get => _answers;
		set
		{
			var oldValue = _answers;
			_answers = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Answers", this, oldValue, value));
		}
	}

	
	public long? AnswersCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("CreatedByUser", this, oldValue, value));
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
			_questionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("QuestionId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _specificHazard;
	public string SpecificHazard
	{
		get => _specificHazard;
		set
		{
			var oldValue = _specificHazard;
			_specificHazard = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("SpecificHazard", this, oldValue, value));
		}
	}

	private string _precautionsToControlHazard;
	public string PrecautionsToControlHazard
	{
		get => _precautionsToControlHazard;
		set
		{
			var oldValue = _precautionsToControlHazard;
			_precautionsToControlHazard = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("PrecautionsToControlHazard", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("Guid", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("Id", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("PersistenceKey", this, oldValue, value));
		}
	}

	private RiskAssessmentQuestion _question;
	public RiskAssessmentQuestion Question
	{
		get => _question;
		set
		{
			var oldValue = _question;
			_question = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("Question", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("CreatedByUser", this, oldValue, value));
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
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("SiteInspectionId", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("CreatedByUserId", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("PersistenceKey", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("CreatedByUser", this, oldValue, value));
		}
	}

	private SiteInspection _siteInspection;
	public SiteInspection SiteInspection
	{
		get => _siteInspection;
		set
		{
			var oldValue = _siteInspection;
			_siteInspection = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("SiteInspection", this, oldValue, value));
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
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("CreatedByUserId", this, oldValue, value));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			var oldValue = _siteId;
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("SiteId", this, oldValue, value));
		}
	}

	private string _emailAddress;
	public string EmailAddress
	{
		get => _emailAddress;
		set
		{
			var oldValue = _emailAddress;
			_emailAddress = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("EmailAddress", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("Guid", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("Id", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("PersistenceKey", this, oldValue, value));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			var oldValue = _site;
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("Site", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("Id", this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("Title", this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("Description", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("CreatedByUserId", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Id", this, oldValue, value));
		}
	}

	private int _categoryId;
	public int CategoryId
	{
		get => _categoryId;
		set
		{
			var oldValue = _categoryId;
			_categoryId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("CategoryId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("CreatedByUserId", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("PersistenceKey", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Name", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Version", this, oldValue, value));
		}
	}

	private ReportCategory _category;
	public ReportCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Category", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<PersonReport> _faultReports;
	public ObservableCollection<PersonReport> FaultReports
	{
		get => _faultReports;
		set
		{
			var oldValue = _faultReports;
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("FaultReports", this, oldValue, value));
		}
	}

	
	public long? FaultReportsCount { get; set; }
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
			_reportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("ReportId", this, oldValue, value));
		}
	}

	private int _recommendationId;
	public int RecommendationId
	{
		get => _recommendationId;
		set
		{
			var oldValue = _recommendationId;
			_recommendationId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("RecommendationId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			var oldValue = _notes;
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Notes", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Guid", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Id", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("PersistenceKey", this, oldValue, value));
		}
	}

	private PersonReport _personReport;
	public PersonReport PersonReport
	{
		get => _personReport;
		set
		{
			var oldValue = _personReport;
			_personReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("PersonReport", this, oldValue, value));
		}
	}

	private ReportDefaultRecommendation _recommendation;
	public ReportDefaultRecommendation Recommendation
	{
		get => _recommendation;
		set
		{
			var oldValue = _recommendation;
			_recommendation = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Recommendation", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Name", this, oldValue, value));
		}
	}

	private string _text;
	public string Text
	{
		get => _text;
		set
		{
			var oldValue = _text;
			_text = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Text", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("PersistenceKey", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<ReportRecommendation> _recommendations;
	public ObservableCollection<ReportRecommendation> Recommendations
	{
		get => _recommendations;
		set
		{
			var oldValue = _recommendations;
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Recommendations", this, oldValue, value));
		}
	}

	
	public long? RecommendationsCount { get; set; }
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("Name", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("PersistenceKey", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<ReportType> _reportTypes;
	public ObservableCollection<ReportType> ReportTypes
	{
		get => _reportTypes;
		set
		{
			var oldValue = _reportTypes;
			_reportTypes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("ReportTypes", this, oldValue, value));
		}
	}

	
	public long? ReportTypesCount { get; set; }
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
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("FaultReportId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			var oldValue = _notes;
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("Notes", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("Guid", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("Id", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("PersistenceKey", this, oldValue, value));
		}
	}

	private PersonReport _personReport;
	public PersonReport PersonReport
	{
		get => _personReport;
		set
		{
			var oldValue = _personReport;
			_personReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("PersonReport", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("CreatedByUser", this, oldValue, value));
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
			_categoryId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("CategoryId", this, oldValue, value));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			var oldValue = _siteId;
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("SiteId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			var oldValue = _title;
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Title", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Guid", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Id", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("PersistenceKey", this, oldValue, value));
		}
	}

	private DocumentCategory _category;
	public DocumentCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Category", this, oldValue, value));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			var oldValue = _site;
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Site", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Id", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Name", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Guid", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("PersistenceKey", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<SiteDocument> _documents;
	public ObservableCollection<SiteDocument> Documents
	{
		get => _documents;
		set
		{
			var oldValue = _documents;
			_documents = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Documents", this, oldValue, value));
		}
	}

	
	public long? DocumentsCount { get; set; }
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>("Id", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>("Name", this, oldValue, value));
		}
	}

	private ObservableCollection<Client> _clients;
	public ObservableCollection<Client> Clients
	{
		get => _clients;
		set
		{
			var oldValue = _clients;
			_clients = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>("Clients", this, oldValue, value));
		}
	}

	
	public long? ClientsCount { get; set; }
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
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("TypeId", this, oldValue, value));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			var oldValue = _createdByUserId;
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("CreatedByUserId", this, oldValue, value));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			var oldValue = _name;
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Name", this, oldValue, value));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			var oldValue = _description;
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Description", this, oldValue, value));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			var oldValue = _guid;
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Guid", this, oldValue, value));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Id", this, oldValue, value));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			var oldValue = _createdDate;
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("CreatedDate", this, oldValue, value));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			var oldValue = _version;
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Version", this, oldValue, value));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			var oldValue = _persistenceKey;
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<ApplicationUser> _users;
	public ObservableCollection<ApplicationUser> Users
	{
		get => _users;
		set
		{
			var oldValue = _users;
			_users = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Users", this, oldValue, value));
		}
	}

	
	public long? UsersCount { get; set; }
	private ClientType _type;
	public ClientType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Type", this, oldValue, value));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<Person> _people;
	public ObservableCollection<Person> People
	{
		get => _people;
		set
		{
			var oldValue = _people;
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("People", this, oldValue, value));
		}
	}

	
	public long? PeopleCount { get; set; }
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("Id", this, oldValue, value));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			var oldValue = _clientId;
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ClientId", this, oldValue, value));
		}
	}

	private string _email;
	public string Email
	{
		get => _email;
		set
		{
			var oldValue = _email;
			_email = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("Email", this, oldValue, value));
		}
	}

	private string _fullName;
	public string FullName
	{
		get => _fullName;
		set
		{
			var oldValue = _fullName;
			_fullName = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FullName", this, oldValue, value));
		}
	}

	private bool _emailConfirmed;
	public bool EmailConfirmed
	{
		get => _emailConfirmed;
		set
		{
			var oldValue = _emailConfirmed;
			_emailConfirmed = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("EmailConfirmed", this, oldValue, value));
		}
	}

	private UserType _userType;
	public UserType UserType
	{
		get => _userType;
		set
		{
			var oldValue = _userType;
			_userType = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("UserType", this, oldValue, value));
		}
	}

	private bool _isLockedOut;
	public bool IsLockedOut
	{
		get => _isLockedOut;
		set
		{
			var oldValue = _isLockedOut;
			_isLockedOut = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("IsLockedOut", this, oldValue, value));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			var oldValue = _client;
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("Client", this, oldValue, value));
		}
	}

	private ObservableCollection<Client> _clientsCreated;
	public ObservableCollection<Client> ClientsCreated
	{
		get => _clientsCreated;
		set
		{
			var oldValue = _clientsCreated;
			_clientsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ClientsCreated", this, oldValue, value));
		}
	}

	
	public long? ClientsCreatedCount { get; set; }
	private ObservableCollection<DocumentCategory> _documentCategoriesCreated;
	public ObservableCollection<DocumentCategory> DocumentCategoriesCreated
	{
		get => _documentCategoriesCreated;
		set
		{
			var oldValue = _documentCategoriesCreated;
			_documentCategoriesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("DocumentCategoriesCreated", this, oldValue, value));
		}
	}

	
	public long? DocumentCategoriesCreatedCount { get; set; }
	private ObservableCollection<SiteDocument> _siteDocumentsCreated;
	public ObservableCollection<SiteDocument> SiteDocumentsCreated
	{
		get => _siteDocumentsCreated;
		set
		{
			var oldValue = _siteDocumentsCreated;
			_siteDocumentsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("SiteDocumentsCreated", this, oldValue, value));
		}
	}

	
	public long? SiteDocumentsCreatedCount { get; set; }
	private ObservableCollection<ReportActionsTaken> _faultActionsTakenCreated;
	public ObservableCollection<ReportActionsTaken> FaultActionsTakenCreated
	{
		get => _faultActionsTakenCreated;
		set
		{
			var oldValue = _faultActionsTakenCreated;
			_faultActionsTakenCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultActionsTakenCreated", this, oldValue, value));
		}
	}

	
	public long? FaultActionsTakenCreatedCount { get; set; }
	private ObservableCollection<ReportCategory> _faultCategoriesCreated;
	public ObservableCollection<ReportCategory> FaultCategoriesCreated
	{
		get => _faultCategoriesCreated;
		set
		{
			var oldValue = _faultCategoriesCreated;
			_faultCategoriesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultCategoriesCreated", this, oldValue, value));
		}
	}

	
	public long? FaultCategoriesCreatedCount { get; set; }
	private ObservableCollection<ReportDefaultRecommendation> _faultDefaultRecommendationsCreated;
	public ObservableCollection<ReportDefaultRecommendation> FaultDefaultRecommendationsCreated
	{
		get => _faultDefaultRecommendationsCreated;
		set
		{
			var oldValue = _faultDefaultRecommendationsCreated;
			_faultDefaultRecommendationsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultDefaultRecommendationsCreated", this, oldValue, value));
		}
	}

	
	public long? FaultDefaultRecommendationsCreatedCount { get; set; }
	private ObservableCollection<ReportRecommendation> _faultRecommendationsCreated;
	public ObservableCollection<ReportRecommendation> FaultRecommendationsCreated
	{
		get => _faultRecommendationsCreated;
		set
		{
			var oldValue = _faultRecommendationsCreated;
			_faultRecommendationsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultRecommendationsCreated", this, oldValue, value));
		}
	}

	
	public long? FaultRecommendationsCreatedCount { get; set; }
	private ObservableCollection<ReportType> _faultTypesCreated;
	public ObservableCollection<ReportType> FaultTypesCreated
	{
		get => _faultTypesCreated;
		set
		{
			var oldValue = _faultTypesCreated;
			_faultTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultTypesCreated", this, oldValue, value));
		}
	}

	
	public long? FaultTypesCreatedCount { get; set; }
	private ObservableCollection<Project> _projectCreated;
	public ObservableCollection<Project> ProjectCreated
	{
		get => _projectCreated;
		set
		{
			var oldValue = _projectCreated;
			_projectCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ProjectCreated", this, oldValue, value));
		}
	}

	
	public long? ProjectCreatedCount { get; set; }
	private ObservableCollection<ReportReceiverEmailAddress> _reportReceiverEmailAddressesCreated;
	public ObservableCollection<ReportReceiverEmailAddress> ReportReceiverEmailAddressesCreated
	{
		get => _reportReceiverEmailAddressesCreated;
		set
		{
			var oldValue = _reportReceiverEmailAddressesCreated;
			_reportReceiverEmailAddressesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ReportReceiverEmailAddressesCreated", this, oldValue, value));
		}
	}

	
	public long? ReportReceiverEmailAddressesCreatedCount { get; set; }
	private ObservableCollection<RiskAssessment> _riskAssessmentsCreated;
	public ObservableCollection<RiskAssessment> RiskAssessmentsCreated
	{
		get => _riskAssessmentsCreated;
		set
		{
			var oldValue = _riskAssessmentsCreated;
			_riskAssessmentsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("RiskAssessmentsCreated", this, oldValue, value));
		}
	}

	
	public long? RiskAssessmentsCreatedCount { get; set; }
	private ObservableCollection<RiskAssessmentAnswer> _riskAssessmentAnswersCreated;
	public ObservableCollection<RiskAssessmentAnswer> RiskAssessmentAnswersCreated
	{
		get => _riskAssessmentAnswersCreated;
		set
		{
			var oldValue = _riskAssessmentAnswersCreated;
			_riskAssessmentAnswersCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("RiskAssessmentAnswersCreated", this, oldValue, value));
		}
	}

	
	public long? RiskAssessmentAnswersCreatedCount { get; set; }
	private ObservableCollection<RiskAssessmentQuestion> _riskAssessmentQuestionsCreated;
	public ObservableCollection<RiskAssessmentQuestion> RiskAssessmentQuestionsCreated
	{
		get => _riskAssessmentQuestionsCreated;
		set
		{
			var oldValue = _riskAssessmentQuestionsCreated;
			_riskAssessmentQuestionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("RiskAssessmentQuestionsCreated", this, oldValue, value));
		}
	}

	
	public long? RiskAssessmentQuestionsCreatedCount { get; set; }
	private ObservableCollection<Person> _peopleCreated;
	public ObservableCollection<Person> PeopleCreated
	{
		get => _peopleCreated;
		set
		{
			var oldValue = _peopleCreated;
			_peopleCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("PeopleCreated", this, oldValue, value));
		}
	}

	
	public long? PeopleCreatedCount { get; set; }
	private ObservableCollection<PersonInspection> _personInspectionsCreated;
	public ObservableCollection<PersonInspection> PersonInspectionsCreated
	{
		get => _personInspectionsCreated;
		set
		{
			var oldValue = _personInspectionsCreated;
			_personInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("PersonInspectionsCreated", this, oldValue, value));
		}
	}

	
	public long? PersonInspectionsCreatedCount { get; set; }
	private ObservableCollection<PersonLoading> _personLoadingsCreated;
	public ObservableCollection<PersonLoading> PersonLoadingsCreated
	{
		get => _personLoadingsCreated;
		set
		{
			var oldValue = _personLoadingsCreated;
			_personLoadingsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("PersonLoadingsCreated", this, oldValue, value));
		}
	}

	
	public long? PersonLoadingsCreatedCount { get; set; }
	private ObservableCollection<PersonType> _personTypesCreated;
	public ObservableCollection<PersonType> PersonTypesCreated
	{
		get => _personTypesCreated;
		set
		{
			var oldValue = _personTypesCreated;
			_personTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("PersonTypesCreated", this, oldValue, value));
		}
	}

	
	public long? PersonTypesCreatedCount { get; set; }
	private ObservableCollection<PersonReport> _faultReportsCreated;
	public ObservableCollection<PersonReport> FaultReportsCreated
	{
		get => _faultReportsCreated;
		set
		{
			var oldValue = _faultReportsCreated;
			_faultReportsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultReportsCreated", this, oldValue, value));
		}
	}

	
	public long? FaultReportsCreatedCount { get; set; }
	private ObservableCollection<Site> _sitesCreated;
	public ObservableCollection<Site> SitesCreated
	{
		get => _sitesCreated;
		set
		{
			var oldValue = _sitesCreated;
			_sitesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("SitesCreated", this, oldValue, value));
		}
	}

	
	public long? SitesCreatedCount { get; set; }
	private ObservableCollection<SiteInspection> _siteInspectionsCreated;
	public ObservableCollection<SiteInspection> SiteInspectionsCreated
	{
		get => _siteInspectionsCreated;
		set
		{
			var oldValue = _siteInspectionsCreated;
			_siteInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("SiteInspectionsCreated", this, oldValue, value));
		}
	}

	
	public long? SiteInspectionsCreatedCount { get; set; }
	private ObservableCollection<UserSite> _sites;
	public ObservableCollection<UserSite> Sites
	{
		get => _sites;
		set
		{
			var oldValue = _sites;
			_sites = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("Sites", this, oldValue, value));
		}
	}

	
	public long? SitesCount { get; set; }
	
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

