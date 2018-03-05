using Iql.Queryable.Data.EntityConfiguration;
using Tunnel.Sets;
using Tunnel.ApiContext.Base;
using Tunnel.App.Data.Entities;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores;
using Iql.OData;
using Iql.Parsing;
using System.Collections.Generic;
using Iql.Queryable.Data.Lists;
using System.Threading.Tasks;
using Iql.Queryable.Data.Methods;
using System;
namespace Tunnel.Sets
{
	public class ApplicationUserSet : DbSet<ApplicationUser, string>
	{
		public ApplicationUserSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		
		// Collection methods
		public virtual async Task<DataMethodResult<IEnumerable<ApplicationUser>>> ForClient(int id,
			int type)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(id, typeof(int), "id", false));
			parameters.Add(new ODataParameter(type, typeof(int), "type", false));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<IEnumerable<ApplicationUser>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Tunnel",
				typeof(ApplicationUser));
		}
		public virtual async Task<DataMethodResult<ApplicationUser>> Me()
		{
			var parameters = new List<ODataParameter>();
			
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<ApplicationUser>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Tunnel",
				typeof(ApplicationUser));
		}
		
		// Entity methods
		public virtual async Task<DataMethodResult<string>> GeneratePasswordResetLink(ApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(ApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Tunnel",
				typeof(ApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> AccountConfirm(ApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(ApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Tunnel",
				typeof(ApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> SendAccountConfirmationEmail(ApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(ApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Tunnel",
				typeof(ApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> SendPasswordResetEmail(ApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(ApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Tunnel",
				typeof(ApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> ReinstateUser(ApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(ApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Tunnel",
				typeof(ApplicationUser));
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ClientSet : DbSet<Client, int>
	{
		public ClientSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		
		// Collection methods
		public virtual async Task<DataMethodResult<IEnumerable<Client>>> All()
		{
			var parameters = new List<ODataParameter>();
			
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<IEnumerable<Client>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Tunnel",
				typeof(Client));
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ClientTypeSet : DbSet<ClientType, int>
	{
		public ClientTypeSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		// Entity methods
		public virtual async Task<DataMethodResult<string>> SayHi(string name,
			ClientType entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(name, typeof(string), "name", false));
			parameters.Add(new ODataParameter(entityKey, typeof(ClientType), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Tunnel",
				typeof(ClientType));
		}
	
	}
}
namespace Tunnel.Sets
{
	public class DocumentCategorySet : DbSet<DocumentCategory, int>
	{
		public DocumentCategorySet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class SiteDocumentSet : DbSet<SiteDocument, int>
	{
		public SiteDocumentSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ReportActionsTakenSet : DbSet<ReportActionsTaken, int>
	{
		public ReportActionsTakenSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ReportCategorySet : DbSet<ReportCategory, int>
	{
		public ReportCategorySet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ReportDefaultRecommendationSet : DbSet<ReportDefaultRecommendation, int>
	{
		public ReportDefaultRecommendationSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ReportRecommendationSet : DbSet<ReportRecommendation, int>
	{
		public ReportRecommendationSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ReportTypeSet : DbSet<ReportType, int>
	{
		public ReportTypeSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ProjectSet : DbSet<Project, int>
	{
		public ProjectSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class ReportReceiverEmailAddressSet : DbSet<ReportReceiverEmailAddress, int>
	{
		public ReportReceiverEmailAddressSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class RiskAssessmentSet : DbSet<RiskAssessment, int>
	{
		public RiskAssessmentSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class RiskAssessmentSolutionSet : DbSet<RiskAssessmentSolution, int>
	{
		public RiskAssessmentSolutionSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class RiskAssessmentAnswerSet : DbSet<RiskAssessmentAnswer, int>
	{
		public RiskAssessmentAnswerSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class RiskAssessmentQuestionSet : DbSet<RiskAssessmentQuestion, int>
	{
		public RiskAssessmentQuestionSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class PersonSet : DbSet<Person, int>
	{
		public PersonSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		// Entity methods
		public virtual async Task<DataMethodResult<string>> IncrementVersion(Person entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Person), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Tunnel",
				typeof(Person));
		}
	
	}
}
namespace Tunnel.Sets
{
	public class PersonInspectionSet : DbSet<PersonInspection, int>
	{
		public PersonInspectionSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class PersonLoadingSet : DbSet<PersonLoading, int>
	{
		public PersonLoadingSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class PersonTypeSet : DbSet<PersonType, int>
	{
		public PersonTypeSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class PersonTypeMapSet : DbSet<PersonTypeMap, CompositeKey>
	{
		public PersonTypeMapSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class PersonReportSet : DbSet<PersonReport, int>
	{
		public PersonReportSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class SiteSet : DbSet<Site, int>
	{
		public SiteSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class SiteInspectionSet : DbSet<SiteInspection, int>
	{
		public SiteInspectionSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Tunnel.Sets
{
	public class UserSiteSet : DbSet<UserSite, CompositeKey>
	{
		public UserSiteSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}

