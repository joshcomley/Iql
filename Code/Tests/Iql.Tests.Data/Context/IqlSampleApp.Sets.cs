using Iql.Entities;
using IqlSampleApp.Sets;
using IqlSampleApp.ApiContext.Base.EntityService;
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
        public virtual ODataDataMethodRequest<IEnumerable<ApplicationUser>>ForClient(int id, int type)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(id, typeof(int), "id", false));
            parameters.Add(new ODataParameter(type, typeof(int), "type", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<ApplicationUser>>(parameters, ODataMethodType.Function, ODataMethodScope.Collection, "IqlSampleApp", "ForClient", typeof(ApplicationUser), typeof(ApplicationUser));
        }
        public virtual ODataDataMethodRequest<ApplicationUser>Me()
        {
            var parameters = new List<ODataParameter>();
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<ApplicationUser>(parameters, ODataMethodType.Function, ODataMethodScope.Collection, "IqlSampleApp", "Me", typeof(ApplicationUser), typeof(ApplicationUser));
        }
        // Entity methods
        public virtual ODataDataMethodRequest<string>GeneratePasswordResetLink(ApplicationUser bindingParameter)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Function, ODataMethodScope.Entity, "IqlSampleApp", "GeneratePasswordResetLink", typeof(ApplicationUser), typeof(String));
        }
        public virtual ODataDataMethodRequest<string>AccountConfirm(ApplicationUser bindingParameter)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Action, ODataMethodScope.Entity, "IqlSampleApp", "AccountConfirm", typeof(ApplicationUser), typeof(String));
        }
        public virtual ODataDataMethodRequest<string>SendPasswordResetEmail(ApplicationUser bindingParameter)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Action, ODataMethodScope.Entity, "IqlSampleApp", "SendPasswordResetEmail", typeof(ApplicationUser), typeof(String));
        }
        public virtual ODataDataMethodRequest<string>ReinstateUser(ApplicationUser bindingParameter)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ApplicationUser), "bindingParameter", true));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Action, ODataMethodScope.Entity, "IqlSampleApp", "ReinstateUser", typeof(ApplicationUser), typeof(String));
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
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<IEnumerable<Client>>(parameters, ODataMethodType.Function, ODataMethodScope.Collection, "IqlSampleApp", "All", typeof(Client), typeof(Client));
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
        public virtual ODataDataMethodRequest<string>SayHi(ClientType bindingParameter, string name)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(bindingParameter, typeof(ClientType), "bindingParameter", true));
            parameters.Add(new ODataParameter(name, typeof(string), "name", false));
            return ((ODataDataStore) this.DataContext.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Function, ODataMethodScope.Entity, "IqlSampleApp", "SayHi", typeof(ClientType), typeof(String));
        }
    }
}
namespace IqlSampleApp.Sets
{
    public class DocumentCategorySet: DbSet<DocumentCategory, int>
    {
        public DocumentCategorySet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class SiteDocumentSet: DbSet<SiteDocument, int>
    {
        public SiteDocumentSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportActionsTakenSet: DbSet<ReportActionsTaken, int>
    {
        public ReportActionsTakenSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportCategorySet: DbSet<ReportCategory, int>
    {
        public ReportCategorySet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportDefaultRecommendationSet: DbSet<ReportDefaultRecommendation, int>
    {
        public ReportDefaultRecommendationSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportRecommendationSet: DbSet<ReportRecommendation, int>
    {
        public ReportRecommendationSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportTypeSet: DbSet<ReportType, int>
    {
        public ReportTypeSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ProjectSet: DbSet<Project, int>
    {
        public ProjectSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class ReportReceiverEmailAddressSet: DbSet<ReportReceiverEmailAddress, int>
    {
        public ReportReceiverEmailAddressSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class RiskAssessmentSet: DbSet<RiskAssessment, int>
    {
        public RiskAssessmentSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
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
    }
}
namespace IqlSampleApp.Sets
{
    public class RiskAssessmentQuestionSet: DbSet<RiskAssessmentQuestion, int>
    {
        public RiskAssessmentQuestionSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonSet: DbSet<Person, int>
    {
        public PersonSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonInspectionSet: DbSet<PersonInspection, int>
    {
        public PersonInspectionSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonLoadingSet: DbSet<PersonLoading, int>
    {
        public PersonLoadingSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonTypeSet: DbSet<PersonType, int>
    {
        public PersonTypeSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonTypeMapSet: DbSet<PersonTypeMap, CompositeKey>
    {
        public PersonTypeMapSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class PersonReportSet: DbSet<PersonReport, int>
    {
        public PersonReportSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}
namespace IqlSampleApp.Sets
{
    public class SiteSet: DbSet<Site, int>
    {
        public SiteSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
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
    }
}
namespace IqlSampleApp.Sets
{
    public class UserSiteSet: DbSet<UserSite, CompositeKey>
    {
        public UserSiteSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore>dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        {}
    }
}