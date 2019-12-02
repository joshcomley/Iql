namespace Iql.Entities.PropertyChangers
{
    public class PrimitivePropertyChanger : PropertyChanger
    {
        private static PrimitivePropertyChanger _instance;
        public static PrimitivePropertyChanger Instance => _instance = _instance ?? new PrimitivePropertyChanger();
    }
}