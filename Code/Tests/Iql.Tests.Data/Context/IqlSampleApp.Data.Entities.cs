using Iql.Data;
using IqlSampleApp.Sets;
using IqlSampleApp.ApiContext.Base;
using Iql.Entities.PropertyChangers;
using Iql.Data.Lists;
using Iql;
using System;
using Iql.Entities.Events;
using Iql.Data.Events;
namespace IqlSampleApp.Data.Entities
{
    public class UserSiteBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "UserSiteBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class UserSettingBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "UserSettingBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class SiteInspectionBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "SiteInspectionBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class SiteAreaBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "SiteAreaBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class SiteBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "SiteBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonReportBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "PersonReportBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonTypeMapBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "PersonTypeMapBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonTypeBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "PersonTypeBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonLoadingBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "PersonLoadingBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonInspectionBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "PersonInspectionBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "PersonBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentQuestionBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "RiskAssessmentQuestionBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentAnswerBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "RiskAssessmentAnswerBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentSolutionBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "RiskAssessmentSolutionBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "RiskAssessmentBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportReceiverEmailAddressBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ReportReceiverEmailAddressBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ProjectBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ProjectBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportTypeBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ReportTypeBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportRecommendationBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ReportRecommendationBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportDefaultRecommendationBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ReportDefaultRecommendationBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportCategoryBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ReportCategoryBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportActionsTakenBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ReportActionsTakenBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class SiteDocumentBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "SiteDocumentBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class DocumentCategoryBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "DocumentCategoryBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ClientTypeBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ClientTypeBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ClientBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ClientBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ApplicationLogBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ApplicationLogBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ApplicationUserBase: IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanging;
        public EventEmitter<IPropertyChangeEvent>PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent>_propertyChanged;
        public EventEmitter<IPropertyChangeEvent>PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent>_existsChanged;
        public EventEmitter<ExistsChangeEvent>ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }
        public static String ClassName
        {
            get;
            set;
        } = "ApplicationUserBase";
    }
}
namespace IqlSampleApp.Data.Entities
{
    public enum FaultReportStatus
    {
        Fail = 0,
        PassWithObservations = 1
    }
}
namespace IqlSampleApp.Data.Entities
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
namespace IqlSampleApp.Data.Entities
{
    public enum PersonInspectionStatus
    {
        Pass = 1,
        Fail = 2,
        PassWithObservations = 3
    }
}
namespace IqlSampleApp.Data.Entities
{
    public enum PersonCategory
    {
        System = 0,
        Conventional = 1,
        AutoDescription = 2
    }
}
namespace IqlSampleApp.Data.Entities
{
    [Flags] public enum PersonSkills
    {
        Chef = 1,
        Coder = 2,
        Ninja = 4
    }
}
namespace IqlSampleApp.Data.Entities
{
    public enum UserType
    {
        Super = 1,
        Client = 2,
        Candidate = 3
    }
}
namespace IqlSampleApp.Data.Entities
{
    [Flags] public enum UserPermissions
    {
        Read = 1,
        Create = 2,
        Delete = 4,
        Edit = 8
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class UserSite: UserSiteBase,
    IEntity
    {
        protected int _siteId;
        public int SiteId
        {
            get => _siteId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteId", _siteId, value, _propertyChanging, _propertyChanged, newValue => this._siteId = newValue);
            }
        }
        protected string _userId;
        public string UserId
        {
            get => _userId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "UserId", _userId, value, _propertyChanging, _propertyChanged, newValue => this._userId = newValue);
            }
        }
        protected ApplicationUser _user;
        public ApplicationUser User
        {
            get => _user;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "User", _user, value, _propertyChanging, _propertyChanged, newValue => this._user = newValue);
            }
        }
        protected Site _site;
        public Site Site
        {
            get => _site;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Site", _site, value, _propertyChanging, _propertyChanged, newValue => this._site = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class UserSetting: UserSettingBase,
    IEntity
    {
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _key1;
        public string Key1
        {
            get => _key1;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Key1", _key1, value, _propertyChanging, _propertyChanged, newValue => this._key1 = newValue);
            }
        }
        protected Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _userId;
        public string UserId
        {
            get => _userId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "UserId", _userId, value, _propertyChanging, _propertyChanged, newValue => this._userId = newValue);
            }
        }
        protected string _key2;
        public string Key2
        {
            get => _key2;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Key2", _key2, value, _propertyChanging, _propertyChanged, newValue => this._key2 = newValue);
            }
        }
        protected string _key3;
        public string Key3
        {
            get => _key3;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Key3", _key3, value, _propertyChanging, _propertyChanged, newValue => this._key3 = newValue);
            }
        }
        protected string _key4;
        public string Key4
        {
            get => _key4;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Key4", _key4, value, _propertyChanging, _propertyChanged, newValue => this._key4 = newValue);
            }
        }
        protected string _value;
        public string Value
        {
            get => _value;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Value", _value, value, _propertyChanging, _propertyChanged, newValue => this._value = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        protected ApplicationUser _user;
        public ApplicationUser User
        {
            get => _user;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "User", _user, value, _propertyChanging, _propertyChanged, newValue => this._user = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class SiteInspection: SiteInspectionBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected int _siteId;
        public int SiteId
        {
            get => _siteId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteId", _siteId, value, _propertyChanging, _propertyChanged, newValue => this._siteId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected DateTimeOffset _startTime;
        public DateTimeOffset StartTime
        {
            get => _startTime;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "StartTime", _startTime, value, _propertyChanging, _propertyChanged, newValue => this._startTime = newValue);
            }
        }
        protected DateTimeOffset _endTime;
        public DateTimeOffset EndTime
        {
            get => _endTime;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "EndTime", _endTime, value, _propertyChanging, _propertyChanged, newValue => this._endTime = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        public Int64 RiskAssessmentsCount
        {
            get;
            set;
        }
        protected RelatedList<SiteInspection, RiskAssessment>_riskAssessments;
        public RelatedList<SiteInspection, RiskAssessment>RiskAssessments
        {
            get
            {
                this._riskAssessments = this._riskAssessments ?? new RelatedList<SiteInspection, RiskAssessment>(this, nameof(RiskAssessments));
                return _riskAssessments;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RiskAssessments", _riskAssessments, value, _propertyChanging, _propertyChanged, newValue => this._riskAssessments = newValue);
            }
        }
        public Int64 PersonInspectionsCount
        {
            get;
            set;
        }
        protected RelatedList<SiteInspection, PersonInspection>_personInspections;
        public RelatedList<SiteInspection, PersonInspection>PersonInspections
        {
            get
            {
                this._personInspections = this._personInspections ?? new RelatedList<SiteInspection, PersonInspection>(this, nameof(PersonInspections));
                return _personInspections;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonInspections", _personInspections, value, _propertyChanging, _propertyChanged, newValue => this._personInspections = newValue);
            }
        }
        protected Site _site;
        public Site Site
        {
            get => _site;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Site", _site, value, _propertyChanging, _propertyChanged, newValue => this._site = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class SiteArea: SiteAreaBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected int _siteId;
        public int SiteId
        {
            get => _siteId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteId", _siteId, value, _propertyChanging, _propertyChanged, newValue => this._siteId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        public Int64 PeopleCount
        {
            get;
            set;
        }
        protected RelatedList<SiteArea, Person>_people;
        public RelatedList<SiteArea, Person>People
        {
            get
            {
                this._people = this._people ?? new RelatedList<SiteArea, Person>(this, nameof(People));
                return _people;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "People", _people, value, _propertyChanging, _propertyChanged, newValue => this._people = newValue);
            }
        }
        protected Site _site;
        public Site Site
        {
            get => _site;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Site", _site, value, _propertyChanging, _propertyChanged, newValue => this._site = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class Site: SiteBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected IqlPointExpression _location;
        public IqlPointExpression Location
        {
            get => _location;
            set
            {
                PointPropertyChanger.Instance.ChangeProperty(this, "Location", _location, value, _propertyChanging, _propertyChanged, newValue => this._location = newValue);
            }
        }
        protected IqlPolygonExpression _area;
        public IqlPolygonExpression Area
        {
            get => _area;
            set
            {
                PolygonPropertyChanger.Instance.ChangeProperty(this, "Area", _area, value, _propertyChanging, _propertyChanged, newValue => this._area = newValue);
            }
        }
        protected IqlLineExpression _line;
        public IqlLineExpression Line
        {
            get => _line;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Line", _line, value, _propertyChanging, _propertyChanged, newValue => this._line = newValue);
            }
        }
        protected int ? _parentId;
        public int ? ParentId
        {
            get => _parentId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ParentId", _parentId, value, _propertyChanging, _propertyChanged, newValue => this._parentId = newValue);
            }
        }
        protected int ? _clientId;
        public int ? ClientId
        {
            get => _clientId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ClientId", _clientId, value, _propertyChanging, _propertyChanged, newValue => this._clientId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _fullAddress;
        public string FullAddress
        {
            get => _fullAddress;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FullAddress", _fullAddress, value, _propertyChanging, _propertyChanged, newValue => this._fullAddress = newValue);
            }
        }
        protected string _address;
        public string Address
        {
            get => _address;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Address", _address, value, _propertyChanging, _propertyChanged, newValue => this._address = newValue);
            }
        }
        protected string _postCode;
        public string PostCode
        {
            get => _postCode;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PostCode", _postCode, value, _propertyChanging, _propertyChanged, newValue => this._postCode = newValue);
            }
        }
        protected string _key;
        public string Key
        {
            get => _key;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Key", _key, value, _propertyChanging, _propertyChanged, newValue => this._key = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        protected int _left;
        public int Left
        {
            get => _left;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Left", _left, value, _propertyChanging, _propertyChanged, newValue => this._left = newValue);
            }
        }
        protected int _right;
        public int Right
        {
            get => _right;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Right", _right, value, _propertyChanging, _propertyChanged, newValue => this._right = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        public Int64 DocumentsCount
        {
            get;
            set;
        }
        protected RelatedList<Site, SiteDocument>_documents;
        public RelatedList<Site, SiteDocument>Documents
        {
            get
            {
                this._documents = this._documents ?? new RelatedList<Site, SiteDocument>(this, nameof(Documents));
                return _documents;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Documents", _documents, value, _propertyChanging, _propertyChanged, newValue => this._documents = newValue);
            }
        }
        public Int64 AdditionalSendReportsToCount
        {
            get;
            set;
        }
        protected RelatedList<Site, ReportReceiverEmailAddress>_additionalSendReportsTo;
        public RelatedList<Site, ReportReceiverEmailAddress>AdditionalSendReportsTo
        {
            get
            {
                this._additionalSendReportsTo = this._additionalSendReportsTo ?? new RelatedList<Site, ReportReceiverEmailAddress>(this, nameof(AdditionalSendReportsTo));
                return _additionalSendReportsTo;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "AdditionalSendReportsTo", _additionalSendReportsTo, value, _propertyChanging, _propertyChanged, newValue => this._additionalSendReportsTo = newValue);
            }
        }
        public Int64 PeopleCount
        {
            get;
            set;
        }
        protected RelatedList<Site, Person>_people;
        public RelatedList<Site, Person>People
        {
            get
            {
                this._people = this._people ?? new RelatedList<Site, Person>(this, nameof(People));
                return _people;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "People", _people, value, _propertyChanging, _propertyChanged, newValue => this._people = newValue);
            }
        }
        protected Site _parent;
        public Site Parent
        {
            get => _parent;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Parent", _parent, value, _propertyChanging, _propertyChanged, newValue => this._parent = newValue);
            }
        }
        public Int64 ChildrenCount
        {
            get;
            set;
        }
        protected RelatedList<Site, Site>_children;
        public RelatedList<Site, Site>Children
        {
            get
            {
                this._children = this._children ?? new RelatedList<Site, Site>(this, nameof(Children));
                return _children;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Children", _children, value, _propertyChanging, _propertyChanged, newValue => this._children = newValue);
            }
        }
        protected Client _client;
        public Client Client
        {
            get => _client;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Client", _client, value, _propertyChanging, _propertyChanged, newValue => this._client = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 AreasCount
        {
            get;
            set;
        }
        protected RelatedList<Site, SiteArea>_areas;
        public RelatedList<Site, SiteArea>Areas
        {
            get
            {
                this._areas = this._areas ?? new RelatedList<Site, SiteArea>(this, nameof(Areas));
                return _areas;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Areas", _areas, value, _propertyChanging, _propertyChanged, newValue => this._areas = newValue);
            }
        }
        public Int64 SiteInspectionsCount
        {
            get;
            set;
        }
        protected RelatedList<Site, SiteInspection>_siteInspections;
        public RelatedList<Site, SiteInspection>SiteInspections
        {
            get
            {
                this._siteInspections = this._siteInspections ?? new RelatedList<Site, SiteInspection>(this, nameof(SiteInspections));
                return _siteInspections;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteInspections", _siteInspections, value, _propertyChanging, _propertyChanged, newValue => this._siteInspections = newValue);
            }
        }
        public Int64 UsersCount
        {
            get;
            set;
        }
        protected RelatedList<Site, UserSite>_users;
        public RelatedList<Site, UserSite>Users
        {
            get
            {
                this._users = this._users ?? new RelatedList<Site, UserSite>(this, nameof(Users));
                return _users;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Users", _users, value, _propertyChanging, _propertyChanged, newValue => this._users = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonReport: PersonReportBase,
    IEntity
    {
        protected int _personId;
        public int PersonId
        {
            get => _personId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonId", _personId, value, _propertyChanging, _propertyChanged, newValue => this._personId = newValue);
            }
        }
        protected int _typeId;
        public int TypeId
        {
            get => _typeId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "TypeId", _typeId, value, _propertyChanging, _propertyChanged, newValue => this._typeId = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _title;
        public string Title
        {
            get => _title;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Title", _title, value, _propertyChanging, _propertyChanged, newValue => this._title = newValue);
            }
        }
        protected FaultReportStatus _status;
        public FaultReportStatus Status
        {
            get => _status;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Status", _status, value, _propertyChanging, _propertyChanged, newValue => this._status = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        public Int64 ActionsTakenCount
        {
            get;
            set;
        }
        protected RelatedList<PersonReport, ReportActionsTaken>_actionsTaken;
        public RelatedList<PersonReport, ReportActionsTaken>ActionsTaken
        {
            get
            {
                this._actionsTaken = this._actionsTaken ?? new RelatedList<PersonReport, ReportActionsTaken>(this, nameof(ActionsTaken));
                return _actionsTaken;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ActionsTaken", _actionsTaken, value, _propertyChanging, _propertyChanged, newValue => this._actionsTaken = newValue);
            }
        }
        public Int64 RecommendationsCount
        {
            get;
            set;
        }
        protected RelatedList<PersonReport, ReportRecommendation>_recommendations;
        public RelatedList<PersonReport, ReportRecommendation>Recommendations
        {
            get
            {
                this._recommendations = this._recommendations ?? new RelatedList<PersonReport, ReportRecommendation>(this, nameof(Recommendations));
                return _recommendations;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Recommendations", _recommendations, value, _propertyChanging, _propertyChanged, newValue => this._recommendations = newValue);
            }
        }
        protected Person _person;
        public Person Person
        {
            get => _person;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Person", _person, value, _propertyChanging, _propertyChanged, newValue => this._person = newValue);
            }
        }
        protected ReportType _type;
        public ReportType Type
        {
            get => _type;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Type", _type, value, _propertyChanging, _propertyChanged, newValue => this._type = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonTypeMap: PersonTypeMapBase,
    IEntity
    {
        protected int _personId;
        public int PersonId
        {
            get => _personId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonId", _personId, value, _propertyChanging, _propertyChanged, newValue => this._personId = newValue);
            }
        }
        protected int _typeId;
        public int TypeId
        {
            get => _typeId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "TypeId", _typeId, value, _propertyChanging, _propertyChanged, newValue => this._typeId = newValue);
            }
        }
        protected string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Notes", _notes, value, _propertyChanging, _propertyChanged, newValue => this._notes = newValue);
            }
        }
        protected string _description;
        public string Description
        {
            get => _description;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Description", _description, value, _propertyChanging, _propertyChanged, newValue => this._description = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected Person _person;
        public Person Person
        {
            get => _person;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Person", _person, value, _propertyChanging, _propertyChanged, newValue => this._person = newValue);
            }
        }
        protected PersonType _type;
        public PersonType Type
        {
            get => _type;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Type", _type, value, _propertyChanging, _propertyChanged, newValue => this._type = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonType: PersonTypeBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _title;
        public string Title
        {
            get => _title;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Title", _title, value, _propertyChanging, _propertyChanged, newValue => this._title = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        public Int64 PeopleCount
        {
            get;
            set;
        }
        protected RelatedList<PersonType, Person>_people;
        public RelatedList<PersonType, Person>People
        {
            get
            {
                this._people = this._people ?? new RelatedList<PersonType, Person>(this, nameof(People));
                return _people;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "People", _people, value, _propertyChanging, _propertyChanged, newValue => this._people = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 PeopleMapCount
        {
            get;
            set;
        }
        protected RelatedList<PersonType, PersonTypeMap>_peopleMap;
        public RelatedList<PersonType, PersonTypeMap>PeopleMap
        {
            get
            {
                this._peopleMap = this._peopleMap ?? new RelatedList<PersonType, PersonTypeMap>(this, nameof(PeopleMap));
                return _peopleMap;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PeopleMap", _peopleMap, value, _propertyChanging, _propertyChanged, newValue => this._peopleMap = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonLoading: PersonLoadingBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        public Int64 PeopleCount
        {
            get;
            set;
        }
        protected RelatedList<PersonLoading, Person>_people;
        public RelatedList<PersonLoading, Person>People
        {
            get
            {
                this._people = this._people ?? new RelatedList<PersonLoading, Person>(this, nameof(People));
                return _people;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "People", _people, value, _propertyChanging, _propertyChanged, newValue => this._people = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class PersonInspection: PersonInspectionBase,
    IEntity
    {
        protected int _siteInspectionId;
        public int SiteInspectionId
        {
            get => _siteInspectionId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteInspectionId", _siteInspectionId, value, _propertyChanging, _propertyChanged, newValue => this._siteInspectionId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected int _personId;
        public int PersonId
        {
            get => _personId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonId", _personId, value, _propertyChanging, _propertyChanged, newValue => this._personId = newValue);
            }
        }
        protected PersonInspectionStatus _inspectionStatus;
        public PersonInspectionStatus InspectionStatus
        {
            get => _inspectionStatus;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "InspectionStatus", _inspectionStatus, value, _propertyChanging, _propertyChanged, newValue => this._inspectionStatus = newValue);
            }
        }
        protected DateTimeOffset _startTime;
        public DateTimeOffset StartTime
        {
            get => _startTime;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "StartTime", _startTime, value, _propertyChanging, _propertyChanged, newValue => this._startTime = newValue);
            }
        }
        protected DateTimeOffset _endTime;
        public DateTimeOffset EndTime
        {
            get => _endTime;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "EndTime", _endTime, value, _propertyChanging, _propertyChanged, newValue => this._endTime = newValue);
            }
        }
        protected InspectionFailReason _reasonForFailure;
        public InspectionFailReason ReasonForFailure
        {
            get => _reasonForFailure;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ReasonForFailure", _reasonForFailure, value, _propertyChanging, _propertyChanged, newValue => this._reasonForFailure = newValue);
            }
        }
        protected bool _isDesignRequired;
        public bool IsDesignRequired
        {
            get => _isDesignRequired;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "IsDesignRequired", _isDesignRequired, value, _propertyChanging, _propertyChanged, newValue => this._isDesignRequired = newValue);
            }
        }
        protected string _drawingNumber;
        public string DrawingNumber
        {
            get => _drawingNumber;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "DrawingNumber", _drawingNumber, value, _propertyChanging, _propertyChanged, newValue => this._drawingNumber = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected SiteInspection _siteInspection;
        public SiteInspection SiteInspection
        {
            get => _siteInspection;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteInspection", _siteInspection, value, _propertyChanging, _propertyChanged, newValue => this._siteInspection = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class Person: PersonBase,
    IEntity
    {
        protected IqlPointExpression _location;
        public IqlPointExpression Location
        {
            get => _location;
            set
            {
                PointPropertyChanger.Instance.ChangeProperty(this, "Location", _location, value, _propertyChanging, _propertyChanged, newValue => this._location = newValue);
            }
        }
        protected int ? _clientId;
        public int ? ClientId
        {
            get => _clientId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ClientId", _clientId, value, _propertyChanging, _propertyChanged, newValue => this._clientId = newValue);
            }
        }
        protected int ? _siteId;
        public int ? SiteId
        {
            get => _siteId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteId", _siteId, value, _propertyChanging, _propertyChanged, newValue => this._siteId = newValue);
            }
        }
        protected int ? _siteAreaId;
        public int ? SiteAreaId
        {
            get => _siteAreaId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteAreaId", _siteAreaId, value, _propertyChanging, _propertyChanged, newValue => this._siteAreaId = newValue);
            }
        }
        protected int ? _typeId;
        public int ? TypeId
        {
            get => _typeId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "TypeId", _typeId, value, _propertyChanging, _propertyChanged, newValue => this._typeId = newValue);
            }
        }
        protected int ? _loadingId;
        public int ? LoadingId
        {
            get => _loadingId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "LoadingId", _loadingId, value, _propertyChanging, _propertyChanged, newValue => this._loadingId = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _key;
        public string Key
        {
            get => _key;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Key", _key, value, _propertyChanging, _propertyChanged, newValue => this._key = newValue);
            }
        }
        protected string _inferredWhenKeyChanges;
        public string InferredWhenKeyChanges
        {
            get => _inferredWhenKeyChanges;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "InferredWhenKeyChanges", _inferredWhenKeyChanges, value, _propertyChanging, _propertyChanged, newValue => this._inferredWhenKeyChanges = newValue);
            }
        }
        protected string _title;
        public string Title
        {
            get => _title;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Title", _title, value, _propertyChanging, _propertyChanged, newValue => this._title = newValue);
            }
        }
        protected string _description;
        public string Description
        {
            get => _description;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Description", _description, value, _propertyChanging, _propertyChanged, newValue => this._description = newValue);
            }
        }
        protected PersonSkills _skills;
        public PersonSkills Skills
        {
            get => _skills;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Skills", _skills, value, _propertyChanging, _propertyChanged, newValue => this._skills = newValue);
            }
        }
        protected PersonCategory _category;
        public PersonCategory Category
        {
            get => _category;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Category", _category, value, _propertyChanging, _propertyChanged, newValue => this._category = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected Client _client;
        public Client Client
        {
            get => _client;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Client", _client, value, _propertyChanging, _propertyChanged, newValue => this._client = newValue);
            }
        }
        protected Site _site;
        public Site Site
        {
            get => _site;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Site", _site, value, _propertyChanging, _propertyChanged, newValue => this._site = newValue);
            }
        }
        protected SiteArea _siteArea;
        public SiteArea SiteArea
        {
            get => _siteArea;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteArea", _siteArea, value, _propertyChanging, _propertyChanged, newValue => this._siteArea = newValue);
            }
        }
        protected PersonType _type;
        public PersonType Type
        {
            get => _type;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Type", _type, value, _propertyChanging, _propertyChanged, newValue => this._type = newValue);
            }
        }
        protected PersonLoading _loading;
        public PersonLoading Loading
        {
            get => _loading;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Loading", _loading, value, _propertyChanging, _propertyChanged, newValue => this._loading = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 TypesCount
        {
            get;
            set;
        }
        protected RelatedList<Person, PersonTypeMap>_types;
        public RelatedList<Person, PersonTypeMap>Types
        {
            get
            {
                this._types = this._types ?? new RelatedList<Person, PersonTypeMap>(this, nameof(Types));
                return _types;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Types", _types, value, _propertyChanging, _propertyChanged, newValue => this._types = newValue);
            }
        }
        public Int64 ReportsCount
        {
            get;
            set;
        }
        protected RelatedList<Person, PersonReport>_reports;
        public RelatedList<Person, PersonReport>Reports
        {
            get
            {
                this._reports = this._reports ?? new RelatedList<Person, PersonReport>(this, nameof(Reports));
                return _reports;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Reports", _reports, value, _propertyChanging, _propertyChanged, newValue => this._reports = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentQuestion: RiskAssessmentQuestionBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        public Int64 AnswersCount
        {
            get;
            set;
        }
        protected RelatedList<RiskAssessmentQuestion, RiskAssessmentAnswer>_answers;
        public RelatedList<RiskAssessmentQuestion, RiskAssessmentAnswer>Answers
        {
            get
            {
                this._answers = this._answers ?? new RelatedList<RiskAssessmentQuestion, RiskAssessmentAnswer>(this, nameof(Answers));
                return _answers;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Answers", _answers, value, _propertyChanging, _propertyChanged, newValue => this._answers = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentAnswer: RiskAssessmentAnswerBase,
    IEntity
    {
        protected int _questionId;
        public int QuestionId
        {
            get => _questionId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "QuestionId", _questionId, value, _propertyChanging, _propertyChanged, newValue => this._questionId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _specificHazard;
        public string SpecificHazard
        {
            get => _specificHazard;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SpecificHazard", _specificHazard, value, _propertyChanging, _propertyChanged, newValue => this._specificHazard = newValue);
            }
        }
        protected string _precautionsToControlHazard;
        public string PrecautionsToControlHazard
        {
            get => _precautionsToControlHazard;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PrecautionsToControlHazard", _precautionsToControlHazard, value, _propertyChanging, _propertyChanged, newValue => this._precautionsToControlHazard = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected RiskAssessmentQuestion _question;
        public RiskAssessmentQuestion Question
        {
            get => _question;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Question", _question, value, _propertyChanging, _propertyChanged, newValue => this._question = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessmentSolution: RiskAssessmentSolutionBase,
    IEntity
    {
        protected int _riskAssessmentId;
        public int RiskAssessmentId
        {
            get => _riskAssessmentId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RiskAssessmentId", _riskAssessmentId, value, _propertyChanging, _propertyChanged, newValue => this._riskAssessmentId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected RiskAssessment _riskAssessment;
        public RiskAssessment RiskAssessment
        {
            get => _riskAssessment;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RiskAssessment", _riskAssessment, value, _propertyChanging, _propertyChanged, newValue => this._riskAssessment = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class RiskAssessment: RiskAssessmentBase,
    IEntity
    {
        protected int _siteInspectionId;
        public int SiteInspectionId
        {
            get => _siteInspectionId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteInspectionId", _siteInspectionId, value, _propertyChanging, _propertyChanged, newValue => this._siteInspectionId = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected SiteInspection _siteInspection;
        public SiteInspection SiteInspection
        {
            get => _siteInspection;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteInspection", _siteInspection, value, _propertyChanging, _propertyChanged, newValue => this._siteInspection = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        protected RiskAssessmentSolution _riskAssessmentSolution;
        public RiskAssessmentSolution RiskAssessmentSolution
        {
            get => _riskAssessmentSolution;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RiskAssessmentSolution", _riskAssessmentSolution, value, _propertyChanging, _propertyChanged, newValue => this._riskAssessmentSolution = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportReceiverEmailAddress: ReportReceiverEmailAddressBase,
    IEntity
    {
        protected int _siteId;
        public int SiteId
        {
            get => _siteId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteId", _siteId, value, _propertyChanging, _propertyChanged, newValue => this._siteId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "EmailAddress", _emailAddress, value, _propertyChanging, _propertyChanged, newValue => this._emailAddress = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected Site _site;
        public Site Site
        {
            get => _site;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Site", _site, value, _propertyChanging, _propertyChanged, newValue => this._site = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class Project: ProjectBase,
    IEntity
    {
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _title;
        public string Title
        {
            get => _title;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Title", _title, value, _propertyChanging, _propertyChanged, newValue => this._title = newValue);
            }
        }
        protected string _description;
        public string Description
        {
            get => _description;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Description", _description, value, _propertyChanging, _propertyChanged, newValue => this._description = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportType: ReportTypeBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected int _categoryId;
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CategoryId", _categoryId, value, _propertyChanging, _propertyChanged, newValue => this._categoryId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected ReportCategory _category;
        public ReportCategory Category
        {
            get => _category;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Category", _category, value, _propertyChanging, _propertyChanged, newValue => this._category = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 FaultReportsCount
        {
            get;
            set;
        }
        protected RelatedList<ReportType, PersonReport>_faultReports;
        public RelatedList<ReportType, PersonReport>FaultReports
        {
            get
            {
                this._faultReports = this._faultReports ?? new RelatedList<ReportType, PersonReport>(this, nameof(FaultReports));
                return _faultReports;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FaultReports", _faultReports, value, _propertyChanging, _propertyChanged, newValue => this._faultReports = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportRecommendation: ReportRecommendationBase,
    IEntity
    {
        protected int _reportId;
        public int ReportId
        {
            get => _reportId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ReportId", _reportId, value, _propertyChanging, _propertyChanged, newValue => this._reportId = newValue);
            }
        }
        protected int _recommendationId;
        public int RecommendationId
        {
            get => _recommendationId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RecommendationId", _recommendationId, value, _propertyChanging, _propertyChanged, newValue => this._recommendationId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Notes", _notes, value, _propertyChanging, _propertyChanged, newValue => this._notes = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected PersonReport _personReport;
        public PersonReport PersonReport
        {
            get => _personReport;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonReport", _personReport, value, _propertyChanging, _propertyChanged, newValue => this._personReport = newValue);
            }
        }
        protected ReportDefaultRecommendation _recommendation;
        public ReportDefaultRecommendation Recommendation
        {
            get => _recommendation;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Recommendation", _recommendation, value, _propertyChanging, _propertyChanged, newValue => this._recommendation = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportDefaultRecommendation: ReportDefaultRecommendationBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        protected string _text;
        public string Text
        {
            get => _text;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Text", _text, value, _propertyChanging, _propertyChanged, newValue => this._text = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 RecommendationsCount
        {
            get;
            set;
        }
        protected RelatedList<ReportDefaultRecommendation, ReportRecommendation>_recommendations;
        public RelatedList<ReportDefaultRecommendation, ReportRecommendation>Recommendations
        {
            get
            {
                this._recommendations = this._recommendations ?? new RelatedList<ReportDefaultRecommendation, ReportRecommendation>(this, nameof(Recommendations));
                return _recommendations;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Recommendations", _recommendations, value, _propertyChanging, _propertyChanged, newValue => this._recommendations = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportCategory: ReportCategoryBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 ReportTypesCount
        {
            get;
            set;
        }
        protected RelatedList<ReportCategory, ReportType>_reportTypes;
        public RelatedList<ReportCategory, ReportType>ReportTypes
        {
            get
            {
                this._reportTypes = this._reportTypes ?? new RelatedList<ReportCategory, ReportType>(this, nameof(ReportTypes));
                return _reportTypes;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ReportTypes", _reportTypes, value, _propertyChanging, _propertyChanged, newValue => this._reportTypes = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ReportActionsTaken: ReportActionsTakenBase,
    IEntity
    {
        protected int _faultReportId;
        public int FaultReportId
        {
            get => _faultReportId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FaultReportId", _faultReportId, value, _propertyChanging, _propertyChanged, newValue => this._faultReportId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Notes", _notes, value, _propertyChanging, _propertyChanged, newValue => this._notes = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected PersonReport _personReport;
        public PersonReport PersonReport
        {
            get => _personReport;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonReport", _personReport, value, _propertyChanging, _propertyChanged, newValue => this._personReport = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class SiteDocument: SiteDocumentBase,
    IEntity
    {
        protected int _categoryId;
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CategoryId", _categoryId, value, _propertyChanging, _propertyChanged, newValue => this._categoryId = newValue);
            }
        }
        protected int _siteId;
        public int SiteId
        {
            get => _siteId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteId", _siteId, value, _propertyChanging, _propertyChanged, newValue => this._siteId = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _title;
        public string Title
        {
            get => _title;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Title", _title, value, _propertyChanging, _propertyChanged, newValue => this._title = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected DocumentCategory _category;
        public DocumentCategory Category
        {
            get => _category;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Category", _category, value, _propertyChanging, _propertyChanged, newValue => this._category = newValue);
            }
        }
        protected Site _site;
        public Site Site
        {
            get => _site;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Site", _site, value, _propertyChanging, _propertyChanged, newValue => this._site = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class DocumentCategory: DocumentCategoryBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 DocumentsCount
        {
            get;
            set;
        }
        protected RelatedList<DocumentCategory, SiteDocument>_documents;
        public RelatedList<DocumentCategory, SiteDocument>Documents
        {
            get
            {
                this._documents = this._documents ?? new RelatedList<DocumentCategory, SiteDocument>(this, nameof(Documents));
                return _documents;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Documents", _documents, value, _propertyChanging, _propertyChanged, newValue => this._documents = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ClientType: ClientTypeBase,
    IEntity
    {
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        public Int64 ClientsCount
        {
            get;
            set;
        }
        protected RelatedList<ClientType, Client>_clients;
        public RelatedList<ClientType, Client>Clients
        {
            get
            {
                this._clients = this._clients ?? new RelatedList<ClientType, Client>(this, nameof(Clients));
                return _clients;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Clients", _clients, value, _propertyChanging, _propertyChanged, newValue => this._clients = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class Client: ClientBase,
    IEntity
    {
        protected int _typeId;
        public int TypeId
        {
            get => _typeId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "TypeId", _typeId, value, _propertyChanging, _propertyChanged, newValue => this._typeId = newValue);
            }
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Name", _name, value, _propertyChanging, _propertyChanged, newValue => this._name = newValue);
            }
        }
        protected double _averageSales;
        public double AverageSales
        {
            get => _averageSales;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "AverageSales", _averageSales, value, _propertyChanging, _propertyChanged, newValue => this._averageSales = newValue);
            }
        }
        protected double _averageIncome;
        public double AverageIncome
        {
            get => _averageIncome;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "AverageIncome", _averageIncome, value, _propertyChanging, _propertyChanged, newValue => this._averageIncome = newValue);
            }
        }
        protected int _category;
        public int Category
        {
            get => _category;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Category", _category, value, _propertyChanging, _propertyChanged, newValue => this._category = newValue);
            }
        }
        protected string _description;
        public string Description
        {
            get => _description;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Description", _description, value, _propertyChanging, _propertyChanged, newValue => this._description = newValue);
            }
        }
        protected double _discount;
        public double Discount
        {
            get => _discount;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Discount", _discount, value, _propertyChanging, _propertyChanged, newValue => this._discount = newValue);
            }
        }
        protected Guid _guid;
        public Guid Guid
        {
            get => _guid;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Guid", _guid, value, _propertyChanging, _propertyChanged, newValue => this._guid = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _revisionKey;
        public string RevisionKey
        {
            get => _revisionKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RevisionKey", _revisionKey, value, _propertyChanging, _propertyChanged, newValue => this._revisionKey = newValue);
            }
        }
        protected Guid _persistenceKey;
        public Guid PersistenceKey
        {
            get => _persistenceKey;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersistenceKey", _persistenceKey, value, _propertyChanging, _propertyChanged, newValue => this._persistenceKey = newValue);
            }
        }
        public Int64 UsersCount
        {
            get;
            set;
        }
        protected RelatedList<Client, ApplicationUser>_users;
        public RelatedList<Client, ApplicationUser>Users
        {
            get
            {
                this._users = this._users ?? new RelatedList<Client, ApplicationUser>(this, nameof(Users));
                return _users;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Users", _users, value, _propertyChanging, _propertyChanged, newValue => this._users = newValue);
            }
        }
        protected ClientType _type;
        public ClientType Type
        {
            get => _type;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Type", _type, value, _propertyChanging, _propertyChanged, newValue => this._type = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 PeopleCount
        {
            get;
            set;
        }
        protected RelatedList<Client, Person>_people;
        public RelatedList<Client, Person>People
        {
            get
            {
                this._people = this._people ?? new RelatedList<Client, Person>(this, nameof(People));
                return _people;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "People", _people, value, _propertyChanging, _propertyChanged, newValue => this._people = newValue);
            }
        }
        public Int64 SitesCount
        {
            get;
            set;
        }
        protected RelatedList<Client, Site>_sites;
        public RelatedList<Client, Site>Sites
        {
            get
            {
                this._sites = this._sites ?? new RelatedList<Client, Site>(this, nameof(Sites));
                return _sites;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Sites", _sites, value, _propertyChanging, _propertyChanged, newValue => this._sites = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ApplicationLog: ApplicationLogBase,
    IEntity
    {
        protected Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected DateTimeOffset _createdDate;
        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "CreatedDate", _createdDate, value, _propertyChanging, _propertyChanged, newValue => this._createdDate = newValue);
            }
        }
        protected string _module;
        public string Module
        {
            get => _module;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Module", _module, value, _propertyChanging, _propertyChanged, newValue => this._module = newValue);
            }
        }
        protected string _message;
        public string Message
        {
            get => _message;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Message", _message, value, _propertyChanging, _propertyChanged, newValue => this._message = newValue);
            }
        }
        protected string _kind;
        public string Kind
        {
            get => _kind;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Kind", _kind, value, _propertyChanging, _propertyChanged, newValue => this._kind = newValue);
            }
        }
    }
}
namespace IqlSampleApp.Data.Entities
{
    public class ApplicationUser: ApplicationUserBase,
    IEntity
    {
        protected bool _isLockedOut;
        public bool IsLockedOut
        {
            get => _isLockedOut;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "IsLockedOut", _isLockedOut, value, _propertyChanging, _propertyChanged, newValue => this._isLockedOut = newValue);
            }
        }
        protected int ? _clientId;
        public int ? ClientId
        {
            get => _clientId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ClientId", _clientId, value, _propertyChanging, _propertyChanged, newValue => this._clientId = newValue);
            }
        }
        protected string _id;
        public string Id
        {
            get => _id;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Id", _id, value, _propertyChanging, _propertyChanged, newValue => this._id = newValue);
            }
        }
        protected string _email;
        public string Email
        {
            get => _email;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Email", _email, value, _propertyChanging, _propertyChanged, newValue => this._email = newValue);
            }
        }
        protected UserPermissions _permissions;
        public UserPermissions Permissions
        {
            get => _permissions;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Permissions", _permissions, value, _propertyChanging, _propertyChanged, newValue => this._permissions = newValue);
            }
        }
        protected UserType _userType;
        public UserType UserType
        {
            get => _userType;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "UserType", _userType, value, _propertyChanging, _propertyChanged, newValue => this._userType = newValue);
            }
        }
        protected string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FullName", _fullName, value, _propertyChanging, _propertyChanged, newValue => this._fullName = newValue);
            }
        }
        protected string _createdByUserId;
        public string CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUserId", _createdByUserId, value, _propertyChanging, _propertyChanged, newValue => this._createdByUserId = newValue);
            }
        }
        protected string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "UserName", _userName, value, _propertyChanging, _propertyChanged, newValue => this._userName = newValue);
            }
        }
        protected bool _emailConfirmed;
        public bool EmailConfirmed
        {
            get => _emailConfirmed;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "EmailConfirmed", _emailConfirmed, value, _propertyChanging, _propertyChanged, newValue => this._emailConfirmed = newValue);
            }
        }
        protected string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PhoneNumber", _phoneNumber, value, _propertyChanging, _propertyChanged, newValue => this._phoneNumber = newValue);
            }
        }
        protected bool _phoneNumberConfirmed;
        public bool PhoneNumberConfirmed
        {
            get => _phoneNumberConfirmed;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PhoneNumberConfirmed", _phoneNumberConfirmed, value, _propertyChanging, _propertyChanged, newValue => this._phoneNumberConfirmed = newValue);
            }
        }
        protected bool _twoFactorEnabled;
        public bool TwoFactorEnabled
        {
            get => _twoFactorEnabled;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "TwoFactorEnabled", _twoFactorEnabled, value, _propertyChanging, _propertyChanged, newValue => this._twoFactorEnabled = newValue);
            }
        }
        protected DateTimeOffset ? _lockoutEnd;
        public DateTimeOffset ? LockoutEnd
        {
            get => _lockoutEnd;
            set
            {
                DatePropertyChanger.Instance.ChangeProperty(this, "LockoutEnd", _lockoutEnd, value, _propertyChanging, _propertyChanged, newValue => this._lockoutEnd = newValue);
            }
        }
        protected bool _lockoutEnabled;
        public bool LockoutEnabled
        {
            get => _lockoutEnabled;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "LockoutEnabled", _lockoutEnabled, value, _propertyChanging, _propertyChanged, newValue => this._lockoutEnabled = newValue);
            }
        }
        protected Client _client;
        public Client Client
        {
            get => _client;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Client", _client, value, _propertyChanging, _propertyChanged, newValue => this._client = newValue);
            }
        }
        protected ApplicationUser _createdByUser;
        public ApplicationUser CreatedByUser
        {
            get => _createdByUser;
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "CreatedByUser", _createdByUser, value, _propertyChanging, _propertyChanged, newValue => this._createdByUser = newValue);
            }
        }
        public Int64 ClientsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, Client>_clientsCreated;
        public RelatedList<ApplicationUser, Client>ClientsCreated
        {
            get
            {
                this._clientsCreated = this._clientsCreated ?? new RelatedList<ApplicationUser, Client>(this, nameof(ClientsCreated));
                return _clientsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ClientsCreated", _clientsCreated, value, _propertyChanging, _propertyChanged, newValue => this._clientsCreated = newValue);
            }
        }
        public Int64 DocumentCategoriesCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, DocumentCategory>_documentCategoriesCreated;
        public RelatedList<ApplicationUser, DocumentCategory>DocumentCategoriesCreated
        {
            get
            {
                this._documentCategoriesCreated = this._documentCategoriesCreated ?? new RelatedList<ApplicationUser, DocumentCategory>(this, nameof(DocumentCategoriesCreated));
                return _documentCategoriesCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "DocumentCategoriesCreated", _documentCategoriesCreated, value, _propertyChanging, _propertyChanged, newValue => this._documentCategoriesCreated = newValue);
            }
        }
        public Int64 SiteDocumentsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, SiteDocument>_siteDocumentsCreated;
        public RelatedList<ApplicationUser, SiteDocument>SiteDocumentsCreated
        {
            get
            {
                this._siteDocumentsCreated = this._siteDocumentsCreated ?? new RelatedList<ApplicationUser, SiteDocument>(this, nameof(SiteDocumentsCreated));
                return _siteDocumentsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteDocumentsCreated", _siteDocumentsCreated, value, _propertyChanging, _propertyChanged, newValue => this._siteDocumentsCreated = newValue);
            }
        }
        public Int64 FaultActionsTakenCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, ReportActionsTaken>_faultActionsTakenCreated;
        public RelatedList<ApplicationUser, ReportActionsTaken>FaultActionsTakenCreated
        {
            get
            {
                this._faultActionsTakenCreated = this._faultActionsTakenCreated ?? new RelatedList<ApplicationUser, ReportActionsTaken>(this, nameof(FaultActionsTakenCreated));
                return _faultActionsTakenCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FaultActionsTakenCreated", _faultActionsTakenCreated, value, _propertyChanging, _propertyChanged, newValue => this._faultActionsTakenCreated = newValue);
            }
        }
        public Int64 FaultCategoriesCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, ReportCategory>_faultCategoriesCreated;
        public RelatedList<ApplicationUser, ReportCategory>FaultCategoriesCreated
        {
            get
            {
                this._faultCategoriesCreated = this._faultCategoriesCreated ?? new RelatedList<ApplicationUser, ReportCategory>(this, nameof(FaultCategoriesCreated));
                return _faultCategoriesCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FaultCategoriesCreated", _faultCategoriesCreated, value, _propertyChanging, _propertyChanged, newValue => this._faultCategoriesCreated = newValue);
            }
        }
        public Int64 FaultDefaultRecommendationsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, ReportDefaultRecommendation>_faultDefaultRecommendationsCreated;
        public RelatedList<ApplicationUser, ReportDefaultRecommendation>FaultDefaultRecommendationsCreated
        {
            get
            {
                this._faultDefaultRecommendationsCreated = this._faultDefaultRecommendationsCreated ?? new RelatedList<ApplicationUser, ReportDefaultRecommendation>(this, nameof(FaultDefaultRecommendationsCreated));
                return _faultDefaultRecommendationsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FaultDefaultRecommendationsCreated", _faultDefaultRecommendationsCreated, value, _propertyChanging, _propertyChanged, newValue => this._faultDefaultRecommendationsCreated = newValue);
            }
        }
        public Int64 FaultRecommendationsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, ReportRecommendation>_faultRecommendationsCreated;
        public RelatedList<ApplicationUser, ReportRecommendation>FaultRecommendationsCreated
        {
            get
            {
                this._faultRecommendationsCreated = this._faultRecommendationsCreated ?? new RelatedList<ApplicationUser, ReportRecommendation>(this, nameof(FaultRecommendationsCreated));
                return _faultRecommendationsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FaultRecommendationsCreated", _faultRecommendationsCreated, value, _propertyChanging, _propertyChanged, newValue => this._faultRecommendationsCreated = newValue);
            }
        }
        public Int64 FaultTypesCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, ReportType>_faultTypesCreated;
        public RelatedList<ApplicationUser, ReportType>FaultTypesCreated
        {
            get
            {
                this._faultTypesCreated = this._faultTypesCreated ?? new RelatedList<ApplicationUser, ReportType>(this, nameof(FaultTypesCreated));
                return _faultTypesCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FaultTypesCreated", _faultTypesCreated, value, _propertyChanging, _propertyChanged, newValue => this._faultTypesCreated = newValue);
            }
        }
        public Int64 ProjectCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, Project>_projectCreated;
        public RelatedList<ApplicationUser, Project>ProjectCreated
        {
            get
            {
                this._projectCreated = this._projectCreated ?? new RelatedList<ApplicationUser, Project>(this, nameof(ProjectCreated));
                return _projectCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ProjectCreated", _projectCreated, value, _propertyChanging, _propertyChanged, newValue => this._projectCreated = newValue);
            }
        }
        public Int64 ReportReceiverEmailAddressesCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, ReportReceiverEmailAddress>_reportReceiverEmailAddressesCreated;
        public RelatedList<ApplicationUser, ReportReceiverEmailAddress>ReportReceiverEmailAddressesCreated
        {
            get
            {
                this._reportReceiverEmailAddressesCreated = this._reportReceiverEmailAddressesCreated ?? new RelatedList<ApplicationUser, ReportReceiverEmailAddress>(this, nameof(ReportReceiverEmailAddressesCreated));
                return _reportReceiverEmailAddressesCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "ReportReceiverEmailAddressesCreated", _reportReceiverEmailAddressesCreated, value, _propertyChanging, _propertyChanged, newValue => this._reportReceiverEmailAddressesCreated = newValue);
            }
        }
        public Int64 RiskAssessmentsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, RiskAssessment>_riskAssessmentsCreated;
        public RelatedList<ApplicationUser, RiskAssessment>RiskAssessmentsCreated
        {
            get
            {
                this._riskAssessmentsCreated = this._riskAssessmentsCreated ?? new RelatedList<ApplicationUser, RiskAssessment>(this, nameof(RiskAssessmentsCreated));
                return _riskAssessmentsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RiskAssessmentsCreated", _riskAssessmentsCreated, value, _propertyChanging, _propertyChanged, newValue => this._riskAssessmentsCreated = newValue);
            }
        }
        public Int64 RiskAssessmentSolutionsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, RiskAssessmentSolution>_riskAssessmentSolutionsCreated;
        public RelatedList<ApplicationUser, RiskAssessmentSolution>RiskAssessmentSolutionsCreated
        {
            get
            {
                this._riskAssessmentSolutionsCreated = this._riskAssessmentSolutionsCreated ?? new RelatedList<ApplicationUser, RiskAssessmentSolution>(this, nameof(RiskAssessmentSolutionsCreated));
                return _riskAssessmentSolutionsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RiskAssessmentSolutionsCreated", _riskAssessmentSolutionsCreated, value, _propertyChanging, _propertyChanged, newValue => this._riskAssessmentSolutionsCreated = newValue);
            }
        }
        public Int64 RiskAssessmentAnswersCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, RiskAssessmentAnswer>_riskAssessmentAnswersCreated;
        public RelatedList<ApplicationUser, RiskAssessmentAnswer>RiskAssessmentAnswersCreated
        {
            get
            {
                this._riskAssessmentAnswersCreated = this._riskAssessmentAnswersCreated ?? new RelatedList<ApplicationUser, RiskAssessmentAnswer>(this, nameof(RiskAssessmentAnswersCreated));
                return _riskAssessmentAnswersCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RiskAssessmentAnswersCreated", _riskAssessmentAnswersCreated, value, _propertyChanging, _propertyChanged, newValue => this._riskAssessmentAnswersCreated = newValue);
            }
        }
        public Int64 RiskAssessmentQuestionsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, RiskAssessmentQuestion>_riskAssessmentQuestionsCreated;
        public RelatedList<ApplicationUser, RiskAssessmentQuestion>RiskAssessmentQuestionsCreated
        {
            get
            {
                this._riskAssessmentQuestionsCreated = this._riskAssessmentQuestionsCreated ?? new RelatedList<ApplicationUser, RiskAssessmentQuestion>(this, nameof(RiskAssessmentQuestionsCreated));
                return _riskAssessmentQuestionsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "RiskAssessmentQuestionsCreated", _riskAssessmentQuestionsCreated, value, _propertyChanging, _propertyChanged, newValue => this._riskAssessmentQuestionsCreated = newValue);
            }
        }
        public Int64 PeopleCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, Person>_peopleCreated;
        public RelatedList<ApplicationUser, Person>PeopleCreated
        {
            get
            {
                this._peopleCreated = this._peopleCreated ?? new RelatedList<ApplicationUser, Person>(this, nameof(PeopleCreated));
                return _peopleCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PeopleCreated", _peopleCreated, value, _propertyChanging, _propertyChanged, newValue => this._peopleCreated = newValue);
            }
        }
        public Int64 PersonInspectionsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, PersonInspection>_personInspectionsCreated;
        public RelatedList<ApplicationUser, PersonInspection>PersonInspectionsCreated
        {
            get
            {
                this._personInspectionsCreated = this._personInspectionsCreated ?? new RelatedList<ApplicationUser, PersonInspection>(this, nameof(PersonInspectionsCreated));
                return _personInspectionsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonInspectionsCreated", _personInspectionsCreated, value, _propertyChanging, _propertyChanged, newValue => this._personInspectionsCreated = newValue);
            }
        }
        public Int64 PersonLoadingsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, PersonLoading>_personLoadingsCreated;
        public RelatedList<ApplicationUser, PersonLoading>PersonLoadingsCreated
        {
            get
            {
                this._personLoadingsCreated = this._personLoadingsCreated ?? new RelatedList<ApplicationUser, PersonLoading>(this, nameof(PersonLoadingsCreated));
                return _personLoadingsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonLoadingsCreated", _personLoadingsCreated, value, _propertyChanging, _propertyChanged, newValue => this._personLoadingsCreated = newValue);
            }
        }
        public Int64 PersonTypesCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, PersonType>_personTypesCreated;
        public RelatedList<ApplicationUser, PersonType>PersonTypesCreated
        {
            get
            {
                this._personTypesCreated = this._personTypesCreated ?? new RelatedList<ApplicationUser, PersonType>(this, nameof(PersonTypesCreated));
                return _personTypesCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "PersonTypesCreated", _personTypesCreated, value, _propertyChanging, _propertyChanged, newValue => this._personTypesCreated = newValue);
            }
        }
        public Int64 FaultReportsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, PersonReport>_faultReportsCreated;
        public RelatedList<ApplicationUser, PersonReport>FaultReportsCreated
        {
            get
            {
                this._faultReportsCreated = this._faultReportsCreated ?? new RelatedList<ApplicationUser, PersonReport>(this, nameof(FaultReportsCreated));
                return _faultReportsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "FaultReportsCreated", _faultReportsCreated, value, _propertyChanging, _propertyChanged, newValue => this._faultReportsCreated = newValue);
            }
        }
        public Int64 SitesCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, Site>_sitesCreated;
        public RelatedList<ApplicationUser, Site>SitesCreated
        {
            get
            {
                this._sitesCreated = this._sitesCreated ?? new RelatedList<ApplicationUser, Site>(this, nameof(SitesCreated));
                return _sitesCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SitesCreated", _sitesCreated, value, _propertyChanging, _propertyChanged, newValue => this._sitesCreated = newValue);
            }
        }
        public Int64 SiteAreasCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, SiteArea>_siteAreasCreated;
        public RelatedList<ApplicationUser, SiteArea>SiteAreasCreated
        {
            get
            {
                this._siteAreasCreated = this._siteAreasCreated ?? new RelatedList<ApplicationUser, SiteArea>(this, nameof(SiteAreasCreated));
                return _siteAreasCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteAreasCreated", _siteAreasCreated, value, _propertyChanging, _propertyChanged, newValue => this._siteAreasCreated = newValue);
            }
        }
        public Int64 SiteInspectionsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, SiteInspection>_siteInspectionsCreated;
        public RelatedList<ApplicationUser, SiteInspection>SiteInspectionsCreated
        {
            get
            {
                this._siteInspectionsCreated = this._siteInspectionsCreated ?? new RelatedList<ApplicationUser, SiteInspection>(this, nameof(SiteInspectionsCreated));
                return _siteInspectionsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "SiteInspectionsCreated", _siteInspectionsCreated, value, _propertyChanging, _propertyChanged, newValue => this._siteInspectionsCreated = newValue);
            }
        }
        public Int64 UserSettingsCreatedCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, UserSetting>_userSettingsCreated;
        public RelatedList<ApplicationUser, UserSetting>UserSettingsCreated
        {
            get
            {
                this._userSettingsCreated = this._userSettingsCreated ?? new RelatedList<ApplicationUser, UserSetting>(this, nameof(UserSettingsCreated));
                return _userSettingsCreated;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "UserSettingsCreated", _userSettingsCreated, value, _propertyChanging, _propertyChanged, newValue => this._userSettingsCreated = newValue);
            }
        }
        public Int64 UserSettingsCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, UserSetting>_userSettings;
        public RelatedList<ApplicationUser, UserSetting>UserSettings
        {
            get
            {
                this._userSettings = this._userSettings ?? new RelatedList<ApplicationUser, UserSetting>(this, nameof(UserSettings));
                return _userSettings;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "UserSettings", _userSettings, value, _propertyChanging, _propertyChanged, newValue => this._userSettings = newValue);
            }
        }
        public Int64 SitesCount
        {
            get;
            set;
        }
        protected RelatedList<ApplicationUser, UserSite>_sites;
        public RelatedList<ApplicationUser, UserSite>Sites
        {
            get
            {
                this._sites = this._sites ?? new RelatedList<ApplicationUser, UserSite>(this, nameof(Sites));
                return _sites;
            }
            set
            {
                PrimitivePropertyChanger.Instance.ChangeProperty(this, "Sites", _sites, value, _propertyChanging, _propertyChanged, newValue => this._sites = newValue);
            }
        }
    }
}