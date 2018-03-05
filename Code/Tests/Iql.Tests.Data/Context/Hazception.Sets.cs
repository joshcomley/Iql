using Iql.Queryable.Data.Context;
using Hazception.Sets;
using Hazception.ApiContext.Base;
using Haz.App.Data.Entities;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.DataStores;
using Iql.OData;
using Iql.Parsing;
using System.Collections.Generic;
using Iql.Queryable.Data.Lists;
using System.Threading.Tasks;
using Iql.Queryable.Data.Methods;
using System;
namespace Hazception.Sets
{
	public class HazClientTypeSet : DbSet<HazClientType, int>
	{
		public HazClientTypeSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Hazception.Sets
{
	public class HazApplicationUserSet : DbSet<HazApplicationUser, string>
	{
		public HazApplicationUserSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		
		// Collection methods
		public virtual async Task<DataMethodResult<HazApplicationUser>> Me()
		{
			var parameters = new List<ODataParameter>();
			
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<HazApplicationUser>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				typeof(HazApplicationUser));
		}
		public virtual async Task<DataMethodResult<IEnumerable<HazApplicationUser>>> ForClient(int id,
			int type)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(id, typeof(int), "id", false));
			parameters.Add(new ODataParameter(type, typeof(int), "type", false));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<IEnumerable<HazApplicationUser>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				typeof(HazApplicationUser));
		}
		
		// Entity methods
		public virtual async Task<DataMethodResult<string>> GeneratePasswordResetLink(HazApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(HazApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(HazApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> SendAccountConfirmationEmail(HazApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(HazApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(HazApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> SendPasswordResetEmail(HazApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(HazApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(HazApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> AccountConfirm(HazApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(HazApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(HazApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> ReinstateUser(HazApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(HazApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(HazApplicationUser));
		}
		public virtual async Task<DataMethodResult<string>> ValidateEntity(string SetName,
			HazApplicationUser Entity,
			HazApplicationUser entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(HazApplicationUser), "Entity", false));
			parameters.Add(new ODataParameter(entityKey, typeof(HazApplicationUser), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(HazApplicationUser));
		}
	
	}
}
namespace Hazception.Sets
{
	public class HazClientSet : DbSet<HazClient, int>
	{
		public HazClientSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		
		// Collection methods
		public virtual async Task<DataMethodResult<IEnumerable<HazClient>>> All()
		{
			var parameters = new List<ODataParameter>();
			
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<IEnumerable<HazClient>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				typeof(HazClient));
		}
		
		// Entity methods
		public virtual async Task<DataMethodResult<string>> ValidateEntity(string SetName,
			HazClient Entity,
			HazClient entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(HazClient), "Entity", false));
			parameters.Add(new ODataParameter(entityKey, typeof(HazClient), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(HazClient));
		}
	
	}
}
namespace Hazception.Sets
{
	public class VideoSet : DbSet<Video, int>
	{
		public VideoSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		
		// Collection methods
		public virtual async Task<DataMethodResult<IEnumerable<Video>>> ForClient(int id)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(id, typeof(int), "id", false));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<IEnumerable<Video>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				typeof(Video));
		}
		
		// Entity methods
		public virtual async Task<DataMethodResult<string>> ValidateEntity(string SetName,
			Video Entity,
			Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(Video), "Entity", false));
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<string>> UpdateTitle(string title,
			Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(title, typeof(string), "title", false));
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<string>> VideoUrl(Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<string>> VideoUploadUrl(Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<string>> ScreenshotUploadUrl(Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<string>> ScreenshotMiniUploadUrl(Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<string>> ScreenshotMiniUrl(Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<string>> IncrementVersion(Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<int>> Clone(int clientId,
			bool cloneHazards,
			string title,
			string description,
			Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(clientId, typeof(int), "clientId", false));
			parameters.Add(new ODataParameter(cloneHazards, typeof(bool), "cloneHazards", false));
			parameters.Add(new ODataParameter(title, typeof(string), "title", false));
			parameters.Add(new ODataParameter(description, typeof(string), "description", false));
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<int>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
		public virtual async Task<DataMethodResult<string>> Description(Video entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Video), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Video));
		}
	
	}
}
namespace Hazception.Sets
{
	public class ExamSet : DbSet<Exam, int>
	{
		public ExamSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		
		// Collection methods
		public virtual async Task<DataMethodResult<IEnumerable<Exam>>> ForClient(int id)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(id, typeof(int), "id", false));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<IEnumerable<Exam>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				typeof(Exam));
		}
		
		// Entity methods
		public virtual async Task<MethodResult> SetCandidateExamOpened(Exam entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Exam), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodAsync(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Exam));
		}
		public virtual async Task<MethodResult> SetCandidateExamStarted(Exam entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Exam), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodAsync(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Exam));
		}
		public virtual async Task<DataMethodResult<ExamCandidateResult>> LogClicks(IEnumerable<double> time,
			IEnumerable<double> x,
			IEnumerable<double> y,
			Exam entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(time, typeof(IEnumerable<double>), "time", false));
			parameters.Add(new ODataParameter(x, typeof(IEnumerable<double>), "x", false));
			parameters.Add(new ODataParameter(y, typeof(IEnumerable<double>), "y", false));
			parameters.Add(new ODataParameter(entityKey, typeof(Exam), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<ExamCandidateResult>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Exam));
		}
		public virtual async Task<DataMethodResult<Exam>> SetStarted(Exam entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Exam), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<Exam>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Exam));
		}
		public virtual async Task<DataMethodResult<string>> ValidateEntity(string SetName,
			Exam Entity,
			Exam entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(Exam), "Entity", false));
			parameters.Add(new ODataParameter(entityKey, typeof(Exam), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Exam));
		}
	
	}
}
namespace Hazception.Sets
{
	public class ExamManagerSet : DbSet<ExamManager, int>
	{
		public ExamManagerSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Hazception.Sets
{
	public class ExamResultSet : DbSet<ExamResult, int>
	{
		public ExamResultSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Hazception.Sets
{
	public class ExamCandidateResultSet : DbSet<ExamCandidateResult, int>
	{
		public ExamCandidateResultSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
	
	}
}
namespace Hazception.Sets
{
	public class ExamCandidateSet : DbSet<ExamCandidate, int>
	{
		public ExamCandidateSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		// Entity methods
		public virtual async Task<MethodResult> SetExamCandidateLatestTime(ExamCandidate entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(ExamCandidate), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodAsync(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(ExamCandidate));
		}
	
	}
}
namespace Hazception.Sets
{
	public class HazardSet : DbSet<Hazard, int>
	{
		public HazardSet(EntityConfigurationBuilder entityConfigurationBuilder,
			Func<IDataStore> dataStoreGetter,
			EvaluateContext evaluateContext = null,
			IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
		{
		
		}
		
		
		// Collection methods
		public virtual async Task<MethodResult> IncrementVersionsForVideo()
		{
			var parameters = new List<ODataParameter>();
			
			return await ((ODataDataStore)this.DataContext.DataStore).MethodAsync(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Collection,
				"Hazception",
				typeof(Hazard));
		}
		
		// Entity methods
		public virtual async Task<DataMethodResult<string>> ScreenshotUploadUrl(Hazard entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Hazard), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Hazard));
		}
		public virtual async Task<DataMethodResult<string>> IncrementVersion(Hazard entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(entityKey, typeof(Hazard), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Hazard));
		}
		public virtual async Task<DataMethodResult<string>> ValidateEntity(string SetName,
			Hazard Entity,
			Hazard entityKey)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(Hazard), "Entity", false));
			parameters.Add(new ODataParameter(entityKey, typeof(Hazard), "entityKey", true));
			return await ((ODataDataStore)this.DataContext.DataStore).MethodWithResponseAsync<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				typeof(Hazard));
		}
	
	}
}

