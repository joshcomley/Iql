namespace Iql.Forms.Geography
{
    public class GetLocationSettings
    {
        public IqlLocationAccuracy Accuracy { get; set; } = IqlLocationAccuracy.High;
        public int? MaximumAge { get; set; }
        public int? Timeout { get; set; }

        public GetLocationSettings(IqlLocationAccuracy accuracy = IqlLocationAccuracy.High, int? maximumAge = null, int? timeout = null)
        {
            Accuracy = accuracy;
            MaximumAge = maximumAge;
            Timeout = timeout;
        }
    }
}