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
using Iql.OData.Methods;
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
		public virtual ODataDataMethodRequest<HazApplicationUser> Me()
		{
			var parameters = new List<ODataParameter>();
			
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<HazApplicationUser>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				"Me",
				typeof(HazApplicationUser),
				typeof(HazApplicationUser));
		}
		public virtual ODataDataMethodRequest<IEnumerable<HazApplicationUser>> ForClient(int id,
			int type)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(id, typeof(int), "id", false));
			parameters.Add(new ODataParameter(type, typeof(int), "type", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<IEnumerable<HazApplicationUser>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				"ForClient",
				typeof(HazApplicationUser),
				typeof(HazApplicationUser));
		}
		
		// Entity methods
		public virtual ODataDataMethodRequest<string> GeneratePasswordResetLink(HazApplicationUser bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(HazApplicationUser), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				"GeneratePasswordResetLink",
				typeof(HazApplicationUser),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> SendAccountConfirmationEmail(HazApplicationUser bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(HazApplicationUser), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"SendAccountConfirmationEmail",
				typeof(HazApplicationUser),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> SendPasswordResetEmail(HazApplicationUser bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(HazApplicationUser), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"SendPasswordResetEmail",
				typeof(HazApplicationUser),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> AccountConfirm(HazApplicationUser bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(HazApplicationUser), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"AccountConfirm",
				typeof(HazApplicationUser),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> ReinstateUser(HazApplicationUser bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(HazApplicationUser), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"ReinstateUser",
				typeof(HazApplicationUser),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> ValidateEntity(HazApplicationUser bindingParameter,
			string SetName,
			HazApplicationUser Entity)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(HazApplicationUser), "bindingParameter", true));
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(HazApplicationUser), "Entity", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"ValidateEntity",
				typeof(HazApplicationUser),
				typeof(string));
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
		public virtual ODataDataMethodRequest<IEnumerable<HazClient>> All()
		{
			var parameters = new List<ODataParameter>();
			
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<IEnumerable<HazClient>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				"All",
				typeof(HazClient),
				typeof(HazClient));
		}
		
		// Entity methods
		public virtual ODataDataMethodRequest<string> ValidateEntity(HazClient bindingParameter,
			string SetName,
			HazClient Entity)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(HazClient), "bindingParameter", true));
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(HazClient), "Entity", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"ValidateEntity",
				typeof(HazClient),
				typeof(string));
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
		public virtual ODataDataMethodRequest<IEnumerable<Video>> ForClient(int id)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(id, typeof(int), "id", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<IEnumerable<Video>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				"ForClient",
				typeof(Video),
				typeof(Video));
		}
		
		// Entity methods
		public virtual ODataDataMethodRequest<string> ValidateEntity(Video bindingParameter,
			string SetName,
			Video Entity)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(Video), "Entity", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"ValidateEntity",
				typeof(Video),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> UpdateTitle(Video bindingParameter,
			string title)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			parameters.Add(new ODataParameter(title, typeof(string), "title", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"UpdateTitle",
				typeof(Video),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> VideoUrl(Video bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				"VideoUrl",
				typeof(Video),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> VideoUploadUrl(Video bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				"VideoUploadUrl",
				typeof(Video),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> ScreenshotUploadUrl(Video bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				"ScreenshotUploadUrl",
				typeof(Video),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> ScreenshotMiniUploadUrl(Video bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				"ScreenshotMiniUploadUrl",
				typeof(Video),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> ScreenshotMiniUrl(Video bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				"ScreenshotMiniUrl",
				typeof(Video),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> IncrementVersion(Video bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"IncrementVersion",
				typeof(Video),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<int> Clone(Video bindingParameter,
			int clientId,
			bool cloneHazards,
			string title,
			string description)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			parameters.Add(new ODataParameter(clientId, typeof(int), "clientId", false));
			parameters.Add(new ODataParameter(cloneHazards, typeof(bool), "cloneHazards", false));
			parameters.Add(new ODataParameter(title, typeof(string), "title", false));
			parameters.Add(new ODataParameter(description, typeof(string), "description", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<int>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"Clone",
				typeof(Video),
				typeof(int));
		}
		public virtual ODataDataMethodRequest<string> Description(Video bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Video), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				"Description",
				typeof(Video),
				typeof(string));
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
		public virtual ODataDataMethodRequest<IEnumerable<Exam>> ForClient(int id)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(id, typeof(int), "id", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<IEnumerable<Exam>>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Collection,
				"Hazception",
				"ForClient",
				typeof(Exam),
				typeof(Exam));
		}
		
		// Entity methods
		public virtual ODataMethodRequest SetCandidateExamOpened(Exam bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Exam), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).Method(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"SetCandidateExamOpened",
				typeof(Exam));
		}
		public virtual ODataMethodRequest SetCandidateExamStarted(Exam bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Exam), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).Method(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"SetCandidateExamStarted",
				typeof(Exam));
		}
		public virtual ODataDataMethodRequest<ExamCandidateResult> LogClicks(Exam bindingParameter,
			IEnumerable<double> time,
			IEnumerable<double> x,
			IEnumerable<double> y)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Exam), "bindingParameter", true));
			parameters.Add(new ODataParameter(time, typeof(IEnumerable<double>), "time", false));
			parameters.Add(new ODataParameter(x, typeof(IEnumerable<double>), "x", false));
			parameters.Add(new ODataParameter(y, typeof(IEnumerable<double>), "y", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<ExamCandidateResult>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"LogClicks",
				typeof(Exam),
				typeof(ExamCandidateResult));
		}
		public virtual ODataDataMethodRequest<Exam> SetStarted(Exam bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Exam), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<Exam>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"SetStarted",
				typeof(Exam),
				typeof(Exam));
		}
		public virtual ODataDataMethodRequest<string> ValidateEntity(Exam bindingParameter,
			string SetName,
			Exam Entity)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Exam), "bindingParameter", true));
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(Exam), "Entity", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"ValidateEntity",
				typeof(Exam),
				typeof(string));
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
		public virtual ODataMethodRequest SetExamCandidateLatestTime(ExamCandidate bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(ExamCandidate), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).Method(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"SetExamCandidateLatestTime",
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
		public virtual ODataMethodRequest IncrementVersionsForVideo()
		{
			var parameters = new List<ODataParameter>();
			
			return ((ODataDataStore)this.DataContext.DataStore).Method(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Collection,
				"Hazception",
				"IncrementVersionsForVideo",
				typeof(Hazard));
		}
		
		// Entity methods
		public virtual ODataDataMethodRequest<string> ScreenshotUploadUrl(Hazard bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Hazard), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Function,
				ODataMethodScope.Entity,
				"Hazception",
				"ScreenshotUploadUrl",
				typeof(Hazard),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> IncrementVersion(Hazard bindingParameter)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Hazard), "bindingParameter", true));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"IncrementVersion",
				typeof(Hazard),
				typeof(string));
		}
		public virtual ODataDataMethodRequest<string> ValidateEntity(Hazard bindingParameter,
			string SetName,
			Hazard Entity)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(bindingParameter, typeof(Hazard), "bindingParameter", true));
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Entity, typeof(Hazard), "Entity", false));
			return ((ODataDataStore)this.DataContext.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScope.Entity,
				"Hazception",
				"ValidateEntity",
				typeof(Hazard),
				typeof(string));
		}
	
	}
}

