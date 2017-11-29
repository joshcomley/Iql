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


public class ScaffoldTypeBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
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


public class FaultTypeBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
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

	private ObservableCollection<ScaffoldInspection> _scaffoldInspections;
	public ObservableCollection<ScaffoldInspection> ScaffoldInspections
	{
		get => _scaffoldInspections;
		set
		{
			var oldValue = _scaffoldInspections;
			_scaffoldInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("ScaffoldInspections", this, oldValue, value));
		}
	}

	
	public long? ScaffoldInspectionsCount { get; set; }
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

public class ScaffoldType : ScaffoldTypeBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Title", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Description", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<Scaffold> _scaffolds;
	public ObservableCollection<Scaffold> Scaffolds
	{
		get => _scaffolds;
		set
		{
			var oldValue = _scaffolds;
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Scaffolds", this, oldValue, value));
		}
	}

	
	public long? ScaffoldsCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Name", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<Scaffold> _scaffolds;
	public ObservableCollection<Scaffold> Scaffolds
	{
		get => _scaffolds;
		set
		{
			var oldValue = _scaffolds;
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Scaffolds", this, oldValue, value));
		}
	}

	
	public long? ScaffoldsCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			var oldValue = _createdByUser;
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("CreatedByUser", this, oldValue, value));
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
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("SiteInspectionId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("CreatedByUserId", this, oldValue, value));
		}
	}

	private int _scaffoldId;
	public int ScaffoldId
	{
		get => _scaffoldId;
		set
		{
			var oldValue = _scaffoldId;
			_scaffoldId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("ScaffoldId", this, oldValue, value));
		}
	}

	private ScaffoldInspectionStatus _inspectionStatus;
	public ScaffoldInspectionStatus InspectionStatus
	{
		get => _inspectionStatus;
		set
		{
			var oldValue = _inspectionStatus;
			_inspectionStatus = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("InspectionStatus", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("StartTime", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("EndTime", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("ReasonForFailure", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("IsDesignRequired", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("DrawingNumber", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("PersistenceKey", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("SiteInspection", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("TypeId", this, oldValue, value));
		}
	}

	private int _loadingId;
	public int LoadingId
	{
		get => _loadingId;
		set
		{
			var oldValue = _loadingId;
			_loadingId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("LoadingId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Key", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Title", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Description", this, oldValue, value));
		}
	}

	private ScaffoldCategory _category;
	public ScaffoldCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Category", this, oldValue, value));
		}
	}

	private int _clientId;
	public int ClientId
	{
		get => _clientId;
		set
		{
			var oldValue = _clientId;
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("ClientId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<FaultReport> _faultReports;
	public ObservableCollection<FaultReport> FaultReports
	{
		get => _faultReports;
		set
		{
			var oldValue = _faultReports;
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("FaultReports", this, oldValue, value));
		}
	}

	
	public long? FaultReportsCount { get; set; }
	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			var oldValue = _client;
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Client", this, oldValue, value));
		}
	}

	private ScaffoldType _type;
	public ScaffoldType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Type", this, oldValue, value));
		}
	}

	private ScaffoldLoading _loading;
	public ScaffoldLoading Loading
	{
		get => _loading;
		set
		{
			var oldValue = _loading;
			_loading = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Loading", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("CreatedByUser", this, oldValue, value));
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

public class FaultType : FaultTypeBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			var oldValue = _id;
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("CategoryId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Name", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<FaultReport> _faultReports;
	public ObservableCollection<FaultReport> FaultReports
	{
		get => _faultReports;
		set
		{
			var oldValue = _faultReports;
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("FaultReports", this, oldValue, value));
		}
	}

	
	public long? FaultReportsCount { get; set; }
	private FaultCategory _category;
	public FaultCategory Category
	{
		get => _category;
		set
		{
			var oldValue = _category;
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Category", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Id", this, oldValue, value));
		}
	}

	private int _scaffoldId;
	public int ScaffoldId
	{
		get => _scaffoldId;
		set
		{
			var oldValue = _scaffoldId;
			_scaffoldId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("ScaffoldId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("TypeId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Status", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("PersistenceKey", this, oldValue, value));
		}
	}

	private ObservableCollection<FaultActionsTaken> _actionsTaken;
	public ObservableCollection<FaultActionsTaken> ActionsTaken
	{
		get => _actionsTaken;
		set
		{
			var oldValue = _actionsTaken;
			_actionsTaken = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("ActionsTaken", this, oldValue, value));
		}
	}

	
	public long? ActionsTakenCount { get; set; }
	private ObservableCollection<FaultRecommendation> _recommendations;
	public ObservableCollection<FaultRecommendation> Recommendations
	{
		get => _recommendations;
		set
		{
			var oldValue = _recommendations;
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Recommendations", this, oldValue, value));
		}
	}

	
	public long? RecommendationsCount { get; set; }
	private Scaffold _scaffold;
	public Scaffold Scaffold
	{
		get => _scaffold;
		set
		{
			var oldValue = _scaffold;
			_scaffold = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Scaffold", this, oldValue, value));
		}
	}

	private FaultType _type;
	public FaultType Type
	{
		get => _type;
		set
		{
			var oldValue = _type;
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Type", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("CreatedByUser", this, oldValue, value));
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
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("FaultReportId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("RecommendationId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Notes", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("PersistenceKey", this, oldValue, value));
		}
	}

	private FaultReport _faultReport;
	public FaultReport FaultReport
	{
		get => _faultReport;
		set
		{
			var oldValue = _faultReport;
			_faultReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("FaultReport", this, oldValue, value));
		}
	}

	private FaultDefaultRecommendation _recommendation;
	public FaultDefaultRecommendation Recommendation
	{
		get => _recommendation;
		set
		{
			var oldValue = _recommendation;
			_recommendation = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Recommendation", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("CreatedByUser", this, oldValue, value));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Name", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Text", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("PersistenceKey", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<FaultRecommendation> _recommendations;
	public ObservableCollection<FaultRecommendation> Recommendations
	{
		get => _recommendations;
		set
		{
			var oldValue = _recommendations;
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Recommendations", this, oldValue, value));
		}
	}

	
	public long? RecommendationsCount { get; set; }
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("Name", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("PersistenceKey", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("CreatedByUser", this, oldValue, value));
		}
	}

	private ObservableCollection<FaultType> _faultTypes;
	public ObservableCollection<FaultType> FaultTypes
	{
		get => _faultTypes;
		set
		{
			var oldValue = _faultTypes;
			_faultTypes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("FaultTypes", this, oldValue, value));
		}
	}

	
	public long? FaultTypesCount { get; set; }
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
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("FaultReportId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("CreatedByUserId", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("Notes", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("Guid", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("Id", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("CreatedDate", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("Version", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("PersistenceKey", this, oldValue, value));
		}
	}

	private FaultReport _faultReport;
	public FaultReport FaultReport
	{
		get => _faultReport;
		set
		{
			var oldValue = _faultReport;
			_faultReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("FaultReport", this, oldValue, value));
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
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("CreatedByUser", this, oldValue, value));
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

	private ObservableCollection<Scaffold> _scaffolds;
	public ObservableCollection<Scaffold> Scaffolds
	{
		get => _scaffolds;
		set
		{
			var oldValue = _scaffolds;
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Scaffolds", this, oldValue, value));
		}
	}

	
	public long? ScaffoldsCount { get; set; }
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
	private ObservableCollection<FaultActionsTaken> _faultActionsTakenCreated;
	public ObservableCollection<FaultActionsTaken> FaultActionsTakenCreated
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
	private ObservableCollection<FaultCategory> _faultCategoriesCreated;
	public ObservableCollection<FaultCategory> FaultCategoriesCreated
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
	private ObservableCollection<FaultDefaultRecommendation> _faultDefaultRecommendationsCreated;
	public ObservableCollection<FaultDefaultRecommendation> FaultDefaultRecommendationsCreated
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
	private ObservableCollection<FaultRecommendation> _faultRecommendationsCreated;
	public ObservableCollection<FaultRecommendation> FaultRecommendationsCreated
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
	private ObservableCollection<FaultReport> _faultReportsCreated;
	public ObservableCollection<FaultReport> FaultReportsCreated
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
	private ObservableCollection<FaultType> _faultTypesCreated;
	public ObservableCollection<FaultType> FaultTypesCreated
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
	private ObservableCollection<Scaffold> _scaffoldsCreated;
	public ObservableCollection<Scaffold> ScaffoldsCreated
	{
		get => _scaffoldsCreated;
		set
		{
			var oldValue = _scaffoldsCreated;
			_scaffoldsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ScaffoldsCreated", this, oldValue, value));
		}
	}

	
	public long? ScaffoldsCreatedCount { get; set; }
	private ObservableCollection<ScaffoldInspection> _scaffoldInspectionsCreated;
	public ObservableCollection<ScaffoldInspection> ScaffoldInspectionsCreated
	{
		get => _scaffoldInspectionsCreated;
		set
		{
			var oldValue = _scaffoldInspectionsCreated;
			_scaffoldInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ScaffoldInspectionsCreated", this, oldValue, value));
		}
	}

	
	public long? ScaffoldInspectionsCreatedCount { get; set; }
	private ObservableCollection<ScaffoldLoading> _scaffoldLoadingsCreated;
	public ObservableCollection<ScaffoldLoading> ScaffoldLoadingsCreated
	{
		get => _scaffoldLoadingsCreated;
		set
		{
			var oldValue = _scaffoldLoadingsCreated;
			_scaffoldLoadingsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ScaffoldLoadingsCreated", this, oldValue, value));
		}
	}

	
	public long? ScaffoldLoadingsCreatedCount { get; set; }
	private ObservableCollection<ScaffoldType> _scaffoldTypesCreated;
	public ObservableCollection<ScaffoldType> ScaffoldTypesCreated
	{
		get => _scaffoldTypesCreated;
		set
		{
			var oldValue = _scaffoldTypesCreated;
			_scaffoldTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ScaffoldTypesCreated", this, oldValue, value));
		}
	}

	
	public long? ScaffoldTypesCreatedCount { get; set; }
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

