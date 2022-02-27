using Iql.OData;
using Hazception.Sets;
using Hazception.ApiContext.Base;
using Haz.App.Data.Entities;
using System;
using System.Collections.Generic;
using Iql.Data.Context;
using Iql.Data.DataStores;
using Iql.Entities;
using Iql.OData.Methods;
namespace Hazception.ApiContext.Base
{
	public class HazceptionDataContextBase : DataContext
	{
	    public HazceptionDataContextBase(IDataStore dataStore) : base(dataStore)
		{
			this.ClientTypes = this.AsCustomDbSet<HazClientType, int, HazClientTypeSet>();
			
			this.Users = this.AsCustomDbSet<HazApplicationUser, String, HazApplicationUserSet>();
			
			this.Clients = this.AsCustomDbSet<HazClient, int, HazClientSet>();
			
			this.Videos = this.AsCustomDbSet<Video, int, VideoSet>();
			
			this.Exams = this.AsCustomDbSet<Exam, int, ExamSet>();
			
			this.ExamManagers = this.AsCustomDbSet<ExamManager, int, ExamManagerSet>();
			
			this.ExamResults = this.AsCustomDbSet<ExamResult, int, ExamResultSet>();
			
			this.ExamCandidateResults = this.AsCustomDbSet<ExamCandidateResult, int, ExamCandidateResultSet>();
			
			this.ExamCandidates = this.AsCustomDbSet<ExamCandidate, int, ExamCandidateSet>();
			
			this.Hazards = this.AsCustomDbSet<Hazard, int, HazardSet>();
			
			this.RegisterConfiguration<ODataConfiguration>(this.ODataConfiguration);
			this.ODataConfiguration.RegisterEntitySet<HazClientType>(nameof(ClientTypes));
			this.ODataConfiguration.RegisterEntitySet<HazApplicationUser>(nameof(Users));
			this.ODataConfiguration.RegisterEntitySet<HazClient>(nameof(Clients));
			this.ODataConfiguration.RegisterEntitySet<Video>(nameof(Videos));
			this.ODataConfiguration.RegisterEntitySet<Exam>(nameof(Exams));
			this.ODataConfiguration.RegisterEntitySet<ExamManager>(nameof(ExamManagers));
			this.ODataConfiguration.RegisterEntitySet<ExamResult>(nameof(ExamResults));
			this.ODataConfiguration.RegisterEntitySet<ExamCandidateResult>(nameof(ExamCandidateResults));
			this.ODataConfiguration.RegisterEntitySet<ExamCandidate>(nameof(ExamCandidates));
			this.ODataConfiguration.RegisterEntitySet<Hazard>(nameof(Hazards));
		
		}

	    private ODataConfiguration _oDataConfiguration;
	    public ODataConfiguration ODataConfiguration => _oDataConfiguration = _oDataConfiguration ?? new ODataConfiguration(() => EntityConfigurationContext);

	    public override void Configure(EntityConfigurationBuilder builder)
		{
			builder.EntityType<HazClientType>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.Name, true)
				.DefineCollectionProperty(p => p.Client, p => p.ClientCount);
			
			builder.EntityType<HazApplicationUser>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineConvertedProperty(p => p.ClientGuid, "Guid", true)
				.DefineProperty(p => p.ClientId, true)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Email, true)
				.DefineProperty(p => p.UserType, false)
				.DefineProperty(p => p.FullName, false)
				.DefineProperty(p => p.AssessedExams, false)
				.DefineProperty(p => p.TrainingExams, false)
				.DefineProperty(p => p.AverageScore, true)
				.DefineProperty(p => p.IsLockedOut, false)
				.DefineProperty(p => p.UserName, true)
				.DefineProperty(p => p.NormalizedUserName, true)
				.DefineProperty(p => p.NormalizedEmail, true)
				.DefineProperty(p => p.EmailConfirmed, false)
				.DefineProperty(p => p.PhoneNumber, true)
				.DefineProperty(p => p.PhoneNumberConfirmed, false)
				.DefineProperty(p => p.LockoutEnd, true)
				.DefineProperty(p => p.AccessFailedCount, false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.UsersCreated, p => p.UsersCreatedCount)
				.DefineProperty(p => p.Client, true)
				.DefineCollectionProperty(p => p.ClientsCreated, p => p.ClientsCreatedCount)
				.DefineCollectionProperty(p => p.VideosCreated, p => p.VideosCreatedCount)
				.DefineCollectionProperty(p => p.ExamsCreated, p => p.ExamsCreatedCount)
				.DefineCollectionProperty(p => p.ExamManagersCreated, p => p.ExamManagersCreatedCount)
				.DefineCollectionProperty(p => p.Results, p => p.ResultsCount)
				.DefineCollectionProperty(p => p.ExamResultsCreated, p => p.ExamResultsCreatedCount)
				.DefineCollectionProperty(p => p.ExamResults, p => p.ExamResultsCount)
				.DefineCollectionProperty(p => p.ExamCandidateResultsCreated, p => p.ExamCandidateResultsCreatedCount)
				.DefineCollectionProperty(p => p.Exams, p => p.ExamsCount)
				.DefineCollectionProperty(p => p.ExamCandidatesCreated, p => p.ExamCandidatesCreatedCount)
				.DefineCollectionProperty(p => p.HazardsCreated, p => p.HazardsCreatedCount);
			
			builder.EntityType<HazApplicationUser>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.UsersCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<HazApplicationUser>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Users)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<HazClient>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.TypeId, false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Name, false)
				.DefineProperty(p => p.Description, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.Users, p => p.UsersCount)
				.DefineProperty(p => p.Type, false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.Videos, p => p.VideosCount)
				.DefineCollectionProperty(p => p.Exams, p => p.ExamsCount)
				.DefineCollectionProperty(p => p.ExamManagers, p => p.ExamManagersCount)
				.DefineCollectionProperty(p => p.ExamResults, p => p.ExamResultsCount)
				.DefineCollectionProperty(p => p.ExamCandidateResults, p => p.ExamCandidateResultsCount)
				.DefineCollectionProperty(p => p.ExamCandidates, p => p.ExamCandidatesCount)
				.DefineCollectionProperty(p => p.Hazards, p => p.HazardsCount);
			
			builder.EntityType<HazClient>()
				.HasOne(p => p.Type)
				.WithMany(p => p.Client)
				.WithConstraint(p => p.TypeId, p => p.Id);
			
			builder.EntityType<HazClient>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ClientsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Video>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.ClientId, false)
				.DefineProperty(p => p.ClonedFromId, true)
				.DefineConvertedProperty(p => p.ClientGuid, "Guid", false)
				.DefineProperty(p => p.Title, false)
				.DefineProperty(p => p.Description, true)
				.DefineProperty(p => p.Duration, false)
				.DefineProperty(p => p.ResultsCount, false)
				.DefineProperty(p => p.CandidateResultsCount, false)
				.DefineProperty(p => p.CandidatesCount, false)
				.DefineProperty(p => p.ExamCount, false)
				.DefineProperty(p => p.HazardCount, false)
				.DefineProperty(p => p.RevisionKey, true)
				.DefineProperty(p => p.ScreenshotUrl, true)
				.DefineProperty(p => p.ScreenshotMiniUrl, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineProperty(p => p.Client, false)
				.DefineProperty(p => p.ClonedFrom, true)
				.DefineCollectionProperty(p => p.ClonedTo, p => p.ClonedToCount)
				.DefineCollectionProperty(p => p.Exams, p => p.ExamsCount)
				.DefineCollectionProperty(p => p.Results, p => p.ResultsCount)
				.DefineCollectionProperty(p => p.CandidateResults, p => p.CandidateResultsCount)
				.DefineCollectionProperty(p => p.Candidates, p => p.CandidatesCount)
				.DefineCollectionProperty(p => p.Hazards, p => p.HazardsCount);
			
			builder.EntityType<Video>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.VideosCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Video>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Videos)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Video>()
				.HasOne(p => p.ClonedFrom)
				.WithMany(p => p.ClonedTo)
				.WithConstraint(p => p.ClonedFromId, p => p.Id);
			
			builder.EntityType<Exam>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.VideoId, false)
				.DefineProperty(p => p.ClientId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineConvertedProperty(p => p.VideoGuid, "Guid", false)
				.DefineConvertedProperty(p => p.ClientGuid, "Guid", false)
				.DefineProperty(p => p.IsTraining, false)
				.DefineProperty(p => p.AvailableToAllUsers, false)
				.DefineProperty(p => p.ScheduledDate, true)
				.DefineProperty(p => p.PassMark, false)
				.DefineProperty(p => p.Title, false)
				.DefineProperty(p => p.AllowAnyArea, false)
				.DefineProperty(p => p.Description, true)
				.DefineProperty(p => p.Status, false)
				.DefineProperty(p => p.NotStarted, false)
				.DefineProperty(p => p.Complete, false)
				.DefineProperty(p => p.InProgress, false)
				.DefineProperty(p => p.CandidateCount, false)
				.DefineProperty(p => p.ImageUrl, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.Video, false)
				.DefineProperty(p => p.Client, false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineCollectionProperty(p => p.Managers, p => p.ManagersCount)
				.DefineCollectionProperty(p => p.Results, p => p.ResultsCount)
				.DefineCollectionProperty(p => p.CandidateResults, p => p.CandidateResultsCount)
				.DefineCollectionProperty(p => p.Candidates, p => p.CandidatesCount);
			
			builder.EntityType<Exam>()
				.HasOne(p => p.Video)
				.WithMany(p => p.Exams)
				.WithConstraint(p => p.VideoId, p => p.Id);
			
			builder.EntityType<Exam>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Exams)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Exam>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ExamsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ExamManager>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.ExamId, false)
				.DefineProperty(p => p.ClientId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineConvertedProperty(p => p.ClientGuid, "Guid", false)
				.DefineProperty(p => p.ManagerId, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.Exam, false)
				.DefineProperty(p => p.Client, false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineProperty(p => p.Manager, true);
			
			builder.EntityType<ExamManager>()
				.HasOne(p => p.Exam)
				.WithMany(p => p.Managers)
				.WithConstraint(p => p.ExamId, p => p.Id);
			
			builder.EntityType<ExamManager>()
				.HasOne(p => p.Client)
				.WithMany(p => p.ExamManagers)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<ExamManager>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ExamManagersCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ExamResult>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.ExamId, false)
				.DefineProperty(p => p.VideoId, false)
				.DefineProperty(p => p.ClientId, false)
				.DefineProperty(p => p.CandidateId, true)
				.DefineProperty(p => p.CandidateResultId, false)
				.DefineProperty(p => p.HazardId, false)
				.DefineProperty(p => p.ExamCandidateId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.Success, false)
				.DefineConvertedProperty(p => p.ClientGuid, "Guid", false)
				.DefineProperty(p => p.X, false)
				.DefineProperty(p => p.Y, false)
				.DefineProperty(p => p.Time, false)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineProperty(p => p.Exam, false)
				.DefineProperty(p => p.Video, false)
				.DefineProperty(p => p.Client, false)
				.DefineProperty(p => p.Candidate, false)
				.DefineProperty(p => p.CandidateResult, false)
				.DefineProperty(p => p.Hazard, false)
				.DefineProperty(p => p.ExamCandidate, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<ExamResult>()
				.HasOne(p => p.Exam)
				.WithMany(p => p.Results)
				.WithConstraint(p => p.ExamId, p => p.Id);
			
			builder.EntityType<ExamResult>()
				.HasOne(p => p.Video)
				.WithMany(p => p.Results)
				.WithConstraint(p => p.VideoId, p => p.Id);
			
			builder.EntityType<ExamResult>()
				.HasOne(p => p.Client)
				.WithMany(p => p.ExamResults)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<ExamResult>()
				.HasOne(p => p.Candidate)
				.WithMany(p => p.Results)
				.WithConstraint(p => p.CandidateId, p => p.Id);
			
			builder.EntityType<ExamResult>()
				.HasOne(p => p.CandidateResult)
				.WithMany(p => p.Results)
				.WithConstraint(p => p.CandidateResultId, p => p.Id);
			
			builder.EntityType<ExamResult>()
				.HasOne(p => p.Hazard)
				.WithMany(p => p.Results)
				.WithConstraint(p => p.HazardId, p => p.Id);
			
			builder.EntityType<ExamResult>()
				.HasOne(p => p.ExamCandidate)
				.WithMany(p => p.Results)
				.WithConstraint(p => p.ExamCandidateId, p => p.Id);
			
			builder.EntityType<ExamResult>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ExamResultsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ExamCandidateResult>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.VideoId, false)
				.DefineProperty(p => p.ExamId, false)
				.DefineProperty(p => p.ClientId, false)
				.DefineProperty(p => p.ExamCandidateId, false)
				.DefineProperty(p => p.CandidateId, true)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineConvertedProperty(p => p.ClientGuid, "Guid", false)
				.DefineProperty(p => p.Score, false)
				.DefineProperty(p => p.Pass, false)
				.DefineProperty(p => p.ClickData, true)
				.DefineProperty(p => p.ClickCount, false)
				.DefineProperty(p => p.HazardCount, false)
				.DefineProperty(p => p.SuccessCount, false)
				.DefineProperty(p => p.Date, false)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.Results, p => p.ResultsCount)
				.DefineProperty(p => p.Video, false)
				.DefineProperty(p => p.Exam, false)
				.DefineProperty(p => p.Client, false)
				.DefineProperty(p => p.ExamCandidate, false)
				.DefineProperty(p => p.Candidate, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<ExamCandidateResult>()
				.HasOne(p => p.Video)
				.WithMany(p => p.CandidateResults)
				.WithConstraint(p => p.VideoId, p => p.Id);
			
			builder.EntityType<ExamCandidateResult>()
				.HasOne(p => p.Exam)
				.WithMany(p => p.CandidateResults)
				.WithConstraint(p => p.ExamId, p => p.Id);
			
			builder.EntityType<ExamCandidateResult>()
				.HasOne(p => p.Client)
				.WithMany(p => p.ExamCandidateResults)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<ExamCandidateResult>()
				.HasOne(p => p.ExamCandidate)
				.WithMany(p => p.CandidateResults)
				.WithConstraint(p => p.ExamCandidateId, p => p.Id);
			
			builder.EntityType<ExamCandidateResult>()
				.HasOne(p => p.Candidate)
				.WithMany(p => p.ExamResults)
				.WithConstraint(p => p.CandidateId, p => p.Id);
			
			builder.EntityType<ExamCandidateResult>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ExamCandidateResultsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<ExamCandidate>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.ExamId, false)
				.DefineProperty(p => p.VideoId, false)
				.DefineProperty(p => p.CandidateId, true)
				.DefineProperty(p => p.ClientId, false)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineProperty(p => p.LastTime, false)
				.DefineConvertedProperty(p => p.ClientGuid, "Guid", false)
				.DefineProperty(p => p.Status, false)
				.DefineProperty(p => p.DateLastTaken, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.Results, p => p.ResultsCount)
				.DefineCollectionProperty(p => p.CandidateResults, p => p.CandidateResultsCount)
				.DefineProperty(p => p.Exam, false)
				.DefineProperty(p => p.Video, false)
				.DefineProperty(p => p.Candidate, false)
				.DefineProperty(p => p.Client, false)
				.DefineProperty(p => p.CreatedByUser, true);
			
			builder.EntityType<ExamCandidate>()
				.HasOne(p => p.Exam)
				.WithMany(p => p.Candidates)
				.WithConstraint(p => p.ExamId, p => p.Id);
			
			builder.EntityType<ExamCandidate>()
				.HasOne(p => p.Video)
				.WithMany(p => p.Candidates)
				.WithConstraint(p => p.VideoId, p => p.Id);
			
			builder.EntityType<ExamCandidate>()
				.HasOne(p => p.Candidate)
				.WithMany(p => p.Exams)
				.WithConstraint(p => p.CandidateId, p => p.Id);
			
			builder.EntityType<ExamCandidate>()
				.HasOne(p => p.Client)
				.WithMany(p => p.ExamCandidates)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<ExamCandidate>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.ExamCandidatesCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
			
			builder.EntityType<Hazard>()
				.HasKey(p => p.Id)
				.DefineProperty(p => p.Id, false)
				.DefineProperty(p => p.ClonedFromId, true)
				.DefineProperty(p => p.VideoId, false)
				.DefineProperty(p => p.ClientId, false)
				.DefineProperty(p => p.CreatedByUserId, true)
				.DefineConvertedProperty(p => p.ClientGuid, "Guid", false)
				.DefineConvertedProperty(p => p.VideoGuid, "Guid", false)
				.DefineProperty(p => p.Title, false)
				.DefineProperty(p => p.Description, true)
				.DefineProperty(p => p.TimeFrom, false)
				.DefineProperty(p => p.Duration, false)
				.DefineProperty(p => p.Left, false)
				.DefineProperty(p => p.Top, false)
				.DefineProperty(p => p.Width, false)
				.DefineProperty(p => p.Height, false)
				.DefineProperty(p => p.RevisionKey, true)
				.DefineProperty(p => p.ImageUrl, true)
				.DefineConvertedProperty(p => p.Guid, "Guid", false)
				.DefineProperty(p => p.CreatedDate, false)
				.DefineProperty(p => p.Version, false)
				.DefineConvertedProperty(p => p.PersistenceKey, "Guid", false)
				.DefineCollectionProperty(p => p.Results, p => p.ResultsCount)
				.DefineProperty(p => p.ClonedFrom, true)
				.DefineCollectionProperty(p => p.ClonedTo, p => p.ClonedToCount)
				.DefineProperty(p => p.Video, false)
				.DefineProperty(p => p.Client, false)
				.DefineProperty(p => p.CreatedByUser, true)
				.DefineEntityValidation(entity => (entity.Title == null ? null : entity.Title.ToUpper()) == null || (entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()) && (entity.Description == null ? null : entity.Description.ToUpper()) == null || (entity.Description.Trim() == null ? null : entity.Description.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()), "Please enter either a title or a description", "16b68667-f7bc-4828-b626-3a6ea1b7c36f")
				.DefineDisplayFormatter(entity => entity.Title, "Default")
				.DefineDisplayFormatter(entity => entity.Title + " (" + entity.Id + ")", "Report");
			
			builder.EntityType<Hazard>()
				.HasOne(p => p.ClonedFrom)
				.WithMany(p => p.ClonedTo)
				.WithConstraint(p => p.ClonedFromId, p => p.Id);
			
			builder.EntityType<Hazard>()
				.HasOne(p => p.Video)
				.WithMany(p => p.Hazards)
				.WithConstraint(p => p.VideoId, p => p.Id);
			
			builder.EntityType<Hazard>()
				.HasOne(p => p.Client)
				.WithMany(p => p.Hazards)
				.WithConstraint(p => p.ClientId, p => p.Id);
			
			builder.EntityType<Hazard>()
				.HasOne(p => p.CreatedByUser)
				.WithMany(p => p.HazardsCreated)
				.WithConstraint(p => p.CreatedByUserId, p => p.Id);
		
		}
		
		
		public HazClientTypeSet ClientTypes { get; set; }
		
		public HazApplicationUserSet Users { get; set; }
		
		public HazClientSet Clients { get; set; }
		
		public VideoSet Videos { get; set; }
		
		public ExamSet Exams { get; set; }
		
		public ExamManagerSet ExamManagers { get; set; }
		
		public ExamResultSet ExamResults { get; set; }
		
		public ExamCandidateResultSet ExamCandidateResults { get; set; }
		
		public ExamCandidateSet ExamCandidates { get; set; }
		
		public HazardSet Hazards { get; set; }
		public virtual ODataDataMethodRequest<string> ValidateField(string SetName,
			string Name,
			string Value)
		{
			var parameters = new List<ODataParameter>();
			
			parameters.Add(new ODataParameter(SetName, typeof(string), "SetName", false));
			parameters.Add(new ODataParameter(Name, typeof(string), "Name", false));
			parameters.Add(new ODataParameter(Value, typeof(string), "Value", false));
			return ((ODataDataStore)this.DataStore).MethodWithResponse<string>(
				parameters,
				ODataMethodType.Action,
				ODataMethodScopeKind.Global,
				"Hazception",
				"ValidateField",
				null,
				typeof(string));
		}
	
	}
}

