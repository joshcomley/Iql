using Iql.Queryable;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Events;
using System.Collections.ObjectModel;
using System;


public class UserSiteBase : IEntity {
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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
	
	public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
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

	private ObservableCollection<PersonInspection> _personInspections;
	public ObservableCollection<PersonInspection> PersonInspections
	{
		get => _personInspections;
		set
		{
			_personInspections = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<SiteInspection>("PersonInspections", this));
		}
	}

	
	public long? PersonInspectionsCount { get; set; }
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

public class PersonReport : PersonReportBase, IEntity {
	private int _personId;
	public int PersonId
	{
		get => _personId;
		set
		{
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("PersonId", this));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("TypeId", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("CreatedByUserId", this));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Title", this));
		}
	}

	private FaultReportStatus _status;
	public FaultReportStatus Status
	{
		get => _status;
		set
		{
			_status = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Status", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("PersistenceKey", this));
		}
	}

	private ObservableCollection<ReportActionsTaken> _actionsTaken;
	public ObservableCollection<ReportActionsTaken> ActionsTaken
	{
		get => _actionsTaken;
		set
		{
			_actionsTaken = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("ActionsTaken", this));
		}
	}

	
	public long? ActionsTakenCount { get; set; }
	private ObservableCollection<ReportRecommendation> _recommendations;
	public ObservableCollection<ReportRecommendation> Recommendations
	{
		get => _recommendations;
		set
		{
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Recommendations", this));
		}
	}

	
	public long? RecommendationsCount { get; set; }
	private Person _person;
	public Person Person
	{
		get => _person;
		set
		{
			_person = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Person", this));
		}
	}

	private ReportType _type;
	public ReportType Type
	{
		get => _type;
		set
		{
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("Type", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonReport>("CreatedByUser", this));
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
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("PersonId", this));
		}
	}

	private int _typeId;
	public int TypeId
	{
		get => _typeId;
		set
		{
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("TypeId", this));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Notes", this));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Description", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("CreatedDate", this));
		}
	}

	private Person _person;
	public Person Person
	{
		get => _person;
		set
		{
			_person = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Person", this));
		}
	}

	private PersonType _type;
	public PersonType Type
	{
		get => _type;
		set
		{
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonTypeMap>("Type", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("CreatedByUserId", this));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("Title", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("PersistenceKey", this));
		}
	}

	private ObservableCollection<Person> _people;
	public ObservableCollection<Person> People
	{
		get => _people;
		set
		{
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("People", this));
		}
	}

	
	public long? PeopleCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("CreatedByUser", this));
		}
	}

	private ObservableCollection<PersonTypeMap> _peopleMap;
	public ObservableCollection<PersonTypeMap> PeopleMap
	{
		get => _peopleMap;
		set
		{
			_peopleMap = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonType>("PeopleMap", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("Name", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("PersistenceKey", this));
		}
	}

	private ObservableCollection<Person> _people;
	public ObservableCollection<Person> People
	{
		get => _people;
		set
		{
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("People", this));
		}
	}

	
	public long? PeopleCount { get; set; }
	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonLoading>("CreatedByUser", this));
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
			_siteInspectionId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("SiteInspectionId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("CreatedByUserId", this));
		}
	}

	private int _personId;
	public int PersonId
	{
		get => _personId;
		set
		{
			_personId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("PersonId", this));
		}
	}

	private PersonInspectionStatus _inspectionStatus;
	public PersonInspectionStatus InspectionStatus
	{
		get => _inspectionStatus;
		set
		{
			_inspectionStatus = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("InspectionStatus", this));
		}
	}

	private DateTimeOffset _startTime;
	public DateTimeOffset StartTime
	{
		get => _startTime;
		set
		{
			_startTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("StartTime", this));
		}
	}

	private DateTimeOffset _endTime;
	public DateTimeOffset EndTime
	{
		get => _endTime;
		set
		{
			_endTime = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("EndTime", this));
		}
	}

	private InspectionFailReason _reasonForFailure;
	public InspectionFailReason ReasonForFailure
	{
		get => _reasonForFailure;
		set
		{
			_reasonForFailure = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("ReasonForFailure", this));
		}
	}

	private bool _isDesignRequired;
	public bool IsDesignRequired
	{
		get => _isDesignRequired;
		set
		{
			_isDesignRequired = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("IsDesignRequired", this));
		}
	}

	private string _drawingNumber;
	public string DrawingNumber
	{
		get => _drawingNumber;
		set
		{
			_drawingNumber = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("DrawingNumber", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("PersistenceKey", this));
		}
	}

	private SiteInspection _siteInspection;
	public SiteInspection SiteInspection
	{
		get => _siteInspection;
		set
		{
			_siteInspection = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("SiteInspection", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<PersonInspection>("CreatedByUser", this));
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
			_typeId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("TypeId", this));
		}
	}

	private int? _loadingId;
	public int? LoadingId
	{
		get => _loadingId;
		set
		{
			_loadingId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("LoadingId", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("CreatedByUserId", this));
		}
	}

	private string _key;
	public string Key
	{
		get => _key;
		set
		{
			_key = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Key", this));
		}
	}

	private string _title;
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Title", this));
		}
	}

	private string _description;
	public string Description
	{
		get => _description;
		set
		{
			_description = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Description", this));
		}
	}

	private PersonCategory _category;
	public PersonCategory Category
	{
		get => _category;
		set
		{
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Category", this));
		}
	}

	private int? _clientId;
	public int? ClientId
	{
		get => _clientId;
		set
		{
			_clientId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("ClientId", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("PersistenceKey", this));
		}
	}

	private Client _client;
	public Client Client
	{
		get => _client;
		set
		{
			_client = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Client", this));
		}
	}

	private PersonType _type;
	public PersonType Type
	{
		get => _type;
		set
		{
			_type = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Type", this));
		}
	}

	private PersonLoading _loading;
	public PersonLoading Loading
	{
		get => _loading;
		set
		{
			_loading = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Loading", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("CreatedByUser", this));
		}
	}

	private ObservableCollection<PersonTypeMap> _types;
	public ObservableCollection<PersonTypeMap> Types
	{
		get => _types;
		set
		{
			_types = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Types", this));
		}
	}

	
	public long? TypesCount { get; set; }
	private ObservableCollection<PersonReport> _reports;
	public ObservableCollection<PersonReport> Reports
	{
		get => _reports;
		set
		{
			_reports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Person>("Reports", this));
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

public class ReportType : ReportTypeBase, IEntity {
	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Id", this));
		}
	}

	private int _categoryId;
	public int CategoryId
	{
		get => _categoryId;
		set
		{
			_categoryId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("CategoryId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("CreatedByUserId", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("PersistenceKey", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Name", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Version", this));
		}
	}

	private ReportCategory _category;
	public ReportCategory Category
	{
		get => _category;
		set
		{
			_category = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("Category", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("CreatedByUser", this));
		}
	}

	private ObservableCollection<PersonReport> _faultReports;
	public ObservableCollection<PersonReport> FaultReports
	{
		get => _faultReports;
		set
		{
			_faultReports = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportType>("FaultReports", this));
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
			_reportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("ReportId", this));
		}
	}

	private int _recommendationId;
	public int RecommendationId
	{
		get => _recommendationId;
		set
		{
			_recommendationId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("RecommendationId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("CreatedByUserId", this));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Notes", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("PersistenceKey", this));
		}
	}

	private PersonReport _personReport;
	public PersonReport PersonReport
	{
		get => _personReport;
		set
		{
			_personReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("PersonReport", this));
		}
	}

	private ReportDefaultRecommendation _recommendation;
	public ReportDefaultRecommendation Recommendation
	{
		get => _recommendation;
		set
		{
			_recommendation = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("Recommendation", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportRecommendation>("CreatedByUser", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Name", this));
		}
	}

	private string _text;
	public string Text
	{
		get => _text;
		set
		{
			_text = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Text", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("PersistenceKey", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("CreatedByUser", this));
		}
	}

	private ObservableCollection<ReportRecommendation> _recommendations;
	public ObservableCollection<ReportRecommendation> Recommendations
	{
		get => _recommendations;
		set
		{
			_recommendations = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportDefaultRecommendation>("Recommendations", this));
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
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("Id", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("CreatedByUserId", this));
		}
	}

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("Name", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("Guid", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("PersistenceKey", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("CreatedByUser", this));
		}
	}

	private ObservableCollection<ReportType> _reportTypes;
	public ObservableCollection<ReportType> ReportTypes
	{
		get => _reportTypes;
		set
		{
			_reportTypes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportCategory>("ReportTypes", this));
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
			_faultReportId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("FaultReportId", this));
		}
	}

	private string _createdByUserId;
	public string CreatedByUserId
	{
		get => _createdByUserId;
		set
		{
			_createdByUserId = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("CreatedByUserId", this));
		}
	}

	private string _notes;
	public string Notes
	{
		get => _notes;
		set
		{
			_notes = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("Notes", this));
		}
	}

	private Guid _guid;
	public Guid Guid
	{
		get => _guid;
		set
		{
			_guid = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("Guid", this));
		}
	}

	private int _id;
	public int Id
	{
		get => _id;
		set
		{
			_id = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("Id", this));
		}
	}

	private DateTimeOffset _createdDate;
	public DateTimeOffset CreatedDate
	{
		get => _createdDate;
		set
		{
			_createdDate = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("CreatedDate", this));
		}
	}

	private long _version;
	public long Version
	{
		get => _version;
		set
		{
			_version = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("Version", this));
		}
	}

	private Guid _persistenceKey;
	public Guid PersistenceKey
	{
		get => _persistenceKey;
		set
		{
			_persistenceKey = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("PersistenceKey", this));
		}
	}

	private PersonReport _personReport;
	public PersonReport PersonReport
	{
		get => _personReport;
		set
		{
			_personReport = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("PersonReport", this));
		}
	}

	private ApplicationUser _createdByUser;
	public ApplicationUser CreatedByUser
	{
		get => _createdByUser;
		set
		{
			_createdByUser = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ReportActionsTaken>("CreatedByUser", this));
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

	private ObservableCollection<Person> _people;
	public ObservableCollection<Person> People
	{
		get => _people;
		set
		{
			_people = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<Client>("People", this));
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
	private ObservableCollection<ReportActionsTaken> _faultActionsTakenCreated;
	public ObservableCollection<ReportActionsTaken> FaultActionsTakenCreated
	{
		get => _faultActionsTakenCreated;
		set
		{
			_faultActionsTakenCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultActionsTakenCreated", this));
		}
	}

	
	public long? FaultActionsTakenCreatedCount { get; set; }
	private ObservableCollection<ReportCategory> _faultCategoriesCreated;
	public ObservableCollection<ReportCategory> FaultCategoriesCreated
	{
		get => _faultCategoriesCreated;
		set
		{
			_faultCategoriesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultCategoriesCreated", this));
		}
	}

	
	public long? FaultCategoriesCreatedCount { get; set; }
	private ObservableCollection<ReportDefaultRecommendation> _faultDefaultRecommendationsCreated;
	public ObservableCollection<ReportDefaultRecommendation> FaultDefaultRecommendationsCreated
	{
		get => _faultDefaultRecommendationsCreated;
		set
		{
			_faultDefaultRecommendationsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultDefaultRecommendationsCreated", this));
		}
	}

	
	public long? FaultDefaultRecommendationsCreatedCount { get; set; }
	private ObservableCollection<ReportRecommendation> _faultRecommendationsCreated;
	public ObservableCollection<ReportRecommendation> FaultRecommendationsCreated
	{
		get => _faultRecommendationsCreated;
		set
		{
			_faultRecommendationsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultRecommendationsCreated", this));
		}
	}

	
	public long? FaultRecommendationsCreatedCount { get; set; }
	private ObservableCollection<ReportType> _faultTypesCreated;
	public ObservableCollection<ReportType> FaultTypesCreated
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
	private ObservableCollection<Person> _peopleCreated;
	public ObservableCollection<Person> PeopleCreated
	{
		get => _peopleCreated;
		set
		{
			_peopleCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("PeopleCreated", this));
		}
	}

	
	public long? PeopleCreatedCount { get; set; }
	private ObservableCollection<PersonInspection> _personInspectionsCreated;
	public ObservableCollection<PersonInspection> PersonInspectionsCreated
	{
		get => _personInspectionsCreated;
		set
		{
			_personInspectionsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("PersonInspectionsCreated", this));
		}
	}

	
	public long? PersonInspectionsCreatedCount { get; set; }
	private ObservableCollection<PersonLoading> _personLoadingsCreated;
	public ObservableCollection<PersonLoading> PersonLoadingsCreated
	{
		get => _personLoadingsCreated;
		set
		{
			_personLoadingsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("PersonLoadingsCreated", this));
		}
	}

	
	public long? PersonLoadingsCreatedCount { get; set; }
	private ObservableCollection<PersonType> _personTypesCreated;
	public ObservableCollection<PersonType> PersonTypesCreated
	{
		get => _personTypesCreated;
		set
		{
			_personTypesCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("PersonTypesCreated", this));
		}
	}

	
	public long? PersonTypesCreatedCount { get; set; }
	private ObservableCollection<PersonReport> _faultReportsCreated;
	public ObservableCollection<PersonReport> FaultReportsCreated
	{
		get => _faultReportsCreated;
		set
		{
			_faultReportsCreated = value;
			this.PropertyChanged.Emit(new PropertyChangeEvent<ApplicationUser>("FaultReportsCreated", this));
		}
	}

	
	public long? FaultReportsCreatedCount { get; set; }
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

