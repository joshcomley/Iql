namespace Iql.Forms.Geography
{
    public class IqlGeographicCoordinatesChangedEvent
    {
        public float NewLatitude { get; }
        public float NewLongitude { get; }
        public float OldLatitude { get; }
        public float OldLongitude { get; }

        public IqlGeographicCoordinatesChangedEvent(
            float oldLatitude,
            float oldLongitude,
            float newLatitude,
            float newLongitude
        )
        {
            OldLatitude = oldLatitude;
            OldLongitude = oldLongitude;
            NewLatitude = newLatitude;
            NewLongitude = newLongitude;
        }
    }
}