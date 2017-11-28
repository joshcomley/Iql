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
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>("SiteId", this));
		}
	}

	private string _userId;
	public string UserId
	{
		get => _userId;
		set
		{
			_userId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>("UserId", this));
		}
	}

	private ApplicationUser _user;
	public ApplicationUser User
	{
		get => _user;
		set
		{
			_user = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>("User", this));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<UserSite>("Site", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("Id", this));
		}
	}

	private int _riskAssessmentId;
	public int RiskAssessmentId
	{
		get => _riskAssessmentId;
		set
		{
			_riskAssessmentId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("RiskAssessmentId", this));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("SiteId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("CreatedByUserId", this));
		}
	}

	private DateTimeOffset _startTime;
	public DateTimeOffset StartTime
	{
		get => _startTime;
		set
		{
			_startTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("StartTime", this));
		}
	}

	private DateTimeOffset _endTime;
	public DateTimeOffset EndTime
	{
		get => _endTime;
		set
		{
			_endTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("EndTime", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("PersistenceKey", this));
		}
	}

	private ObservableCollection<ScaffoldInspection> _scaffoldInspections;
	public ObservableCollection<ScaffoldInspection> ScaffoldInspections
	{
		get => _scaffoldInspections;
		set
		{
			_scaffoldInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("ScaffoldInspections", this));
		}
	}

	
	public long? ScaffoldInspectionsCount { get; set; }
	private RiskAssessment _riskAssessment;
	public RiskAssessment RiskAssessment
	{
		get => _riskAssessment;
		set
		{
			_riskAssessment = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("RiskAssessment", this));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("Site", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Id", this));
		}
	}

	private int? _parentId;
	public int? ParentId
	{
		get => _parentId;
		set
		{
			_parentId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("ParentId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("CreatedByUserId", this));
		}
	}

	private string _address;
	public string Address
	{
		get => _address;
		set
		{
			_address = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Address", this));
		}
	}

	private string _postCode;
	public string PostCode
	{
		get => _postCode;
		set
		{
			_postCode = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("PostCode", this));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("ClientId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Name", this));
		}
	}

	private int _left;
	public int Left
	{
		get => _left;
		set
		{
			_left = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Left", this));
		}
	}

	private int _right;
	public int Right
	{
		get => _right;
		set
		{
			_right = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Right", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("PersistenceKey", this));
		}
	}

	private ObservableCollection<SiteDocument> _documents;
	public ObservableCollection<SiteDocument> Documents
	{
		get => _documents;
		set
		{
			_documents = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Documents", this));
		}
	}

	
	public long? DocumentsCount { get; set; }
	private ObservableCollection<ReportReceiverEmailAddress> _additionalSendReportsTo;
	public ObservableCollection<ReportReceiverEmailAddress> AdditionalSendReportsTo
	{
		get => _additionalSendReportsTo;
		set
		{
			_additionalSendReportsTo = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("AdditionalSendReportsTo", this));
		}
	}

	
	public long? AdditionalSendReportsToCount { get; set; }
	private Site _parent;
	public Site Parent
	{
		get => _parent;
		set
		{
			_parent = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Parent", this));
		}
	}

	private ObservableCollection<Site> _children;
	public ObservableCollection<Site> Children
	{
		get => _children;
		set
		{
			_children = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Children", this));
		}
	}

	
	public long? ChildrenCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("CreatedByUser", this));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Client", this));
		}
	}

	private ObservableCollection<SiteInspection> _siteInspections;
	public ObservableCollection<SiteInspection> SiteInspections
	{
		get => _siteInspections;
		set
		{
			_siteInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("SiteInspections", this));
		}
	}

	
	public long? SiteInspectionsCount { get; set; }
	private ObservableCollection<UserSite> _users;
	public ObservableCollection<UserSite> Users
	{
		get => _users;
		set
		{
			_users = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Site>("Users", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("CreatedByUserId", this));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Title", this));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Description", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("PersistenceKey", this));
		}
	}

	private ObservableCollection<Scaffold> _scaffolds;
	public ObservableCollection<Scaffold> Scaffolds
	{
		get => _scaffolds;
		set
		{
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("Scaffolds", this));
		}
	}

	
	public long? ScaffoldsCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldType>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Name", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("PersistenceKey", this));
		}
	}

	private ObservableCollection<Scaffold> _scaffolds;
	public ObservableCollection<Scaffold> Scaffolds
	{
		get => _scaffolds;
		set
		{
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("Scaffolds", this));
		}
	}

	
	public long? ScaffoldsCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldLoading>("CreatedByUser", this));
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
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("SiteInspectionId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("CreatedByUserId", this));
		}
	}

	private int _scaffoldId;
	public int ScaffoldId
	{
		get => _scaffoldId;
		set
		{
			_scaffoldId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("ScaffoldId", this));
		}
	}

	private ScaffoldInspectionStatus _inspectionStatus;
	public ScaffoldInspectionStatus InspectionStatus
	{
		get => _inspectionStatus;
		set
		{
			_inspectionStatus = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("InspectionStatus", this));
		}
	}

	private DateTimeOffset _startTime;
	public DateTimeOffset StartTime
	{
		get => _startTime;
		set
		{
			_startTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("StartTime", this));
		}
	}

	private DateTimeOffset _endTime;
	public DateTimeOffset EndTime
	{
		get => _endTime;
		set
		{
			_endTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("EndTime", this));
		}
	}

	private InspectionFailReason _reasonForFailure;
	public InspectionFailReason ReasonForFailure
	{
		get => _reasonForFailure;
		set
		{
			_reasonForFailure = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("ReasonForFailure", this));
		}
	}

	private bool _isDesignRequired;
	public bool IsDesignRequired
	{
		get => _isDesignRequired;
		set
		{
			_isDesignRequired = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("IsDesignRequired", this));
		}
	}

	private string _drawingNumber;
	public string DrawingNumber
	{
		get => _drawingNumber;
		set
		{
			_drawingNumber = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("DrawingNumber", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("PersistenceKey", this));
		}
	}

	private SiteInspection _siteInspection;
	public SiteInspection SiteInspection
	{
		get => _siteInspection;
		set
		{
			_siteInspection = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("SiteInspection", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ScaffoldInspection>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Id", this));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("TypeId", this));
		}
	}

	private int _loadingId;
	public int LoadingId
	{
		get => _loadingId;
		set
		{
			_loadingId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("LoadingId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("CreatedByUserId", this));
		}
	}

	private string _key;
	public string Key
	{
		get => _key;
		set
		{
			_key = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Key", this));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Title", this));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Description", this));
		}
	}

	private ScaffoldCategory _category;
	public ScaffoldCategory Category
	{
		get => _category;
		set
		{
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Category", this));
		}
	}

	private int _clientId;
	public int ClientId
	{
		get => _clientId;
		set
		{
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("ClientId", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("PersistenceKey", this));
		}
	}

	private ObservableCollection<FaultReport> _faultReports;
	public ObservableCollection<FaultReport> FaultReports
	{
		get => _faultReports;
		set
		{
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("FaultReports", this));
		}
	}

	
	public long? FaultReportsCount { get; set; }
	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Client", this));
		}
	}

	private ScaffoldType _type;
	public ScaffoldType Type
	{
		get => _type;
		set
		{
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Type", this));
		}
	}

	private ScaffoldLoading _loading;
	public ScaffoldLoading Loading
	{
		get => _loading;
		set
		{
			_loading = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("Loading", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Scaffold>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Name", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("PersistenceKey", this));
		}
	}

	private ObservableCollection<RiskAssessmentAnswer> _answers;
	public ObservableCollection<RiskAssessmentAnswer> Answers
	{
		get => _answers;
		set
		{
			_answers = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("Answers", this));
		}
	}

	
	public long? AnswersCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentQuestion>("CreatedByUser", this));
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
			_questionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("QuestionId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("CreatedByUserId", this));
		}
	}

	private string _specificHazard;
	public string SpecificHazard
	{
		get => _specificHazard;
		set
		{
			_specificHazard = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("SpecificHazard", this));
		}
	}

	private string _precautionsToControlHazard;
	public string PrecautionsToControlHazard
	{
		get => _precautionsToControlHazard;
		set
		{
			_precautionsToControlHazard = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("PrecautionsToControlHazard", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("PersistenceKey", this));
		}
	}

	private RiskAssessmentQuestion _question;
	public RiskAssessmentQuestion Question
	{
		get => _question;
		set
		{
			_question = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("Question", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessmentAnswer>("CreatedByUser", this));
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
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("SiteInspectionId", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("CreatedByUserId", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("PersistenceKey", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("CreatedByUser", this));
		}
	}

	private SiteInspection _siteInspection;
	public SiteInspection SiteInspection
	{
		get => _siteInspection;
		set
		{
			_siteInspection = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<RiskAssessment>("SiteInspection", this));
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
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("CreatedByUserId", this));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("SiteId", this));
		}
	}

	private string _emailAddress;
	public string EmailAddress
	{
		get => _emailAddress;
		set
		{
			_emailAddress = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("EmailAddress", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("PersistenceKey", this));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("Site", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportReceiverEmailAddress>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("Id", this));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("Title", this));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("Description", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("CreatedByUserId", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Project>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Id", this));
		}
	}

	private int _categoryId;
	public int CategoryId
	{
		get => _categoryId;
		set
		{
			_categoryId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("CategoryId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Name", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("PersistenceKey", this));
		}
	}

	private ObservableCollection<FaultReport> _faultReports;
	public ObservableCollection<FaultReport> FaultReports
	{
		get => _faultReports;
		set
		{
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("FaultReports", this));
		}
	}

	
	public long? FaultReportsCount { get; set; }
	private FaultCategory _category;
	public FaultCategory Category
	{
		get => _category;
		set
		{
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("Category", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultType>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Id", this));
		}
	}

	private int _scaffoldId;
	public int ScaffoldId
	{
		get => _scaffoldId;
		set
		{
			_scaffoldId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("ScaffoldId", this));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("TypeId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("CreatedByUserId", this));
		}
	}

	private FaultReportStatus _status;
	public FaultReportStatus Status
	{
		get => _status;
		set
		{
			_status = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Status", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("PersistenceKey", this));
		}
	}

	private ObservableCollection<FaultActionsTaken> _actionsTaken;
	public ObservableCollection<FaultActionsTaken> ActionsTaken
	{
		get => _actionsTaken;
		set
		{
			_actionsTaken = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("ActionsTaken", this));
		}
	}

	
	public long? ActionsTakenCount { get; set; }
	private ObservableCollection<FaultRecommendation> _recommendations;
	public ObservableCollection<FaultRecommendation> Recommendations
	{
		get => _recommendations;
		set
		{
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Recommendations", this));
		}
	}

	
	public long? RecommendationsCount { get; set; }
	private Scaffold _scaffold;
	public Scaffold Scaffold
	{
		get => _scaffold;
		set
		{
			_scaffold = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Scaffold", this));
		}
	}

	private FaultType _type;
	public FaultType Type
	{
		get => _type;
		set
		{
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("Type", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultReport>("CreatedByUser", this));
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
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("FaultReportId", this));
		}
	}

	private int _recommendationId;
	public int RecommendationId
	{
		get => _recommendationId;
		set
		{
			_recommendationId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("RecommendationId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("CreatedByUserId", this));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Notes", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("PersistenceKey", this));
		}
	}

	private FaultReport _faultReport;
	public FaultReport FaultReport
	{
		get => _faultReport;
		set
		{
			_faultReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("FaultReport", this));
		}
	}

	private FaultDefaultRecommendation _recommendation;
	public FaultDefaultRecommendation Recommendation
	{
		get => _recommendation;
		set
		{
			_recommendation = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("Recommendation", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultRecommendation>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Name", this));
		}
	}

	private string _text;
	public string Text
	{
		get => _text;
		set
		{
			_text = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Text", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("PersistenceKey", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("CreatedByUser", this));
		}
	}

	private ObservableCollection<FaultRecommendation> _recommendations;
	public ObservableCollection<FaultRecommendation> Recommendations
	{
		get => _recommendations;
		set
		{
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultDefaultRecommendation>("Recommendations", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("Name", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("PersistenceKey", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("CreatedByUser", this));
		}
	}

	private ObservableCollection<FaultType> _faultTypes;
	public ObservableCollection<FaultType> FaultTypes
	{
		get => _faultTypes;
		set
		{
			_faultTypes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultCategory>("FaultTypes", this));
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
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("FaultReportId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("CreatedByUserId", this));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("Notes", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("PersistenceKey", this));
		}
	}

	private FaultReport _faultReport;
	public FaultReport FaultReport
	{
		get => _faultReport;
		set
		{
			_faultReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("FaultReport", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<FaultActionsTaken>("CreatedByUser", this));
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
			_categoryId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("CategoryId", this));
		}
	}

	private int _siteId;
	public int SiteId
	{
		get => _siteId;
		set
		{
			_siteId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("SiteId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("CreatedByUserId", this));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Title", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("PersistenceKey", this));
		}
	}

	private DocumentCategory _category;
	public DocumentCategory Category
	{
		get => _category;
		set
		{
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Category", this));
		}
	}

	private Site _site;
	public Site Site
	{
		get => _site;
		set
		{
			_site = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("Site", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteDocument>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Name", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("PersistenceKey", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("CreatedByUser", this));
		}
	}

	private ObservableCollection<SiteDocument> _documents;
	public ObservableCollection<SiteDocument> Documents
	{
		get => _documents;
		set
		{
			_documents = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<DocumentCategory>("Documents", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>("Id", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>("Name", this));
		}
	}

	private ObservableCollection<Client> _clients;
	public ObservableCollection<Client> Clients
	{
		get => _clients;
		set
		{
			_clients = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ClientType>("Clients", this));
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
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("TypeId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Name", this));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Description", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("PersistenceKey", this));
		}
	}

	private ObservableCollection<ApplicationUser> _users;
	public ObservableCollection<ApplicationUser> Users
	{
		get => _users;
		set
		{
			_users = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Users", this));
		}
	}

	
	public long? UsersCount { get; set; }
	private ClientType _type;
	public ClientType Type
	{
		get => _type;
		set
		{
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Type", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("CreatedByUser", this));
		}
	}

	private ObservableCollection<Scaffold> _scaffolds;
	public ObservableCollection<Scaffold> Scaffolds
	{
		get => _scaffolds;
		set
		{
			_scaffolds = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("Scaffolds", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("Id", this));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ClientId", this));
		}
	}

	private string _email;
	public string Email
	{
		get => _email;
		set
		{
			_email = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("Email", this));
		}
	}

	private string _fullName;
	public string FullName
	{
		get => _fullName;
		set
		{
			_fullName = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FullName", this));
		}
	}

	private bool _emailConfirmed;
	public bool EmailConfirmed
	{
		get => _emailConfirmed;
		set
		{
			_emailConfirmed = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("EmailConfirmed", this));
		}
	}

	private UserType _userType;
	public UserType UserType
	{
		get => _userType;
		set
		{
			_userType = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("UserType", this));
		}
	}

	private bool _isLockedOut;
	public bool IsLockedOut
	{
		get => _isLockedOut;
		set
		{
			_isLockedOut = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("IsLockedOut", this));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("Client", this));
		}
	}

	private ObservableCollection<Client> _clientsCreated;
	public ObservableCollection<Client> ClientsCreated
	{
		get => _clientsCreated;
		set
		{
			_clientsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ClientsCreated", this));
		}
	}

	
	public long? ClientsCreatedCount { get; set; }
	private ObservableCollection<DocumentCategory> _documentCategoriesCreated;
	public ObservableCollection<DocumentCategory> DocumentCategoriesCreated
	{
		get => _documentCategoriesCreated;
		set
		{
			_documentCategoriesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("DocumentCategoriesCreated", this));
		}
	}

	
	public long? DocumentCategoriesCreatedCount { get; set; }
	private ObservableCollection<SiteDocument> _siteDocumentsCreated;
	public ObservableCollection<SiteDocument> SiteDocumentsCreated
	{
		get => _siteDocumentsCreated;
		set
		{
			_siteDocumentsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("SiteDocumentsCreated", this));
		}
	}

	
	public long? SiteDocumentsCreatedCount { get; set; }
	private ObservableCollection<FaultActionsTaken> _faultActionsTakenCreated;
	public ObservableCollection<FaultActionsTaken> FaultActionsTakenCreated
	{
		get => _faultActionsTakenCreated;
		set
		{
			_faultActionsTakenCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultActionsTakenCreated", this));
		}
	}

	
	public long? FaultActionsTakenCreatedCount { get; set; }
	private ObservableCollection<FaultCategory> _faultCategoriesCreated;
	public ObservableCollection<FaultCategory> FaultCategoriesCreated
	{
		get => _faultCategoriesCreated;
		set
		{
			_faultCategoriesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultCategoriesCreated", this));
		}
	}

	
	public long? FaultCategoriesCreatedCount { get; set; }
	private ObservableCollection<FaultDefaultRecommendation> _faultDefaultRecommendationsCreated;
	public ObservableCollection<FaultDefaultRecommendation> FaultDefaultRecommendationsCreated
	{
		get => _faultDefaultRecommendationsCreated;
		set
		{
			_faultDefaultRecommendationsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultDefaultRecommendationsCreated", this));
		}
	}

	
	public long? FaultDefaultRecommendationsCreatedCount { get; set; }
	private ObservableCollection<FaultRecommendation> _faultRecommendationsCreated;
	public ObservableCollection<FaultRecommendation> FaultRecommendationsCreated
	{
		get => _faultRecommendationsCreated;
		set
		{
			_faultRecommendationsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultRecommendationsCreated", this));
		}
	}

	
	public long? FaultRecommendationsCreatedCount { get; set; }
	private ObservableCollection<FaultReport> _faultReportsCreated;
	public ObservableCollection<FaultReport> FaultReportsCreated
	{
		get => _faultReportsCreated;
		set
		{
			_faultReportsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultReportsCreated", this));
		}
	}

	
	public long? FaultReportsCreatedCount { get; set; }
	private ObservableCollection<FaultType> _faultTypesCreated;
	public ObservableCollection<FaultType> FaultTypesCreated
	{
		get => _faultTypesCreated;
		set
		{
			_faultTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultTypesCreated", this));
		}
	}

	
	public long? FaultTypesCreatedCount { get; set; }
	private ObservableCollection<Project> _projectCreated;
	public ObservableCollection<Project> ProjectCreated
	{
		get => _projectCreated;
		set
		{
			_projectCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ProjectCreated", this));
		}
	}

	
	public long? ProjectCreatedCount { get; set; }
	private ObservableCollection<ReportReceiverEmailAddress> _reportReceiverEmailAddressesCreated;
	public ObservableCollection<ReportReceiverEmailAddress> ReportReceiverEmailAddressesCreated
	{
		get => _reportReceiverEmailAddressesCreated;
		set
		{
			_reportReceiverEmailAddressesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ReportReceiverEmailAddressesCreated", this));
		}
	}

	
	public long? ReportReceiverEmailAddressesCreatedCount { get; set; }
	private ObservableCollection<RiskAssessment> _riskAssessmentsCreated;
	public ObservableCollection<RiskAssessment> RiskAssessmentsCreated
	{
		get => _riskAssessmentsCreated;
		set
		{
			_riskAssessmentsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("RiskAssessmentsCreated", this));
		}
	}

	
	public long? RiskAssessmentsCreatedCount { get; set; }
	private ObservableCollection<RiskAssessmentAnswer> _riskAssessmentAnswersCreated;
	public ObservableCollection<RiskAssessmentAnswer> RiskAssessmentAnswersCreated
	{
		get => _riskAssessmentAnswersCreated;
		set
		{
			_riskAssessmentAnswersCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("RiskAssessmentAnswersCreated", this));
		}
	}

	
	public long? RiskAssessmentAnswersCreatedCount { get; set; }
	private ObservableCollection<RiskAssessmentQuestion> _riskAssessmentQuestionsCreated;
	public ObservableCollection<RiskAssessmentQuestion> RiskAssessmentQuestionsCreated
	{
		get => _riskAssessmentQuestionsCreated;
		set
		{
			_riskAssessmentQuestionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("RiskAssessmentQuestionsCreated", this));
		}
	}

	
	public long? RiskAssessmentQuestionsCreatedCount { get; set; }
	private ObservableCollection<Scaffold> _scaffoldsCreated;
	public ObservableCollection<Scaffold> ScaffoldsCreated
	{
		get => _scaffoldsCreated;
		set
		{
			_scaffoldsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ScaffoldsCreated", this));
		}
	}

	
	public long? ScaffoldsCreatedCount { get; set; }
	private ObservableCollection<ScaffoldInspection> _scaffoldInspectionsCreated;
	public ObservableCollection<ScaffoldInspection> ScaffoldInspectionsCreated
	{
		get => _scaffoldInspectionsCreated;
		set
		{
			_scaffoldInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ScaffoldInspectionsCreated", this));
		}
	}

	
	public long? ScaffoldInspectionsCreatedCount { get; set; }
	private ObservableCollection<ScaffoldLoading> _scaffoldLoadingsCreated;
	public ObservableCollection<ScaffoldLoading> ScaffoldLoadingsCreated
	{
		get => _scaffoldLoadingsCreated;
		set
		{
			_scaffoldLoadingsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ScaffoldLoadingsCreated", this));
		}
	}

	
	public long? ScaffoldLoadingsCreatedCount { get; set; }
	private ObservableCollection<ScaffoldType> _scaffoldTypesCreated;
	public ObservableCollection<ScaffoldType> ScaffoldTypesCreated
	{
		get => _scaffoldTypesCreated;
		set
		{
			_scaffoldTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("ScaffoldTypesCreated", this));
		}
	}

	
	public long? ScaffoldTypesCreatedCount { get; set; }
	private ObservableCollection<Site> _sitesCreated;
	public ObservableCollection<Site> SitesCreated
	{
		get => _sitesCreated;
		set
		{
			_sitesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("SitesCreated", this));
		}
	}

	
	public long? SitesCreatedCount { get; set; }
	private ObservableCollection<SiteInspection> _siteInspectionsCreated;
	public ObservableCollection<SiteInspection> SiteInspectionsCreated
	{
		get => _siteInspectionsCreated;
		set
		{
			_siteInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("SiteInspectionsCreated", this));
		}
	}

	
	public long? SiteInspectionsCreatedCount { get; set; }
	private ObservableCollection<UserSite> _sites;
	public ObservableCollection<UserSite> Sites
	{
		get => _sites;
		set
		{
			_sites = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("Sites", this));
		}
	}

	
	public long? SitesCount { get; set; }
	
	public override EntityValidationResult ValidateEntity() {
		var entity = this;
		var validationResult = new EntityValidationResult(this.GetType());
		return validationResult;
	}
}

