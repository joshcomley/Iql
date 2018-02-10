using Iql.Queryable;
using Tunnel.ApiContext.Base;
using Tunnel.App.Data.Entities;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Events;
using System;


namespace Tunnel.App.Data.Entities
{
	public class UserSiteBase : IEntity
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
			return "UserSite";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class SiteInspectionBase : IEntity
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
			return "SiteInspection";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class SiteBase : IEntity
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
			return "Site";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class PersonReportBase : IEntity
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
			return "PersonReport";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class PersonTypeMapBase : IEntity
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
			return "PersonTypeMap";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class PersonTypeBase : IEntity
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
			return "PersonType";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class PersonLoadingBase : IEntity
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
			return "PersonLoading";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class PersonInspectionBase : IEntity
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
			return "PersonInspection";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class PersonBase : IEntity
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
			return "Person";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class RiskAssessmentQuestionBase : IEntity
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
			return "RiskAssessmentQuestion";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class RiskAssessmentAnswerBase : IEntity
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
			return "RiskAssessmentAnswer";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class RiskAssessmentSolutionBase : IEntity
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
			return "RiskAssessmentSolution";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class RiskAssessmentBase : IEntity
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
			return "RiskAssessment";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ReportReceiverEmailAddressBase : IEntity
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
			return "ReportReceiverEmailAddress";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ProjectBase : IEntity
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
			return "Project";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ReportTypeBase : IEntity
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
			return "ReportType";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ReportRecommendationBase : IEntity
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
			return "ReportRecommendation";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ReportDefaultRecommendationBase : IEntity
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
			return "ReportDefaultRecommendation";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ReportCategoryBase : IEntity
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
			return "ReportCategory";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ReportActionsTakenBase : IEntity
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
			return "ReportActionsTaken";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class SiteDocumentBase : IEntity
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
			return "SiteDocument";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class DocumentCategoryBase : IEntity
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
			return "DocumentCategory";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ClientTypeBase : IEntity
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
			return "ClientType";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ClientBase : IEntity
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
			return "Client";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public class ApplicationUserBase : IEntity
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
			return "ApplicationUser";
		}
		public virtual EntityValidationResult ValidateEntity()
		{
			return new EntityValidationResult(this.GetType());
		}
	
	}
}


namespace Tunnel.App.Data.Entities
{
	public enum FaultReportStatus
	{
		Fail = 0,
		PassWithObservations = 1
	
	}
}

namespace Tunnel.App.Data.Entities
{
	public enum InspectionFailReason
	{
		None = 0,
		UnableToAccess = 1,
		PersistentFaults = 2,
		FailuresInFaultReports = 3,
		TooManyMinorObservations = 4,
		NoDesignSupplied = 5
	
	}
}

namespace Tunnel.App.Data.Entities
{
	public enum PersonInspectionStatus
	{
		Pass = 0,
		Fail = 1,
		PassWithObservations = 2
	
	}
}

namespace Tunnel.App.Data.Entities
{
	public enum PersonCategory
	{
		System = 0,
		Conventional = 1
	
	}
}

namespace Tunnel.App.Data.Entities
{
	public enum UserType
	{
		Super = 1,
		Client = 2,
		Candidate = 3
	
	}
}

namespace Tunnel.App.Data.Entities
{
	public class UserSite : UserSiteBase, IEntity
	{
				
		protected int _siteId;

		public int SiteId
		{
			get => _siteId;
			set
			{
				var changedSet = false;
				var oldValue = _siteId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<UserSite>(nameof(SiteId), this, oldValue, value));
					}
				
				}
				_siteId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<UserSite>(nameof(SiteId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _userId;

		public string UserId
		{
			get => _userId;
			set
			{
				var changedSet = false;
				var oldValue = _userId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<UserSite>(nameof(UserId), this, oldValue, value));
					}
				
				}
				_userId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<UserSite>(nameof(UserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _user;

		public ApplicationUser User
		{
			get => _user;
			set
			{
				var changedSet = false;
				var oldValue = _user;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<UserSite>(nameof(User), this, oldValue, value));
					}
				
				}
				_user = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<UserSite>(nameof(User), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Site _site;

		public Site Site
		{
			get => _site;
			set
			{
				var changedSet = false;
				var oldValue = _site;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<UserSite>(nameof(Site), this, oldValue, value));
					}
				
				}
				_site = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<UserSite>(nameof(Site), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class SiteInspection : SiteInspectionBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _siteId;

		public int SiteId
		{
			get => _siteId;
			set
			{
				var changedSet = false;
				var oldValue = _siteId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(SiteId), this, oldValue, value));
					}
				
				}
				_siteId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(SiteId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _startTime;

		public DateTimeOffset StartTime
		{
			get => _startTime;
			set
			{
				var changedSet = false;
				var oldValue = _startTime;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(StartTime), this, oldValue, value));
					}
				
				}
				_startTime = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(StartTime), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _endTime;

		public DateTimeOffset EndTime
		{
			get => _endTime;
			set
			{
				var changedSet = false;
				var oldValue = _endTime;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(EndTime), this, oldValue, value));
					}
				
				}
				_endTime = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(EndTime), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected RiskAssessment _riskAssessment;

		public RiskAssessment RiskAssessment
		{
			get => _riskAssessment;
			set
			{
				var changedSet = false;
				var oldValue = _riskAssessment;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(RiskAssessment), this, oldValue, value));
					}
				
				}
				_riskAssessment = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(RiskAssessment), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PersonInspectionsCount { get; set; }
				
		protected RelatedList<SiteInspection,PersonInspection> _personInspections;

		public RelatedList<SiteInspection,PersonInspection> PersonInspections
		{
			get
			{
				this._personInspections = this._personInspections ?? new RelatedList<SiteInspection,PersonInspection>(this, nameof(PersonInspections));
				
				return _personInspections;
			}
			set
			{
				var changedSet = false;
				var oldValue = _personInspections;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(PersonInspections), this, oldValue, value));
					}
				
				}
				_personInspections = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(PersonInspections), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Site _site;

		public Site Site
		{
			get => _site;
			set
			{
				var changedSet = false;
				var oldValue = _site;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(Site), this, oldValue, value));
					}
				
				}
				_site = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(Site), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteInspection>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class Site : SiteBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int? _parentId;

		public int? ParentId
		{
			get => _parentId;
			set
			{
				var changedSet = false;
				var oldValue = _parentId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(ParentId), this, oldValue, value));
					}
				
				}
				_parentId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(ParentId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(ClientId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _address;

		public string Address
		{
			get => _address;
			set
			{
				var changedSet = false;
				var oldValue = _address;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Address), this, oldValue, value));
					}
				
				}
				_address = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Address), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _postCode;

		public string PostCode
		{
			get => _postCode;
			set
			{
				var changedSet = false;
				var oldValue = _postCode;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(PostCode), this, oldValue, value));
					}
				
				}
				_postCode = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(PostCode), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Name), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _left;

		public int Left
		{
			get => _left;
			set
			{
				var changedSet = false;
				var oldValue = _left;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Left), this, oldValue, value));
					}
				
				}
				_left = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Left), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _right;

		public int Right
		{
			get => _right;
			set
			{
				var changedSet = false;
				var oldValue = _right;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Right), this, oldValue, value));
					}
				
				}
				_right = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Right), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 DocumentsCount { get; set; }
				
		protected RelatedList<Site,SiteDocument> _documents;

		public RelatedList<Site,SiteDocument> Documents
		{
			get
			{
				this._documents = this._documents ?? new RelatedList<Site,SiteDocument>(this, nameof(Documents));
				
				return _documents;
			}
			set
			{
				var changedSet = false;
				var oldValue = _documents;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Documents), this, oldValue, value));
					}
				
				}
				_documents = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Documents), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 AdditionalSendReportsToCount { get; set; }
				
		protected RelatedList<Site,ReportReceiverEmailAddress> _additionalSendReportsTo;

		public RelatedList<Site,ReportReceiverEmailAddress> AdditionalSendReportsTo
		{
			get
			{
				this._additionalSendReportsTo = this._additionalSendReportsTo ?? new RelatedList<Site,ReportReceiverEmailAddress>(this, nameof(AdditionalSendReportsTo));
				
				return _additionalSendReportsTo;
			}
			set
			{
				var changedSet = false;
				var oldValue = _additionalSendReportsTo;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(AdditionalSendReportsTo), this, oldValue, value));
					}
				
				}
				_additionalSendReportsTo = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(AdditionalSendReportsTo), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Site _parent;

		public Site Parent
		{
			get => _parent;
			set
			{
				var changedSet = false;
				var oldValue = _parent;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Parent), this, oldValue, value));
					}
				
				}
				_parent = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Parent), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ChildrenCount { get; set; }
				
		protected RelatedList<Site,Site> _children;

		public RelatedList<Site,Site> Children
		{
			get
			{
				this._children = this._children ?? new RelatedList<Site,Site>(this, nameof(Children));
				
				return _children;
			}
			set
			{
				var changedSet = false;
				var oldValue = _children;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Children), this, oldValue, value));
					}
				
				}
				_children = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Children), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Client _client;

		public Client Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 SiteInspectionsCount { get; set; }
				
		protected RelatedList<Site,SiteInspection> _siteInspections;

		public RelatedList<Site,SiteInspection> SiteInspections
		{
			get
			{
				this._siteInspections = this._siteInspections ?? new RelatedList<Site,SiteInspection>(this, nameof(SiteInspections));
				
				return _siteInspections;
			}
			set
			{
				var changedSet = false;
				var oldValue = _siteInspections;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(SiteInspections), this, oldValue, value));
					}
				
				}
				_siteInspections = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(SiteInspections), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 UsersCount { get; set; }
				
		protected RelatedList<Site,UserSite> _users;

		public RelatedList<Site,UserSite> Users
		{
			get
			{
				this._users = this._users ?? new RelatedList<Site,UserSite>(this, nameof(Users));
				
				return _users;
			}
			set
			{
				var changedSet = false;
				var oldValue = _users;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Site>(nameof(Users), this, oldValue, value));
					}
				
				}
				_users = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Site>(nameof(Users), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class PersonReport : PersonReportBase, IEntity
	{
				
		protected int _personId;

		public int PersonId
		{
			get => _personId;
			set
			{
				var changedSet = false;
				var oldValue = _personId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(PersonId), this, oldValue, value));
					}
				
				}
				_personId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(PersonId), this, oldValue, value));
					}
				
				}
			}
		}

				
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(TypeId), this, oldValue, value));
					}
				
				}
				_typeId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(TypeId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Title), this, oldValue, value));
					}
				
				}
				_title = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Title), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected FaultReportStatus _status;

		public FaultReportStatus Status
		{
			get => _status;
			set
			{
				var changedSet = false;
				var oldValue = _status;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Status), this, oldValue, value));
					}
				
				}
				_status = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Status), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ActionsTakenCount { get; set; }
				
		protected RelatedList<PersonReport,ReportActionsTaken> _actionsTaken;

		public RelatedList<PersonReport,ReportActionsTaken> ActionsTaken
		{
			get
			{
				this._actionsTaken = this._actionsTaken ?? new RelatedList<PersonReport,ReportActionsTaken>(this, nameof(ActionsTaken));
				
				return _actionsTaken;
			}
			set
			{
				var changedSet = false;
				var oldValue = _actionsTaken;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(ActionsTaken), this, oldValue, value));
					}
				
				}
				_actionsTaken = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(ActionsTaken), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 RecommendationsCount { get; set; }
				
		protected RelatedList<PersonReport,ReportRecommendation> _recommendations;

		public RelatedList<PersonReport,ReportRecommendation> Recommendations
		{
			get
			{
				this._recommendations = this._recommendations ?? new RelatedList<PersonReport,ReportRecommendation>(this, nameof(Recommendations));
				
				return _recommendations;
			}
			set
			{
				var changedSet = false;
				var oldValue = _recommendations;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Recommendations), this, oldValue, value));
					}
				
				}
				_recommendations = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Recommendations), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Person _person;

		public Person Person
		{
			get => _person;
			set
			{
				var changedSet = false;
				var oldValue = _person;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Person), this, oldValue, value));
					}
				
				}
				_person = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Person), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ReportType _type;

		public ReportType Type
		{
			get => _type;
			set
			{
				var changedSet = false;
				var oldValue = _type;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Type), this, oldValue, value));
					}
				
				}
				_type = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(Type), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonReport>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class PersonTypeMap : PersonTypeMapBase, IEntity
	{
				
		protected int _personId;

		public int PersonId
		{
			get => _personId;
			set
			{
				var changedSet = false;
				var oldValue = _personId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(PersonId), this, oldValue, value));
					}
				
				}
				_personId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(PersonId), this, oldValue, value));
					}
				
				}
			}
		}

				
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(TypeId), this, oldValue, value));
					}
				
				}
				_typeId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(TypeId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _notes;

		public string Notes
		{
			get => _notes;
			set
			{
				var changedSet = false;
				var oldValue = _notes;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Notes), this, oldValue, value));
					}
				
				}
				_notes = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Notes), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Description), this, oldValue, value));
					}
				
				}
				_description = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Description), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Person _person;

		public Person Person
		{
			get => _person;
			set
			{
				var changedSet = false;
				var oldValue = _person;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Person), this, oldValue, value));
					}
				
				}
				_person = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Person), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected PersonType _type;

		public PersonType Type
		{
			get => _type;
			set
			{
				var changedSet = false;
				var oldValue = _type;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Type), this, oldValue, value));
					}
				
				}
				_type = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonTypeMap>(nameof(Type), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class PersonType : PersonTypeBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(Title), this, oldValue, value));
					}
				
				}
				_title = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(Title), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PeopleCount { get; set; }
				
		protected RelatedList<PersonType,Person> _people;

		public RelatedList<PersonType,Person> People
		{
			get
			{
				this._people = this._people ?? new RelatedList<PersonType,Person>(this, nameof(People));
				
				return _people;
			}
			set
			{
				var changedSet = false;
				var oldValue = _people;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(People), this, oldValue, value));
					}
				
				}
				_people = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(People), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PeopleMapCount { get; set; }
				
		protected RelatedList<PersonType,PersonTypeMap> _peopleMap;

		public RelatedList<PersonType,PersonTypeMap> PeopleMap
		{
			get
			{
				this._peopleMap = this._peopleMap ?? new RelatedList<PersonType,PersonTypeMap>(this, nameof(PeopleMap));
				
				return _peopleMap;
			}
			set
			{
				var changedSet = false;
				var oldValue = _peopleMap;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonType>(nameof(PeopleMap), this, oldValue, value));
					}
				
				}
				_peopleMap = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonType>(nameof(PeopleMap), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class PersonLoading : PersonLoadingBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(Name), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PeopleCount { get; set; }
				
		protected RelatedList<PersonLoading,Person> _people;

		public RelatedList<PersonLoading,Person> People
		{
			get
			{
				this._people = this._people ?? new RelatedList<PersonLoading,Person>(this, nameof(People));
				
				return _people;
			}
			set
			{
				var changedSet = false;
				var oldValue = _people;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(People), this, oldValue, value));
					}
				
				}
				_people = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(People), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonLoading>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			validationResult.AddPropertyValidationResult(this.ValidateName());
			return validationResult;
		}
		public PropertyValidationResult ValidateName()
		{
			var validationResult = new PropertyValidationResult(this.GetType(), "Name");
			var entity = this;
			if(!(true))
			{
				validationResult.AddFailure("Please enter a loading name");
			
			}
			return validationResult;
		}
	
	}
}

namespace Tunnel.App.Data.Entities
{
	public class PersonInspection : PersonInspectionBase, IEntity
	{
				
		protected int _siteInspectionId;

		public int SiteInspectionId
		{
			get => _siteInspectionId;
			set
			{
				var changedSet = false;
				var oldValue = _siteInspectionId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(SiteInspectionId), this, oldValue, value));
					}
				
				}
				_siteInspectionId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(SiteInspectionId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _personId;

		public int PersonId
		{
			get => _personId;
			set
			{
				var changedSet = false;
				var oldValue = _personId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(PersonId), this, oldValue, value));
					}
				
				}
				_personId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(PersonId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected PersonInspectionStatus _inspectionStatus;

		public PersonInspectionStatus InspectionStatus
		{
			get => _inspectionStatus;
			set
			{
				var changedSet = false;
				var oldValue = _inspectionStatus;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(InspectionStatus), this, oldValue, value));
					}
				
				}
				_inspectionStatus = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(InspectionStatus), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _startTime;

		public DateTimeOffset StartTime
		{
			get => _startTime;
			set
			{
				var changedSet = false;
				var oldValue = _startTime;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(StartTime), this, oldValue, value));
					}
				
				}
				_startTime = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(StartTime), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DateTimeOffset _endTime;

		public DateTimeOffset EndTime
		{
			get => _endTime;
			set
			{
				var changedSet = false;
				var oldValue = _endTime;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(EndTime), this, oldValue, value));
					}
				
				}
				_endTime = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(EndTime), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected InspectionFailReason _reasonForFailure;

		public InspectionFailReason ReasonForFailure
		{
			get => _reasonForFailure;
			set
			{
				var changedSet = false;
				var oldValue = _reasonForFailure;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(ReasonForFailure), this, oldValue, value));
					}
				
				}
				_reasonForFailure = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(ReasonForFailure), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected bool _isDesignRequired;

		public bool IsDesignRequired
		{
			get => _isDesignRequired;
			set
			{
				var changedSet = false;
				var oldValue = _isDesignRequired;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(IsDesignRequired), this, oldValue, value));
					}
				
				}
				_isDesignRequired = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(IsDesignRequired), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _drawingNumber;

		public string DrawingNumber
		{
			get => _drawingNumber;
			set
			{
				var changedSet = false;
				var oldValue = _drawingNumber;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(DrawingNumber), this, oldValue, value));
					}
				
				}
				_drawingNumber = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(DrawingNumber), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected SiteInspection _siteInspection;

		public SiteInspection SiteInspection
		{
			get => _siteInspection;
			set
			{
				var changedSet = false;
				var oldValue = _siteInspection;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(SiteInspection), this, oldValue, value));
					}
				
				}
				_siteInspection = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(SiteInspection), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<PersonInspection>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class Person : PersonBase, IEntity
	{
				
		protected int? _typeId;

		public int? TypeId
		{
			get => _typeId;
			set
			{
				var changedSet = false;
				var oldValue = _typeId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(TypeId), this, oldValue, value));
					}
				
				}
				_typeId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(TypeId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int? _loadingId;

		public int? LoadingId
		{
			get => _loadingId;
			set
			{
				var changedSet = false;
				var oldValue = _loadingId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(LoadingId), this, oldValue, value));
					}
				
				}
				_loadingId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(LoadingId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _key;

		public string Key
		{
			get => _key;
			set
			{
				var changedSet = false;
				var oldValue = _key;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Key), this, oldValue, value));
					}
				
				}
				_key = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Key), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Title), this, oldValue, value));
					}
				
				}
				_title = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Title), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Description), this, oldValue, value));
					}
				
				}
				_description = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Description), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected PersonCategory _category;

		public PersonCategory Category
		{
			get => _category;
			set
			{
				var changedSet = false;
				var oldValue = _category;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Category), this, oldValue, value));
					}
				
				}
				_category = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Category), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(ClientId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Client _client;

		public Client Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected PersonType _type;

		public PersonType Type
		{
			get => _type;
			set
			{
				var changedSet = false;
				var oldValue = _type;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Type), this, oldValue, value));
					}
				
				}
				_type = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Type), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected PersonLoading _loading;

		public PersonLoading Loading
		{
			get => _loading;
			set
			{
				var changedSet = false;
				var oldValue = _loading;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Loading), this, oldValue, value));
					}
				
				}
				_loading = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Loading), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 TypesCount { get; set; }
				
		protected RelatedList<Person,PersonTypeMap> _types;

		public RelatedList<Person,PersonTypeMap> Types
		{
			get
			{
				this._types = this._types ?? new RelatedList<Person,PersonTypeMap>(this, nameof(Types));
				
				return _types;
			}
			set
			{
				var changedSet = false;
				var oldValue = _types;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Types), this, oldValue, value));
					}
				
				}
				_types = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Types), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ReportsCount { get; set; }
				
		protected RelatedList<Person,PersonReport> _reports;

		public RelatedList<Person,PersonReport> Reports
		{
			get
			{
				this._reports = this._reports ?? new RelatedList<Person,PersonReport>(this, nameof(Reports));
				
				return _reports;
			}
			set
			{
				var changedSet = false;
				var oldValue = _reports;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Person>(nameof(Reports), this, oldValue, value));
					}
				
				}
				_reports = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Person>(nameof(Reports), this, oldValue, value));
					}
				
				}
			}
		}

		
		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			if(!(true))
			{
				validationResult.AddFailure("Please enter either a title or a description");
			
			}
			if(!(true))
			{
				validationResult.AddFailure("If the name is 'Josh' please match it in the description");
			
			}
			validationResult.AddPropertyValidationResult(this.ValidateTitle());
			validationResult.AddPropertyValidationResult(this.ValidateDescription());
			return validationResult;
		}
		public PropertyValidationResult ValidateTitle()
		{
			var validationResult = new PropertyValidationResult(this.GetType(), "Title");
			var entity = this;
			if(!(true))
			{
				validationResult.AddFailure("Please enter a valid person title");
			
			}
			if(!(true))
			{
				validationResult.AddFailure("Please enter less than fifty characters");
			
			}
			if(!(true))
			{
				validationResult.AddFailure("Please enter at least three characters for the person's title");
			
			}
			if(!(true))
			{
				validationResult.AddFailure("Please enter a valid report title");
			
			}
			if(!(true))
			{
				validationResult.AddFailure("Please enter less than five characters");
			
			}
			return validationResult;
		}
		public PropertyValidationResult ValidateDescription()
		{
			var validationResult = new PropertyValidationResult(this.GetType(), "Description");
			var entity = this;
			if(!(true))
			{
				validationResult.AddFailure("Please enter a valid person description");
			
			}
			return validationResult;
		}
	
	}
}

namespace Tunnel.App.Data.Entities
{
	public class RiskAssessmentQuestion : RiskAssessmentQuestionBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Name), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 AnswersCount { get; set; }
				
		protected RelatedList<RiskAssessmentQuestion,RiskAssessmentAnswer> _answers;

		public RelatedList<RiskAssessmentQuestion,RiskAssessmentAnswer> Answers
		{
			get
			{
				this._answers = this._answers ?? new RelatedList<RiskAssessmentQuestion,RiskAssessmentAnswer>(this, nameof(Answers));
				
				return _answers;
			}
			set
			{
				var changedSet = false;
				var oldValue = _answers;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Answers), this, oldValue, value));
					}
				
				}
				_answers = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(Answers), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentQuestion>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class RiskAssessmentAnswer : RiskAssessmentAnswerBase, IEntity
	{
				
		protected int _questionId;

		public int QuestionId
		{
			get => _questionId;
			set
			{
				var changedSet = false;
				var oldValue = _questionId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(QuestionId), this, oldValue, value));
					}
				
				}
				_questionId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(QuestionId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _specificHazard;

		public string SpecificHazard
		{
			get => _specificHazard;
			set
			{
				var changedSet = false;
				var oldValue = _specificHazard;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(SpecificHazard), this, oldValue, value));
					}
				
				}
				_specificHazard = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(SpecificHazard), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _precautionsToControlHazard;

		public string PrecautionsToControlHazard
		{
			get => _precautionsToControlHazard;
			set
			{
				var changedSet = false;
				var oldValue = _precautionsToControlHazard;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(PrecautionsToControlHazard), this, oldValue, value));
					}
				
				}
				_precautionsToControlHazard = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(PrecautionsToControlHazard), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected RiskAssessmentQuestion _question;

		public RiskAssessmentQuestion Question
		{
			get => _question;
			set
			{
				var changedSet = false;
				var oldValue = _question;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Question), this, oldValue, value));
					}
				
				}
				_question = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(Question), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentAnswer>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class RiskAssessmentSolution : RiskAssessmentSolutionBase, IEntity
	{
				
		protected int _riskAssessmentId;

		public int RiskAssessmentId
		{
			get => _riskAssessmentId;
			set
			{
				var changedSet = false;
				var oldValue = _riskAssessmentId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(RiskAssessmentId), this, oldValue, value));
					}
				
				}
				_riskAssessmentId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(RiskAssessmentId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected RiskAssessment _riskAssessment;

		public RiskAssessment RiskAssessment
		{
			get => _riskAssessment;
			set
			{
				var changedSet = false;
				var oldValue = _riskAssessment;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(RiskAssessment), this, oldValue, value));
					}
				
				}
				_riskAssessment = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(RiskAssessment), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessmentSolution>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class RiskAssessment : RiskAssessmentBase, IEntity
	{
				
		protected int _siteInspectionId;

		public int SiteInspectionId
		{
			get => _siteInspectionId;
			set
			{
				var changedSet = false;
				var oldValue = _siteInspectionId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(SiteInspectionId), this, oldValue, value));
					}
				
				}
				_siteInspectionId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(SiteInspectionId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected SiteInspection _siteInspection;

		public SiteInspection SiteInspection
		{
			get => _siteInspection;
			set
			{
				var changedSet = false;
				var oldValue = _siteInspection;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(SiteInspection), this, oldValue, value));
					}
				
				}
				_siteInspection = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(SiteInspection), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected RiskAssessmentSolution _riskAssessmentSolution;

		public RiskAssessmentSolution RiskAssessmentSolution
		{
			get => _riskAssessmentSolution;
			set
			{
				var changedSet = false;
				var oldValue = _riskAssessmentSolution;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(RiskAssessmentSolution), this, oldValue, value));
					}
				
				}
				_riskAssessmentSolution = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<RiskAssessment>(nameof(RiskAssessmentSolution), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class ReportReceiverEmailAddress : ReportReceiverEmailAddressBase, IEntity
	{
				
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _siteId;

		public int SiteId
		{
			get => _siteId;
			set
			{
				var changedSet = false;
				var oldValue = _siteId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(SiteId), this, oldValue, value));
					}
				
				}
				_siteId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(SiteId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _emailAddress;

		public string EmailAddress
		{
			get => _emailAddress;
			set
			{
				var changedSet = false;
				var oldValue = _emailAddress;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(EmailAddress), this, oldValue, value));
					}
				
				}
				_emailAddress = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(EmailAddress), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Site _site;

		public Site Site
		{
			get => _site;
			set
			{
				var changedSet = false;
				var oldValue = _site;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Site), this, oldValue, value));
					}
				
				}
				_site = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(Site), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportReceiverEmailAddress>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class Project : ProjectBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Project>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Project>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Project>(nameof(Title), this, oldValue, value));
					}
				
				}
				_title = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Project>(nameof(Title), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Project>(nameof(Description), this, oldValue, value));
					}
				
				}
				_description = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Project>(nameof(Description), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Project>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Project>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Project>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Project>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class ReportType : ReportTypeBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Id), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _categoryId;

		public int CategoryId
		{
			get => _categoryId;
			set
			{
				var changedSet = false;
				var oldValue = _categoryId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(CategoryId), this, oldValue, value));
					}
				
				}
				_categoryId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(CategoryId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(PersistenceKey), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Name), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Version), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ReportCategory _category;

		public ReportCategory Category
		{
			get => _category;
			set
			{
				var changedSet = false;
				var oldValue = _category;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Category), this, oldValue, value));
					}
				
				}
				_category = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(Category), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 FaultReportsCount { get; set; }
				
		protected RelatedList<ReportType,PersonReport> _faultReports;

		public RelatedList<ReportType,PersonReport> FaultReports
		{
			get
			{
				this._faultReports = this._faultReports ?? new RelatedList<ReportType,PersonReport>(this, nameof(FaultReports));
				
				return _faultReports;
			}
			set
			{
				var changedSet = false;
				var oldValue = _faultReports;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportType>(nameof(FaultReports), this, oldValue, value));
					}
				
				}
				_faultReports = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportType>(nameof(FaultReports), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class ReportRecommendation : ReportRecommendationBase, IEntity
	{
				
		protected int _reportId;

		public int ReportId
		{
			get => _reportId;
			set
			{
				var changedSet = false;
				var oldValue = _reportId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(ReportId), this, oldValue, value));
					}
				
				}
				_reportId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(ReportId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _recommendationId;

		public int RecommendationId
		{
			get => _recommendationId;
			set
			{
				var changedSet = false;
				var oldValue = _recommendationId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(RecommendationId), this, oldValue, value));
					}
				
				}
				_recommendationId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(RecommendationId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _notes;

		public string Notes
		{
			get => _notes;
			set
			{
				var changedSet = false;
				var oldValue = _notes;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Notes), this, oldValue, value));
					}
				
				}
				_notes = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Notes), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected PersonReport _personReport;

		public PersonReport PersonReport
		{
			get => _personReport;
			set
			{
				var changedSet = false;
				var oldValue = _personReport;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(PersonReport), this, oldValue, value));
					}
				
				}
				_personReport = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(PersonReport), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ReportDefaultRecommendation _recommendation;

		public ReportDefaultRecommendation Recommendation
		{
			get => _recommendation;
			set
			{
				var changedSet = false;
				var oldValue = _recommendation;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Recommendation), this, oldValue, value));
					}
				
				}
				_recommendation = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(Recommendation), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportRecommendation>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class ReportDefaultRecommendation : ReportDefaultRecommendationBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Name), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _text;

		public string Text
		{
			get => _text;
			set
			{
				var changedSet = false;
				var oldValue = _text;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Text), this, oldValue, value));
					}
				
				}
				_text = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Text), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 RecommendationsCount { get; set; }
				
		protected RelatedList<ReportDefaultRecommendation,ReportRecommendation> _recommendations;

		public RelatedList<ReportDefaultRecommendation,ReportRecommendation> Recommendations
		{
			get
			{
				this._recommendations = this._recommendations ?? new RelatedList<ReportDefaultRecommendation,ReportRecommendation>(this, nameof(Recommendations));
				
				return _recommendations;
			}
			set
			{
				var changedSet = false;
				var oldValue = _recommendations;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Recommendations), this, oldValue, value));
					}
				
				}
				_recommendations = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportDefaultRecommendation>(nameof(Recommendations), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class ReportCategory : ReportCategoryBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(Name), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ReportTypesCount { get; set; }
				
		protected RelatedList<ReportCategory,ReportType> _reportTypes;

		public RelatedList<ReportCategory,ReportType> ReportTypes
		{
			get
			{
				this._reportTypes = this._reportTypes ?? new RelatedList<ReportCategory,ReportType>(this, nameof(ReportTypes));
				
				return _reportTypes;
			}
			set
			{
				var changedSet = false;
				var oldValue = _reportTypes;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(ReportTypes), this, oldValue, value));
					}
				
				}
				_reportTypes = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportCategory>(nameof(ReportTypes), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class ReportActionsTaken : ReportActionsTakenBase, IEntity
	{
				
		protected int _faultReportId;

		public int FaultReportId
		{
			get => _faultReportId;
			set
			{
				var changedSet = false;
				var oldValue = _faultReportId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(FaultReportId), this, oldValue, value));
					}
				
				}
				_faultReportId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(FaultReportId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected string _notes;

		public string Notes
		{
			get => _notes;
			set
			{
				var changedSet = false;
				var oldValue = _notes;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(Notes), this, oldValue, value));
					}
				
				}
				_notes = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(Notes), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected PersonReport _personReport;

		public PersonReport PersonReport
		{
			get => _personReport;
			set
			{
				var changedSet = false;
				var oldValue = _personReport;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(PersonReport), this, oldValue, value));
					}
				
				}
				_personReport = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(PersonReport), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ReportActionsTaken>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		public override EntityValidationResult ValidateEntity()
		{
			var entity = this;
			var validationResult = new EntityValidationResult(this.GetType());
			validationResult.AddPropertyValidationResult(this.ValidateNotes());
			return validationResult;
		}
		public PropertyValidationResult ValidateNotes()
		{
			var validationResult = new PropertyValidationResult(this.GetType(), "Notes");
			var entity = this;
			if(!(true))
			{
				validationResult.AddFailure("Please enter some actions taken notes");
			
			}
			if(!(true))
			{
				validationResult.AddFailure("Please enter at least five characters for notes");
			
			}
			return validationResult;
		}
	
	}
}

namespace Tunnel.App.Data.Entities
{
	public class SiteDocument : SiteDocumentBase, IEntity
	{
				
		protected int _categoryId;

		public int CategoryId
		{
			get => _categoryId;
			set
			{
				var changedSet = false;
				var oldValue = _categoryId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(CategoryId), this, oldValue, value));
					}
				
				}
				_categoryId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(CategoryId), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected int _siteId;

		public int SiteId
		{
			get => _siteId;
			set
			{
				var changedSet = false;
				var oldValue = _siteId;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(SiteId), this, oldValue, value));
					}
				
				}
				_siteId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(SiteId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Title), this, oldValue, value));
					}
				
				}
				_title = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Title), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected DocumentCategory _category;

		public DocumentCategory Category
		{
			get => _category;
			set
			{
				var changedSet = false;
				var oldValue = _category;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Category), this, oldValue, value));
					}
				
				}
				_category = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Category), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Site _site;

		public Site Site
		{
			get => _site;
			set
			{
				var changedSet = false;
				var oldValue = _site;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Site), this, oldValue, value));
					}
				
				}
				_site = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(Site), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<SiteDocument>(nameof(CreatedByUser), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class DocumentCategory : DocumentCategoryBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Name), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 DocumentsCount { get; set; }
				
		protected RelatedList<DocumentCategory,SiteDocument> _documents;

		public RelatedList<DocumentCategory,SiteDocument> Documents
		{
			get
			{
				this._documents = this._documents ?? new RelatedList<DocumentCategory,SiteDocument>(this, nameof(Documents));
				
				return _documents;
			}
			set
			{
				var changedSet = false;
				var oldValue = _documents;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Documents), this, oldValue, value));
					}
				
				}
				_documents = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<DocumentCategory>(nameof(Documents), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class ClientType : ClientTypeBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ClientType>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ClientType>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ClientType>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ClientType>(nameof(Name), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ClientsCount { get; set; }
				
		protected RelatedList<ClientType,Client> _clients;

		public RelatedList<ClientType,Client> Clients
		{
			get
			{
				this._clients = this._clients ?? new RelatedList<ClientType,Client>(this, nameof(Clients));
				
				return _clients;
			}
			set
			{
				var changedSet = false;
				var oldValue = _clients;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ClientType>(nameof(Clients), this, oldValue, value));
					}
				
				}
				_clients = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ClientType>(nameof(Clients), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class Client : ClientBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(TypeId), this, oldValue, value));
					}
				
				}
				_typeId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(TypeId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(CreatedByUserId), this, oldValue, value));
					}
				
				}
				_createdByUserId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(CreatedByUserId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(Name), this, oldValue, value));
					}
				
				}
				_name = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(Name), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(Description), this, oldValue, value));
					}
				
				}
				_description = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(Description), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(Guid), this, oldValue, value));
					}
				
				}
				_guid = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(Guid), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(CreatedDate), this, oldValue, value));
					}
				
				}
				_createdDate = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(CreatedDate), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(Version), this, oldValue, value));
					}
				
				}
				_version = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(Version), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
				_persistenceKey = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(PersistenceKey), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 UsersCount { get; set; }
				
		protected RelatedList<Client,ApplicationUser> _users;

		public RelatedList<Client,ApplicationUser> Users
		{
			get
			{
				this._users = this._users ?? new RelatedList<Client,ApplicationUser>(this, nameof(Users));
				
				return _users;
			}
			set
			{
				var changedSet = false;
				var oldValue = _users;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(Users), this, oldValue, value));
					}
				
				}
				_users = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(Users), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ClientType _type;

		public ClientType Type
		{
			get => _type;
			set
			{
				var changedSet = false;
				var oldValue = _type;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(Type), this, oldValue, value));
					}
				
				}
				_type = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(Type), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected ApplicationUser _createdByUser;

		public ApplicationUser CreatedByUser
		{
			get => _createdByUser;
			set
			{
				var changedSet = false;
				var oldValue = _createdByUser;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
				_createdByUser = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(CreatedByUser), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PeopleCount { get; set; }
				
		protected RelatedList<Client,Person> _people;

		public RelatedList<Client,Person> People
		{
			get
			{
				this._people = this._people ?? new RelatedList<Client,Person>(this, nameof(People));
				
				return _people;
			}
			set
			{
				var changedSet = false;
				var oldValue = _people;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(People), this, oldValue, value));
					}
				
				}
				_people = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(People), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 SitesCount { get; set; }
				
		protected RelatedList<Client,Site> _sites;

		public RelatedList<Client,Site> Sites
		{
			get
			{
				this._sites = this._sites ?? new RelatedList<Client,Site>(this, nameof(Sites));
				
				return _sites;
			}
			set
			{
				var changedSet = false;
				var oldValue = _sites;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<Client>(nameof(Sites), this, oldValue, value));
					}
				
				}
				_sites = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<Client>(nameof(Sites), this, oldValue, value));
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

namespace Tunnel.App.Data.Entities
{
	public class ApplicationUser : ApplicationUserBase, IEntity
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(Id), this, oldValue, value));
					}
				
				}
				_id = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(Id), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(ClientId), this, oldValue, value));
					}
				
				}
				_clientId = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(ClientId), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(Email), this, oldValue, value));
					}
				
				}
				_email = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(Email), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FullName), this, oldValue, value));
					}
				
				}
				_fullName = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FullName), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(EmailConfirmed), this, oldValue, value));
					}
				
				}
				_emailConfirmed = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(EmailConfirmed), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected UserType _userType;

		public UserType UserType
		{
			get => _userType;
			set
			{
				var changedSet = false;
				var oldValue = _userType;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(UserType), this, oldValue, value));
					}
				
				}
				_userType = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(UserType), this, oldValue, value));
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
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(IsLockedOut), this, oldValue, value));
					}
				
				}
				_isLockedOut = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(IsLockedOut), this, oldValue, value));
					}
				
				}
			}
		}

				
		protected Client _client;

		public Client Client
		{
			get => _client;
			set
			{
				var changedSet = false;
				var oldValue = _client;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(Client), this, oldValue, value));
					}
				
				}
				_client = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(Client), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ClientsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,Client> _clientsCreated;

		public RelatedList<ApplicationUser,Client> ClientsCreated
		{
			get
			{
				this._clientsCreated = this._clientsCreated ?? new RelatedList<ApplicationUser,Client>(this, nameof(ClientsCreated));
				
				return _clientsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _clientsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(ClientsCreated), this, oldValue, value));
					}
				
				}
				_clientsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(ClientsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 DocumentCategoriesCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,DocumentCategory> _documentCategoriesCreated;

		public RelatedList<ApplicationUser,DocumentCategory> DocumentCategoriesCreated
		{
			get
			{
				this._documentCategoriesCreated = this._documentCategoriesCreated ?? new RelatedList<ApplicationUser,DocumentCategory>(this, nameof(DocumentCategoriesCreated));
				
				return _documentCategoriesCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _documentCategoriesCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(DocumentCategoriesCreated), this, oldValue, value));
					}
				
				}
				_documentCategoriesCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(DocumentCategoriesCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 SiteDocumentsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,SiteDocument> _siteDocumentsCreated;

		public RelatedList<ApplicationUser,SiteDocument> SiteDocumentsCreated
		{
			get
			{
				this._siteDocumentsCreated = this._siteDocumentsCreated ?? new RelatedList<ApplicationUser,SiteDocument>(this, nameof(SiteDocumentsCreated));
				
				return _siteDocumentsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _siteDocumentsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(SiteDocumentsCreated), this, oldValue, value));
					}
				
				}
				_siteDocumentsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(SiteDocumentsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 FaultActionsTakenCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,ReportActionsTaken> _faultActionsTakenCreated;

		public RelatedList<ApplicationUser,ReportActionsTaken> FaultActionsTakenCreated
		{
			get
			{
				this._faultActionsTakenCreated = this._faultActionsTakenCreated ?? new RelatedList<ApplicationUser,ReportActionsTaken>(this, nameof(FaultActionsTakenCreated));
				
				return _faultActionsTakenCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _faultActionsTakenCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultActionsTakenCreated), this, oldValue, value));
					}
				
				}
				_faultActionsTakenCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultActionsTakenCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 FaultCategoriesCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,ReportCategory> _faultCategoriesCreated;

		public RelatedList<ApplicationUser,ReportCategory> FaultCategoriesCreated
		{
			get
			{
				this._faultCategoriesCreated = this._faultCategoriesCreated ?? new RelatedList<ApplicationUser,ReportCategory>(this, nameof(FaultCategoriesCreated));
				
				return _faultCategoriesCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _faultCategoriesCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultCategoriesCreated), this, oldValue, value));
					}
				
				}
				_faultCategoriesCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultCategoriesCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 FaultDefaultRecommendationsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,ReportDefaultRecommendation> _faultDefaultRecommendationsCreated;

		public RelatedList<ApplicationUser,ReportDefaultRecommendation> FaultDefaultRecommendationsCreated
		{
			get
			{
				this._faultDefaultRecommendationsCreated = this._faultDefaultRecommendationsCreated ?? new RelatedList<ApplicationUser,ReportDefaultRecommendation>(this, nameof(FaultDefaultRecommendationsCreated));
				
				return _faultDefaultRecommendationsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _faultDefaultRecommendationsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultDefaultRecommendationsCreated), this, oldValue, value));
					}
				
				}
				_faultDefaultRecommendationsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultDefaultRecommendationsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 FaultRecommendationsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,ReportRecommendation> _faultRecommendationsCreated;

		public RelatedList<ApplicationUser,ReportRecommendation> FaultRecommendationsCreated
		{
			get
			{
				this._faultRecommendationsCreated = this._faultRecommendationsCreated ?? new RelatedList<ApplicationUser,ReportRecommendation>(this, nameof(FaultRecommendationsCreated));
				
				return _faultRecommendationsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _faultRecommendationsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultRecommendationsCreated), this, oldValue, value));
					}
				
				}
				_faultRecommendationsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultRecommendationsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 FaultTypesCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,ReportType> _faultTypesCreated;

		public RelatedList<ApplicationUser,ReportType> FaultTypesCreated
		{
			get
			{
				this._faultTypesCreated = this._faultTypesCreated ?? new RelatedList<ApplicationUser,ReportType>(this, nameof(FaultTypesCreated));
				
				return _faultTypesCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _faultTypesCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultTypesCreated), this, oldValue, value));
					}
				
				}
				_faultTypesCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultTypesCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ProjectCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,Project> _projectCreated;

		public RelatedList<ApplicationUser,Project> ProjectCreated
		{
			get
			{
				this._projectCreated = this._projectCreated ?? new RelatedList<ApplicationUser,Project>(this, nameof(ProjectCreated));
				
				return _projectCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _projectCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(ProjectCreated), this, oldValue, value));
					}
				
				}
				_projectCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(ProjectCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 ReportReceiverEmailAddressesCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,ReportReceiverEmailAddress> _reportReceiverEmailAddressesCreated;

		public RelatedList<ApplicationUser,ReportReceiverEmailAddress> ReportReceiverEmailAddressesCreated
		{
			get
			{
				this._reportReceiverEmailAddressesCreated = this._reportReceiverEmailAddressesCreated ?? new RelatedList<ApplicationUser,ReportReceiverEmailAddress>(this, nameof(ReportReceiverEmailAddressesCreated));
				
				return _reportReceiverEmailAddressesCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _reportReceiverEmailAddressesCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(ReportReceiverEmailAddressesCreated), this, oldValue, value));
					}
				
				}
				_reportReceiverEmailAddressesCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(ReportReceiverEmailAddressesCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 RiskAssessmentsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,RiskAssessment> _riskAssessmentsCreated;

		public RelatedList<ApplicationUser,RiskAssessment> RiskAssessmentsCreated
		{
			get
			{
				this._riskAssessmentsCreated = this._riskAssessmentsCreated ?? new RelatedList<ApplicationUser,RiskAssessment>(this, nameof(RiskAssessmentsCreated));
				
				return _riskAssessmentsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _riskAssessmentsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentsCreated), this, oldValue, value));
					}
				
				}
				_riskAssessmentsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 RiskAssessmentAnswersCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,RiskAssessmentAnswer> _riskAssessmentAnswersCreated;

		public RelatedList<ApplicationUser,RiskAssessmentAnswer> RiskAssessmentAnswersCreated
		{
			get
			{
				this._riskAssessmentAnswersCreated = this._riskAssessmentAnswersCreated ?? new RelatedList<ApplicationUser,RiskAssessmentAnswer>(this, nameof(RiskAssessmentAnswersCreated));
				
				return _riskAssessmentAnswersCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _riskAssessmentAnswersCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentAnswersCreated), this, oldValue, value));
					}
				
				}
				_riskAssessmentAnswersCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentAnswersCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 RiskAssessmentQuestionsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,RiskAssessmentQuestion> _riskAssessmentQuestionsCreated;

		public RelatedList<ApplicationUser,RiskAssessmentQuestion> RiskAssessmentQuestionsCreated
		{
			get
			{
				this._riskAssessmentQuestionsCreated = this._riskAssessmentQuestionsCreated ?? new RelatedList<ApplicationUser,RiskAssessmentQuestion>(this, nameof(RiskAssessmentQuestionsCreated));
				
				return _riskAssessmentQuestionsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _riskAssessmentQuestionsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentQuestionsCreated), this, oldValue, value));
					}
				
				}
				_riskAssessmentQuestionsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(RiskAssessmentQuestionsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PeopleCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,Person> _peopleCreated;

		public RelatedList<ApplicationUser,Person> PeopleCreated
		{
			get
			{
				this._peopleCreated = this._peopleCreated ?? new RelatedList<ApplicationUser,Person>(this, nameof(PeopleCreated));
				
				return _peopleCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _peopleCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(PeopleCreated), this, oldValue, value));
					}
				
				}
				_peopleCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(PeopleCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PersonInspectionsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,PersonInspection> _personInspectionsCreated;

		public RelatedList<ApplicationUser,PersonInspection> PersonInspectionsCreated
		{
			get
			{
				this._personInspectionsCreated = this._personInspectionsCreated ?? new RelatedList<ApplicationUser,PersonInspection>(this, nameof(PersonInspectionsCreated));
				
				return _personInspectionsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _personInspectionsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(PersonInspectionsCreated), this, oldValue, value));
					}
				
				}
				_personInspectionsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(PersonInspectionsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PersonLoadingsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,PersonLoading> _personLoadingsCreated;

		public RelatedList<ApplicationUser,PersonLoading> PersonLoadingsCreated
		{
			get
			{
				this._personLoadingsCreated = this._personLoadingsCreated ?? new RelatedList<ApplicationUser,PersonLoading>(this, nameof(PersonLoadingsCreated));
				
				return _personLoadingsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _personLoadingsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(PersonLoadingsCreated), this, oldValue, value));
					}
				
				}
				_personLoadingsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(PersonLoadingsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 PersonTypesCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,PersonType> _personTypesCreated;

		public RelatedList<ApplicationUser,PersonType> PersonTypesCreated
		{
			get
			{
				this._personTypesCreated = this._personTypesCreated ?? new RelatedList<ApplicationUser,PersonType>(this, nameof(PersonTypesCreated));
				
				return _personTypesCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _personTypesCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(PersonTypesCreated), this, oldValue, value));
					}
				
				}
				_personTypesCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(PersonTypesCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 FaultReportsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,PersonReport> _faultReportsCreated;

		public RelatedList<ApplicationUser,PersonReport> FaultReportsCreated
		{
			get
			{
				this._faultReportsCreated = this._faultReportsCreated ?? new RelatedList<ApplicationUser,PersonReport>(this, nameof(FaultReportsCreated));
				
				return _faultReportsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _faultReportsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultReportsCreated), this, oldValue, value));
					}
				
				}
				_faultReportsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(FaultReportsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 SitesCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,Site> _sitesCreated;

		public RelatedList<ApplicationUser,Site> SitesCreated
		{
			get
			{
				this._sitesCreated = this._sitesCreated ?? new RelatedList<ApplicationUser,Site>(this, nameof(SitesCreated));
				
				return _sitesCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _sitesCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(SitesCreated), this, oldValue, value));
					}
				
				}
				_sitesCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(SitesCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 SiteInspectionsCreatedCount { get; set; }
				
		protected RelatedList<ApplicationUser,SiteInspection> _siteInspectionsCreated;

		public RelatedList<ApplicationUser,SiteInspection> SiteInspectionsCreated
		{
			get
			{
				this._siteInspectionsCreated = this._siteInspectionsCreated ?? new RelatedList<ApplicationUser,SiteInspection>(this, nameof(SiteInspectionsCreated));
				
				return _siteInspectionsCreated;
			}
			set
			{
				var changedSet = false;
				var oldValue = _siteInspectionsCreated;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(SiteInspectionsCreated), this, oldValue, value));
					}
				
				}
				_siteInspectionsCreated = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(SiteInspectionsCreated), this, oldValue, value));
					}
				
				}
			}
		}

		
		public Int64 SitesCount { get; set; }
				
		protected RelatedList<ApplicationUser,UserSite> _sites;

		public RelatedList<ApplicationUser,UserSite> Sites
		{
			get
			{
				this._sites = this._sites ?? new RelatedList<ApplicationUser,UserSite>(this, nameof(Sites));
				
				return _sites;
			}
			set
			{
				var changedSet = false;
				var oldValue = _sites;
				var changed = false;
				if(_propertyChangingSet)
				{
					changed = value != oldValue;
					changedSet = true;
					if(changed)
					{
						this.PropertyChanging.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(Sites), this, oldValue, value));
					}
				
				}
				_sites = value;
				if(_propertyChangedSet)
				{
					if(!(changedSet))
					{
						changed = value != oldValue;
					
					}
					if(changed)
					{
						this.PropertyChanged.Emit(() => new PropertyChangeEvent<ApplicationUser>(nameof(Sites), this, oldValue, value));
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

