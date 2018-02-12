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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
			return "HazClient";
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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
			return "HazApplicationUser";
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
		
		protected Boolean _propertyChangingSet;
		
		protected Boolean _propertyChangedSet;
				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanged;

		public EventEmitter<IPropertyChangeEvent> PropertyChanged
		{
			get => _propertyChanged;
			set
			{
				_propertyChanged = value;
				this._propertyChangedSet = value != null;
				
			}
		}

				
		protected EventEmitter<IPropertyChangeEvent> _propertyChanging;

		public EventEmitter<IPropertyChangeEvent> PropertyChanging
		{
			get => _propertyChanging;
			set
			{
				_propertyChanging = value;
				this._propertyChangingSet = value != null;
				
			}
		}

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
			return "HazClientType";
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
				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int? _clonedFromId;

		public int? ClonedFromId
		{
			get => _clonedFromId;
			set
			{
				var changedSet = false;
				var oldValue = _clonedFromId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clonedFromId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClonedFromId), this, oldValue, value));
					}
				
				}
				_clonedFromId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clonedFromId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClonedFromId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _videoId;

		public int VideoId
		{
			get => _videoId;
			set
			{
				var changedSet = false;
				var oldValue = _videoId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videoId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(VideoId), this, oldValue, value));
					}
				
				}
				_videoId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videoId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(VideoId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _clientId;

		public int ClientId
		{
			get => _clientId;
			set
			{
				var changedSet = false;
				var oldValue = _clientId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClientId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _clientGuid;

		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var changedSet = false;
				var oldValue = _clientGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
				_clientGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _videoGuid;

		public Guid VideoGuid
		{
			get => _videoGuid;
			set
			{
				var changedSet = false;
				var oldValue = _videoGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videoGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(VideoGuid), this, oldValue, value));
					}
				
				}
				_videoGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videoGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(VideoGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _title;

		public string Title
		{
			get => _title;
			set
			{
				var changedSet = false;
				var oldValue = _title;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _title != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Title), this, oldValue, value));
					}
				
				}
				_title = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _title != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Title), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _description;

		public string Description
		{
			get => _description;
			set
			{
				var changedSet = false;
				var oldValue = _description;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _description != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Description), this, oldValue, value));
					}
				
				}
				_description = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _description != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Description), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _timeFrom;

		public double TimeFrom
		{
			get => _timeFrom;
			set
			{
				var changedSet = false;
				var oldValue = _timeFrom;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _timeFrom != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(TimeFrom), this, oldValue, value));
					}
				
				}
				_timeFrom = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _timeFrom != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(TimeFrom), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _duration;

		public double Duration
		{
			get => _duration;
			set
			{
				var changedSet = false;
				var oldValue = _duration;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _duration != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Duration), this, oldValue, value));
					}
				
				}
				_duration = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _duration != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Duration), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _left;

		public double Left
		{
			get => _left;
			set
			{
				var changedSet = false;
				var oldValue = _left;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _left != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Left), this, oldValue, value));
					}
				
				}
				_left = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _left != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Left), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _top;

		public double Top
		{
			get => _top;
			set
			{
				var changedSet = false;
				var oldValue = _top;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _top != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Top), this, oldValue, value));
					}
				
				}
				_top = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _top != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Top), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _width;

		public double Width
		{
			get => _width;
			set
			{
				var changedSet = false;
				var oldValue = _width;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _width != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Width), this, oldValue, value));
					}
				
				}
				_width = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _width != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Width), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _height;

		public double Height
		{
			get => _height;
			set
			{
				var changedSet = false;
				var oldValue = _height;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _height != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Height), this, oldValue, value));
					}
				
				}
				_height = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _height != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Height), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _revisionKey;

		public string RevisionKey
		{
			get => _revisionKey;
			set
			{
				var changedSet = false;
				var oldValue = _revisionKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _revisionKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(RevisionKey), this, oldValue, value));
					}
				
				}
				_revisionKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _revisionKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(RevisionKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _imageUrl;

		public string ImageUrl
		{
			get => _imageUrl;
			set
			{
				var changedSet = false;
				var oldValue = _imageUrl;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _imageUrl != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ImageUrl), this, oldValue, value));
					}
				
				}
				_imageUrl = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _imageUrl != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ImageUrl), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _guid;

		public Guid Guid
		{
			get => _guid;
			set
			{
				var changedSet = false;
				var oldValue = _guid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _guid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _guid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Guid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _createdDate;

		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var changedSet = false;
				var oldValue = _createdDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected long _version;

		public long Version
		{
			get => _version;
			set
			{
				var changedSet = false;
				var oldValue = _version;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _version != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _version != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _persistenceKey;

		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var changedSet = false;
				var oldValue = _persistenceKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _persistenceKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _persistenceKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ResultsCount { get; set; }
				
		protected RelatedList<Hazard,ExamResult> _results;

		public RelatedList<Hazard,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<Hazard,ExamResult>(this, nameof(Results));
				
				return _results;
			}
			set
			{
				var changedSet = false;
				var oldValue = _results;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _results != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Results), this, oldValue, value));
					}
				
				}
				_results = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _results != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Results), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Hazard _clonedFrom;

		public Hazard ClonedFrom
		{
			get => _clonedFrom;
			set
			{
				var changedSet = false;
				var oldValue = _clonedFrom;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clonedFrom != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClonedFrom), this, oldValue, value));
					}
				
				}
				_clonedFrom = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clonedFrom != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClonedFrom), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ClonedToCount { get; set; }
				
		protected RelatedList<Hazard,Hazard> _clonedTo;

		public RelatedList<Hazard,Hazard> ClonedTo
		{
			get
			{
				this._clonedTo = this._clonedTo ?? new RelatedList<Hazard,Hazard>(this, nameof(ClonedTo));
				
				return _clonedTo;
			}
			set
			{
				var changedSet = false;
				var oldValue = _clonedTo;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clonedTo != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClonedTo), this, oldValue, value));
					}
				
				}
				_clonedTo = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clonedTo != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(ClonedTo), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Video _video;

		public Video Video
		{
			get => _video;
			set
			{
				var changedSet = false;
				var oldValue = _video;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _video != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Video), this, oldValue, value));
					}
				
				}
				_video = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _video != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Video), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClient _client;

		public HazClient Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Hazard>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Hazard>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
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
				
		protected int _examId;

		public int ExamId
		{
			get => _examId;
			set
			{
				var changedSet = false;
				var oldValue = _examId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(ExamId), this, oldValue, value));
					}
				
				}
				_examId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(ExamId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _videoId;

		public int VideoId
		{
			get => _videoId;
			set
			{
				var changedSet = false;
				var oldValue = _videoId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videoId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(VideoId), this, oldValue, value));
					}
				
				}
				_videoId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videoId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(VideoId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _candidateId;

		public string CandidateId
		{
			get => _candidateId;
			set
			{
				var changedSet = false;
				var oldValue = _candidateId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CandidateId), this, oldValue, value));
					}
				
				}
				_candidateId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CandidateId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _clientId;

		public int ClientId
		{
			get => _clientId;
			set
			{
				var changedSet = false;
				var oldValue = _clientId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(ClientId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _lastTime;

		public double LastTime
		{
			get => _lastTime;
			set
			{
				var changedSet = false;
				var oldValue = _lastTime;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _lastTime != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(LastTime), this, oldValue, value));
					}
				
				}
				_lastTime = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _lastTime != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(LastTime), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _clientGuid;

		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var changedSet = false;
				var oldValue = _clientGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
				_clientGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ExamCandidateStatus _status;

		public ExamCandidateStatus Status
		{
			get => _status;
			set
			{
				var changedSet = false;
				var oldValue = _status;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _status != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Status), this, oldValue, value));
					}
				
				}
				_status = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _status != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Status), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset? _dateLastTaken;

		public DateTimeOffset? DateLastTaken
		{
			get => _dateLastTaken;
			set
			{
				var changedSet = false;
				var oldValue = _dateLastTaken;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _dateLastTaken != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(DateLastTaken), this, oldValue, value));
					}
				
				}
				_dateLastTaken = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _dateLastTaken != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(DateLastTaken), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _guid;

		public Guid Guid
		{
			get => _guid;
			set
			{
				var changedSet = false;
				var oldValue = _guid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _guid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _guid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Guid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _createdDate;

		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var changedSet = false;
				var oldValue = _createdDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected long _version;

		public long Version
		{
			get => _version;
			set
			{
				var changedSet = false;
				var oldValue = _version;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _version != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _version != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _persistenceKey;

		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var changedSet = false;
				var oldValue = _persistenceKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _persistenceKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _persistenceKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ResultsCount { get; set; }
				
		protected RelatedList<ExamCandidate,ExamResult> _results;

		public RelatedList<ExamCandidate,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<ExamCandidate,ExamResult>(this, nameof(Results));
				
				return _results;
			}
			set
			{
				var changedSet = false;
				var oldValue = _results;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _results != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Results), this, oldValue, value));
					}
				
				}
				_results = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _results != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Results), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 CandidateResultsCount { get; set; }
				
		protected RelatedList<ExamCandidate,ExamCandidateResult> _candidateResults;

		public RelatedList<ExamCandidate,ExamCandidateResult> CandidateResults
		{
			get
			{
				this._candidateResults = this._candidateResults ?? new RelatedList<ExamCandidate,ExamCandidateResult>(this, nameof(CandidateResults));
				
				return _candidateResults;
			}
			set
			{
				var changedSet = false;
				var oldValue = _candidateResults;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateResults != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CandidateResults), this, oldValue, value));
					}
				
				}
				_candidateResults = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateResults != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CandidateResults), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Exam _exam;

		public Exam Exam
		{
			get => _exam;
			set
			{
				var changedSet = false;
				var oldValue = _exam;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _exam != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Exam), this, oldValue, value));
					}
				
				}
				_exam = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _exam != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Exam), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Video _video;

		public Video Video
		{
			get => _video;
			set
			{
				var changedSet = false;
				var oldValue = _video;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _video != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Video), this, oldValue, value));
					}
				
				}
				_video = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _video != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Video), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _candidate;

		public HazApplicationUser Candidate
		{
			get => _candidate;
			set
			{
				var changedSet = false;
				var oldValue = _candidate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Candidate), this, oldValue, value));
					}
				
				}
				_candidate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Candidate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClient _client;

		public HazClient Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidate>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
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
				
		protected int _videoId;

		public int VideoId
		{
			get => _videoId;
			set
			{
				var changedSet = false;
				var oldValue = _videoId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videoId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(VideoId), this, oldValue, value));
					}
				
				}
				_videoId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videoId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(VideoId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _examId;

		public int ExamId
		{
			get => _examId;
			set
			{
				var changedSet = false;
				var oldValue = _examId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamId), this, oldValue, value));
					}
				
				}
				_examId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _clientId;

		public int ClientId
		{
			get => _clientId;
			set
			{
				var changedSet = false;
				var oldValue = _clientId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ClientId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _examCandidateId;

		public int ExamCandidateId
		{
			get => _examCandidateId;
			set
			{
				var changedSet = false;
				var oldValue = _examCandidateId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCandidateId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamCandidateId), this, oldValue, value));
					}
				
				}
				_examCandidateId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCandidateId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamCandidateId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _candidateId;

		public string CandidateId
		{
			get => _candidateId;
			set
			{
				var changedSet = false;
				var oldValue = _candidateId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(CandidateId), this, oldValue, value));
					}
				
				}
				_candidateId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(CandidateId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _clientGuid;

		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var changedSet = false;
				var oldValue = _clientGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
				_clientGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _score;

		public double Score
		{
			get => _score;
			set
			{
				var changedSet = false;
				var oldValue = _score;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _score != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Score), this, oldValue, value));
					}
				
				}
				_score = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _score != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Score), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _pass;

		public bool Pass
		{
			get => _pass;
			set
			{
				var changedSet = false;
				var oldValue = _pass;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _pass != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Pass), this, oldValue, value));
					}
				
				}
				_pass = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _pass != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Pass), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _clickData;

		public string ClickData
		{
			get => _clickData;
			set
			{
				var changedSet = false;
				var oldValue = _clickData;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clickData != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ClickData), this, oldValue, value));
					}
				
				}
				_clickData = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clickData != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ClickData), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _clickCount;

		public int ClickCount
		{
			get => _clickCount;
			set
			{
				var changedSet = false;
				var oldValue = _clickCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clickCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ClickCount), this, oldValue, value));
					}
				
				}
				_clickCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clickCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ClickCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _hazardCount;

		public int HazardCount
		{
			get => _hazardCount;
			set
			{
				var changedSet = false;
				var oldValue = _hazardCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _hazardCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(HazardCount), this, oldValue, value));
					}
				
				}
				_hazardCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _hazardCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(HazardCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _successCount;

		public int SuccessCount
		{
			get => _successCount;
			set
			{
				var changedSet = false;
				var oldValue = _successCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _successCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(SuccessCount), this, oldValue, value));
					}
				
				}
				_successCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _successCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(SuccessCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _date;

		public DateTimeOffset Date
		{
			get => _date;
			set
			{
				var changedSet = false;
				var oldValue = _date;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _date != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Date), this, oldValue, value));
					}
				
				}
				_date = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _date != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Date), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _guid;

		public Guid Guid
		{
			get => _guid;
			set
			{
				var changedSet = false;
				var oldValue = _guid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _guid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _guid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Guid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _createdDate;

		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var changedSet = false;
				var oldValue = _createdDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected long _version;

		public long Version
		{
			get => _version;
			set
			{
				var changedSet = false;
				var oldValue = _version;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _version != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _version != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _persistenceKey;

		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var changedSet = false;
				var oldValue = _persistenceKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _persistenceKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _persistenceKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ResultsCount { get; set; }
				
		protected RelatedList<ExamCandidateResult,ExamResult> _results;

		public RelatedList<ExamCandidateResult,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<ExamCandidateResult,ExamResult>(this, nameof(Results));
				
				return _results;
			}
			set
			{
				var changedSet = false;
				var oldValue = _results;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _results != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Results), this, oldValue, value));
					}
				
				}
				_results = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _results != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Results), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Video _video;

		public Video Video
		{
			get => _video;
			set
			{
				var changedSet = false;
				var oldValue = _video;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _video != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Video), this, oldValue, value));
					}
				
				}
				_video = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _video != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Video), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Exam _exam;

		public Exam Exam
		{
			get => _exam;
			set
			{
				var changedSet = false;
				var oldValue = _exam;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _exam != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Exam), this, oldValue, value));
					}
				
				}
				_exam = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _exam != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Exam), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClient _client;

		public HazClient Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ExamCandidate _examCandidate;

		public ExamCandidate ExamCandidate
		{
			get => _examCandidate;
			set
			{
				var changedSet = false;
				var oldValue = _examCandidate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCandidate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamCandidate), this, oldValue, value));
					}
				
				}
				_examCandidate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCandidate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(ExamCandidate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _candidate;

		public HazApplicationUser Candidate
		{
			get => _candidate;
			set
			{
				var changedSet = false;
				var oldValue = _candidate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Candidate), this, oldValue, value));
					}
				
				}
				_candidate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(Candidate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamCandidateResult>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
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
				
		protected int _examId;

		public int ExamId
		{
			get => _examId;
			set
			{
				var changedSet = false;
				var oldValue = _examId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ExamId), this, oldValue, value));
					}
				
				}
				_examId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ExamId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _videoId;

		public int VideoId
		{
			get => _videoId;
			set
			{
				var changedSet = false;
				var oldValue = _videoId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videoId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(VideoId), this, oldValue, value));
					}
				
				}
				_videoId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videoId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(VideoId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _clientId;

		public int ClientId
		{
			get => _clientId;
			set
			{
				var changedSet = false;
				var oldValue = _clientId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ClientId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _candidateId;

		public string CandidateId
		{
			get => _candidateId;
			set
			{
				var changedSet = false;
				var oldValue = _candidateId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CandidateId), this, oldValue, value));
					}
				
				}
				_candidateId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CandidateId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _candidateResultId;

		public int CandidateResultId
		{
			get => _candidateResultId;
			set
			{
				var changedSet = false;
				var oldValue = _candidateResultId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateResultId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CandidateResultId), this, oldValue, value));
					}
				
				}
				_candidateResultId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateResultId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CandidateResultId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _hazardId;

		public int HazardId
		{
			get => _hazardId;
			set
			{
				var changedSet = false;
				var oldValue = _hazardId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _hazardId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(HazardId), this, oldValue, value));
					}
				
				}
				_hazardId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _hazardId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(HazardId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _examCandidateId;

		public int ExamCandidateId
		{
			get => _examCandidateId;
			set
			{
				var changedSet = false;
				var oldValue = _examCandidateId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCandidateId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ExamCandidateId), this, oldValue, value));
					}
				
				}
				_examCandidateId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCandidateId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ExamCandidateId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _success;

		public bool Success
		{
			get => _success;
			set
			{
				var changedSet = false;
				var oldValue = _success;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _success != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Success), this, oldValue, value));
					}
				
				}
				_success = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _success != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Success), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _clientGuid;

		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var changedSet = false;
				var oldValue = _clientGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
				_clientGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _x;

		public double X
		{
			get => _x;
			set
			{
				var changedSet = false;
				var oldValue = _x;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _x != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(X), this, oldValue, value));
					}
				
				}
				_x = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _x != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(X), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _y;

		public double Y
		{
			get => _y;
			set
			{
				var changedSet = false;
				var oldValue = _y;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _y != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Y), this, oldValue, value));
					}
				
				}
				_y = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _y != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Y), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _time;

		public double Time
		{
			get => _time;
			set
			{
				var changedSet = false;
				var oldValue = _time;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _time != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Time), this, oldValue, value));
					}
				
				}
				_time = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _time != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Time), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _guid;

		public Guid Guid
		{
			get => _guid;
			set
			{
				var changedSet = false;
				var oldValue = _guid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _guid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _guid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Guid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _createdDate;

		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var changedSet = false;
				var oldValue = _createdDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected long _version;

		public long Version
		{
			get => _version;
			set
			{
				var changedSet = false;
				var oldValue = _version;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _version != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _version != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _persistenceKey;

		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var changedSet = false;
				var oldValue = _persistenceKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _persistenceKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _persistenceKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Exam _exam;

		public Exam Exam
		{
			get => _exam;
			set
			{
				var changedSet = false;
				var oldValue = _exam;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _exam != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Exam), this, oldValue, value));
					}
				
				}
				_exam = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _exam != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Exam), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Video _video;

		public Video Video
		{
			get => _video;
			set
			{
				var changedSet = false;
				var oldValue = _video;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _video != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Video), this, oldValue, value));
					}
				
				}
				_video = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _video != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Video), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClient _client;

		public HazClient Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _candidate;

		public HazApplicationUser Candidate
		{
			get => _candidate;
			set
			{
				var changedSet = false;
				var oldValue = _candidate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Candidate), this, oldValue, value));
					}
				
				}
				_candidate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Candidate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ExamCandidateResult _candidateResult;

		public ExamCandidateResult CandidateResult
		{
			get => _candidateResult;
			set
			{
				var changedSet = false;
				var oldValue = _candidateResult;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateResult != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CandidateResult), this, oldValue, value));
					}
				
				}
				_candidateResult = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateResult != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CandidateResult), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Hazard _hazard;

		public Hazard Hazard
		{
			get => _hazard;
			set
			{
				var changedSet = false;
				var oldValue = _hazard;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _hazard != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Hazard), this, oldValue, value));
					}
				
				}
				_hazard = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _hazard != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(Hazard), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ExamCandidate _examCandidate;

		public ExamCandidate ExamCandidate
		{
			get => _examCandidate;
			set
			{
				var changedSet = false;
				var oldValue = _examCandidate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCandidate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ExamCandidate), this, oldValue, value));
					}
				
				}
				_examCandidate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCandidate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(ExamCandidate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamResult>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
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
				
		protected int _examId;

		public int ExamId
		{
			get => _examId;
			set
			{
				var changedSet = false;
				var oldValue = _examId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(ExamId), this, oldValue, value));
					}
				
				}
				_examId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(ExamId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _clientId;

		public int ClientId
		{
			get => _clientId;
			set
			{
				var changedSet = false;
				var oldValue = _clientId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(ClientId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _clientGuid;

		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var changedSet = false;
				var oldValue = _clientGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
				_clientGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _managerId;

		public string ManagerId
		{
			get => _managerId;
			set
			{
				var changedSet = false;
				var oldValue = _managerId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _managerId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(ManagerId), this, oldValue, value));
					}
				
				}
				_managerId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _managerId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(ManagerId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _guid;

		public Guid Guid
		{
			get => _guid;
			set
			{
				var changedSet = false;
				var oldValue = _guid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _guid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _guid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Guid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _createdDate;

		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var changedSet = false;
				var oldValue = _createdDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected long _version;

		public long Version
		{
			get => _version;
			set
			{
				var changedSet = false;
				var oldValue = _version;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _version != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _version != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _persistenceKey;

		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var changedSet = false;
				var oldValue = _persistenceKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _persistenceKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _persistenceKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Exam _exam;

		public Exam Exam
		{
			get => _exam;
			set
			{
				var changedSet = false;
				var oldValue = _exam;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _exam != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Exam), this, oldValue, value));
					}
				
				}
				_exam = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _exam != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Exam), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClient _client;

		public HazClient Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _manager;

		public HazApplicationUser Manager
		{
			get => _manager;
			set
			{
				var changedSet = false;
				var oldValue = _manager;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _manager != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Manager), this, oldValue, value));
					}
				
				}
				_manager = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _manager != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ExamManager>(nameof(Manager), this, oldValue, value));
					}
				
				}
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
				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _videoId;

		public int VideoId
		{
			get => _videoId;
			set
			{
				var changedSet = false;
				var oldValue = _videoId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videoId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(VideoId), this, oldValue, value));
					}
				
				}
				_videoId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videoId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(VideoId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _clientId;

		public int ClientId
		{
			get => _clientId;
			set
			{
				var changedSet = false;
				var oldValue = _clientId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(ClientId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _videoGuid;

		public Guid VideoGuid
		{
			get => _videoGuid;
			set
			{
				var changedSet = false;
				var oldValue = _videoGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videoGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(VideoGuid), this, oldValue, value));
					}
				
				}
				_videoGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videoGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(VideoGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _clientGuid;

		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var changedSet = false;
				var oldValue = _clientGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
				_clientGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _isTraining;

		public bool IsTraining
		{
			get => _isTraining;
			set
			{
				var changedSet = false;
				var oldValue = _isTraining;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _isTraining != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(IsTraining), this, oldValue, value));
					}
				
				}
				_isTraining = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _isTraining != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(IsTraining), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _availableToAllUsers;

		public bool AvailableToAllUsers
		{
			get => _availableToAllUsers;
			set
			{
				var changedSet = false;
				var oldValue = _availableToAllUsers;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _availableToAllUsers != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(AvailableToAllUsers), this, oldValue, value));
					}
				
				}
				_availableToAllUsers = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _availableToAllUsers != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(AvailableToAllUsers), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset? _scheduledDate;

		public DateTimeOffset? ScheduledDate
		{
			get => _scheduledDate;
			set
			{
				var changedSet = false;
				var oldValue = _scheduledDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _scheduledDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(ScheduledDate), this, oldValue, value));
					}
				
				}
				_scheduledDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _scheduledDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(ScheduledDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _passMark;

		public int PassMark
		{
			get => _passMark;
			set
			{
				var changedSet = false;
				var oldValue = _passMark;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _passMark != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(PassMark), this, oldValue, value));
					}
				
				}
				_passMark = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _passMark != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(PassMark), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _title;

		public string Title
		{
			get => _title;
			set
			{
				var changedSet = false;
				var oldValue = _title;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _title != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Title), this, oldValue, value));
					}
				
				}
				_title = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _title != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Title), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _allowAnyArea;

		public bool AllowAnyArea
		{
			get => _allowAnyArea;
			set
			{
				var changedSet = false;
				var oldValue = _allowAnyArea;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _allowAnyArea != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(AllowAnyArea), this, oldValue, value));
					}
				
				}
				_allowAnyArea = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _allowAnyArea != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(AllowAnyArea), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _description;

		public string Description
		{
			get => _description;
			set
			{
				var changedSet = false;
				var oldValue = _description;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _description != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Description), this, oldValue, value));
					}
				
				}
				_description = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _description != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Description), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ExamStatus _status;

		public ExamStatus Status
		{
			get => _status;
			set
			{
				var changedSet = false;
				var oldValue = _status;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _status != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Status), this, oldValue, value));
					}
				
				}
				_status = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _status != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Status), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _notStarted;

		public bool NotStarted
		{
			get => _notStarted;
			set
			{
				var changedSet = false;
				var oldValue = _notStarted;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _notStarted != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(NotStarted), this, oldValue, value));
					}
				
				}
				_notStarted = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _notStarted != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(NotStarted), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _complete;

		public bool Complete
		{
			get => _complete;
			set
			{
				var changedSet = false;
				var oldValue = _complete;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _complete != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Complete), this, oldValue, value));
					}
				
				}
				_complete = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _complete != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Complete), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _inProgress;

		public bool InProgress
		{
			get => _inProgress;
			set
			{
				var changedSet = false;
				var oldValue = _inProgress;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _inProgress != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(InProgress), this, oldValue, value));
					}
				
				}
				_inProgress = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _inProgress != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(InProgress), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _candidateCount;

		public int CandidateCount
		{
			get => _candidateCount;
			set
			{
				var changedSet = false;
				var oldValue = _candidateCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(CandidateCount), this, oldValue, value));
					}
				
				}
				_candidateCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(CandidateCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _imageUrl;

		public string ImageUrl
		{
			get => _imageUrl;
			set
			{
				var changedSet = false;
				var oldValue = _imageUrl;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _imageUrl != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(ImageUrl), this, oldValue, value));
					}
				
				}
				_imageUrl = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _imageUrl != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(ImageUrl), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _guid;

		public Guid Guid
		{
			get => _guid;
			set
			{
				var changedSet = false;
				var oldValue = _guid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _guid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _guid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Guid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _createdDate;

		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var changedSet = false;
				var oldValue = _createdDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected long _version;

		public long Version
		{
			get => _version;
			set
			{
				var changedSet = false;
				var oldValue = _version;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _version != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _version != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _persistenceKey;

		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var changedSet = false;
				var oldValue = _persistenceKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _persistenceKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _persistenceKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Video _video;

		public Video Video
		{
			get => _video;
			set
			{
				var changedSet = false;
				var oldValue = _video;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _video != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Video), this, oldValue, value));
					}
				
				}
				_video = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _video != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Video), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClient _client;

		public HazClient Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ManagersCount { get; set; }
				
		protected RelatedList<Exam,ExamManager> _managers;

		public RelatedList<Exam,ExamManager> Managers
		{
			get
			{
				this._managers = this._managers ?? new RelatedList<Exam,ExamManager>(this, nameof(Managers));
				
				return _managers;
			}
			set
			{
				var changedSet = false;
				var oldValue = _managers;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _managers != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Managers), this, oldValue, value));
					}
				
				}
				_managers = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _managers != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Managers), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ResultsCount { get; set; }
				
		protected RelatedList<Exam,ExamResult> _results;

		public RelatedList<Exam,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<Exam,ExamResult>(this, nameof(Results));
				
				return _results;
			}
			set
			{
				var changedSet = false;
				var oldValue = _results;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _results != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Results), this, oldValue, value));
					}
				
				}
				_results = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _results != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Results), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 CandidateResultsCount { get; set; }
				
		protected RelatedList<Exam,ExamCandidateResult> _candidateResults;

		public RelatedList<Exam,ExamCandidateResult> CandidateResults
		{
			get
			{
				this._candidateResults = this._candidateResults ?? new RelatedList<Exam,ExamCandidateResult>(this, nameof(CandidateResults));
				
				return _candidateResults;
			}
			set
			{
				var changedSet = false;
				var oldValue = _candidateResults;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateResults != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(CandidateResults), this, oldValue, value));
					}
				
				}
				_candidateResults = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateResults != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(CandidateResults), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 CandidatesCount { get; set; }
				
		protected RelatedList<Exam,ExamCandidate> _candidates;

		public RelatedList<Exam,ExamCandidate> Candidates
		{
			get
			{
				this._candidates = this._candidates ?? new RelatedList<Exam,ExamCandidate>(this, nameof(Candidates));
				
				return _candidates;
			}
			set
			{
				var changedSet = false;
				var oldValue = _candidates;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidates != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Exam>(nameof(Candidates), this, oldValue, value));
					}
				
				}
				_candidates = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidates != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Exam>(nameof(Candidates), this, oldValue, value));
					}
				
				}
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
				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _clientId;

		public int ClientId
		{
			get => _clientId;
			set
			{
				var changedSet = false;
				var oldValue = _clientId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ClientId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int? _clonedFromId;

		public int? ClonedFromId
		{
			get => _clonedFromId;
			set
			{
				var changedSet = false;
				var oldValue = _clonedFromId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clonedFromId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ClonedFromId), this, oldValue, value));
					}
				
				}
				_clonedFromId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clonedFromId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ClonedFromId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _clientGuid;

		public Guid ClientGuid
		{
			get => _clientGuid;
			set
			{
				var changedSet = false;
				var oldValue = _clientGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
				_clientGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _title;

		public string Title
		{
			get => _title;
			set
			{
				var changedSet = false;
				var oldValue = _title;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _title != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Title), this, oldValue, value));
					}
				
				}
				_title = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _title != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Title), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _description;

		public string Description
		{
			get => _description;
			set
			{
				var changedSet = false;
				var oldValue = _description;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _description != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Description), this, oldValue, value));
					}
				
				}
				_description = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _description != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Description), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected double _duration;

		public double Duration
		{
			get => _duration;
			set
			{
				var changedSet = false;
				var oldValue = _duration;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _duration != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Duration), this, oldValue, value));
					}
				
				}
				_duration = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _duration != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Duration), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _resultsCount;

		public int ResultsCount
		{
			get => _resultsCount;
			set
			{
				var changedSet = false;
				var oldValue = _resultsCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _resultsCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ResultsCount), this, oldValue, value));
					}
				
				}
				_resultsCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _resultsCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ResultsCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _candidateResultsCount;

		public int CandidateResultsCount
		{
			get => _candidateResultsCount;
			set
			{
				var changedSet = false;
				var oldValue = _candidateResultsCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateResultsCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(CandidateResultsCount), this, oldValue, value));
					}
				
				}
				_candidateResultsCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateResultsCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(CandidateResultsCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _candidatesCount;

		public int CandidatesCount
		{
			get => _candidatesCount;
			set
			{
				var changedSet = false;
				var oldValue = _candidatesCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidatesCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(CandidatesCount), this, oldValue, value));
					}
				
				}
				_candidatesCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidatesCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(CandidatesCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _examCount;

		public int ExamCount
		{
			get => _examCount;
			set
			{
				var changedSet = false;
				var oldValue = _examCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ExamCount), this, oldValue, value));
					}
				
				}
				_examCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ExamCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _hazardCount;

		public int HazardCount
		{
			get => _hazardCount;
			set
			{
				var changedSet = false;
				var oldValue = _hazardCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _hazardCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(HazardCount), this, oldValue, value));
					}
				
				}
				_hazardCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _hazardCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(HazardCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _revisionKey;

		public string RevisionKey
		{
			get => _revisionKey;
			set
			{
				var changedSet = false;
				var oldValue = _revisionKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _revisionKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(RevisionKey), this, oldValue, value));
					}
				
				}
				_revisionKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _revisionKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(RevisionKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _screenshotUrl;

		public string ScreenshotUrl
		{
			get => _screenshotUrl;
			set
			{
				var changedSet = false;
				var oldValue = _screenshotUrl;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _screenshotUrl != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ScreenshotUrl), this, oldValue, value));
					}
				
				}
				_screenshotUrl = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _screenshotUrl != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ScreenshotUrl), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _screenshotMiniUrl;

		public string ScreenshotMiniUrl
		{
			get => _screenshotMiniUrl;
			set
			{
				var changedSet = false;
				var oldValue = _screenshotMiniUrl;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _screenshotMiniUrl != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ScreenshotMiniUrl), this, oldValue, value));
					}
				
				}
				_screenshotMiniUrl = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _screenshotMiniUrl != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ScreenshotMiniUrl), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _guid;

		public Guid Guid
		{
			get => _guid;
			set
			{
				var changedSet = false;
				var oldValue = _guid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _guid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _guid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Guid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _createdDate;

		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var changedSet = false;
				var oldValue = _createdDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected long _version;

		public long Version
		{
			get => _version;
			set
			{
				var changedSet = false;
				var oldValue = _version;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _version != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _version != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _persistenceKey;

		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var changedSet = false;
				var oldValue = _persistenceKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _persistenceKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _persistenceKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClient _client;

		public HazClient Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Video _clonedFrom;

		public Video ClonedFrom
		{
			get => _clonedFrom;
			set
			{
				var changedSet = false;
				var oldValue = _clonedFrom;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clonedFrom != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ClonedFrom), this, oldValue, value));
					}
				
				}
				_clonedFrom = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clonedFrom != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ClonedFrom), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ClonedToCount { get; set; }
				
		protected RelatedList<Video,Video> _clonedTo;

		public RelatedList<Video,Video> ClonedTo
		{
			get
			{
				this._clonedTo = this._clonedTo ?? new RelatedList<Video,Video>(this, nameof(ClonedTo));
				
				return _clonedTo;
			}
			set
			{
				var changedSet = false;
				var oldValue = _clonedTo;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clonedTo != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(ClonedTo), this, oldValue, value));
					}
				
				}
				_clonedTo = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clonedTo != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(ClonedTo), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamsCount { get; set; }
				
		protected RelatedList<Video,Exam> _exams;

		public RelatedList<Video,Exam> Exams
		{
			get
			{
				this._exams = this._exams ?? new RelatedList<Video,Exam>(this, nameof(Exams));
				
				return _exams;
			}
			set
			{
				var changedSet = false;
				var oldValue = _exams;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _exams != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Exams), this, oldValue, value));
					}
				
				}
				_exams = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _exams != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Exams), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected RelatedList<Video,ExamResult> _results;

		public RelatedList<Video,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<Video,ExamResult>(this, nameof(Results));
				
				return _results;
			}
			set
			{
				var changedSet = false;
				var oldValue = _results;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _results != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Results), this, oldValue, value));
					}
				
				}
				_results = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _results != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Results), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected RelatedList<Video,ExamCandidateResult> _candidateResults;

		public RelatedList<Video,ExamCandidateResult> CandidateResults
		{
			get
			{
				this._candidateResults = this._candidateResults ?? new RelatedList<Video,ExamCandidateResult>(this, nameof(CandidateResults));
				
				return _candidateResults;
			}
			set
			{
				var changedSet = false;
				var oldValue = _candidateResults;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidateResults != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(CandidateResults), this, oldValue, value));
					}
				
				}
				_candidateResults = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidateResults != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(CandidateResults), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected RelatedList<Video,ExamCandidate> _candidates;

		public RelatedList<Video,ExamCandidate> Candidates
		{
			get
			{
				this._candidates = this._candidates ?? new RelatedList<Video,ExamCandidate>(this, nameof(Candidates));
				
				return _candidates;
			}
			set
			{
				var changedSet = false;
				var oldValue = _candidates;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _candidates != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Candidates), this, oldValue, value));
					}
				
				}
				_candidates = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _candidates != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Candidates), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 HazardsCount { get; set; }
				
		protected RelatedList<Video,Hazard> _hazards;

		public RelatedList<Video,Hazard> Hazards
		{
			get
			{
				this._hazards = this._hazards ?? new RelatedList<Video,Hazard>(this, nameof(Hazards));
				
				return _hazards;
			}
			set
			{
				var changedSet = false;
				var oldValue = _hazards;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _hazards != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Video>(nameof(Hazards), this, oldValue, value));
					}
				
				}
				_hazards = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _hazards != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Video>(nameof(Hazards), this, oldValue, value));
					}
				
				}
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
				
		protected int _typeId;

		public int TypeId
		{
			get => _typeId;
			set
			{
				var changedSet = false;
				var oldValue = _typeId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _typeId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(TypeId), this, oldValue, value));
					}
				
				}
				_typeId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _typeId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(TypeId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _name;

		public string Name
		{
			get => _name;
			set
			{
				var changedSet = false;
				var oldValue = _name;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _name != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _name != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Name), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _description;

		public string Description
		{
			get => _description;
			set
			{
				var changedSet = false;
				var oldValue = _description;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _description != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Description), this, oldValue, value));
					}
				
				}
				_description = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _description != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Description), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _guid;

		public Guid Guid
		{
			get => _guid;
			set
			{
				var changedSet = false;
				var oldValue = _guid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _guid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _guid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Guid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _createdDate;

		public DateTimeOffset CreatedDate
		{
			get => _createdDate;
			set
			{
				var changedSet = false;
				var oldValue = _createdDate;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdDate != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdDate != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected long _version;

		public long Version
		{
			get => _version;
			set
			{
				var changedSet = false;
				var oldValue = _version;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _version != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _version != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid _persistenceKey;

		public Guid PersistenceKey
		{
			get => _persistenceKey;
			set
			{
				var changedSet = false;
				var oldValue = _persistenceKey;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _persistenceKey != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _persistenceKey != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 UsersCount { get; set; }
				
		protected RelatedList<HazClient,HazApplicationUser> _users;

		public RelatedList<HazClient,HazApplicationUser> Users
		{
			get
			{
				this._users = this._users ?? new RelatedList<HazClient,HazApplicationUser>(this, nameof(Users));
				
				return _users;
			}
			set
			{
				var changedSet = false;
				var oldValue = _users;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _users != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Users), this, oldValue, value));
					}
				
				}
				_users = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _users != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Users), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClientType _type;

		public HazClientType Type
		{
			get => _type;
			set
			{
				var changedSet = false;
				var oldValue = _type;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _type != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Type), this, oldValue, value));
					}
				
				}
				_type = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _type != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Type), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 VideosCount { get; set; }
				
		protected RelatedList<HazClient,Video> _videos;

		public RelatedList<HazClient,Video> Videos
		{
			get
			{
				this._videos = this._videos ?? new RelatedList<HazClient,Video>(this, nameof(Videos));
				
				return _videos;
			}
			set
			{
				var changedSet = false;
				var oldValue = _videos;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videos != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Videos), this, oldValue, value));
					}
				
				}
				_videos = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videos != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Videos), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamsCount { get; set; }
				
		protected RelatedList<HazClient,Exam> _exams;

		public RelatedList<HazClient,Exam> Exams
		{
			get
			{
				this._exams = this._exams ?? new RelatedList<HazClient,Exam>(this, nameof(Exams));
				
				return _exams;
			}
			set
			{
				var changedSet = false;
				var oldValue = _exams;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _exams != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Exams), this, oldValue, value));
					}
				
				}
				_exams = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _exams != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Exams), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamManagersCount { get; set; }
				
		protected RelatedList<HazClient,ExamManager> _examManagers;

		public RelatedList<HazClient,ExamManager> ExamManagers
		{
			get
			{
				this._examManagers = this._examManagers ?? new RelatedList<HazClient,ExamManager>(this, nameof(ExamManagers));
				
				return _examManagers;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examManagers;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examManagers != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(ExamManagers), this, oldValue, value));
					}
				
				}
				_examManagers = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examManagers != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(ExamManagers), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamResultsCount { get; set; }
				
		protected RelatedList<HazClient,ExamResult> _examResults;

		public RelatedList<HazClient,ExamResult> ExamResults
		{
			get
			{
				this._examResults = this._examResults ?? new RelatedList<HazClient,ExamResult>(this, nameof(ExamResults));
				
				return _examResults;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examResults;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examResults != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(ExamResults), this, oldValue, value));
					}
				
				}
				_examResults = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examResults != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(ExamResults), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamCandidateResultsCount { get; set; }
				
		protected RelatedList<HazClient,ExamCandidateResult> _examCandidateResults;

		public RelatedList<HazClient,ExamCandidateResult> ExamCandidateResults
		{
			get
			{
				this._examCandidateResults = this._examCandidateResults ?? new RelatedList<HazClient,ExamCandidateResult>(this, nameof(ExamCandidateResults));
				
				return _examCandidateResults;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examCandidateResults;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCandidateResults != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(ExamCandidateResults), this, oldValue, value));
					}
				
				}
				_examCandidateResults = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCandidateResults != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(ExamCandidateResults), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamCandidatesCount { get; set; }
				
		protected RelatedList<HazClient,ExamCandidate> _examCandidates;

		public RelatedList<HazClient,ExamCandidate> ExamCandidates
		{
			get
			{
				this._examCandidates = this._examCandidates ?? new RelatedList<HazClient,ExamCandidate>(this, nameof(ExamCandidates));
				
				return _examCandidates;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examCandidates;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCandidates != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(ExamCandidates), this, oldValue, value));
					}
				
				}
				_examCandidates = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCandidates != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(ExamCandidates), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 HazardsCount { get; set; }
				
		protected RelatedList<HazClient,Hazard> _hazards;

		public RelatedList<HazClient,Hazard> Hazards
		{
			get
			{
				this._hazards = this._hazards ?? new RelatedList<HazClient,Hazard>(this, nameof(Hazards));
				
				return _hazards;
			}
			set
			{
				var changedSet = false;
				var oldValue = _hazards;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _hazards != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Hazards), this, oldValue, value));
					}
				
				}
				_hazards = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _hazards != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClient>(nameof(Hazards), this, oldValue, value));
					}
				
				}
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
				
		protected string _id;

		public string Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Guid? _clientGuid;

		public Guid? ClientGuid
		{
			get => _clientGuid;
			set
			{
				var changedSet = false;
				var oldValue = _clientGuid;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientGuid != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
				_clientGuid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientGuid != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ClientGuid), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int? _clientId;

		public int? ClientId
		{
			get => _clientId;
			set
			{
				var changedSet = false;
				var oldValue = _clientId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ClientId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _createdByUserId;

		public string CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUserId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUserId != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUserId != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _email;

		public string Email
		{
			get => _email;
			set
			{
				var changedSet = false;
				var oldValue = _email;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _email != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Email), this, oldValue, value));
					}
				
				}
				_email = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _email != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Email), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazUserType _userType;

		public HazUserType UserType
		{
			get => _userType;
			set
			{
				var changedSet = false;
				var oldValue = _userType;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _userType != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(UserType), this, oldValue, value));
					}
				
				}
				_userType = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _userType != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(UserType), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _fullName;

		public string FullName
		{
			get => _fullName;
			set
			{
				var changedSet = false;
				var oldValue = _fullName;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _fullName != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(FullName), this, oldValue, value));
					}
				
				}
				_fullName = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _fullName != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(FullName), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _isLockedOut;

		public bool IsLockedOut
		{
			get => _isLockedOut;
			set
			{
				var changedSet = false;
				var oldValue = _isLockedOut;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _isLockedOut != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(IsLockedOut), this, oldValue, value));
					}
				
				}
				_isLockedOut = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _isLockedOut != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(IsLockedOut), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _userName;

		public string UserName
		{
			get => _userName;
			set
			{
				var changedSet = false;
				var oldValue = _userName;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _userName != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(UserName), this, oldValue, value));
					}
				
				}
				_userName = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _userName != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(UserName), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _normalizedUserName;

		public string NormalizedUserName
		{
			get => _normalizedUserName;
			set
			{
				var changedSet = false;
				var oldValue = _normalizedUserName;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _normalizedUserName != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(NormalizedUserName), this, oldValue, value));
					}
				
				}
				_normalizedUserName = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _normalizedUserName != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(NormalizedUserName), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _normalizedEmail;

		public string NormalizedEmail
		{
			get => _normalizedEmail;
			set
			{
				var changedSet = false;
				var oldValue = _normalizedEmail;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _normalizedEmail != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(NormalizedEmail), this, oldValue, value));
					}
				
				}
				_normalizedEmail = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _normalizedEmail != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(NormalizedEmail), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _emailConfirmed;

		public bool EmailConfirmed
		{
			get => _emailConfirmed;
			set
			{
				var changedSet = false;
				var oldValue = _emailConfirmed;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _emailConfirmed != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(EmailConfirmed), this, oldValue, value));
					}
				
				}
				_emailConfirmed = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _emailConfirmed != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(EmailConfirmed), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _passwordHash;

		public string PasswordHash
		{
			get => _passwordHash;
			set
			{
				var changedSet = false;
				var oldValue = _passwordHash;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _passwordHash != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(PasswordHash), this, oldValue, value));
					}
				
				}
				_passwordHash = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _passwordHash != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(PasswordHash), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _securityStamp;

		public string SecurityStamp
		{
			get => _securityStamp;
			set
			{
				var changedSet = false;
				var oldValue = _securityStamp;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _securityStamp != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(SecurityStamp), this, oldValue, value));
					}
				
				}
				_securityStamp = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _securityStamp != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(SecurityStamp), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _concurrencyStamp;

		public string ConcurrencyStamp
		{
			get => _concurrencyStamp;
			set
			{
				var changedSet = false;
				var oldValue = _concurrencyStamp;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _concurrencyStamp != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ConcurrencyStamp), this, oldValue, value));
					}
				
				}
				_concurrencyStamp = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _concurrencyStamp != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ConcurrencyStamp), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _phoneNumber;

		public string PhoneNumber
		{
			get => _phoneNumber;
			set
			{
				var changedSet = false;
				var oldValue = _phoneNumber;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _phoneNumber != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(PhoneNumber), this, oldValue, value));
					}
				
				}
				_phoneNumber = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _phoneNumber != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(PhoneNumber), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _phoneNumberConfirmed;

		public bool PhoneNumberConfirmed
		{
			get => _phoneNumberConfirmed;
			set
			{
				var changedSet = false;
				var oldValue = _phoneNumberConfirmed;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _phoneNumberConfirmed != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(PhoneNumberConfirmed), this, oldValue, value));
					}
				
				}
				_phoneNumberConfirmed = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _phoneNumberConfirmed != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(PhoneNumberConfirmed), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _twoFactorEnabled;

		public bool TwoFactorEnabled
		{
			get => _twoFactorEnabled;
			set
			{
				var changedSet = false;
				var oldValue = _twoFactorEnabled;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _twoFactorEnabled != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(TwoFactorEnabled), this, oldValue, value));
					}
				
				}
				_twoFactorEnabled = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _twoFactorEnabled != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(TwoFactorEnabled), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset? _lockoutEnd;

		public DateTimeOffset? LockoutEnd
		{
			get => _lockoutEnd;
			set
			{
				var changedSet = false;
				var oldValue = _lockoutEnd;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _lockoutEnd != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(LockoutEnd), this, oldValue, value));
					}
				
				}
				_lockoutEnd = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _lockoutEnd != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(LockoutEnd), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _lockoutEnabled;

		public bool LockoutEnabled
		{
			get => _lockoutEnabled;
			set
			{
				var changedSet = false;
				var oldValue = _lockoutEnabled;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _lockoutEnabled != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(LockoutEnabled), this, oldValue, value));
					}
				
				}
				_lockoutEnabled = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _lockoutEnabled != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(LockoutEnabled), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _accessFailedCount;

		public int AccessFailedCount
		{
			get => _accessFailedCount;
			set
			{
				var changedSet = false;
				var oldValue = _accessFailedCount;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _accessFailedCount != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(AccessFailedCount), this, oldValue, value));
					}
				
				}
				_accessFailedCount = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _accessFailedCount != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(AccessFailedCount), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazClient _client;

		public HazClient Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected HazApplicationUser _createdByUser;

		public HazApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _createdByUser != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _createdByUser != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 UsersCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,HazApplicationUser> _usersCreated;

		public RelatedList<HazApplicationUser,HazApplicationUser> UsersCreated
		{
			get
			{
				this._usersCreated = this._usersCreated ?? new RelatedList<HazApplicationUser,HazApplicationUser>(this, nameof(UsersCreated));
				
				return _usersCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _usersCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _usersCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(UsersCreated), this, oldValue, value));
					}
				
				}
				_usersCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _usersCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(UsersCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ClientsCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,HazClient> _clientsCreated;

		public RelatedList<HazApplicationUser,HazClient> ClientsCreated
		{
			get
			{
				this._clientsCreated = this._clientsCreated ?? new RelatedList<HazApplicationUser,HazClient>(this, nameof(ClientsCreated));
				
				return _clientsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _clientsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _clientsCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ClientsCreated), this, oldValue, value));
					}
				
				}
				_clientsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _clientsCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ClientsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 VideosCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,Video> _videosCreated;

		public RelatedList<HazApplicationUser,Video> VideosCreated
		{
			get
			{
				this._videosCreated = this._videosCreated ?? new RelatedList<HazApplicationUser,Video>(this, nameof(VideosCreated));
				
				return _videosCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _videosCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _videosCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(VideosCreated), this, oldValue, value));
					}
				
				}
				_videosCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _videosCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(VideosCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamsCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,Exam> _examsCreated;

		public RelatedList<HazApplicationUser,Exam> ExamsCreated
		{
			get
			{
				this._examsCreated = this._examsCreated ?? new RelatedList<HazApplicationUser,Exam>(this, nameof(ExamsCreated));
				
				return _examsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examsCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamsCreated), this, oldValue, value));
					}
				
				}
				_examsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examsCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamManagersCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,ExamManager> _examManagersCreated;

		public RelatedList<HazApplicationUser,ExamManager> ExamManagersCreated
		{
			get
			{
				this._examManagersCreated = this._examManagersCreated ?? new RelatedList<HazApplicationUser,ExamManager>(this, nameof(ExamManagersCreated));
				
				return _examManagersCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examManagersCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examManagersCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamManagersCreated), this, oldValue, value));
					}
				
				}
				_examManagersCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examManagersCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamManagersCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ResultsCount { get; set; }
				
		protected RelatedList<HazApplicationUser,ExamResult> _results;

		public RelatedList<HazApplicationUser,ExamResult> Results
		{
			get
			{
				this._results = this._results ?? new RelatedList<HazApplicationUser,ExamResult>(this, nameof(Results));
				
				return _results;
			}
			set
			{
				var changedSet = false;
				var oldValue = _results;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _results != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Results), this, oldValue, value));
					}
				
				}
				_results = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _results != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Results), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamResultsCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,ExamResult> _examResultsCreated;

		public RelatedList<HazApplicationUser,ExamResult> ExamResultsCreated
		{
			get
			{
				this._examResultsCreated = this._examResultsCreated ?? new RelatedList<HazApplicationUser,ExamResult>(this, nameof(ExamResultsCreated));
				
				return _examResultsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examResultsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examResultsCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamResultsCreated), this, oldValue, value));
					}
				
				}
				_examResultsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examResultsCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamResultsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamResultsCount { get; set; }
				
		protected RelatedList<HazApplicationUser,ExamCandidateResult> _examResults;

		public RelatedList<HazApplicationUser,ExamCandidateResult> ExamResults
		{
			get
			{
				this._examResults = this._examResults ?? new RelatedList<HazApplicationUser,ExamCandidateResult>(this, nameof(ExamResults));
				
				return _examResults;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examResults;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examResults != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamResults), this, oldValue, value));
					}
				
				}
				_examResults = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examResults != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamResults), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamCandidateResultsCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,ExamCandidateResult> _examCandidateResultsCreated;

		public RelatedList<HazApplicationUser,ExamCandidateResult> ExamCandidateResultsCreated
		{
			get
			{
				this._examCandidateResultsCreated = this._examCandidateResultsCreated ?? new RelatedList<HazApplicationUser,ExamCandidateResult>(this, nameof(ExamCandidateResultsCreated));
				
				return _examCandidateResultsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examCandidateResultsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCandidateResultsCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamCandidateResultsCreated), this, oldValue, value));
					}
				
				}
				_examCandidateResultsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCandidateResultsCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamCandidateResultsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamsCount { get; set; }
				
		protected RelatedList<HazApplicationUser,ExamCandidate> _exams;

		public RelatedList<HazApplicationUser,ExamCandidate> Exams
		{
			get
			{
				this._exams = this._exams ?? new RelatedList<HazApplicationUser,ExamCandidate>(this, nameof(Exams));
				
				return _exams;
			}
			set
			{
				var changedSet = false;
				var oldValue = _exams;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _exams != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Exams), this, oldValue, value));
					}
				
				}
				_exams = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _exams != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(Exams), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ExamCandidatesCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,ExamCandidate> _examCandidatesCreated;

		public RelatedList<HazApplicationUser,ExamCandidate> ExamCandidatesCreated
		{
			get
			{
				this._examCandidatesCreated = this._examCandidatesCreated ?? new RelatedList<HazApplicationUser,ExamCandidate>(this, nameof(ExamCandidatesCreated));
				
				return _examCandidatesCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _examCandidatesCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _examCandidatesCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamCandidatesCreated), this, oldValue, value));
					}
				
				}
				_examCandidatesCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _examCandidatesCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(ExamCandidatesCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 HazardsCreatedCount { get; set; }
				
		protected RelatedList<HazApplicationUser,Hazard> _hazardsCreated;

		public RelatedList<HazApplicationUser,Hazard> HazardsCreated
		{
			get
			{
				this._hazardsCreated = this._hazardsCreated ?? new RelatedList<HazApplicationUser,Hazard>(this, nameof(HazardsCreated));
				
				return _hazardsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _hazardsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _hazardsCreated != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(HazardsCreated), this, oldValue, value));
					}
				
				}
				_hazardsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _hazardsCreated != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazApplicationUser>(nameof(HazardsCreated), this, oldValue, value));
					}
				
				}
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
				
		protected int _id;

		public int Id
		{
			get => _id;
			set
			{
				var changedSet = false;
				var oldValue = _id;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _id != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClientType>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _id != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClientType>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _name;

		public string Name
		{
			get => _name;
			set
			{
				var changedSet = false;
				var oldValue = _name;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _name != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClientType>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _name != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClientType>(nameof(Name), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ClientCount { get; set; }
				
		protected RelatedList<HazClientType,HazClient> _client;

		public RelatedList<HazClientType,HazClient> Client
		{
			get
			{
				this._client = this._client ?? new RelatedList<HazClientType,HazClient>(this, nameof(Client));
				
				return _client;
			}
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = _client != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<HazClientType>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = _client != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<HazClientType>(nameof(Client), this, oldValue, value));
					}
				
				}
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

