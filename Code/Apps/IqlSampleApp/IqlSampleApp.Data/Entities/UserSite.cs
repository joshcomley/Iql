namespace IqlSampleApp.Data.Entities
{
    public class UserSite
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Site Site { get; set; }
        public int SiteId { get; set; }
    }
}
