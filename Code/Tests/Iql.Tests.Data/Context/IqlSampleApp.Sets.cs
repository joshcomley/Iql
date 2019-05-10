using Iql.Entities;
using IqlSampleApp.ApiContext.Base;
using IqlSampleApp.Data.Entities;
using Iql.Data.Context;
using Iql.Data.DataStores;
using Iql.OData;
using Iql.Parsing;
using System.Collections.Generic;
using Iql.Data.Lists;
using Iql.OData.Methods;
using System;
namespace IqlSampleApp.Sets
{
    public class ApplicationUserSet: DbSet<ApplicationUser, string>
    {
        public ApplicationUserSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Collection methods
        public virtual ODataDataMethodRequest<IEnumerable<ApplicationUser>>OldUsers()
        {
            var parameters = new List<ODataParameter>();
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<ApplicationUser>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Collection, "IqlSampleApp", "OldUsers", typeof(ApplicationUser), typeof(IEnumerable<ApplicationUser>));
        }
        public virtual ODataDataMethodRequest<IEnumerable<ApplicationUser>>ForClient(int id, int type)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(id, typeof(int), "id", false));
            parameters.Add(new ODataParameter(type, typeof(int), "type", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<ApplicationUser>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Collection, "IqlSampleApp", "ForClient", typeof(ApplicationUser), typeof(IEnumerable<ApplicationUser>));
        }
        public virtual ODataDataMethodRequest<ApplicationUser>Me()
        {
            var parameters = new List<ODataParameter>();
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<ApplicationUser>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Collection, "IqlSampleApp", "Me", typeof(ApplicationUser), typeof(ApplicationUser));
        }
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(ApplicationUser bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(ApplicationUser));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(ApplicationUser bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(ApplicationUser), typeof(IEnumerable<string>));
        }
        public virtual ODataDataMethodRequest<string>GeneratePasswordResetLink(ApplicationUser bindingParameter)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GeneratePasswordResetLink", typeof(ApplicationUser), typeof(String));
        }
        public virtual ODataDataMethodRequest<string>AccountConfirm(ApplicationUser bindingParameter)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "AccountConfirm", typeof(ApplicationUser), typeof(String));
        }
        public virtual ODataDataMethodRequest<string>SendPasswordResetEmail(ApplicationUser bindingParameter)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "SendPasswordResetEmail", typeof(ApplicationUser), typeof(String));
        }
        public virtual ODataDataMethodRequest<string>ReinstateUser(ApplicationUser bindingParameter)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "ReinstateUser", typeof(ApplicationUser), typeof(String));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ApplicationLogSet: DbSet<ApplicationLog, Guid>
    {
        public ApplicationLogSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ClientSet: DbSet<Client, int>
    {
        public ClientSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Collection methods
        public virtual ODataDataMethodRequest<IEnumerable<Client>>All()
        {
            var parameters = new List<ODataParameter>();
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<Client>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Collection, "IqlSampleApp", "All", typeof(Client), typeof(IEnumerable<Client>));
        }
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(Client bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(Client), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(Client));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(Client bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(Client), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(Client), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ClientTypeSet: DbSet<ClientType, int>
    {
        public ClientTypeSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataDataMethodRequest<IEnumerable<string>>SayHi(ClientType bindingParameter, string name)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ClientType), "bindingParameter", true));
            parameters.Add(new ODataParameter(name, typeof(string), "name", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "SayHi", typeof(ClientType), typeof(IEnumerable<string>));
        }
        public virtual ODataMethodRequest IncrementVersion(ClientType bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ClientType), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(ClientType));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(ClientType bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ClientType), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(ClientType), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ClientCategorySet: DbSet<ClientCategory, int>
    {
        public ClientCategorySet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ClientCategoryPivotSet: DbSet<ClientCategoryPivot, CompositeKey>
    {
        public ClientCategoryPivotSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class DocumentCategorySet: DbSet<DocumentCategory, int>
    {
        public DocumentCategorySet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(DocumentCategory bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(DocumentCategory), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(DocumentCategory));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(DocumentCategory bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(DocumentCategory), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(DocumentCategory), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class SiteDocumentSet: DbSet<SiteDocument, int>
    {
        public SiteDocumentSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(SiteDocument bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(SiteDocument), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(SiteDocument));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(SiteDocument bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(SiteDocument), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(SiteDocument), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportActionsTakenSet: DbSet<ReportActionsTaken, int>
    {
        public ReportActionsTakenSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(ReportActionsTaken bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportActionsTaken), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(ReportActionsTaken));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(ReportActionsTaken bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportActionsTaken), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(ReportActionsTaken), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportCategorySet: DbSet<ReportCategory, int>
    {
        public ReportCategorySet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(ReportCategory bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportCategory), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(ReportCategory));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(ReportCategory bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportCategory), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(ReportCategory), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportDefaultRecommendationSet: DbSet<ReportDefaultRecommendation, int>
    {
        public ReportDefaultRecommendationSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(ReportDefaultRecommendation bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportDefaultRecommendation), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(ReportDefaultRecommendation));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(ReportDefaultRecommendation bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportDefaultRecommendation), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(ReportDefaultRecommendation), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportRecommendationSet: DbSet<ReportRecommendation, int>
    {
        public ReportRecommendationSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(ReportRecommendation bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportRecommendation), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(ReportRecommendation));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(ReportRecommendation bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportRecommendation), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(ReportRecommendation), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportTypeSet: DbSet<ReportType, int>
    {
        public ReportTypeSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(ReportType bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportType), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(ReportType));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(ReportType bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportType), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(ReportType), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ProjectSet: DbSet<Project, int>
    {
        public ProjectSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(Project bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(Project), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(Project));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(Project bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(Project), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(Project), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportReceiverEmailAddressSet: DbSet<ReportReceiverEmailAddress, int>
    {
        public ReportReceiverEmailAddressSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(ReportReceiverEmailAddress bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportReceiverEmailAddress), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(ReportReceiverEmailAddress));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(ReportReceiverEmailAddress bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ReportReceiverEmailAddress), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(ReportReceiverEmailAddress), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class RiskAssessmentSet: DbSet<RiskAssessment, int>
    {
        public RiskAssessmentSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(RiskAssessment bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(RiskAssessment), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(RiskAssessment));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(RiskAssessment bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(RiskAssessment), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(RiskAssessment), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class RiskAssessmentSolutionSet: DbSet<RiskAssessmentSolution, int>
    {
        public RiskAssessmentSolutionSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class RiskAssessmentAnswerSet: DbSet<RiskAssessmentAnswer, int>
    {
        public RiskAssessmentAnswerSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(RiskAssessmentAnswer bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(RiskAssessmentAnswer), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(RiskAssessmentAnswer));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(RiskAssessmentAnswer bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(RiskAssessmentAnswer), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(RiskAssessmentAnswer), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class RiskAssessmentQuestionSet: DbSet<RiskAssessmentQuestion, int>
    {
        public RiskAssessmentQuestionSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(RiskAssessmentQuestion bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(RiskAssessmentQuestion), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(RiskAssessmentQuestion));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(RiskAssessmentQuestion bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(RiskAssessmentQuestion), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(RiskAssessmentQuestion), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonSet: DbSet<Person, int>
    {
        public PersonSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(Person bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(Person), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(Person));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(Person bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(Person), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(Person), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonInspectionSet: DbSet<PersonInspection, int>
    {
        public PersonInspectionSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(PersonInspection bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonInspection), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(PersonInspection));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(PersonInspection bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonInspection), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(PersonInspection), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonLoadingSet: DbSet<PersonLoading, int>
    {
        public PersonLoadingSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(PersonLoading bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonLoading), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(PersonLoading));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(PersonLoading bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonLoading), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(PersonLoading), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonTypeSet: DbSet<PersonType, int>
    {
        public PersonTypeSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(PersonType bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonType), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(PersonType));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(PersonType bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonType), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(PersonType), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonTypeMapSet: DbSet<PersonTypeMap, CompositeKey>
    {
        public PersonTypeMapSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(PersonTypeMap bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonTypeMap), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(PersonTypeMap));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(PersonTypeMap bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonTypeMap), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(PersonTypeMap), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonReportSet: DbSet<PersonReport, int>
    {
        public PersonReportSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(PersonReport bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonReport), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(PersonReport));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(PersonReport bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(PersonReport), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(PersonReport), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class SiteSet: DbSet<Site, int>
    {
        public SiteSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(Site bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(Site), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(Site));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(Site bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(Site), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(Site), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class SiteAreaSet: DbSet<SiteArea, int>
    {
        public SiteAreaSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class SiteInspectionSet: DbSet<SiteInspection, int>
    {
        public SiteInspectionSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(SiteInspection bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(SiteInspection), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(SiteInspection));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(SiteInspection bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(SiteInspection), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(SiteInspection), typeof(IEnumerable<string>));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class UserSettingSet: DbSet<UserSetting, Guid>
    {
        public UserSettingSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class UserSiteSet: DbSet<UserSite, CompositeKey>
    {
        public UserSiteSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
        // Entity methods
        public virtual ODataMethodRequest IncrementVersion(UserSite bindingParameter, string PropertyName)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(UserSite), "bindingParameter", true));
            parameters.Add(new ODataParameter(PropertyName, typeof(string), "PropertyName", false));
            return ((ODataDataStore) this.DataContext.DataStore).Method(parameters, ODataMethodType.Action, ODataMethodScopeKind.Entity, "IqlSampleApp", "IncrementVersion", typeof(UserSite));
        }
        public virtual ODataDataMethodRequest<IEnumerable<string>>GetMediaUploadUrl(UserSite bindingParameter, string property)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(UserSite), "bindingParameter", true));
            parameters.Add(new ODataParameter(property, typeof(string), "property", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<string>>(parameters, ODataMethodType.Function, ODataMethodScopeKind.Entity, "IqlSampleApp", "GetMediaUploadUrl", typeof(UserSite), typeof(IEnumerable<string>));
        }
    }
}