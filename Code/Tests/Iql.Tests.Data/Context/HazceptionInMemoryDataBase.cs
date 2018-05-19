using System.Collections.Generic;
using Haz.App.Data.Entities;

namespace Iql.Tests.Data.Context
{
    public class HazceptionInMemoryDataBase
    {
        public IList<HazClientType> ClientTypes { get; set; } = new List<HazClientType>();
        public IList<HazApplicationUser> Users { get; set; } = new List<HazApplicationUser>();
        public IList<HazClient> Clients { get; set; } = new List<HazClient>();
        public IList<Video> Videos { get; set; } = new List<Video>();
        public IList<Exam> Exams { get; set; } = new List<Exam>();
        public IList<ExamManager> ExamManagers { get; set; } = new List<ExamManager>();
        public IList<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
        public IList<ExamCandidateResult> ExamCandidateResults { get; set; } = new List<ExamCandidateResult>();
        public IList<ExamCandidate> ExamCandidates { get; set; } = new List<ExamCandidate>();
        public IList<Hazard> Hazards { get; set; } = new List<Hazard>();
    }
}