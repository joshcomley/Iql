namespace Iql.Forms.Geography
{
    public class IqlGetLocationSettings
    {
        public IqlLocationAccuracy Accuracy { get; set; } = IqlLocationAccuracy.High;
        public int? MaximumAge { get; set; }
        public int? Timeout { get; set; }

        public IqlGetLocationSettings(IqlLocationAccuracy accuracy = IqlLocationAccuracy.High, int? maximumAge = null, int? timeout = null)
        {
            Accuracy = accuracy;
            MaximumAge = maximumAge;
            Timeout = timeout;
        }
    }
}