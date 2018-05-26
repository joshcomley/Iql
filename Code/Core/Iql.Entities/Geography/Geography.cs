namespace Iql.Entities.Geography
{
    public class Geography
    {
        public string Address { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public Geography(string address = null, double? longitude = null, double? latitude = null)
        {
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}