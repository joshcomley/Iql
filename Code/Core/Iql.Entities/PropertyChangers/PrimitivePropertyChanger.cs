namespace Iql.Entities.PropertyChangers
{
    public class PrimitivePropertyChanger : PropertyChanger
    {
        public static PrimitivePropertyChanger Instance { get; } = new PrimitivePropertyChanger();
    }
}