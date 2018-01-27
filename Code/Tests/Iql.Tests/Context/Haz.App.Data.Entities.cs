using Iql.Queryable;
using Hazception.ApiContext.Base;
using Haz.App.Data.Entities;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Events;
using System;


namespace Haz.App.Data.Entities
{
	public class HazardBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "Hazard";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class ExamCandidateBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "ExamCandidate";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class ExamCandidateResultBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "ExamCandidateResult";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class ExamResultBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "ExamResult";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class ExamManagerBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "ExamManager";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class ExamBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "Exam";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class VideoBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "Video";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class HazClientBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "Client";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class HazApplicationUserBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "ApplicationUser";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public class HazClientTypeBase : IEntity
	{
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		
		public EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; } = new EventEmitter<IPropertyChangeEvent>();
		public virtual bool OnSaving()
		{
			return true;
		}
		public virtual bool OnDeleting()
		{
			return true;
		}
		public static string ClassName()
		{
			return "ClientType";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	}
}


namespace Haz.App.Data.Entities
{
	public enum ExamCandidateStatus
	{
		NotStarted = 0,
		InProgress = 1,
		Completed = 2
	}
}

namespace Haz.App.Data.Entities
{
	public enum ExamStatus
	{
		NotStarted = 0,
		InProgress = 1,
		Completed = 2
	}
}

namespace Haz.App.Data.Entities
{
	public enum HazUserType
	{
		Super = 1,
		Client = 2,
		Candidate = 3
	}
}

namespace Haz.App.Data.Entities
{
	public class Hazard : HazardBase, IEntity
	{
		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Id), this, oldValue, value));
			}
		}

		private int _videoId;
		public int VideoId
		{
			get => _videoId;
			set
			{
				var oldValue = this._videoId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(VideoId), this, oldValue, value));
				_videoId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(VideoId), this, oldValue, value));
			}
		}

		private int _clientId;
		public int ClientId
		{
			get => _clientId;
			set
			{
				var oldValue = this._clientId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(ClientId), this, oldValue, value));
				_clientId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(ClientId), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private Guid _clientGuid;
		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var oldValue = this._clientGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(ClientGuid), this, oldValue, value));
				_clientGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(ClientGuid), this, oldValue, value));
			}
		}

		private Guid _videoGuid;
		public Guid VideoGuid
		{
			get => _videoGuid;
			set
			{
				var oldValue = this._videoGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(VideoGuid), this, oldValue, value));
				_videoGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(VideoGuid), this, oldValue, value));
			}
		}

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				var oldValue = this._title;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Title), this, oldValue, value));
				_title = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Title), this, oldValue, value));
			}
		}

		private string _description;
		public string Description
		{
			get => _description;
			set
			{
				var oldValue = this._description;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Description), this, oldValue, value));
				_description = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Description), this, oldValue, value));
			}
		}

		private double _timeFrom;
		public double TimeFrom
		{
			get => _timeFrom;
			set
			{
				var oldValue = this._timeFrom;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(TimeFrom), this, oldValue, value));
				_timeFrom = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(TimeFrom), this, oldValue, value));
			}
		}

		private double _duration;
		public double Duration
		{
			get => _duration;
			set
			{
				var oldValue = this._duration;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Duration), this, oldValue, value));
				_duration = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Duration), this, oldValue, value));
			}
		}

		private double _left;
		public double Left
		{
			get => _left;
			set
			{
				var oldValue = this._left;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Left), this, oldValue, value));
				_left = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Left), this, oldValue, value));
			}
		}

		private double _top;
		public double Top
		{
			get => _top;
			set
			{
				var oldValue = this._top;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Top), this, oldValue, value));
				_top = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Top), this, oldValue, value));
			}
		}

		private double _width;
		public double Width
		{
			get => _width;
			set
			{
				var oldValue = this._width;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Width), this, oldValue, value));
				_width = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Width), this, oldValue, value));
			}
		}

		private double _height;
		public double Height
		{
			get => _height;
			set
			{
				var oldValue = this._height;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Height), this, oldValue, value));
				_height = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Height), this, oldValue, value));
			}
		}

		private string _revisionKey;
		public string RevisionKey
		{
			get => _revisionKey;
			set
			{
				var oldValue = this._revisionKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(RevisionKey), this, oldValue, value));
				_revisionKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(RevisionKey), this, oldValue, value));
			}
		}

		private string _imageUrl;
		public string ImageUrl
		{
			get => _imageUrl;
			set
			{
				var oldValue = this._imageUrl;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(ImageUrl), this, oldValue, value));
				_imageUrl = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(ImageUrl), this, oldValue, value));
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get => _guid;
			set
			{
				var oldValue = this._guid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Guid), this, oldValue, value));
				_guid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Guid), this, oldValue, value));
			}
		}

		private DateTimeOffset _createdDate;
		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var oldValue = this._createdDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(CreatedDate), this, oldValue, value));
				_createdDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(CreatedDate), this, oldValue, value));
			}
		}

		private long _version;
		public long Version
		{
			get => _version;
			set
			{
				var oldValue = this._version;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Version), this, oldValue, value));
				_version = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Version), this, oldValue, value));
			}
		}

		private Guid _persistenceKey;
		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var oldValue = this._persistenceKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(PersistenceKey), this, oldValue, value));
				_persistenceKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(PersistenceKey), this, oldValue, value));
			}
		}

		
		public Int64 ResultsCount { get; set; }
		private RelatedList<Hazard,ExamResult> _results;
		public RelatedList<Hazard,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<Hazard,ExamResult>(this, nameof(Results));
				return _results;
			}
			set
			{
				var oldValue = this._results;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Results), this, oldValue, value));
				_results = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Results), this, oldValue, value));
			}
		}

		private Video _video;
		public Video Video
		{
			get => _video;
			set
			{
				var oldValue = this._video;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Video), this, oldValue, value));
				_video = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Video), this, oldValue, value));
			}
		}

		private HazClient _client;
		public HazClient Client
		{
			get => _client;
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(Client), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Hazard>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Hazard>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		
		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class ExamCandidate : ExamCandidateBase, IEntity
	{
		private int _examId;
		public int ExamId
		{
			get => _examId;
			set
			{
				var oldValue = this._examId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(ExamId), this, oldValue, value));
				_examId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(ExamId), this, oldValue, value));
			}
		}

		private int _videoId;
		public int VideoId
		{
			get => _videoId;
			set
			{
				var oldValue = this._videoId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(VideoId), this, oldValue, value));
				_videoId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(VideoId), this, oldValue, value));
			}
		}

		private string _candidateId;
		public string CandidateId
		{
			get => _candidateId;
			set
			{
				var oldValue = this._candidateId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CandidateId), this, oldValue, value));
				_candidateId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CandidateId), this, oldValue, value));
			}
		}

		private int _clientId;
		public int ClientId
		{
			get => _clientId;
			set
			{
				var oldValue = this._clientId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(ClientId), this, oldValue, value));
				_clientId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(ClientId), this, oldValue, value));
			}
		}

		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Id), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private double _lastTime;
		public double LastTime
		{
			get => _lastTime;
			set
			{
				var oldValue = this._lastTime;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(LastTime), this, oldValue, value));
				_lastTime = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(LastTime), this, oldValue, value));
			}
		}

		private Guid _clientGuid;
		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var oldValue = this._clientGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(ClientGuid), this, oldValue, value));
				_clientGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(ClientGuid), this, oldValue, value));
			}
		}

		private ExamCandidateStatus _status;
		public ExamCandidateStatus Status
		{
			get => _status;
			set
			{
				var oldValue = this._status;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Status), this, oldValue, value));
				_status = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Status), this, oldValue, value));
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get => _guid;
			set
			{
				var oldValue = this._guid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Guid), this, oldValue, value));
				_guid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Guid), this, oldValue, value));
			}
		}

		private DateTimeOffset _createdDate;
		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var oldValue = this._createdDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CreatedDate), this, oldValue, value));
				_createdDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CreatedDate), this, oldValue, value));
			}
		}

		private long _version;
		public long Version
		{
			get => _version;
			set
			{
				var oldValue = this._version;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Version), this, oldValue, value));
				_version = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Version), this, oldValue, value));
			}
		}

		private Guid _persistenceKey;
		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var oldValue = this._persistenceKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(PersistenceKey), this, oldValue, value));
				_persistenceKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(PersistenceKey), this, oldValue, value));
			}
		}

		
		public Int64 ResultsCount { get; set; }
		private RelatedList<ExamCandidate,ExamResult> _results;
		public RelatedList<ExamCandidate,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<ExamCandidate,ExamResult>(this, nameof(Results));
				return _results;
			}
			set
			{
				var oldValue = this._results;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Results), this, oldValue, value));
				_results = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Results), this, oldValue, value));
			}
		}

		
		public Int64 CandidateResultsCount { get; set; }
		private RelatedList<ExamCandidate,ExamCandidateResult> _candidateResults;
		public RelatedList<ExamCandidate,ExamCandidateResult> CandidateResults
		{
			get
			{
				this._candidateResults = this._candidateResults ?? new RelatedList<ExamCandidate,ExamCandidateResult>(this, nameof(CandidateResults));
				return _candidateResults;
			}
			set
			{
				var oldValue = this._candidateResults;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CandidateResults), this, oldValue, value));
				_candidateResults = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CandidateResults), this, oldValue, value));
			}
		}

		private Exam _exam;
		public Exam Exam
		{
			get => _exam;
			set
			{
				var oldValue = this._exam;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Exam), this, oldValue, value));
				_exam = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Exam), this, oldValue, value));
			}
		}

		private Video _video;
		public Video Video
		{
			get => _video;
			set
			{
				var oldValue = this._video;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Video), this, oldValue, value));
				_video = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Video), this, oldValue, value));
			}
		}

		private HazApplicationUser _candidate;
		public HazApplicationUser Candidate
		{
			get => _candidate;
			set
			{
				var oldValue = this._candidate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Candidate), this, oldValue, value));
				_candidate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Candidate), this, oldValue, value));
			}
		}

		private HazClient _client;
		public HazClient Client
		{
			get => _client;
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(Client), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidate>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		
		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class ExamCandidateResult : ExamCandidateResultBase, IEntity
	{
		private int _videoId;
		public int VideoId
		{
			get => _videoId;
			set
			{
				var oldValue = this._videoId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(VideoId), this, oldValue, value));
				_videoId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(VideoId), this, oldValue, value));
			}
		}

		private int _examId;
		public int ExamId
		{
			get => _examId;
			set
			{
				var oldValue = this._examId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamId), this, oldValue, value));
				_examId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamId), this, oldValue, value));
			}
		}

		private int _clientId;
		public int ClientId
		{
			get => _clientId;
			set
			{
				var oldValue = this._clientId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ClientId), this, oldValue, value));
				_clientId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ClientId), this, oldValue, value));
			}
		}

		private int _examCandidateId;
		public int ExamCandidateId
		{
			get => _examCandidateId;
			set
			{
				var oldValue = this._examCandidateId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamCandidateId), this, oldValue, value));
				_examCandidateId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamCandidateId), this, oldValue, value));
			}
		}

		private string _candidateId;
		public string CandidateId
		{
			get => _candidateId;
			set
			{
				var oldValue = this._candidateId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(CandidateId), this, oldValue, value));
				_candidateId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(CandidateId), this, oldValue, value));
			}
		}

		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Id), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private Guid _clientGuid;
		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var oldValue = this._clientGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ClientGuid), this, oldValue, value));
				_clientGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ClientGuid), this, oldValue, value));
			}
		}

		private double _score;
		public double Score
		{
			get => _score;
			set
			{
				var oldValue = this._score;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Score), this, oldValue, value));
				_score = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Score), this, oldValue, value));
			}
		}

		private bool _pass;
		public bool Pass
		{
			get => _pass;
			set
			{
				var oldValue = this._pass;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Pass), this, oldValue, value));
				_pass = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Pass), this, oldValue, value));
			}
		}

		private string _clickData;
		public string ClickData
		{
			get => _clickData;
			set
			{
				var oldValue = this._clickData;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ClickData), this, oldValue, value));
				_clickData = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ClickData), this, oldValue, value));
			}
		}

		private int _clickCount;
		public int ClickCount
		{
			get => _clickCount;
			set
			{
				var oldValue = this._clickCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ClickCount), this, oldValue, value));
				_clickCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ClickCount), this, oldValue, value));
			}
		}

		private int _hazardCount;
		public int HazardCount
		{
			get => _hazardCount;
			set
			{
				var oldValue = this._hazardCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(HazardCount), this, oldValue, value));
				_hazardCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(HazardCount), this, oldValue, value));
			}
		}

		private int _successCount;
		public int SuccessCount
		{
			get => _successCount;
			set
			{
				var oldValue = this._successCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(SuccessCount), this, oldValue, value));
				_successCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(SuccessCount), this, oldValue, value));
			}
		}

		private DateTimeOffset _date;
		public DateTimeOffset Date
		{
			get => _date;
			set
			{
				var oldValue = this._date;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Date), this, oldValue, value));
				_date = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Date), this, oldValue, value));
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get => _guid;
			set
			{
				var oldValue = this._guid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Guid), this, oldValue, value));
				_guid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Guid), this, oldValue, value));
			}
		}

		private DateTimeOffset _createdDate;
		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var oldValue = this._createdDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedDate), this, oldValue, value));
				_createdDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedDate), this, oldValue, value));
			}
		}

		private long _version;
		public long Version
		{
			get => _version;
			set
			{
				var oldValue = this._version;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Version), this, oldValue, value));
				_version = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Version), this, oldValue, value));
			}
		}

		private Guid _persistenceKey;
		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var oldValue = this._persistenceKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(PersistenceKey), this, oldValue, value));
				_persistenceKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(PersistenceKey), this, oldValue, value));
			}
		}

		
		public Int64 ResultsCount { get; set; }
		private RelatedList<ExamCandidateResult,ExamResult> _results;
		public RelatedList<ExamCandidateResult,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<ExamCandidateResult,ExamResult>(this, nameof(Results));
				return _results;
			}
			set
			{
				var oldValue = this._results;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Results), this, oldValue, value));
				_results = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Results), this, oldValue, value));
			}
		}

		private Video _video;
		public Video Video
		{
			get => _video;
			set
			{
				var oldValue = this._video;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Video), this, oldValue, value));
				_video = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Video), this, oldValue, value));
			}
		}

		private Exam _exam;
		public Exam Exam
		{
			get => _exam;
			set
			{
				var oldValue = this._exam;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Exam), this, oldValue, value));
				_exam = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Exam), this, oldValue, value));
			}
		}

		private HazClient _client;
		public HazClient Client
		{
			get => _client;
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Client), this, oldValue, value));
			}
		}

		private ExamCandidate _examCandidate;
		public ExamCandidate ExamCandidate
		{
			get => _examCandidate;
			set
			{
				var oldValue = this._examCandidate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamCandidate), this, oldValue, value));
				_examCandidate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamCandidate), this, oldValue, value));
			}
		}

		private HazApplicationUser _candidate;
		public HazApplicationUser Candidate
		{
			get => _candidate;
			set
			{
				var oldValue = this._candidate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Candidate), this, oldValue, value));
				_candidate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(Candidate), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class ExamResult : ExamResultBase, IEntity
	{
		private int _examId;
		public int ExamId
		{
			get => _examId;
			set
			{
				var oldValue = this._examId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(ExamId), this, oldValue, value));
				_examId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(ExamId), this, oldValue, value));
			}
		}

		private int _videoId;
		public int VideoId
		{
			get => _videoId;
			set
			{
				var oldValue = this._videoId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(VideoId), this, oldValue, value));
				_videoId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(VideoId), this, oldValue, value));
			}
		}

		private int _clientId;
		public int ClientId
		{
			get => _clientId;
			set
			{
				var oldValue = this._clientId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(ClientId), this, oldValue, value));
				_clientId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(ClientId), this, oldValue, value));
			}
		}

		private string _candidateId;
		public string CandidateId
		{
			get => _candidateId;
			set
			{
				var oldValue = this._candidateId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(CandidateId), this, oldValue, value));
				_candidateId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(CandidateId), this, oldValue, value));
			}
		}

		private int _candidateResultId;
		public int CandidateResultId
		{
			get => _candidateResultId;
			set
			{
				var oldValue = this._candidateResultId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(CandidateResultId), this, oldValue, value));
				_candidateResultId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(CandidateResultId), this, oldValue, value));
			}
		}

		private int _hazardId;
		public int HazardId
		{
			get => _hazardId;
			set
			{
				var oldValue = this._hazardId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(HazardId), this, oldValue, value));
				_hazardId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(HazardId), this, oldValue, value));
			}
		}

		private int _examCandidateId;
		public int ExamCandidateId
		{
			get => _examCandidateId;
			set
			{
				var oldValue = this._examCandidateId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(ExamCandidateId), this, oldValue, value));
				_examCandidateId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(ExamCandidateId), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private bool _success;
		public bool Success
		{
			get => _success;
			set
			{
				var oldValue = this._success;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Success), this, oldValue, value));
				_success = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Success), this, oldValue, value));
			}
		}

		private Guid _clientGuid;
		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var oldValue = this._clientGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(ClientGuid), this, oldValue, value));
				_clientGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(ClientGuid), this, oldValue, value));
			}
		}

		private double _x;
		public double X
		{
			get => _x;
			set
			{
				var oldValue = this._x;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(X), this, oldValue, value));
				_x = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(X), this, oldValue, value));
			}
		}

		private double _y;
		public double Y
		{
			get => _y;
			set
			{
				var oldValue = this._y;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Y), this, oldValue, value));
				_y = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Y), this, oldValue, value));
			}
		}

		private double _time;
		public double Time
		{
			get => _time;
			set
			{
				var oldValue = this._time;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Time), this, oldValue, value));
				_time = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Time), this, oldValue, value));
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get => _guid;
			set
			{
				var oldValue = this._guid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Guid), this, oldValue, value));
				_guid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Guid), this, oldValue, value));
			}
		}

		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Id), this, oldValue, value));
			}
		}

		private DateTimeOffset _createdDate;
		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var oldValue = this._createdDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(CreatedDate), this, oldValue, value));
				_createdDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(CreatedDate), this, oldValue, value));
			}
		}

		private long _version;
		public long Version
		{
			get => _version;
			set
			{
				var oldValue = this._version;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Version), this, oldValue, value));
				_version = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Version), this, oldValue, value));
			}
		}

		private Guid _persistenceKey;
		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var oldValue = this._persistenceKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(PersistenceKey), this, oldValue, value));
				_persistenceKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(PersistenceKey), this, oldValue, value));
			}
		}

		private Exam _exam;
		public Exam Exam
		{
			get => _exam;
			set
			{
				var oldValue = this._exam;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Exam), this, oldValue, value));
				_exam = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Exam), this, oldValue, value));
			}
		}

		private Video _video;
		public Video Video
		{
			get => _video;
			set
			{
				var oldValue = this._video;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Video), this, oldValue, value));
				_video = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Video), this, oldValue, value));
			}
		}

		private HazClient _client;
		public HazClient Client
		{
			get => _client;
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Client), this, oldValue, value));
			}
		}

		private HazApplicationUser _candidate;
		public HazApplicationUser Candidate
		{
			get => _candidate;
			set
			{
				var oldValue = this._candidate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Candidate), this, oldValue, value));
				_candidate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Candidate), this, oldValue, value));
			}
		}

		private ExamCandidateResult _candidateResult;
		public ExamCandidateResult CandidateResult
		{
			get => _candidateResult;
			set
			{
				var oldValue = this._candidateResult;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(CandidateResult), this, oldValue, value));
				_candidateResult = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(CandidateResult), this, oldValue, value));
			}
		}

		private Hazard _hazard;
		public Hazard Hazard
		{
			get => _hazard;
			set
			{
				var oldValue = this._hazard;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(Hazard), this, oldValue, value));
				_hazard = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(Hazard), this, oldValue, value));
			}
		}

		private ExamCandidate _examCandidate;
		public ExamCandidate ExamCandidate
		{
			get => _examCandidate;
			set
			{
				var oldValue = this._examCandidate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(ExamCandidate), this, oldValue, value));
				_examCandidate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(ExamCandidate), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamResult>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamResult>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class ExamManager : ExamManagerBase, IEntity
	{
		private int _examId;
		public int ExamId
		{
			get => _examId;
			set
			{
				var oldValue = this._examId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(ExamId), this, oldValue, value));
				_examId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(ExamId), this, oldValue, value));
			}
		}

		private int _clientId;
		public int ClientId
		{
			get => _clientId;
			set
			{
				var oldValue = this._clientId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(ClientId), this, oldValue, value));
				_clientId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(ClientId), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private Guid _clientGuid;
		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var oldValue = this._clientGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(ClientGuid), this, oldValue, value));
				_clientGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(ClientGuid), this, oldValue, value));
			}
		}

		private string _managerId;
		public string ManagerId
		{
			get => _managerId;
			set
			{
				var oldValue = this._managerId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(ManagerId), this, oldValue, value));
				_managerId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(ManagerId), this, oldValue, value));
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get => _guid;
			set
			{
				var oldValue = this._guid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(Guid), this, oldValue, value));
				_guid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(Guid), this, oldValue, value));
			}
		}

		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(Id), this, oldValue, value));
			}
		}

		private DateTimeOffset _createdDate;
		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var oldValue = this._createdDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(CreatedDate), this, oldValue, value));
				_createdDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(CreatedDate), this, oldValue, value));
			}
		}

		private long _version;
		public long Version
		{
			get => _version;
			set
			{
				var oldValue = this._version;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(Version), this, oldValue, value));
				_version = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(Version), this, oldValue, value));
			}
		}

		private Guid _persistenceKey;
		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var oldValue = this._persistenceKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(PersistenceKey), this, oldValue, value));
				_persistenceKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(PersistenceKey), this, oldValue, value));
			}
		}

		private Exam _exam;
		public Exam Exam
		{
			get => _exam;
			set
			{
				var oldValue = this._exam;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(Exam), this, oldValue, value));
				_exam = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(Exam), this, oldValue, value));
			}
		}

		private HazClient _client;
		public HazClient Client
		{
			get => _client;
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(Client), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		private HazApplicationUser _manager;
		public HazApplicationUser Manager
		{
			get => _manager;
			set
			{
				var oldValue = this._manager;
				this.PropertyChanging.Emit(new PropertyChangeEvent<ExamManager>(nameof(Manager), this, oldValue, value));
				_manager = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<ExamManager>(nameof(Manager), this, oldValue, value));
			}
		}

		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class Exam : ExamBase, IEntity
	{
		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Id), this, oldValue, value));
			}
		}

		private int _videoId;
		public int VideoId
		{
			get => _videoId;
			set
			{
				var oldValue = this._videoId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(VideoId), this, oldValue, value));
				_videoId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(VideoId), this, oldValue, value));
			}
		}

		private int _clientId;
		public int ClientId
		{
			get => _clientId;
			set
			{
				var oldValue = this._clientId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(ClientId), this, oldValue, value));
				_clientId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(ClientId), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private Guid _videoGuid;
		public Guid VideoGuid
		{
			get => _videoGuid;
			set
			{
				var oldValue = this._videoGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(VideoGuid), this, oldValue, value));
				_videoGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(VideoGuid), this, oldValue, value));
			}
		}

		private Guid _clientGuid;
		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var oldValue = this._clientGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(ClientGuid), this, oldValue, value));
				_clientGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(ClientGuid), this, oldValue, value));
			}
		}

		private bool _isTraining;
		public bool IsTraining
		{
			get => _isTraining;
			set
			{
				var oldValue = this._isTraining;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(IsTraining), this, oldValue, value));
				_isTraining = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(IsTraining), this, oldValue, value));
			}
		}

		private bool _availableToAllUsers;
		public bool AvailableToAllUsers
		{
			get => _availableToAllUsers;
			set
			{
				var oldValue = this._availableToAllUsers;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(AvailableToAllUsers), this, oldValue, value));
				_availableToAllUsers = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(AvailableToAllUsers), this, oldValue, value));
			}
		}

		private DateTimeOffset? _scheduledDate;
		public DateTimeOffset? ScheduledDate
		{
			get => _scheduledDate;
			set
			{
				var oldValue = this._scheduledDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(ScheduledDate), this, oldValue, value));
				_scheduledDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(ScheduledDate), this, oldValue, value));
			}
		}

		private int _passMark;
		public int PassMark
		{
			get => _passMark;
			set
			{
				var oldValue = this._passMark;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(PassMark), this, oldValue, value));
				_passMark = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(PassMark), this, oldValue, value));
			}
		}

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				var oldValue = this._title;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Title), this, oldValue, value));
				_title = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Title), this, oldValue, value));
			}
		}

		private bool _allowAnyArea;
		public bool AllowAnyArea
		{
			get => _allowAnyArea;
			set
			{
				var oldValue = this._allowAnyArea;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(AllowAnyArea), this, oldValue, value));
				_allowAnyArea = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(AllowAnyArea), this, oldValue, value));
			}
		}

		private string _description;
		public string Description
		{
			get => _description;
			set
			{
				var oldValue = this._description;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Description), this, oldValue, value));
				_description = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Description), this, oldValue, value));
			}
		}

		private ExamStatus _status;
		public ExamStatus Status
		{
			get => _status;
			set
			{
				var oldValue = this._status;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Status), this, oldValue, value));
				_status = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Status), this, oldValue, value));
			}
		}

		private bool _notStarted;
		public bool NotStarted
		{
			get => _notStarted;
			set
			{
				var oldValue = this._notStarted;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(NotStarted), this, oldValue, value));
				_notStarted = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(NotStarted), this, oldValue, value));
			}
		}

		private bool _complete;
		public bool Complete
		{
			get => _complete;
			set
			{
				var oldValue = this._complete;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Complete), this, oldValue, value));
				_complete = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Complete), this, oldValue, value));
			}
		}

		private bool _inProgress;
		public bool InProgress
		{
			get => _inProgress;
			set
			{
				var oldValue = this._inProgress;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(InProgress), this, oldValue, value));
				_inProgress = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(InProgress), this, oldValue, value));
			}
		}

		private int _candidateCount;
		public int CandidateCount
		{
			get => _candidateCount;
			set
			{
				var oldValue = this._candidateCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(CandidateCount), this, oldValue, value));
				_candidateCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(CandidateCount), this, oldValue, value));
			}
		}

		private string _imageUrl;
		public string ImageUrl
		{
			get => _imageUrl;
			set
			{
				var oldValue = this._imageUrl;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(ImageUrl), this, oldValue, value));
				_imageUrl = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(ImageUrl), this, oldValue, value));
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get => _guid;
			set
			{
				var oldValue = this._guid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Guid), this, oldValue, value));
				_guid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Guid), this, oldValue, value));
			}
		}

		private DateTimeOffset _createdDate;
		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var oldValue = this._createdDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(CreatedDate), this, oldValue, value));
				_createdDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(CreatedDate), this, oldValue, value));
			}
		}

		private long _version;
		public long Version
		{
			get => _version;
			set
			{
				var oldValue = this._version;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Version), this, oldValue, value));
				_version = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Version), this, oldValue, value));
			}
		}

		private Guid _persistenceKey;
		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var oldValue = this._persistenceKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(PersistenceKey), this, oldValue, value));
				_persistenceKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(PersistenceKey), this, oldValue, value));
			}
		}

		private Video _video;
		public Video Video
		{
			get => _video;
			set
			{
				var oldValue = this._video;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Video), this, oldValue, value));
				_video = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Video), this, oldValue, value));
			}
		}

		private HazClient _client;
		public HazClient Client
		{
			get => _client;
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Client), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		
		public Int64 ManagersCount { get; set; }
		private RelatedList<Exam,ExamManager> _managers;
		public RelatedList<Exam,ExamManager> Managers
		{
			get
			{
				this._managers = this._managers ?? new RelatedList<Exam,ExamManager>(this, nameof(Managers));
				return _managers;
			}
			set
			{
				var oldValue = this._managers;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Managers), this, oldValue, value));
				_managers = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Managers), this, oldValue, value));
			}
		}

		
		public Int64 ResultsCount { get; set; }
		private RelatedList<Exam,ExamResult> _results;
		public RelatedList<Exam,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<Exam,ExamResult>(this, nameof(Results));
				return _results;
			}
			set
			{
				var oldValue = this._results;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Results), this, oldValue, value));
				_results = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Results), this, oldValue, value));
			}
		}

		
		public Int64 CandidateResultsCount { get; set; }
		private RelatedList<Exam,ExamCandidateResult> _candidateResults;
		public RelatedList<Exam,ExamCandidateResult> CandidateResults
		{
			get
			{
				this._candidateResults = this._candidateResults ?? new RelatedList<Exam,ExamCandidateResult>(this, nameof(CandidateResults));
				return _candidateResults;
			}
			set
			{
				var oldValue = this._candidateResults;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(CandidateResults), this, oldValue, value));
				_candidateResults = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(CandidateResults), this, oldValue, value));
			}
		}

		
		public Int64 CandidatesCount { get; set; }
		private RelatedList<Exam,ExamCandidate> _candidates;
		public RelatedList<Exam,ExamCandidate> Candidates
		{
			get
			{
				this._candidates = this._candidates ?? new RelatedList<Exam,ExamCandidate>(this, nameof(Candidates));
				return _candidates;
			}
			set
			{
				var oldValue = this._candidates;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Exam>(nameof(Candidates), this, oldValue, value));
				_candidates = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Exam>(nameof(Candidates), this, oldValue, value));
			}
		}

		
		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class Video : VideoBase, IEntity
	{
		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Id), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private int _clientId;
		public int ClientId
		{
			get => _clientId;
			set
			{
				var oldValue = this._clientId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(ClientId), this, oldValue, value));
				_clientId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(ClientId), this, oldValue, value));
			}
		}

		private Guid _clientGuid;
		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var oldValue = this._clientGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(ClientGuid), this, oldValue, value));
				_clientGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(ClientGuid), this, oldValue, value));
			}
		}

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				var oldValue = this._title;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Title), this, oldValue, value));
				_title = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Title), this, oldValue, value));
			}
		}

		private string _description;
		public string Description
		{
			get => _description;
			set
			{
				var oldValue = this._description;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Description), this, oldValue, value));
				_description = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Description), this, oldValue, value));
			}
		}

		private double _duration;
		public double Duration
		{
			get => _duration;
			set
			{
				var oldValue = this._duration;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Duration), this, oldValue, value));
				_duration = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Duration), this, oldValue, value));
			}
		}

		private int _resultsCount;
		public int ResultsCount
		{
			get => _resultsCount;
			set
			{
				var oldValue = this._resultsCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(ResultsCount), this, oldValue, value));
				_resultsCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(ResultsCount), this, oldValue, value));
			}
		}

		private int _candidateResultsCount;
		public int CandidateResultsCount
		{
			get => _candidateResultsCount;
			set
			{
				var oldValue = this._candidateResultsCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(CandidateResultsCount), this, oldValue, value));
				_candidateResultsCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(CandidateResultsCount), this, oldValue, value));
			}
		}

		private int _candidatesCount;
		public int CandidatesCount
		{
			get => _candidatesCount;
			set
			{
				var oldValue = this._candidatesCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(CandidatesCount), this, oldValue, value));
				_candidatesCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(CandidatesCount), this, oldValue, value));
			}
		}

		private int _examCount;
		public int ExamCount
		{
			get => _examCount;
			set
			{
				var oldValue = this._examCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(ExamCount), this, oldValue, value));
				_examCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(ExamCount), this, oldValue, value));
			}
		}

		private int _hazardCount;
		public int HazardCount
		{
			get => _hazardCount;
			set
			{
				var oldValue = this._hazardCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(HazardCount), this, oldValue, value));
				_hazardCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(HazardCount), this, oldValue, value));
			}
		}

		private string _revisionKey;
		public string RevisionKey
		{
			get => _revisionKey;
			set
			{
				var oldValue = this._revisionKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(RevisionKey), this, oldValue, value));
				_revisionKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(RevisionKey), this, oldValue, value));
			}
		}

		private string _screenshotUrl;
		public string ScreenshotUrl
		{
			get => _screenshotUrl;
			set
			{
				var oldValue = this._screenshotUrl;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(ScreenshotUrl), this, oldValue, value));
				_screenshotUrl = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(ScreenshotUrl), this, oldValue, value));
			}
		}

		private string _screenshotMiniUrl;
		public string ScreenshotMiniUrl
		{
			get => _screenshotMiniUrl;
			set
			{
				var oldValue = this._screenshotMiniUrl;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(ScreenshotMiniUrl), this, oldValue, value));
				_screenshotMiniUrl = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(ScreenshotMiniUrl), this, oldValue, value));
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get => _guid;
			set
			{
				var oldValue = this._guid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Guid), this, oldValue, value));
				_guid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Guid), this, oldValue, value));
			}
		}

		private DateTimeOffset _createdDate;
		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var oldValue = this._createdDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(CreatedDate), this, oldValue, value));
				_createdDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(CreatedDate), this, oldValue, value));
			}
		}

		private long _version;
		public long Version
		{
			get => _version;
			set
			{
				var oldValue = this._version;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Version), this, oldValue, value));
				_version = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Version), this, oldValue, value));
			}
		}

		private Guid _persistenceKey;
		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var oldValue = this._persistenceKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(PersistenceKey), this, oldValue, value));
				_persistenceKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(PersistenceKey), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		private HazClient _client;
		public HazClient Client
		{
			get => _client;
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Client), this, oldValue, value));
			}
		}

		
		public Int64 ExamsCount { get; set; }
		private RelatedList<Video,Exam> _exams;
		public RelatedList<Video,Exam> Exams
		{
			get
			{
				this._exams = this._exams ?? new RelatedList<Video,Exam>(this, nameof(Exams));
				return _exams;
			}
			set
			{
				var oldValue = this._exams;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Exams), this, oldValue, value));
				_exams = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Exams), this, oldValue, value));
			}
		}

		private RelatedList<Video,ExamResult> _results;
		public RelatedList<Video,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<Video,ExamResult>(this, nameof(Results));
				return _results;
			}
			set
			{
				var oldValue = this._results;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Results), this, oldValue, value));
				_results = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Results), this, oldValue, value));
			}
		}

		private RelatedList<Video,ExamCandidateResult> _candidateResults;
		public RelatedList<Video,ExamCandidateResult> CandidateResults
		{
			get
			{
				this._candidateResults = this._candidateResults ?? new RelatedList<Video,ExamCandidateResult>(this, nameof(CandidateResults));
				return _candidateResults;
			}
			set
			{
				var oldValue = this._candidateResults;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(CandidateResults), this, oldValue, value));
				_candidateResults = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(CandidateResults), this, oldValue, value));
			}
		}

		private RelatedList<Video,ExamCandidate> _candidates;
		public RelatedList<Video,ExamCandidate> Candidates
		{
			get
			{
				this._candidates = this._candidates ?? new RelatedList<Video,ExamCandidate>(this, nameof(Candidates));
				return _candidates;
			}
			set
			{
				var oldValue = this._candidates;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Candidates), this, oldValue, value));
				_candidates = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Candidates), this, oldValue, value));
			}
		}

		
		public Int64 HazardsCount { get; set; }
		private RelatedList<Video,Hazard> _hazards;
		public RelatedList<Video,Hazard> Hazards
		{
			get
			{
				this._hazards = this._hazards ?? new RelatedList<Video,Hazard>(this, nameof(Hazards));
				return _hazards;
			}
			set
			{
				var oldValue = this._hazards;
				this.PropertyChanging.Emit(new PropertyChangeEvent<Video>(nameof(Hazards), this, oldValue, value));
				_hazards = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<Video>(nameof(Hazards), this, oldValue, value));
			}
		}

		
		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class HazClient : HazClientBase, IEntity
	{
		private int _typeId;
		public int TypeId
		{
			get => _typeId;
			set
			{
				var oldValue = this._typeId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(TypeId), this, oldValue, value));
				_typeId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(TypeId), this, oldValue, value));
			}
		}

		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Id), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				var oldValue = this._name;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Name), this, oldValue, value));
				_name = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Name), this, oldValue, value));
			}
		}

		private string _description;
		public string Description
		{
			get => _description;
			set
			{
				var oldValue = this._description;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Description), this, oldValue, value));
				_description = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Description), this, oldValue, value));
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get => _guid;
			set
			{
				var oldValue = this._guid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Guid), this, oldValue, value));
				_guid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Guid), this, oldValue, value));
			}
		}

		private DateTimeOffset _createdDate;
		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var oldValue = this._createdDate;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(CreatedDate), this, oldValue, value));
				_createdDate = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(CreatedDate), this, oldValue, value));
			}
		}

		private long _version;
		public long Version
		{
			get => _version;
			set
			{
				var oldValue = this._version;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Version), this, oldValue, value));
				_version = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Version), this, oldValue, value));
			}
		}

		private Guid _persistenceKey;
		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var oldValue = this._persistenceKey;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(PersistenceKey), this, oldValue, value));
				_persistenceKey = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(PersistenceKey), this, oldValue, value));
			}
		}

		
		public Int64 UsersCount { get; set; }
		private RelatedList<HazClient,HazApplicationUser> _users;
		public RelatedList<HazClient,HazApplicationUser> Users
		{
			get
			{
				this._users = this._users ?? new RelatedList<HazClient,HazApplicationUser>(this, nameof(Users));
				return _users;
			}
			set
			{
				var oldValue = this._users;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Users), this, oldValue, value));
				_users = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Users), this, oldValue, value));
			}
		}

		private HazClientType _type;
		public HazClientType Type
		{
			get => _type;
			set
			{
				var oldValue = this._type;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Type), this, oldValue, value));
				_type = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Type), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		
		public Int64 VideosCount { get; set; }
		private RelatedList<HazClient,Video> _videos;
		public RelatedList<HazClient,Video> Videos
		{
			get
			{
				this._videos = this._videos ?? new RelatedList<HazClient,Video>(this, nameof(Videos));
				return _videos;
			}
			set
			{
				var oldValue = this._videos;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Videos), this, oldValue, value));
				_videos = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Videos), this, oldValue, value));
			}
		}

		
		public Int64 ExamsCount { get; set; }
		private RelatedList<HazClient,Exam> _exams;
		public RelatedList<HazClient,Exam> Exams
		{
			get
			{
				this._exams = this._exams ?? new RelatedList<HazClient,Exam>(this, nameof(Exams));
				return _exams;
			}
			set
			{
				var oldValue = this._exams;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Exams), this, oldValue, value));
				_exams = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Exams), this, oldValue, value));
			}
		}

		
		public Int64 ExamManagersCount { get; set; }
		private RelatedList<HazClient,ExamManager> _examManagers;
		public RelatedList<HazClient,ExamManager> ExamManagers
		{
			get
			{
				this._examManagers = this._examManagers ?? new RelatedList<HazClient,ExamManager>(this, nameof(ExamManagers));
				return _examManagers;
			}
			set
			{
				var oldValue = this._examManagers;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(ExamManagers), this, oldValue, value));
				_examManagers = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(ExamManagers), this, oldValue, value));
			}
		}

		
		public Int64 ExamResultsCount { get; set; }
		private RelatedList<HazClient,ExamResult> _examResults;
		public RelatedList<HazClient,ExamResult> ExamResults
		{
			get
			{
				this._examResults = this._examResults ?? new RelatedList<HazClient,ExamResult>(this, nameof(ExamResults));
				return _examResults;
			}
			set
			{
				var oldValue = this._examResults;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(ExamResults), this, oldValue, value));
				_examResults = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(ExamResults), this, oldValue, value));
			}
		}

		
		public Int64 ExamCandidateResultsCount { get; set; }
		private RelatedList<HazClient,ExamCandidateResult> _examCandidateResults;
		public RelatedList<HazClient,ExamCandidateResult> ExamCandidateResults
		{
			get
			{
				this._examCandidateResults = this._examCandidateResults ?? new RelatedList<HazClient,ExamCandidateResult>(this, nameof(ExamCandidateResults));
				return _examCandidateResults;
			}
			set
			{
				var oldValue = this._examCandidateResults;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(ExamCandidateResults), this, oldValue, value));
				_examCandidateResults = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(ExamCandidateResults), this, oldValue, value));
			}
		}

		
		public Int64 ExamCandidatesCount { get; set; }
		private RelatedList<HazClient,ExamCandidate> _examCandidates;
		public RelatedList<HazClient,ExamCandidate> ExamCandidates
		{
			get
			{
				this._examCandidates = this._examCandidates ?? new RelatedList<HazClient,ExamCandidate>(this, nameof(ExamCandidates));
				return _examCandidates;
			}
			set
			{
				var oldValue = this._examCandidates;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(ExamCandidates), this, oldValue, value));
				_examCandidates = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(ExamCandidates), this, oldValue, value));
			}
		}

		
		public Int64 HazardsCount { get; set; }
		private RelatedList<HazClient,Hazard> _hazards;
		public RelatedList<HazClient,Hazard> Hazards
		{
			get
			{
				this._hazards = this._hazards ?? new RelatedList<HazClient,Hazard>(this, nameof(Hazards));
				return _hazards;
			}
			set
			{
				var oldValue = this._hazards;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClient>(nameof(Hazards), this, oldValue, value));
				_hazards = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClient>(nameof(Hazards), this, oldValue, value));
			}
		}

		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class HazApplicationUser : HazApplicationUserBase, IEntity
	{
		private string _id;
		public string Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Id), this, oldValue, value));
			}
		}

		private Guid? _clientGuid;
		public Guid? ClientGuid
		{
			get => _clientGuid;
			set
			{
				var oldValue = this._clientGuid;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ClientGuid), this, oldValue, value));
				_clientGuid = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ClientGuid), this, oldValue, value));
			}
		}

		private int? _clientId;
		public int? ClientId
		{
			get => _clientId;
			set
			{
				var oldValue = this._clientId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ClientId), this, oldValue, value));
				_clientId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ClientId), this, oldValue, value));
			}
		}

		private string _createdByUserId;
		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var oldValue = this._createdByUserId;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(CreatedByUserId), this, oldValue, value));
				_createdByUserId = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(CreatedByUserId), this, oldValue, value));
			}
		}

		private string _email;
		public string Email
		{
			get => _email;
			set
			{
				var oldValue = this._email;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Email), this, oldValue, value));
				_email = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Email), this, oldValue, value));
			}
		}

		private HazUserType _userType;
		public HazUserType UserType
		{
			get => _userType;
			set
			{
				var oldValue = this._userType;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(UserType), this, oldValue, value));
				_userType = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(UserType), this, oldValue, value));
			}
		}

		private string _fullName;
		public string FullName
		{
			get => _fullName;
			set
			{
				var oldValue = this._fullName;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(FullName), this, oldValue, value));
				_fullName = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(FullName), this, oldValue, value));
			}
		}

		private bool _isLockedOut;
		public bool IsLockedOut
		{
			get => _isLockedOut;
			set
			{
				var oldValue = this._isLockedOut;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(IsLockedOut), this, oldValue, value));
				_isLockedOut = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(IsLockedOut), this, oldValue, value));
			}
		}

		private string _userName;
		public string UserName
		{
			get => _userName;
			set
			{
				var oldValue = this._userName;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(UserName), this, oldValue, value));
				_userName = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(UserName), this, oldValue, value));
			}
		}

		private string _normalizedUserName;
		public string NormalizedUserName
		{
			get => _normalizedUserName;
			set
			{
				var oldValue = this._normalizedUserName;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(NormalizedUserName), this, oldValue, value));
				_normalizedUserName = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(NormalizedUserName), this, oldValue, value));
			}
		}

		private string _normalizedEmail;
		public string NormalizedEmail
		{
			get => _normalizedEmail;
			set
			{
				var oldValue = this._normalizedEmail;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(NormalizedEmail), this, oldValue, value));
				_normalizedEmail = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(NormalizedEmail), this, oldValue, value));
			}
		}

		private bool _emailConfirmed;
		public bool EmailConfirmed
		{
			get => _emailConfirmed;
			set
			{
				var oldValue = this._emailConfirmed;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(EmailConfirmed), this, oldValue, value));
				_emailConfirmed = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(EmailConfirmed), this, oldValue, value));
			}
		}

		private string _passwordHash;
		public string PasswordHash
		{
			get => _passwordHash;
			set
			{
				var oldValue = this._passwordHash;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(PasswordHash), this, oldValue, value));
				_passwordHash = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(PasswordHash), this, oldValue, value));
			}
		}

		private string _securityStamp;
		public string SecurityStamp
		{
			get => _securityStamp;
			set
			{
				var oldValue = this._securityStamp;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(SecurityStamp), this, oldValue, value));
				_securityStamp = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(SecurityStamp), this, oldValue, value));
			}
		}

		private string _concurrencyStamp;
		public string ConcurrencyStamp
		{
			get => _concurrencyStamp;
			set
			{
				var oldValue = this._concurrencyStamp;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ConcurrencyStamp), this, oldValue, value));
				_concurrencyStamp = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ConcurrencyStamp), this, oldValue, value));
			}
		}

		private string _phoneNumber;
		public string PhoneNumber
		{
			get => _phoneNumber;
			set
			{
				var oldValue = this._phoneNumber;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(PhoneNumber), this, oldValue, value));
				_phoneNumber = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(PhoneNumber), this, oldValue, value));
			}
		}

		private bool _phoneNumberConfirmed;
		public bool PhoneNumberConfirmed
		{
			get => _phoneNumberConfirmed;
			set
			{
				var oldValue = this._phoneNumberConfirmed;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(PhoneNumberConfirmed), this, oldValue, value));
				_phoneNumberConfirmed = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(PhoneNumberConfirmed), this, oldValue, value));
			}
		}

		private bool _twoFactorEnabled;
		public bool TwoFactorEnabled
		{
			get => _twoFactorEnabled;
			set
			{
				var oldValue = this._twoFactorEnabled;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(TwoFactorEnabled), this, oldValue, value));
				_twoFactorEnabled = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(TwoFactorEnabled), this, oldValue, value));
			}
		}

		private DateTimeOffset? _lockoutEnd;
		public DateTimeOffset? LockoutEnd
		{
			get => _lockoutEnd;
			set
			{
				var oldValue = this._lockoutEnd;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(LockoutEnd), this, oldValue, value));
				_lockoutEnd = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(LockoutEnd), this, oldValue, value));
			}
		}

		private bool _lockoutEnabled;
		public bool LockoutEnabled
		{
			get => _lockoutEnabled;
			set
			{
				var oldValue = this._lockoutEnabled;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(LockoutEnabled), this, oldValue, value));
				_lockoutEnabled = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(LockoutEnabled), this, oldValue, value));
			}
		}

		private int _accessFailedCount;
		public int AccessFailedCount
		{
			get => _accessFailedCount;
			set
			{
				var oldValue = this._accessFailedCount;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(AccessFailedCount), this, oldValue, value));
				_accessFailedCount = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(AccessFailedCount), this, oldValue, value));
			}
		}

		private HazClient _client;
		public HazClient Client
		{
			get => _client;
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Client), this, oldValue, value));
			}
		}

		private HazApplicationUser _createdByUser;
		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var oldValue = this._createdByUser;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(CreatedByUser), this, oldValue, value));
				_createdByUser = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(CreatedByUser), this, oldValue, value));
			}
		}

		
		public Int64 UsersCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,HazApplicationUser> _usersCreated;
		public RelatedList<HazApplicationUser,HazApplicationUser> UsersCreated
		{
			get
			{
				this._usersCreated = this._usersCreated ?? new RelatedList<HazApplicationUser,HazApplicationUser>(this, nameof(UsersCreated));
				return _usersCreated;
			}
			set
			{
				var oldValue = this._usersCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(UsersCreated), this, oldValue, value));
				_usersCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(UsersCreated), this, oldValue, value));
			}
		}

		
		public Int64 ClientsCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,HazClient> _clientsCreated;
		public RelatedList<HazApplicationUser,HazClient> ClientsCreated
		{
			get
			{
				this._clientsCreated = this._clientsCreated ?? new RelatedList<HazApplicationUser,HazClient>(this, nameof(ClientsCreated));
				return _clientsCreated;
			}
			set
			{
				var oldValue = this._clientsCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ClientsCreated), this, oldValue, value));
				_clientsCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ClientsCreated), this, oldValue, value));
			}
		}

		
		public Int64 VideosCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,Video> _videosCreated;
		public RelatedList<HazApplicationUser,Video> VideosCreated
		{
			get
			{
				this._videosCreated = this._videosCreated ?? new RelatedList<HazApplicationUser,Video>(this, nameof(VideosCreated));
				return _videosCreated;
			}
			set
			{
				var oldValue = this._videosCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(VideosCreated), this, oldValue, value));
				_videosCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(VideosCreated), this, oldValue, value));
			}
		}

		
		public Int64 ExamsCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,Exam> _examsCreated;
		public RelatedList<HazApplicationUser,Exam> ExamsCreated
		{
			get
			{
				this._examsCreated = this._examsCreated ?? new RelatedList<HazApplicationUser,Exam>(this, nameof(ExamsCreated));
				return _examsCreated;
			}
			set
			{
				var oldValue = this._examsCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamsCreated), this, oldValue, value));
				_examsCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamsCreated), this, oldValue, value));
			}
		}

		
		public Int64 ExamManagersCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,ExamManager> _examManagersCreated;
		public RelatedList<HazApplicationUser,ExamManager> ExamManagersCreated
		{
			get
			{
				this._examManagersCreated = this._examManagersCreated ?? new RelatedList<HazApplicationUser,ExamManager>(this, nameof(ExamManagersCreated));
				return _examManagersCreated;
			}
			set
			{
				var oldValue = this._examManagersCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamManagersCreated), this, oldValue, value));
				_examManagersCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamManagersCreated), this, oldValue, value));
			}
		}

		
		public Int64 ResultsCount { get; set; }
		private RelatedList<HazApplicationUser,ExamResult> _results;
		public RelatedList<HazApplicationUser,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<HazApplicationUser,ExamResult>(this, nameof(Results));
				return _results;
			}
			set
			{
				var oldValue = this._results;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Results), this, oldValue, value));
				_results = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Results), this, oldValue, value));
			}
		}

		
		public Int64 ExamResultsCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,ExamResult> _examResultsCreated;
		public RelatedList<HazApplicationUser,ExamResult> ExamResultsCreated
		{
			get
			{
				this._examResultsCreated = this._examResultsCreated ?? new RelatedList<HazApplicationUser,ExamResult>(this, nameof(ExamResultsCreated));
				return _examResultsCreated;
			}
			set
			{
				var oldValue = this._examResultsCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamResultsCreated), this, oldValue, value));
				_examResultsCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamResultsCreated), this, oldValue, value));
			}
		}

		
		public Int64 ExamResultsCount { get; set; }
		private RelatedList<HazApplicationUser,ExamCandidateResult> _examResults;
		public RelatedList<HazApplicationUser,ExamCandidateResult> ExamResults
		{
			get
			{
				this._examResults = this._examResults ?? new RelatedList<HazApplicationUser,ExamCandidateResult>(this, nameof(ExamResults));
				return _examResults;
			}
			set
			{
				var oldValue = this._examResults;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamResults), this, oldValue, value));
				_examResults = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamResults), this, oldValue, value));
			}
		}

		
		public Int64 ExamCandidateResultsCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,ExamCandidateResult> _examCandidateResultsCreated;
		public RelatedList<HazApplicationUser,ExamCandidateResult> ExamCandidateResultsCreated
		{
			get
			{
				this._examCandidateResultsCreated = this._examCandidateResultsCreated ?? new RelatedList<HazApplicationUser,ExamCandidateResult>(this, nameof(ExamCandidateResultsCreated));
				return _examCandidateResultsCreated;
			}
			set
			{
				var oldValue = this._examCandidateResultsCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamCandidateResultsCreated), this, oldValue, value));
				_examCandidateResultsCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamCandidateResultsCreated), this, oldValue, value));
			}
		}

		
		public Int64 ExamsCount { get; set; }
		private RelatedList<HazApplicationUser,ExamCandidate> _exams;
		public RelatedList<HazApplicationUser,ExamCandidate> Exams
		{
			get
			{
				this._exams = this._exams ?? new RelatedList<HazApplicationUser,ExamCandidate>(this, nameof(Exams));
				return _exams;
			}
			set
			{
				var oldValue = this._exams;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Exams), this, oldValue, value));
				_exams = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(Exams), this, oldValue, value));
			}
		}

		
		public Int64 ExamCandidatesCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,ExamCandidate> _examCandidatesCreated;
		public RelatedList<HazApplicationUser,ExamCandidate> ExamCandidatesCreated
		{
			get
			{
				this._examCandidatesCreated = this._examCandidatesCreated ?? new RelatedList<HazApplicationUser,ExamCandidate>(this, nameof(ExamCandidatesCreated));
				return _examCandidatesCreated;
			}
			set
			{
				var oldValue = this._examCandidatesCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamCandidatesCreated), this, oldValue, value));
				_examCandidatesCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(ExamCandidatesCreated), this, oldValue, value));
			}
		}

		
		public Int64 HazardsCreatedCount { get; set; }
		private RelatedList<HazApplicationUser,Hazard> _hazardsCreated;
		public RelatedList<HazApplicationUser,Hazard> HazardsCreated
		{
			get
			{
				this._hazardsCreated = this._hazardsCreated ?? new RelatedList<HazApplicationUser,Hazard>(this, nameof(HazardsCreated));
				return _hazardsCreated;
			}
			set
			{
				var oldValue = this._hazardsCreated;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(HazardsCreated), this, oldValue, value));
				_hazardsCreated = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazApplicationUser>(nameof(HazardsCreated), this, oldValue, value));
			}
		}

		
		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

namespace Haz.App.Data.Entities
{
	public class HazClientType : HazClientTypeBase, IEntity
	{
		private int _id;
		public int Id
		{
			get => _id;
			set
			{
				var oldValue = this._id;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClientType>(nameof(Id), this, oldValue, value));
				_id = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClientType>(nameof(Id), this, oldValue, value));
			}
		}

		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				var oldValue = this._name;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClientType>(nameof(Name), this, oldValue, value));
				_name = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClientType>(nameof(Name), this, oldValue, value));
			}
		}

		
		public Int64 ClientCount { get; set; }
		private RelatedList<HazClientType,HazClient> _client;
		public RelatedList<HazClientType,HazClient> Client
		{
			get
			{
				this._client = this._client ?? new RelatedList<HazClientType,HazClient>(this, nameof(Client));
				return _client;
			}
			set
			{
				var oldValue = this._client;
				this.PropertyChanging.Emit(new PropertyChangeEvent<HazClientType>(nameof(Client), this, oldValue, value));
				_client = value;
				this.PropertyChanged.Emit(new PropertyChangeEvent<HazClientType>(nameof(Client), this, oldValue, value));
			}
		}

		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			return validationResult;
		}
	}
}

